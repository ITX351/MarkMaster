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

namespace MarkMaster
{
    public partial class usrctlSkill : UserControl
    {
        private readonly Skill _skill;

        public usrctlSkill(Skill skill)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            _skill = skill;
            _skill.LevelChanged += RefreshSkillLevelColor;
            this.Visible = false;
        }

        public Skill Skill
        {
            get { return _skill; }
        }

        private void usrctlSkill_Load(object sender, EventArgs e)
        {
            lblSkillName.Text = _skill.SkillName;
            var imgFilePath = _skill.ImgSkillPic;
            var image = GlobalData.Instance.GetPreloadedImage(imgFilePath);
            if (image != null)
            {
                picboxSkill.Image = image;
            }
            else
            {
                picboxSkill.Image = Resources.DefaultSkill;
            }

            lblSkillLevel.Text = _skill.Level.ToString();
            lblSkillLevel.Visible = _skill.Level > 0;

            if (_skill.NPCs.Count > 0)
            {
                lblNPCNames.Visible = true;
                lblNPCNames.Text = string.Join("/", _skill.NPCs.Select(npc => npc.NPCName[0]));
                //lblNPCNames.Left = this.Width - lblNPCNames.Width - 4; // 确保右对齐
                lblNPCNames.SendToBack(); // 设置为最下层
            }
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void Control_MouseClick(object sender, EventArgs e)
        {
            OnMouseClick(e as MouseEventArgs);
        }

        public void RefreshSkillLevelColor()
        {
            lblSkillLevel.Text = _skill.NewLevel.ToString();
            lblSkillLevel.ForeColor = _skill.NewLevel != _skill.Level ? Color.Red : Color.Black;
            lblSkillLevel.Visible = _skill.NewLevel != _skill.Level || _skill.Level > 0;
        }
    }
}
