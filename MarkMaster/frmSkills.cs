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
        private static readonly Color[] FlagColors = {
            Color.Transparent, // 0
            Color.LightBlue,   // 1 Ctrl左
            Color.LightGreen,  // 2 Ctrl右
            Color.Gold,        // 3 Alt左
            Color.LightCoral,  // 4 Alt右
            Color.LightPink,   // 5 Shift左
            Color.DarkGray     // 6 Shift右
        };
        private bool isLoaded = false;
        private bool ShowingAll = false;
        private readonly List<Button> skillTypeButtons;

        public frmSkills()
        {
            InitializeComponent();
            skillControls = new List<usrctlSkill>();
            selectedSkills = new List<Skill>();

            skillTypeButtons = new List<Button> { btnSkillAll, btnSkillType1, btnSkillType2, btnSkillType3, btnSkillType4 };
            for (int i = 0; i < skillTypeButtons.Count; i++)
            {
                int type = i; // 需要一个局部变量来捕获当前的i值
                skillTypeButtons[i].Click += (s, ev) => btnSkillTypeChoose_Click(s, ev, type);
            }
        }

        private void frmSkills_Load(object sender, EventArgs e)
        {
            cboSkillUpperTypeFilter.SelectedIndex = 2;
            cboSkillLevelFilter.SelectedIndex = 0;

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
                skillControl.BackColor = GetFlagColor(skill.Flag);
            }
            btnSkillTypeChoose_Click(btnSkillAll, EventArgs.Empty, 0);
            //ReloadAndLayoutSkills();
            isLoaded = true; // 在 Load 事件结束时设置标记
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
                    bool isCtrlPressed = (Control.ModifierKeys & Keys.Control) == Keys.Control;
                    bool isAltPressed = (Control.ModifierKeys & Keys.Alt) == Keys.Alt;
                    bool isShiftPressed = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

                    if (isCtrlPressed || isAltPressed || isShiftPressed)
                    {
                        int flag = 0;
                        if (isCtrlPressed && mouseEventArgs.Button == MouseButtons.Left) flag = 1;
                        else if (isCtrlPressed && mouseEventArgs.Button == MouseButtons.Right) flag = 2;
                        else if (isAltPressed && mouseEventArgs.Button == MouseButtons.Left) flag = 3;
                        else if (isAltPressed && mouseEventArgs.Button == MouseButtons.Right) flag = 4;
                        else if (isShiftPressed && mouseEventArgs.Button == MouseButtons.Left) flag = 5;
                        else if (isShiftPressed && mouseEventArgs.Button == MouseButtons.Right) flag = 6;

                        if (flag > 0)
                        {
                            if (skill.Flag == flag)
                            {
                                skill.Flag = 0;
                                skillControl.BackColor = Color.Transparent;
                            }
                            else
                            {
                                skill.Flag = flag;
                                skillControl.BackColor = GetFlagColor(flag);
                            }
                        }
                    }
                    else if (!isEditing)
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
                        UpdateSelectedSkillsCount();
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

        private Color GetFlagColor(int flag)
        {
            if (flag >= 0 && flag < FlagColors.Length)
            {
                return FlagColors[flag];
            }
            return Color.Transparent;
        }

        private void frmSkills_Resize(object sender, EventArgs e)
        {
            if (!isLoaded) return; // 检查标记
            ReloadAndLayoutSkills();
            AdjustButtonPositions();
        }

        private void ReloadSkills()
        {
            if (skillControls.Count == 0) return;
            int controlWidth = skillControls[0].Width; // usrctlSkill控件的宽度
            int controlHeight = skillControls[0].Height; // usrctlSkill控件的高度
            int margin = 10; // 控件之间的间隔
            int formWidth = this.ClientSize.Width; // 当前窗体的宽度
            int controlsPerRow = formWidth / (controlWidth + margin); // 每行显示的控件数量

            int x = margin;
            int y = btnSkillAll.Bottom + margin; // 从按钮下方开始布局

            int controlCount = 0; // 计数器
            lnklblShowAll.Visible = false;
            bool lnklblShowAllShown = false;

            foreach (var skillControl in skillControls)
            {
                var skill = skillControl.Skill;
                int skillType = skill.GetSkillTypeValue();
                int cboSkillUpperTypeFilterIndex = cboSkillUpperTypeFilter.SelectedIndex > 2 ? 2 : cboSkillUpperTypeFilter.SelectedIndex;
                bool thisVisible = (nowType == 0 || nowType == skillType) &&
                    (cboSkillLevelFilter.SelectedIndex == 0 || cboSkillLevelFilter.SelectedIndex - 1 == skill.Level || 
                    (cboSkillLevelFilter.SelectedIndex == 5 && skill.Level >= 0 && skill.Level <= 2)) &&
                    //(string.IsNullOrEmpty(txtSearch.Text) || skill.SkillName.Contains(txtSearch.Text) || skill.SkillDesc.Contains(txtSearch.Text));
                    //skill.DoesSkillNameContain(txtSearch.Text);
                    skill.MatchesSearchTerm(txtSearch.Text);
                thisVisible = thisVisible &&
                    (cboSkillUpperTypeFilter.SelectedIndex == 0 || cboSkillUpperTypeFilter.Items[cboSkillUpperTypeFilterIndex]?.ToString() == skill.GetSkillUpperTypeValue()) &&
                    (cboSkillUpperTypeFilter.SelectedIndex < 2 ||
                    (cboSkillUpperTypeFilter.SelectedIndex == 2 && (skill.Memories.Count > 0 || skill.NPCs.Count > 0)) ||
                    (cboSkillUpperTypeFilter.SelectedIndex == 3 && skill.NPCs.Count > 0) ||
                    (cboSkillUpperTypeFilter.SelectedIndex == 4 && skill.Memories.Count > 0 && skill.NPCs.Count == 0) ||
                    (cboSkillUpperTypeFilter.SelectedIndex == 5 && skill.Memories.Count == 0 && skill.NPCs.Count == 0));

                skillControl.Visible = false;
                if (thisVisible)
                {
                    controlCount++;
                    if (!ShowingAll && controlCount > controlsPerRow * 9)
                    {
                        if (!lnklblShowAllShown)
                        {
                            lnklblShowAllShown = true;
                            lnklblShowAll.Location = new Point(x, y);
                            lnklblShowAll.Visible = true;
                        }
                    }
                    else
                    {
                        skillControl.Visible = true;
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
            ReloadAndLayoutSkills();

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

        private void FilterChanged(object sender, EventArgs e)
        {
            if (skillControls.Count > 0)
            {
                ReloadAndLayoutSkills();
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
                ClearSelectedSkills();
                switchEditingStatus(true);
            }
        }

        private void ClearSelectedSkills()
        {
            foreach (var skill in selectedSkills)
            {
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
            selectedSkills.Clear();
            foreach (var skillControl in skillControls)
            {
                skillControl.BorderStyle = BorderStyle.None;
                //skillControl.Font = new Font(skillControl.Font, FontStyle.Regular);
            }
            UpdateSelectedSkillsCount();
        }

        private void switchEditingStatus(bool newStatus)
        {
            isEditing = newStatus;
            btnStartEditing.Visible = !newStatus;
            btnSave.Visible = newStatus;
            btnRestore.Visible = newStatus;

            skillTypeButtons[nowType].Focus();
        }

        private void UpdateSkillMemoryLayout()
        {
            int controlWidth = skillControls[0].Width; // 控件的宽度
            int controlHeight = skillControls[0].Height; // 控件的高度
            int margin = 10; // 控件之间的间隔
            int formWidth = this.ClientSize.Width; // 当前窗体的宽度

            int x = margin + controlWidth + margin; // 从第二列开始布局
            var lastVisibleSkillControl = skillControls.LastOrDefault(sc => sc.Visible);
            int y = lastVisibleSkillControl != null ? lastVisibleSkillControl.Bottom + margin : btnSkillAll.Bottom + margin; // 从最后一个非隐藏的skillControl下方开始布局

            // 更新横线位置
            lblSeparator.Width = formWidth - 2 * margin;
            lblSeparator.Location = new Point(margin, y);
            lblSeparator.Visible = true;
            y += lblSeparator.Height + margin;

            // 更新Label和Button的位置
            const int ExtraHeight = 60;
            lblSelectedSkillsCount.Location = new Point(margin, y + ExtraHeight);
            btnClearSelectedSkills.Location = new Point(margin, y + ExtraHeight + lblSelectedSkillsCount.Height + margin);
            y += lblSelectedSkillsCount.Height + btnClearSelectedSkills.Height + 2 * margin;

            if (selectedSkills.Count == 0)
            {
                //lblSeparator.Visible = false;
                return;
            }

            foreach (var skill in selectedSkills)
            {
                var skillControl = skillControls.FirstOrDefault(sc => sc.Skill == skill);
                if (skillControl != null)
                {
                    var memoryControls = skillMemoryControls.FirstOrDefault(mc => mc.First().Tag == skill);
                    if (memoryControls == null)
                    {
                        memoryControls = new List<Control>();
                        var newSkillControl = new usrctlSkill(skill) { Tag = skill, Visible = true };
                        newSkillControl.MouseClick += NewSkillControl_MouseClick; // 添加右键单击事件
                        memoryControls.Add(newSkillControl);
                        this.Controls.Add(newSkillControl);

                        foreach (var memory in skill.Memories)
                        {
                            var memoryControl = new usrctlMemory(memory) { Tag = skill };
                            memoryControl.MouseClick += usrctlMemory_MouseClick; // 添加Click事件
                            memoryControls.Add(memoryControl);
                            this.Controls.Add(memoryControl);
                        }
                        skillMemoryControls.Add(memoryControls);
                    }

                    y = lastVisibleSkillControl != null ? lastVisibleSkillControl.Bottom + margin : btnSkillAll.Bottom + margin; // 每个skill单独占据一列，从顶部开始布局
                    foreach (var control in memoryControls)
                    {
                        control.Location = new Point(x, y);
                        y += controlHeight + margin;

                        if (control is usrctlMemory memoryControl)
                        {
                            memoryControl.BackColor = memoryControl._memory.IsSelected ? Color.Khaki : Color.Transparent;
                        }
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

        private void NewSkillControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var skillControl = sender as usrctlSkill;
                if (skillControl != null)
                {
                    var skill = skillControl.Skill;
                    if (selectedSkills.Contains(skill))
                    {
                        selectedSkills.Remove(skill);
                        var originalSkillControl = skillControls.FirstOrDefault(sc => sc.Skill == skill);
                        if (originalSkillControl != null)
                        {
                            originalSkillControl.BorderStyle = BorderStyle.None;
                        }
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
                        UpdateSkillMemoryLayout();
                        UpdateSelectedSkillsCount();
                    }
                }
            }
        }

        private void usrctlMemory_MouseClick(object sender, EventArgs e)
        {
            var memoryControl = sender as usrctlMemory;
            if (memoryControl != null)
            {
                memoryControl._memory.IsSelected = !memoryControl._memory.IsSelected;
                UpdateSkillMemoryLayout(); // 调用UpdateSkillMemoryLayout
            }
        }

        private void ReloadAndLayoutSkills()
        {
            ReloadSkills();
            UpdateSkillMemoryLayout();
        }

        private void frmSkills_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalData.Instance.SaveSkillFlags();
        }

        private void lnklblShowAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnklblShowAll.Visible = false;
            ShowingAll = true;
            ReloadAndLayoutSkills();
        }

        private void BtnClearSelectedSkills_Click(object sender, EventArgs e)
        {
            ClearSelectedSkills();
            UpdateSelectedSkillsCount();
        }

        private void UpdateSelectedSkillsCount()
        {
            lblSelectedSkillsCount.Text = $"已选择技能: {selectedSkills.Count}";
        }
    }
}
