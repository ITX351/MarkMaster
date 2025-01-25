using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarkMaster.Models;

namespace MarkMaster
{
    public partial class frmImport : Form
    {
        public frmImport()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var skillNames = rchtxtSkillNames.Lines;
            var unmatchedSkillNames = new List<string>();
            int matchedCount = 0;

            foreach (var line in skillNames)
            {
                var trimmedLine = line.Trim();
                var skillName = trimmedLine;
                int level = 3;

                if (trimmedLine.Length > 1 && char.IsDigit(trimmedLine[^1]) && int.TryParse(trimmedLine[^1].ToString(), out int parsedLevel) && parsedLevel >= 0 && parsedLevel <= 3)
                {
                    skillName = trimmedLine.Substring(0, trimmedLine.Length - 1).Trim();
                    level = parsedLevel;
                }

                var skill = GlobalData.Instance.Skills.FirstOrDefault(s => s.IsSkillNameEqual(skillName));
                if (skill != null)
                {
                    skill.SetLevel(level);
                    matchedCount++;
                }
                else
                {
                    unmatchedSkillNames.Add(line);
                }
            }

            GlobalData.Instance.SaveUserData();
            rchtxtSkillNames.Lines = unmatchedSkillNames.ToArray();

            MessageBox.Show($"成功导入的技能数量: {matchedCount}\n未成功匹配的技能数量: {unmatchedSkillNames.Count}", "导入结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
