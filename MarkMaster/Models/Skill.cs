using MarkMaster.Utils;

namespace MarkMaster.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string ImgSkillRarity { get; set; }
        public string ImgSkillPic { get; set; }
        public string SkillName { get; set; }
        public string[] DataParams { get; set; }
        public List<Memory> Memories { get; set; }
        public List<NPC> NPCs { get; set; }

        public int Level { get; set; }

        public Skill()
        {
            DataParams = new string[0];
            Memories = new List<Memory>();
            NPCs = new List<NPC>();
            Level = 0;
        }

        public int GetSkillTypeValue()
        {
            Constants.SkillTypeValues.TryGetValue(DataParams.Length > 2 ? DataParams[2] : string.Empty, out int skillTypeValue);
            return skillTypeValue;
        }
    }
}
