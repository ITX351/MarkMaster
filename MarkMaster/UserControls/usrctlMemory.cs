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
    public partial class usrctlMemory : UserControl
    {
        public Memory _memory;
        public usrctlMemory(Memory memory)
        {
            InitializeComponent();
            _memory = memory;
        }

        private void usrctlMemory_Load(object sender, EventArgs e)
        {
            lblMemoryName.Text = _memory.MemoryName;

            if (_memory.SkillUnlockRate != 0)
            {
                lblMemoryName.Font = new Font(lblMemoryName.Font, FontStyle.Bold);
                lblSkillUnlockRate.Text = $"{_memory.SkillUnlockRate}";
                lblSkillUnlockRate.Visible = true;
            }

            if (Constants.ImageKeywords.TryGetValue(_memory.DataParams[1], out string rarityImage))
            {
                picMemoryRarity.Image = GlobalData.Instance.GetPreloadedImage(rarityImage);
            }

            if (Constants.ImageKeywords.TryGetValue(_memory.DataParams[2], out string typeImage))
            {
                picMemoryType.Image = GlobalData.Instance.GetPreloadedImage(typeImage);
            }
        }
    }
}
