using System;
using System.Net.Http;
using HtmlAgilityPack;
using System.IO;
using Newtonsoft.Json;
using MarkMaster.Utils;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Diagnostics;

namespace MarkMaster.Scripts
{
    public class Crawler
    {
        private static readonly string domain_url = Constants.DomainUrl;
        private static readonly string skill_url = Constants.SkillUrl;
        private static readonly string memory_url = Constants.MemoryUrl;
        private static readonly string npc_url = Constants.NpcUrl;
        public const string ResourcesDirectory = Constants.ResourcesDirectory;
        public const string ImgDirectory = Constants.ImgDirectory;
        private const string SkillsFileName = Constants.SkillsFileName;
        private const string imgSrcDictionaryFileName = Constants.ImgSrcDictionaryFileName;
        private const string MemoriesFileName = Constants.MemoriesFileName;
        private const string NPCsFileName = Constants.NpcsFileName;
        const string skill_data_prefix = Constants.SkillDataPrefix;

        public event Action<string> OnProgressChanged;

        public Crawler()
        {
            // Constructor
        }

        private async Task<HtmlDocument> GetHtmlDocumentFromUrl(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(response);
                return htmlDoc;
            }
        }

        public async Task<string> GetSkillUnlockRate(HtmlDocument htmlDoc)
        {
            var divTag = htmlDoc.DocumentNode.SelectSingleNode("//div[@title='漫巡属性']");
            if (divTag != null)
            {
                var trTags = divTag.SelectNodes(".//tr");
                if (trTags != null)
                {
                    foreach (var trTag in trTags)
                    {
                        var tdTag = trTag.SelectSingleNode($".//td[contains(text(), '{Constants.MemorySkillUnlockUpText}')]");
                        if (tdTag != null)
                        {
                            var tdTags = trTag.SelectNodes(".//td");
                            if (tdTags != null && tdTags.Count > 0)
                            {
                                return tdTags.Last().InnerText.Trim().TrimEnd('％');
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }

        public async Task CrawlSkills(HtmlDocument htmlDoc, string fileName = SkillsFileName)
        {
            var newImgSrcDictionary = new Dictionary<string, string>();

            var skillItems = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'divsort skill-item')]");
            if (skillItems != null)
            {
                var skills = new List<Dictionary<string, string>>();
                foreach (var item in skillItems)
                {
                    var skillData = new Dictionary<string, string>();
                    foreach (var attr in item.Attributes)
                    {
                        if (attr.Name.StartsWith("data-param"))
                        {
                            skillData[attr.Name] = attr.Value;
                        }
                    }

                    // 存储第一个a标签的链接内容
                    var aTag = item.SelectSingleNode(".//a");
                    if (aTag != null)
                    {
                        skillData["link"] = aTag.GetAttributeValue("href", string.Empty);
                    }

                    // 存储img标签的alt内容和src
                    var imgTags = item.SelectNodes(".//img");
                    if (imgTags != null)
                    {
                        foreach (var imgTag in imgTags)
                        {
                            var alt = imgTag.GetAttributeValue("alt", string.Empty);
                            var src = imgTag.GetAttributeValue("src", string.Empty);
                            if (!string.IsNullOrEmpty(alt))
                            {
                                if (imgTag.GetAttributeValue("height", string.Empty) == "56")
                                {
                                    skillData["img_alt_56"] = alt;
                                }
                                else if (imgTag.GetAttributeValue("height", string.Empty) == "128")
                                {
                                    skillData["img_alt_128"] = alt;
                                }
                                newImgSrcDictionary[alt] = src;
                            }
                        }
                    }

                    // 存储class为"skill-name"的div标签中的文字内容
                    var skillNameDiv = item.SelectSingleNode(".//div[contains(@class, 'skill-name')]");
                    if (skillNameDiv != null)
                    {
                        skillData["skill_name"] = skillNameDiv.InnerText.Trim();
                    }

                    skills.Add(skillData);
                }
                var json = JsonConvert.SerializeObject(skills, Formatting.Indented);

                var filePath = fileName != SkillsFileName ? fileName : Tools.GetAbsolutePath($"{ResourcesDirectory}{fileName}");
                await File.WriteAllTextAsync(filePath, json);
                await MergeImgSrcDictionary(newImgSrcDictionary);

                Console.WriteLine($"技能数据已写入文件: {skills.Count} 条记录");
            }
        }

        public async Task CrawlMemories(HtmlDocument htmlDoc, string fileName = MemoriesFileName)
        {
            var newImgSrcDictionary = new Dictionary<string, string>();

            var memoryItems = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'divsort')]");
            if (memoryItems != null)
            {
                var memories = new List<Dictionary<string, string>>();
                int totalMemories = memoryItems.Count;
                int currentMemory = 0;
                foreach (var item in memoryItems)
                {
                    currentMemory++;
                    OnProgressChanged?.Invoke($"正在获取记忆烙痕列表(2/4)，正在获取烙痕({currentMemory}/{totalMemories})");
                    var memoryData = new Dictionary<string, string>();
                    foreach (var attr in item.Attributes)
                    {
                        if (attr.Name.StartsWith("data-param"))
                        {
                            memoryData[attr.Name] = attr.Value;
                        }
                    }

                    // 存储第一个a标签的链接内容
                    var aTag = item.SelectSingleNode(".//a");
                    if (aTag != null)
                    {
                        memoryData["link"] = aTag.GetAttributeValue("href", string.Empty);
                    }

                    // 存储img标签的alt内容和src
                    var imgTags = item.SelectNodes(".//img");
                    if (imgTags != null)
                    {
                        foreach (var imgTag in imgTags)
                        {
                            var alt = imgTag.GetAttributeValue("alt", string.Empty);
                            var src = imgTag.GetAttributeValue("src", string.Empty);
                            if (!string.IsNullOrEmpty(alt))
                            {
                                newImgSrcDictionary[alt] = src;
                                if (imgTag.GetAttributeValue("height", string.Empty) == "76")
                                {
                                    var width = imgTag.GetAttributeValue("width", string.Empty);
                                    memoryData[$"img_alt_{width}"] = alt;
                                }
                            }
                        }
                    }

                    // 存储style中包括"font-family:SimHei"的div标签中的文字内容
                    var memoryNameDiv = item.SelectSingleNode(".//div[contains(@style, 'font-family:SimHei')]");
                    if (memoryNameDiv != null)
                    {
                        memoryData["memory_name"] = memoryNameDiv.InnerText.Trim();
                    }

                    // 存储class为"bili-tt"的span标签中的data-name内容
                    var biliTtSpans = item.SelectNodes(".//span[contains(@class, 'bili-tt')]");
                    if (biliTtSpans != null)
                    {
                        var skillNames = new List<string>();
                        foreach (var span in biliTtSpans)
                        {
                            var dataName = span.GetAttributeValue("data-name", string.Empty);
                            Debug.Assert(dataName.StartsWith(skill_data_prefix), $"data-name does not start with '{skill_data_prefix}': {dataName}");
                            var skillName = dataName[skill_data_prefix.Length..];
                            skillNames.Add(skillName);
                        }
                        memoryData["skill_names"] = string.Join(", ", skillNames);
                    }

                    // 新增逻辑：判断data-param3是否包含“技能解锁提升”
                    if (memoryData.ContainsKey("data-param3") && memoryData["data-param3"].Contains(Constants.MemorySkillUnlockUpText))
                    {
                        var link = domain_url + memoryData["link"] + Constants.MemoryPropertyPageSuffix;
                        var propertyHtmlDoc = await GetHtmlDocumentFromUrl(link);
                        var skillUnlockRate = await GetSkillUnlockRate(propertyHtmlDoc);
                        Debug.Assert(!string.IsNullOrEmpty(skillUnlockRate), "技能解锁提升几率 should not be empty");
                        memoryData["skill_unlock_rate"] = skillUnlockRate;
                    }

                    memories.Add(memoryData);
                }
                var json = JsonConvert.SerializeObject(memories, Formatting.Indented);

                var filePath = fileName != MemoriesFileName ? fileName : Tools.GetAbsolutePath($"{ResourcesDirectory}{fileName}");
                await File.WriteAllTextAsync(filePath, json);
                await MergeImgSrcDictionary(newImgSrcDictionary);

                Console.WriteLine($"记忆数据已写入文件: {memories.Count} 条记录");
            }
        }

        public async Task CrawlNPCs(HtmlDocument htmlDoc, string fileName = NPCsFileName)
        {
            var newImgSrcDictionary = new Dictionary<string, string>();

            var npcItems = htmlDoc.DocumentNode.SelectNodes("//tr[@data-param0='0']");
            if (npcItems != null)
            {
                var npcs = new List<Dictionary<string, string>>();
                foreach (var item in npcItems)
                {
                    var npcData = new Dictionary<string, string>();

                    // 存储data-param开头的量
                    foreach (var attr in item.Attributes)
                    {
                        if (attr.Name.StartsWith("data-param"))
                        {
                            npcData[attr.Name] = attr.Value;
                        }
                    }

                    // 存储第2个td标签中的文本为npc的名字
                    var tdTags = item.SelectNodes(".//td");
                    if (tdTags != null && tdTags.Count > 1)
                    {
                        npcData["npc_name"] = tdTags[1].InnerText.Trim();
                    }

                    // 存储img标签的alt内容和src
                    var imgTags = item.SelectNodes(".//img");
                    if (imgTags != null)
                    {
                        foreach (var imgTag in imgTags)
                        {
                            var alt = imgTag.GetAttributeValue("alt", string.Empty);
                            var src = imgTag.GetAttributeValue("src", string.Empty);
                            if (!string.IsNullOrEmpty(alt))
                            {
                                if (imgTag.GetAttributeValue("height", string.Empty) == "96")
                                {
                                    npcData["img_alt_96"] = alt;
                                }
                                newImgSrcDictionary[alt] = src;
                            }
                        }
                    }

                    // 存储class为"bili-tt"的a标签中的data-name内容
                    var biliTtATags = item.SelectNodes(".//span[contains(@class, 'bili-tt')]");
                    if (biliTtATags != null)
                    {
                        var skillNames = new List<string>();
                        foreach (var aTag in biliTtATags)
                        {
                            var dataName = aTag.GetAttributeValue("data-name", string.Empty);
                            Debug.Assert(dataName.StartsWith(skill_data_prefix), $"data-name does not start with '{skill_data_prefix}': {dataName}");
                            var skillName = dataName[skill_data_prefix.Length..];
                            skillNames.Add(skillName);
                        }
                        npcData["skill_names"] = string.Join(", ", skillNames);
                    }

                    npcs.Add(npcData);
                }
                var json = JsonConvert.SerializeObject(npcs, Formatting.Indented);

                var filePath = fileName != NPCsFileName ? fileName : Tools.GetAbsolutePath($"{ResourcesDirectory}{fileName}");
                await File.WriteAllTextAsync(filePath, json);
                await MergeImgSrcDictionary(newImgSrcDictionary);

                Console.WriteLine($"NPC数据已写入文件: {npcs.Count} 条记录");
            }
        }

        public async Task MergeImgSrcDictionary(Dictionary<string, string> newImgSrcDictionary)
        {
            var imgSrcFilePath = Tools.GetAbsolutePath($"{ResourcesDirectory}{imgSrcDictionaryFileName}");
            if (File.Exists(imgSrcFilePath))
            {
                var existingJson = await File.ReadAllTextAsync(imgSrcFilePath);
                var existingDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(existingJson);

                foreach (var kvp in existingDictionary)
                {
                    if (!newImgSrcDictionary.ContainsKey(kvp.Key) || !kvp.Value.Contains("thumb"))
                    {
                        newImgSrcDictionary[kvp.Key] = kvp.Value;
                    }
                }
            }

            var mergedJson = JsonConvert.SerializeObject(newImgSrcDictionary, Formatting.Indented);
            await File.WriteAllTextAsync(imgSrcFilePath, mergedJson);
        }

        public async Task DownloadImages()
        {
            var imgSrcFilePath = Tools.GetAbsolutePath($"{ResourcesDirectory}{imgSrcDictionaryFileName}");
            if (File.Exists(imgSrcFilePath))
            {
                var json = await File.ReadAllTextAsync(imgSrcFilePath);
                var imgSrcDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                int totalImages = imgSrcDictionary.Count;
                int currentImage = 0;
                using (HttpClient client = new HttpClient())
                {
                    foreach (var kvp in imgSrcDictionary)
                    {
                        currentImage++;
                        OnProgressChanged?.Invoke($"正在下载图片资源(4/4)，正在获取图片({currentImage}/{totalImages})");
                        var imgUrl = kvp.Value;
                        var imgFileName = kvp.Key;
                        var imgFilePath = Path.Combine(Tools.GetAbsolutePath(ImgDirectory), imgFileName);

                        if (!File.Exists(imgFilePath))
                        {
                            var imgData = await client.GetByteArrayAsync(imgUrl);
                            await File.WriteAllBytesAsync(imgFilePath, imgData);
                            Console.WriteLine($"图片已下载: {imgFileName}");
                        }
                    }
                }
            }
        }

        public async Task StartCrawl()
        {
            OnProgressChanged?.Invoke("正在获取技能列表(1/4)");
            var skillsFilePath = Tools.GetAbsolutePath($"{ResourcesDirectory}{SkillsFileName}");
            if (!File.Exists(skillsFilePath))
            {
                var htmlDoc = await GetHtmlDocumentFromUrl(skill_url);
                await CrawlSkills(htmlDoc);
            }

            OnProgressChanged?.Invoke("正在获取记忆烙痕列表(2/4)");
            var memoriesFilePath = Tools.GetAbsolutePath($"{ResourcesDirectory}{MemoriesFileName}");
            if (!File.Exists(memoriesFilePath))
            {
                var htmlDoc = await GetHtmlDocumentFromUrl(memory_url);
                await CrawlMemories(htmlDoc);
            }

            OnProgressChanged?.Invoke("正在获取角色列表(3/4)");
            var npcsFilePath = Tools.GetAbsolutePath($"{ResourcesDirectory}{NPCsFileName}");
            if (!File.Exists(npcsFilePath))
            {
                var htmlDoc = await GetHtmlDocumentFromUrl(npc_url);
                await CrawlNPCs(htmlDoc);
            }

            OnProgressChanged?.Invoke("正在下载图片资源(4/4)");
            await DownloadImages();
        }
    }
}
