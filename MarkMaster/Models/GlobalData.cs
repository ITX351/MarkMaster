using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using MarkMaster.Utils;
using System.Drawing;

namespace MarkMaster.Models
{
    public class GlobalData
    {
        private static GlobalData? _instance;
        public static GlobalData Instance => _instance ??= new GlobalData();

        public List<Skill> Skills { get; private set; }
        public List<Memory> Memories { get; private set; }
        public List<NPC> NPCs { get; private set; }
        private Dictionary<string, Skill> _skillDictionary;
        private Dictionary<string, Image> _preloadedImages;

        private GlobalData()
        {
            Skills = [];
            Memories = [];
            NPCs = [];
            _skillDictionary = null!;
            _preloadedImages = [];
        }

        public bool LoadData()
        {
            var result = LoadSystemData();
            LoadUserData();
            LoadSkillFlags();
            PreloadImages();
            return result;
        }

        public bool LoadSystemData()
        {
            Skills = LoadSkills(Constants.SkillsFileName);
            Memories = LoadMemories(Constants.MemoriesFileName);
            NPCs = LoadNPCs(Constants.NpcsFileName);
            _skillDictionary = Skills.ToDictionary(skill => skill.SkillName, skill => skill);
            EstablishRelationships();
            return Skills.Count > 0 && Memories.Count > 0 && NPCs.Count > 0;
        }

        public bool LoadUserData()
        {
            var filePath = Tools.GetAbsolutePath($"{Constants.UserDataFileName}");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var userData = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);

                foreach (var kvp in userData)
                {
                    if (_skillDictionary.TryGetValue(kvp.Key, out var skill))
                    {
                        skill.SetLevel(kvp.Value);
                    }
                }
                return true;
            }
            return false;
        }

        public bool SaveUserData()
        {
            var userData = Skills.Where(skill => skill.Level > 0)
                                 .ToDictionary(skill => skill.SkillName, skill => skill.Level);

            var json = JsonConvert.SerializeObject(userData, Formatting.Indented);
            var filePath = Tools.GetAbsolutePath($"{Constants.UserDataFileName}");
            File.WriteAllText(filePath, json);

            return true;
        }

        public void LoadSkillFlags()
        {
            var filePath = Tools.GetAbsolutePath($"{Constants.SkillFlagsFileName}");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var skillFlags = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);

                foreach (var kvp in skillFlags)
                {
                    if (_skillDictionary.TryGetValue(kvp.Key, out var skill))
                    {
                        skill.Flag = kvp.Value;
                    }
                }
            }
        }

        public void SaveSkillFlags()
        {
            var skillFlags = Skills.Where(skill => skill.Flag > 0)
                                   .ToDictionary(skill => skill.SkillName, skill => skill.Flag);

            var json = JsonConvert.SerializeObject(skillFlags, Formatting.Indented);
            var filePath = Tools.GetAbsolutePath($"{Constants.SkillFlagsFileName}");
            File.WriteAllText(filePath, json);
        }

        private static List<Skill> LoadSkills(string fileName)
        {
            var filePath = Tools.GetAbsolutePath($"{Constants.ResourcesDirectory}{fileName}");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var skillDictionaries = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
                var skills = new List<Skill>();

                int id = 1;
                foreach (var dict in skillDictionaries)
                {
                    var skill = new Skill
                    {
                        Id = id++,
                        Link = dict.GetValueOrDefault("link"),
                        ImgSkillRarity = dict.GetValueOrDefault("img_alt_56"),
                        ImgSkillPic = dict.GetValueOrDefault("img_alt_128"),
                        SkillName = dict.GetValueOrDefault("skill_name"),
                        DataParams = dict.Where(kvp => kvp.Key.StartsWith("data-param"))
                                         .Select(kvp => kvp.Value)
                                         .ToArray()
                    };
                    skills.Add(skill);
                }
                return skills;
            }
            return [];
        }

        private static List<Memory> LoadMemories(string fileName)
        {
            var filePath = Tools.GetAbsolutePath($"{Constants.ResourcesDirectory}{fileName}");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var memoryDictionaries = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
                var memories = new List<Memory>();

                int id = 1;
                foreach (var dict in memoryDictionaries)
                {
                    var memory = new Memory
                    {
                        Id = id++,
                        Link = dict.GetValueOrDefault("link"),
                        MemoryName = dict.GetValueOrDefault("memory_name"),
                        DataParams = dict.Where(kvp => kvp.Key.StartsWith("data-param"))
                                         .Select(kvp => kvp.Value)
                                         .ToArray(),
                        ImgMemoryPic = dict.GetValueOrDefault("img_alt_192"),
                        ImgMemoryType = dict.GetValueOrDefault("img_alt_44"),
                        ImgMemoryRarity = dict.GetValueOrDefault("img_alt_105"),
                        SkillNames = dict.GetValueOrDefault("skill_names")?.Split(", ").ToList() ?? [],
                        SkillUnlockRate = int.TryParse(dict.GetValueOrDefault("skill_unlock_rate"), out int rate) ? rate : 0
                    };
                    memories.Add(memory);
                }
                return memories;
            }
            return [];
        }

        private static List<NPC> LoadNPCs(string fileName)
        {
            var filePath = Tools.GetAbsolutePath($"{Constants.ResourcesDirectory}{fileName}");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var npcDictionaries = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
                var npcs = new List<NPC>();

                int id = 1;
                foreach (var dict in npcDictionaries)
                {
                    var npc = new NPC
                    {
                        Id = id++,
                        Link = dict.GetValueOrDefault("link"),
                        NPCName = dict.GetValueOrDefault("npc_name"),
                        DataParams = dict.Where(kvp => kvp.Key.StartsWith("data-param"))
                                         .Select(kvp => kvp.Value)
                                         .ToArray(),
                        ImgNPCPic = dict.GetValueOrDefault("img_alt_96"),
                        SkillNames = dict.GetValueOrDefault("skill_names")?.Split(", ").ToList() ?? []
                    };
                    npcs.Add(npc);
                }
                return npcs;
            }
            return [];
        }

        private void EstablishRelationships()
        {
            foreach (var memory in Memories)
            {
                foreach (var skillName in memory.SkillNames)
                {
                    if (_skillDictionary.TryGetValue(skillName, out var skill))
                    {
                        memory.Skills.Add(skill);
                        skill.Memories.Add(memory);
                    }
                }
            }

            foreach (var npc in NPCs)
            {
                foreach (var skillName in npc.SkillNames)
                {
                    if (_skillDictionary.TryGetValue(skillName, out var skill))
                    {
                        npc.Skills.Add(skill);
                        skill.NPCs.Add(npc);
                    }
                }
            }
        }

        private void PreloadImages()
        {
            var dictImg2SrcPath = Tools.GetAbsolutePath($"{Constants.ResourcesDirectory}dict_img2src.json");
            if (File.Exists(dictImg2SrcPath))
            {
                var json = File.ReadAllText(dictImg2SrcPath);
                var imgDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                foreach (var kvp in imgDict)
                {
                    var imgPath = Tools.GetAbsolutePath($"{Constants.ImgDirectory}{kvp.Key}");
                    if (File.Exists(imgPath))
                    {
                        var image = Image.FromFile(imgPath);
                        if (kvp.Key.StartsWith("Skill") || kvp.Key.StartsWith("角色"))
                        {
                            image = new Bitmap(image, new Size(30, 30));
                        }
                        _preloadedImages[kvp.Key] = image;
                    }
                }
            }

            // foreach (var kvp in Constants.ImageKeywords)
            // {
            //     var imgPath = Tools.GetAbsolutePath($"{Constants.StaticImgDirectory}{kvp.Value}");
            //     if (File.Exists(imgPath))
            //     {
            //         _preloadedImages[kvp.Value] = Image.FromFile(imgPath);
            //     }
            // }
        }

        public Image GetPreloadedImage(string key)
        {
            if (_preloadedImages.TryGetValue(key, out var image))
            {
                return image;
            }
            return null;
        }
    }
}
