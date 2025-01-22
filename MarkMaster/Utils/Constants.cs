namespace MarkMaster.Utils
{
    public static class Constants
    {
        public const string DomainUrl = "https://wiki.biligame.com/";
        public const string SkillUrl = DomainUrl + "bjhl/%E6%8A%80%E8%83%BD%E5%9B%BE%E9%89%B4";
        public const string MemoryUrl = DomainUrl + "bjhl/%E8%AE%B0%E5%BF%86%E7%83%99%E7%97%95%E7%AD%9B%E9%80%89";
        public const string NpcUrl = DomainUrl + "bjhl/%E5%90%8C%E8%B0%83%E8%80%85%E7%AD%9B%E9%80%89";
        public const string ResourcesDirectory = "resources/";
        public const string SkillsFileName = "skills.json";
        public const string ImgSrcDictionaryFileName = "dict_img2src.json";
        public const string MemoriesFileName = "memories.json";
        public const string NpcsFileName = "npcs.json";
        public const string SkillDataPrefix = "刻印技能/";
        public const string UserDataFileName = "user_data.json";
        public const string ImgDirectory = ResourcesDirectory + "img/";
        public const string StaticImgDirectory = ResourcesDirectory + "static_img/";
        public const string MemoryPropertyPageSuffix = "#漫巡属性";
        public const string MemorySkillUnlockUpText = "技能解锁提升";

        public static readonly Dictionary<string, string> ImageKeywords = new Dictionary<string, string>
        {
            { "R", "30px-R.png" },
            { "SR", "30px-SR.png" },
            { "SSR", "30px-SSR.png" },
            { "防御", "30px-烙痕_防御.png" },
            { "攻击", "30px-烙痕_攻击.png" },
            { "体质", "30px-烙痕_体质.png" },
            { "终端", "30px-烙痕_终端.png" },
            { "专精", "30px-烙痕_专精.png" }
        };

        public static readonly Dictionary<string, int> SkillTypeValues = new Dictionary<string, int>
        {
            { "方块", 1 },
            { "三角", 2 },
            { "菱形", 3 },
            { "三角、菱形、方块", 4 }
        };
    }
}
