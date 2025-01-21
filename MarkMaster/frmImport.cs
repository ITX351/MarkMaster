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

            foreach (var skillName in skillNames)
            {
                var skill = GlobalData.Instance.Skills.FirstOrDefault(s => s.SkillName == skillName.Trim());
                if (skill != null)
                {
                    skill.Level = 3;
                    matchedCount++;
                }
                else
                {
                    unmatchedSkillNames.Add(skillName);
                }
            }

            GlobalData.Instance.SaveUserData();
            rchtxtSkillNames.Lines = [.. unmatchedSkillNames];

            MessageBox.Show($"成功导入的技能数量: {matchedCount}\n未成功匹配的技能数量: {unmatchedSkillNames.Count}", "导入结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
