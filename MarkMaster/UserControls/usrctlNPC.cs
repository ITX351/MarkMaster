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

namespace MarkMaster.UserControls
{
    public partial class usrctlNPC : UserControl
    {
        public readonly NPC _npc;

        public usrctlNPC(NPC npc)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            _npc = npc;
        }

        private void usrctlNPC_Load(object sender, EventArgs e)
        {
            lblNPCName.Text = _npc.NPCName;
            var imgFilePath = _npc.ImgNPCPic;
            var image = GlobalData.Instance.GetPreloadedImage(imgFilePath);
            if (image != null)
            {
                picNPC.Image = image;
            }
        }
    }
}
