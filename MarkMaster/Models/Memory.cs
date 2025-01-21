namespace MarkMaster.Models
{
    public class Memory
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string ImgMemoryPic { get; set; }
        public string ImgMemoryType { get; set; }
        public string ImgMemoryRarity { get; set; }
        public string MemoryName { get; set; }
        public List<string> SkillNames { get; set; }
        public string[] DataParams { get; set; }
        public List<Skill> Skills { get; set; }

        public Memory()
        {
            SkillNames = new List<string>();
            DataParams = new string[0];
            Skills = new List<Skill>();
        }
    }
}
