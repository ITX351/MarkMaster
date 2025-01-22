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
    public partial class usrctlSkill : UserControl
    {
        private readonly Skill _skill;

        public usrctlSkill(Skill skill)
        {
            InitializeComponent();
            _skill = skill;
            _skill.LevelChanged += RefreshSkillLevelColor;
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
                var resizedImage = new Bitmap(image, new Size(30, 30));
                picboxSkill.Image = resizedImage;
            }

            lblSkillLevel.Text = _skill.Level.ToString();
            lblSkillLevel.Visible = _skill.Level > 0;
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
