namespace MarkMaster.Models
{
    public class NPC
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string ImgNPCPic { get; set; }
        public string NPCName { get; set; }
        public List<string> SkillNames { get; set; }
        public string[] DataParams { get; set; }
        public List<Skill> Skills { get; set; }

        public NPC()
        {
            SkillNames = new List<string>();
            DataParams = new string[0];
            Skills = new List<Skill>();
        }
    }
}
