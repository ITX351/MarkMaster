namespace MarkMaster.UserControls
{
    partial class usrctlNPC
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
            picNPC = new PictureBox();
            lblNPCName = new Label();
            ((System.ComponentModel.ISupportInitialize)picNPC).BeginInit();
            SuspendLayout();
            // 
            // picNPC
            // 
            picNPC.Location = new Point(3, 5);
            picNPC.Name = "picNPC";
            picNPC.Size = new Size(26, 26);
            picNPC.TabIndex = 1;
            picNPC.TabStop = false;
            // 
            // lblNPCName
            // 
            lblNPCName.AutoSize = true;
            lblNPCName.Location = new Point(33, 9);
            lblNPCName.Name = "lblNPCName";
            lblNPCName.Size = new Size(32, 17);
            lblNPCName.TabIndex = 2;
            lblNPCName.Text = "名字";
            // 
            // usrctlNPC
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblNPCName);
            Controls.Add(picNPC);
            Name = "usrctlNPC";
            Size = new Size(122, 34);
            Load += usrctlNPC_Load;
            ((System.ComponentModel.ISupportInitialize)picNPC).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picNPC;
        private Label lblNPCName;
    }
}
