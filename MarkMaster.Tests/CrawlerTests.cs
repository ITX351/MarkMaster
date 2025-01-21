using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MarkMaster.Scripts;
using MarkMaster.Utils;
using Newtonsoft.Json;
using Xunit;
using System.IO;

namespace MarkMaster.Tests
{
    public class CrawlerTests
    {
        private static string absResourcesPath = "C:/PF/VsProjects/MarkMaster/MarkMaster.Tests/TestResources/";

        [Fact]
        public async Task CrawlSkills_ShouldExtractSkillsCorrectly()
        {
            // Arrange
            //var htmlFilePath = Tools.GetAbsolutePath("TestResources/test_skill.html");
            var htmlFilePath = absResourcesPath + "test_skill.html";
            var html = await File.ReadAllTextAsync(htmlFilePath);
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);
            var crawler = new Scripts.Crawler();
            var tempFileName = "temp_skills.json";
            var tempFilePath = absResourcesPath + tempFileName;

            // Act
            await crawler.CrawlSkills(htmlDoc, tempFilePath);

            // Assert
            var expectedSkills = new List<Dictionary<string, string>>
            {
                new() {
                    { "data-param0", "0" },
                    { "data-param1", "ssr" },
                    { "data-param2", "三角、菱形、方块" },
                    { "data-param3", "无" },
                    { "data-param4", "水, 炎, 雷, 霜" },
                    { "data-param5", "" },
                    { "data-param6", "永久生效" },
                    { "data-param7", "" },
                    { "data-param8", "" },
                    { "data-param9", "目标受伤害增加" },
                    { "data-param10", "" },
                    { "data-param11", "" },
                    { "data-param12", "队长刻印技能" },
                    { "data-param13", "核心技能" },
                    { "link", "/bjhl/%E5%88%BB%E5%8D%B0%E6%8A%80%E8%83%BD/%E9%AB%98%E5%8E%8B%E9%9B%B7%E5%87%BB" },
                    { "img_alt_56", "烙痕技能 ssr.png" },
                    { "img_alt_128", "Skill core 146 all.png" },
                    { "skill_name", "高压雷击" }
                }
            };
            var expectedJson = JsonConvert.SerializeObject(expectedSkills, Formatting.Indented);
            var actualJson = File.ReadAllText(tempFilePath);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public async Task CrawlMemories_ShouldExtractMemoriesCorrectly()
        {
            // Arrange
            var htmlFilePath = absResourcesPath + "test_memory.html";
            var html = await File.ReadAllTextAsync(htmlFilePath);
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);
            var crawler = new Scripts.Crawler();
            var tempFileName = "temp_memories.json";
            var tempFilePath = absResourcesPath + tempFileName;

            // Act
            await crawler.CrawlMemories(htmlDoc, tempFilePath);

            // Assert
            var expectedMemories = new List<Dictionary<string, string>>
            {
                new() {
                    { "data-param0", "0" },
                    { "data-param1", "SSR" },
                    { "data-param2", "体质" },
                    { "data-param3", "体质获取提升, 初始专精提升, 初始体质提升, 初始防御提升, 点亮几率提升, 点亮增加, 点亮获取增幅, 烙痕唤醒奖励增加" },
                    { "data-param4", "Aurogon Shanghai" },
                    { "data-param5", "" },
                    { "data-param6", "物理, 通用" },
                    { "data-param7", "额外伤害" },
                    { "data-param8", "受伤害减少" },
                    { "data-param9", "" },
                    { "data-param10", "忽略减伤" },
                    { "link", "/bjhl/%E8%AE%B0%E5%BF%86%E7%83%99%E7%97%95/%E6%80%AF%E8%80%85%E7%9A%84%E5%A2%93%E5%BF%97%E9%93%AD" },
                    { "img_alt_192", "记忆烙痕 怯者的墓志铭 缩略图2.png" },
                    { "img_alt_44", "UI 烙痕缩略图2 属性 体质.png" },
                    { "img_alt_105", "UI 烙痕缩略图2 稀有度 SSR.png" },
                    { "skill_names", "唤物充能, 人形特攻·α型, 强攻·对地·方块, 无处可藏, 核心充能·方块·α型, 生生不息·方块, 职业联动·方块, 距离把控" }
                }
            };
            var expectedJson = JsonConvert.SerializeObject(expectedMemories, Formatting.Indented);
            var actualJson = File.ReadAllText(tempFilePath);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public async Task CrawlNPCs_ShouldExtractNPCsCorrectly()
        {
            // Arrange
            var htmlFilePath = absResourcesPath + "test_npc.html";
            var html = await File.ReadAllTextAsync(htmlFilePath);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var crawler = new Crawler();
            var tempFileName = "temp_npcs.json";
            var tempFilePath = absResourcesPath + tempFileName;

            // Act
            await crawler.CrawlNPCs(htmlDoc, tempFilePath);

            // Assert
            var expectedNPCs = new List<Dictionary<string, string>>
            {
                new() {
                    { "data-param0", "0" },
                    { "data-param1", "6" },
                    { "data-param2", "雷" },
                    { "data-param3", "轻卫" },
                    { "data-param4", "男" },
                    { "data-param5", "森罗" },
                    { "data-param6", "古剑奇谭二" },
                    { "data-param7", "定向共鸣, 常态共鸣" },
                    { "data-param8", "伤害, 对空, 屏障, 拦截, 格挡条破坏1, 穿透屏障, 自身增益, 负面状态, 输出, 防护" },
                    { "npc_name", "乐无异" },
                    { "img_alt_96", "角色 乐无异 头像.png" },
                    { "skill_names", "一线生机, 卸劲化能" }
                }
            };
            var expectedJson = JsonConvert.SerializeObject(expectedNPCs, Formatting.Indented);
            var actualJson = await File.ReadAllTextAsync(tempFilePath);
            Assert.Equal(expectedJson, actualJson);
        }
    }
}