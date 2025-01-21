﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarkMaster.Models;
using MarkMaster.Utils;

namespace MarkMaster
{
    public partial class frmSkills : Form
    {
        private List<usrctlSkill> skillControls;
        private int nowType = 0;

        public frmSkills()
        {
            InitializeComponent();
            skillControls = new List<usrctlSkill>();
        }

        private void frmSkills_Load(object sender, EventArgs e)
        {
            btnSkillType1.Click += (s, ev) => btnSkillTypeChoose_Click(s, ev, 1);
            btnSkillType2.Click += (s, ev) => btnSkillTypeChoose_Click(s, ev, 2);
            btnSkillType3.Click += (s, ev) => btnSkillTypeChoose_Click(s, ev, 3);
            btnSkillType4.Click += (s, ev) => btnSkillTypeChoose_Click(s, ev, 4);
            btnSkillAll.Click += (s, ev) => btnSkillTypeChoose_Click(s, ev, 0);

            foreach (var skill in GlobalData.Instance.Skills)
            {
                if (skill.DataParams.Length > 13 && skill.DataParams[13] == "常规技能" && (skill.NPCs.Count > 0 || skill.Memories.Count > 0))
                {
                    var skillControl = new usrctlSkill(skill);
                    var skillDetailsControl = new usrctlSkillDetails(skill) { Visible = false };
                    skillControl.MouseEnter += (s, ev) => SkillControl_MouseEnter(s, ev, skillDetailsControl);
                    skillControl.MouseLeave += (s, ev) => SkillControl_MouseLeave(s, ev, skillDetailsControl);
                    skillControls.Add(skillControl);
                    this.Controls.Add(skillControl);
                    this.Controls.Add(skillDetailsControl);
                }
            }
            btnSkillTypeChoose_Click(btnSkillAll, EventArgs.Empty, 0);
        }

        private void SkillControl_MouseEnter(object sender, EventArgs e, usrctlSkillDetails skillDetailsControl)
        {
            var skillControl = sender as usrctlSkill;
            if (skillControl != null)
            {
                //Console.WriteLine("Mouse Enter" + skillDetailsControl._skill.SkillName);
                Point location = this.PointToClient(Cursor.Position);
                skillDetailsControl.ShowDetails(new Point(location.X + 10, location.Y + 10));
                skillDetailsControl.BringToFront(); // 确保控件显示在最上层
            }
        }

        private void SkillControl_MouseLeave(object sender, EventArgs e, usrctlSkillDetails skillDetailsControl)
        {
            skillDetailsControl.HideDetails();
        }

        private void frmSkills_Resize(object sender, EventArgs e)
        {
            AdjustLayout();
            AdjustButtonPositions();
        }

        private void AdjustLayout()
        {
            int controlWidth = 177; // usrctlSkill控件的宽度
            int controlHeight = 37; // usrctlSkill控件的高度
            int margin = 10; // 控件之间的间隔
            int formWidth = this.ClientSize.Width; // 当前窗体的宽度
            int controlsPerRow = formWidth / (controlWidth + margin); // 每行显示的控件数量

            int x = margin;
            int y = btnSkillAll.Bottom + margin; // 从按钮下方开始布局

            foreach (var skillControl in skillControls)
            {
                var skill = skillControl.Skill;
                int skillType = skill.GetSkillTypeValue();
                skillControl.Visible = nowType == 0 || nowType == skillType;

                if (skillControl.Visible)
                {
                    skillControl.Location = new Point(x, y);
                    x += controlWidth + margin;
                    if (x + controlWidth + margin > formWidth)
                    {
                        x = margin;
                        y += controlHeight + margin;
                    }
                }
            }
        }

        private void AdjustButtonPositions()
        {
            int margin = 10;
            int buttonWidth = btnSkillAll.Width;
            int buttonHeight = btnSkillAll.Height;
            int formWidth = this.ClientSize.Width;

            btnSkillAll.Location = new Point(formWidth - (buttonWidth + margin) * 5, btnSkillAll.Location.Y);
            btnSkillType1.Location = new Point(formWidth - (buttonWidth + margin) * 4, btnSkillType1.Location.Y);
            btnSkillType2.Location = new Point(formWidth - (buttonWidth + margin) * 3, btnSkillType2.Location.Y);
            btnSkillType3.Location = new Point(formWidth - (buttonWidth + margin) * 2, btnSkillType3.Location.Y);
            btnSkillType4.Location = new Point(formWidth - (buttonWidth + margin), btnSkillType4.Location.Y);
        }

        private void btnSkillTypeChoose_Click(object sender, EventArgs e, int type)
        {
            nowType = type;
            AdjustLayout();

            btnSkillAll.Font = new Font(btnSkillAll.Font, FontStyle.Regular);
            btnSkillType1.Font = new Font(btnSkillType1.Font, FontStyle.Regular);
            btnSkillType2.Font = new Font(btnSkillType2.Font, FontStyle.Regular);
            btnSkillType3.Font = new Font(btnSkillType3.Font, FontStyle.Regular);
            btnSkillType4.Font = new Font(btnSkillType4.Font, FontStyle.Regular);

            var button = sender as Button;
            if (button != null)
            {
                button.Font = new Font(button.Font, FontStyle.Bold);
            }
        }
    }
}
