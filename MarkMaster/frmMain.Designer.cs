namespace MarkMaster
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCrawler = new Button();
            btnSkills = new Button();
            lblLoadingStatus = new Label();
            btnImport = new Button();
            lblProgress = new Label();
            SuspendLayout();
            // 
            // btnCrawler
            // 
            btnCrawler.Location = new Point(47, 60);
            btnCrawler.Name = "btnCrawler";
            btnCrawler.Size = new Size(170, 68);
            btnCrawler.TabIndex = 0;
            btnCrawler.Text = "更新技能烙痕资源(&U)";
            btnCrawler.UseVisualStyleBackColor = true;
            btnCrawler.Click += btnCrawler_Click;
            // 
            // btnSkills
            // 
            btnSkills.Location = new Point(47, 141);
            btnSkills.Name = "btnSkills";
            btnSkills.Size = new Size(170, 68);
            btnSkills.TabIndex = 1;
            btnSkills.Text = "传承技能一览(&S)";
            btnSkills.UseVisualStyleBackColor = true;
            btnSkills.Click += btnSkills_Click;
            // 
            // lblLoadingStatus
            // 
            lblLoadingStatus.AutoSize = true;
            lblLoadingStatus.Location = new Point(56, 27);
            lblLoadingStatus.Name = "lblLoadingStatus";
            lblLoadingStatus.Size = new Size(104, 17);
            lblLoadingStatus.TabIndex = 2;
            lblLoadingStatus.Text = "本地数据加载状况";
            // 
            // btnImport
            // 
            btnImport.Location = new Point(47, 222);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(170, 68);
            btnImport.TabIndex = 3;
            btnImport.Text = "用户数据导入(&I)";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(56, 309);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(0, 17);
            lblProgress.TabIndex = 4;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(269, 353);
            Controls.Add(lblProgress);
            Controls.Add(btnImport);
            Controls.Add(lblLoadingStatus);
            Controls.Add(btnSkills);
            Controls.Add(btnCrawler);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmMain";
            Text = "传承技能维护查询系统";
            Load += frmMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCrawler;
        private Button btnSkills;
        private Label lblLoadingStatus;
        private Button btnImport;
        private Label lblProgress;
    }
}
