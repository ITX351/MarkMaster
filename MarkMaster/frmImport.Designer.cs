namespace MarkMaster
{
    partial class frmImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rchtxtSkillNames = new RichTextBox();
            btnImport = new Button();
            btnClose = new Button();
            SuspendLayout();
            // 
            // rchtxtSkillNames
            // 
            rchtxtSkillNames.Location = new Point(25, 22);
            rchtxtSkillNames.Name = "rchtxtSkillNames";
            rchtxtSkillNames.Size = new Size(262, 344);
            rchtxtSkillNames.TabIndex = 0;
            rchtxtSkillNames.Text = "";
            // 
            // btnImport
            // 
            btnImport.Location = new Point(117, 372);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(82, 43);
            btnImport.TabIndex = 1;
            btnImport.Text = "导入(&I)";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(205, 372);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(82, 43);
            btnClose.TabIndex = 1;
            btnClose.Text = "关闭(&C)";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // frmImport
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(307, 427);
            Controls.Add(btnClose);
            Controls.Add(btnImport);
            Controls.Add(rchtxtSkillNames);
            Name = "frmImport";
            Text = "导入传承技能";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rchtxtSkillNames;
        private Button btnImport;
        private Button btnClose;
    }
}