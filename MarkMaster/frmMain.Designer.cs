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
            SuspendLayout();
            // 
            // btnCrawler
            // 
            btnCrawler.Location = new Point(399, 167);
            btnCrawler.Name = "btnCrawler";
            btnCrawler.Size = new Size(191, 94);
            btnCrawler.TabIndex = 0;
            btnCrawler.Text = "更新技能烙痕资源(&U)";
            btnCrawler.UseVisualStyleBackColor = true;
            btnCrawler.Click += btnCrawler_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCrawler);
            Name = "frmMain";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnCrawler;
    }
}
