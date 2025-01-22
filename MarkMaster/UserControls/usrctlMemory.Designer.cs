namespace MarkMaster.UserControls
{
    partial class usrctlMemory
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            picMemoryType = new PictureBox();
            picMemoryRarity = new PictureBox();
            lblMemoryName = new Label();
            lblSkillUnlockRate = new Label();
            ((System.ComponentModel.ISupportInitialize)picMemoryType).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMemoryRarity).BeginInit();
            SuspendLayout();
            // 
            // picMemoryType
            // 
            picMemoryType.Location = new Point(3, 5);
            picMemoryType.Name = "picMemoryType";
            picMemoryType.Size = new Size(26, 26);
            picMemoryType.TabIndex = 0;
            picMemoryType.TabStop = false;
            // 
            // picMemoryRarity
            // 
            picMemoryRarity.Location = new Point(35, 5);
            picMemoryRarity.Name = "picMemoryRarity";
            picMemoryRarity.Size = new Size(26, 26);
            picMemoryRarity.TabIndex = 0;
            picMemoryRarity.TabStop = false;
            // 
            // lblMemoryName
            // 
            lblMemoryName.AutoSize = true;
            lblMemoryName.Location = new Point(65, 9);
            lblMemoryName.Name = "lblMemoryName";
            lblMemoryName.Size = new Size(56, 17);
            lblMemoryName.TabIndex = 1;
            lblMemoryName.Text = "烙痕名称";
            // 
            // lblSkillUnlockRate
            // 
            lblSkillUnlockRate.AutoSize = true;
            lblSkillUnlockRate.Location = new Point(130, 9);
            lblSkillUnlockRate.Name = "lblSkillUnlockRate";
            lblSkillUnlockRate.Size = new Size(16, 17);
            lblSkillUnlockRate.TabIndex = 2;
            lblSkillUnlockRate.Text = "X";
            lblSkillUnlockRate.Visible = false;
            // 
            // usrctlMemory
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblSkillUnlockRate);
            Controls.Add(lblMemoryName);
            Controls.Add(picMemoryRarity);
            Controls.Add(picMemoryType);
            Name = "usrctlMemory";
            Size = new Size(157, 35);
            Load += usrctlMemory_Load;
            ((System.ComponentModel.ISupportInitialize)picMemoryType).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMemoryRarity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picMemoryType;
        private PictureBox picMemoryRarity;
        private Label lblMemoryName;
        private Label lblSkillUnlockRate;
    }
}
