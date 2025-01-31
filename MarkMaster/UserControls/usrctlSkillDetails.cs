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
using MarkMaster.UserControls;

namespace MarkMaster
{
    public partial class usrctlSkillDetails : UserControl
    {
        public readonly Skill _skill;

        public usrctlSkillDetails(Skill skill)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            _skill = skill;
        }

        private void usrctlSkillDetails_Load(object sender, EventArgs e)
        {
            int yOffset = 10;
            foreach (var memory in _skill.Memories)
            {
                var memoryControl = new usrctlMemory(memory)
                {
                    Location = new Point(10, yOffset)
                };
                Controls.Add(memoryControl);
                yOffset += memoryControl.Height + 10;
            }

            foreach (var npc in _skill.NPCs)
            {
                var npcControl = new usrctlNPC(npc)
                {
                    Location = new Point(10, yOffset)
                };
                Controls.Add(npcControl);
                yOffset += npcControl.Height + 10;
            }

            this.Size = new Size(this.Width, yOffset + 10);
        }

        public void ShowDetails(Point location)
        {
            this.Location = location;
            this.Visible = true;
        }

        public void HideDetails()
        {
            this.Visible = false;
        }
    }
}
