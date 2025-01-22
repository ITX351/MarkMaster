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
using MarkMaster.UserControls;

namespace MarkMaster
{
    public partial class frmSkills : Form
    {
        private readonly List<usrctlSkill> skillControls;
        private int nowType = 0;
        private bool isEditing = false;
        private readonly List<Skill> selectedSkills;
        private readonly List<List<Control>> skillMemoryControls = new List<List<Control>>();

        public frmSkills()
        {
            InitializeComponent();
            skillControls = [];
            selectedSkills = [];
        }

        private void frmSkills_Load(object sender, EventArgs e)
        {
            cboSkillUpperTypeFilter.SelectedIndex = 2;
            cboSkillLevelFilter.SelectedIndex = 0;
            btnSkillType1.Click += (s, ev) => btnSkillTypeChoose_Click(s, ev, 1);
            btnSkillType2.Click += (s, ev) => btnSkillTypeChoose_Click(s, ev, 2);
            btnSkillType3.Click += (s, ev) => btnSkillTypeChoose_Click(s, ev, 3);
            btnSkillType4.Click += (s, ev) => btnSkillTypeChoose_Click(s, ev, 4);
            btnSkillAll.Click += (s, ev) => btnSkillTypeChoose_Click(s, ev, 0);

            foreach (var skill in GlobalData.Instance.Skills)
            {
                var skillControl = new usrctlSkill(skill);
                skillControls.Add(skillControl);
                this.Controls.Add(skillControl);
                skillControl.MouseClick += SkillControl_MouseClick;
                if (skill.NPCs.Count > 0 || skill.Memories.Count > 0)
                {
                    var skillDetailsControl = new usrctlSkillDetails(skill) { Visible = false };
                    skillControl.MouseEnter += (s, ev) => SkillControl_MouseEnter(s, ev, skillDetailsControl);
                    skillControl.MouseLeave += (s, ev) => SkillControl_MouseLeave(s, ev, skillDetailsControl);
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

        private void SkillControl_MouseClick(object sender, EventArgs e)
        {
            var mouseEventArgs = e as MouseEventArgs;
            if (mouseEventArgs != null)
            {
                var skillControl = sender as usrctlSkill;
                if (skillControl != null)
                {
                    var skill = skillControl.Skill;
                    if (!isEditing)
                    {
                        if (selectedSkills.Contains(skill))
                        {
                            selectedSkills.Remove(skill);
                            skillControl.BorderStyle = BorderStyle.None;
                            // 销毁与它相关的控件
                            var memoryControls = skillMemoryControls.FirstOrDefault(mc => mc.First().Tag == skill);
                            if (memoryControls != null)
                            {
                                foreach (var control in memoryControls)
                                {
                                    this.Controls.Remove(control);
                                    control.Dispose();
                                }
                                skillMemoryControls.Remove(memoryControls);
                            }
                        }
                        else
                        {
                            selectedSkills.Add(skill);
                            skillControl.BorderStyle = BorderStyle.Fixed3D;
                        }
                        UpdateSkillMemoryLayout();
                    }
                    else
                    {
                        if (mouseEventArgs.Button == MouseButtons.Left)
                        {
                            skill.DoUpgrade();
                        }
                        else if (mouseEventArgs.Button == MouseButtons.Right)
                        {
                            skill.DoDowngrade();
                        }
                    }
                }
            }
        }

        private void frmSkills_Resize(object sender, EventArgs e)
        {
            ReloadSkills();
            AdjustButtonPositions();
            UpdateSkillMemoryLayout();
        }

        private void ReloadSkills()
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
                skillControl.Visible = (nowType == 0 || nowType == skillType) &&
                    (cboSkillUpperTypeFilter.SelectedIndex == 0 || cboSkillUpperTypeFilter.Items[cboSkillUpperTypeFilter.SelectedIndex]?.ToString() == skill.GetSkillUpperTypeValue()) &&
                    (cboSkillLevelFilter.SelectedIndex == 0 || cboSkillLevelFilter.SelectedIndex - 1 == skill.Level) &&
                    (string.IsNullOrEmpty(txtSearch.Text) || skill.SkillName.Contains(txtSearch.Text) || skill.SkillDesc.Contains(txtSearch.Text));

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
            ReloadSkills();

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

        private void cboSkillUpperTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skillControls.Count > 0)
            {
                ReloadSkills();
            }
        }

        private void cboSkillLevelFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skillControls.Count > 0)
            {
                ReloadSkills();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (skillControls.Count > 0)
            {
                ReloadSkills();
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                foreach (var skillControl in skillControls)
                {
                    skillControl.Skill.DoRestore();
                    skillControl.RefreshSkillLevelColor();
                }
                switchEditingStatus(false);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                foreach (var skillControl in skillControls)
                {
                    skillControl.Skill.DoSave();
                    skillControl.RefreshSkillLevelColor();
                }
                GlobalData.Instance.SaveUserData();
                switchEditingStatus(false);
            }
        }

        private void btnStartEditing_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                selectedSkills.Clear();
                foreach (var skillControl in skillControls)
                {
                    skillControl.BorderStyle = BorderStyle.None;
                    //skillControl.Font = new Font(skillControl.Font, FontStyle.Regular);
                }
                switchEditingStatus(true);
            }
        }

        private void switchEditingStatus(bool newStatus)
        {
            isEditing = newStatus;
            btnStartEditing.Visible = !newStatus;
            btnSave.Visible = newStatus;
            btnRestore.Visible = newStatus;
        }

        private void UpdateSkillMemoryLayout()
        {
            int controlWidth = 177; // 控件的宽度
            int controlHeight = 37; // 控件的高度
            int margin = 10; // 控件之间的间隔
            int formWidth = this.ClientSize.Width; // 当前窗体的宽度

            int x = margin + controlWidth + margin; // 从第二列开始布局
            var lastVisibleSkillControl = skillControls.LastOrDefault(sc => sc.Visible);
            int y = lastVisibleSkillControl != null ? lastVisibleSkillControl.Bottom + margin : margin; // 从最后一个非隐藏的skillControl下方开始布局

            // 更新横线位置
            lblSeparator.Width = formWidth - 2 * margin;
            lblSeparator.Location = new Point(margin, y);
            lblSeparator.Visible = true;
            y += lblSeparator.Height + margin;

            foreach (var skill in selectedSkills)
            {
                var skillControl = skillControls.FirstOrDefault(sc => sc.Skill == skill);
                if (skillControl != null)
                {
                    var memoryControls = skillMemoryControls.FirstOrDefault(mc => mc.First().Tag == skill);
                    if (memoryControls == null)
                    {
                        memoryControls = new List<Control>();
                        var newSkillControl = new usrctlSkill(skill) { Tag = skill };
                        memoryControls.Add(newSkillControl);
                        this.Controls.Add(newSkillControl);

                        foreach (var memory in skill.Memories)
                        {
                            var memoryControl = new usrctlMemory(memory) { Tag = skill };
                            memoryControls.Add(memoryControl);
                            this.Controls.Add(memoryControl);
                        }
                        skillMemoryControls.Add(memoryControls);
                    }

                    y = lastVisibleSkillControl != null ? lastVisibleSkillControl.Bottom + margin : margin; // 每个skill单独占据一列，从顶部开始布局
                    foreach (var control in memoryControls)
                    {
                        control.Location = new Point(x, y);
                        y += controlHeight + margin;
                    }
                    x += controlWidth + margin; // 新的skill另起一列
                }
            }

            // 检查每个memory是否在其他selectedSkills中存在
            foreach (var skill in selectedSkills)
            {
                foreach (var memory in skill.Memories)
                {
                    bool isShared = selectedSkills.Any(s => s != skill && s.Memories.Contains(memory));

                    foreach (var memoryControl in skillMemoryControls
                        .SelectMany(mc => mc)
                        .Where(c => c is usrctlMemory memory1 && memory1._memory == memory).Cast<usrctlMemory>())
                    {
                        memoryControl.BorderStyle = isShared ? BorderStyle.FixedSingle : BorderStyle.None;
                    }
                }
            }
        }
    }
}
