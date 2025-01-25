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
        public string SkillDesc { get; set; }
        public string[] DataParams { get; set; }
        public List<Memory> Memories { get; set; }
        public List<NPC> NPCs { get; set; }

        public int Level { get; private set; }
        public int NewLevel { get; private set; }
        public int Flag { get; set; }

        public event Action LevelChanged;

        public Skill()
        {
            Link = string.Empty;
            ImgSkillRarity = string.Empty;
            ImgSkillPic = string.Empty;
            SkillName = string.Empty;
            SkillDesc = string.Empty;
            DataParams = [];
            Memories = [];
            NPCs = [];
            Level = 0;
            NewLevel = 0;
            Flag = 0;
        }

        public int GetSkillTypeValue()
        {
            Constants.SkillTypeValues.TryGetValue(DataParams.Length > 2 ? DataParams[2] : string.Empty, out int skillTypeValue);
            return skillTypeValue;
        }

        public string GetSkillUpperTypeValue()
        {
            return DataParams.Length > 13 ? DataParams[13] : string.Empty;
        }

        public void DoRestore()
        {
            NewLevel = Level;
        }
        public void DoSave()
        {
            Level = NewLevel;
        }
        public void DoUpgrade()
        {
            if (++NewLevel > 3)
            {
                NewLevel = 0;
            }
            LevelChanged?.Invoke();
        }
        public void DoDowngrade()
        {
            if (--NewLevel < 0)
            {
                NewLevel = 3;
            }
            LevelChanged?.Invoke();
        }
        public void SetLevel(int level)
        {
            Level = level;
            NewLevel = level;
            LevelChanged?.Invoke();
        }

        private static string NormalizeSkillName(string skillName)
        {
            return skillName.Replace(" ", "").Replace("\t", "").Replace("·", "").Replace("型", "")
                .Replace("α", "alpha").Replace("β", "beta").Replace("alpha", "a").Replace("beta", "b").ToLower();
        }

        public static bool AreSkillNamesEqual(string skillName1, string skillName2)
        {
            return NormalizeSkillName(skillName1) == NormalizeSkillName(skillName2);
        }

        public bool IsSkillNameEqual(string otherSkillName)
        {
            return NormalizeSkillName(this.SkillName) == NormalizeSkillName(otherSkillName);
        }

        public bool DoesSkillNameContain(string otherSkillName)
        {
            return string.IsNullOrEmpty(otherSkillName) || NormalizeSkillName(this.SkillName).Contains(NormalizeSkillName(otherSkillName));
        }

        public bool MatchesSearchTerm(string searchTerm)
        {
            return DoesSkillNameContain(searchTerm) ||
                   Memories.Any(memory => memory.MemoryName.Contains(searchTerm)) ||
                   NPCs.Any(npc => npc.NPCName.Contains(searchTerm));
        }
    }
}
