namespace MarkMaster
{
    partial class usrctlSkill
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
            picboxSkill = new PictureBox();
            lblSkillName = new Label();
            lblSkillLevel = new Label();
            lblNPCNames = new Label();
            ((System.ComponentModel.ISupportInitialize)picboxSkill).BeginInit();
            SuspendLayout();
            // 
            // picboxSkill
            // 
            picboxSkill.Location = new Point(4, 4);
            picboxSkill.Name = "picboxSkill";
            picboxSkill.Size = new Size(30, 30);
            picboxSkill.TabIndex = 0;
            picboxSkill.TabStop = false;
            picboxSkill.MouseEnter += Control_MouseEnter;
            picboxSkill.MouseLeave += Control_MouseLeave;
            picboxSkill.MouseClick += Control_MouseClick;
            // 
            // lblSkillName
            // 
            lblSkillName.AutoSize = true;
            lblSkillName.Location = new Point(38, 11);
            lblSkillName.Name = "lblSkillName";
            lblSkillName.Size = new Size(56, 17);
            lblSkillName.TabIndex = 1;
            lblSkillName.Text = "技能名称";
            lblSkillName.MouseEnter += Control_MouseEnter;
            lblSkillName.MouseLeave += Control_MouseLeave;
            lblSkillName.MouseClick += Control_MouseClick;
            // 
            // lblSkillLevel
            // 
            lblSkillLevel.AutoSize = true;
            lblSkillLevel.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblSkillLevel.Location = new Point(147, 7);
            lblSkillLevel.Name = "lblSkillLevel";
            lblSkillLevel.Size = new Size(24, 26);
            lblSkillLevel.TabIndex = 2;
            lblSkillLevel.Text = "3";
            lblSkillLevel.MouseClick += Control_MouseClick;
            lblSkillLevel.MouseEnter += Control_MouseEnter;
            lblSkillLevel.MouseLeave += Control_MouseLeave;
            // 
            // lblNPCNames
            // 
            lblNPCNames.AutoSize = true;
            lblNPCNames.ForeColor = Color.Gray;
            lblNPCNames.Location = new Point(38, -2); // 调整位置到更靠上
            lblNPCNames.Font = new Font("Microsoft YaHei UI", 7.25F, FontStyle.Regular, GraphicsUnit.Point, 134); // 进一步减小字体大小
            lblNPCNames.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lblNPCNames.TextAlign = ContentAlignment.TopLeft;
            lblNPCNames.Name = "lblNPCNames";
            lblNPCNames.Size = new Size(0, 17);
            lblNPCNames.TabIndex = 3;
            lblNPCNames.Visible = false;
            // 
            // usrctlSkill
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblNPCNames);
            Controls.Add(lblSkillLevel);
            Controls.Add(lblSkillName);
            Controls.Add(picboxSkill);
            Name = "usrctlSkill";
            Size = new Size(177, 38);
            Load += usrctlSkill_Load;
            ((System.ComponentModel.ISupportInitialize)picboxSkill).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picboxSkill;
        private Label lblSkillName;
        private Label lblSkillLevel;
        private Label lblNPCNames;
    }
}
