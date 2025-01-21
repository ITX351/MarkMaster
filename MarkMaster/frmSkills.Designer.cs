namespace MarkMaster
{
    partial class frmSkills
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
            btnSkillType3 = new Button();
            btnSkillType2 = new Button();
            btnSkillType1 = new Button();
            btnSkillAll = new Button();
            btnSkillType4 = new Button();
            SuspendLayout();
            // 
            // btnSkillType3
            // 
            btnSkillType3.Location = new Point(801, 12);
            btnSkillType3.Name = "btnSkillType3";
            btnSkillType3.Size = new Size(61, 33);
            btnSkillType3.TabIndex = 7;
            btnSkillType3.Text = "菱形(&E)";
            btnSkillType3.UseVisualStyleBackColor = true;
            btnSkillType3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // 
            // btnSkillType2
            // 
            btnSkillType2.Location = new Point(736, 12);
            btnSkillType2.Name = "btnSkillType2";
            btnSkillType2.Size = new Size(61, 33);
            btnSkillType2.TabIndex = 8;
            btnSkillType2.Text = "三角(&W)";
            btnSkillType2.UseVisualStyleBackColor = true;
            btnSkillType2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // 
            // btnSkillType1
            // 
            btnSkillType1.Location = new Point(671, 12);
            btnSkillType1.Name = "btnSkillType1";
            btnSkillType1.Size = new Size(61, 33);
            btnSkillType1.TabIndex = 9;
            btnSkillType1.Text = "方块(&Q)";
            btnSkillType1.UseVisualStyleBackColor = true;
            btnSkillType1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // 
            // btnSkillAll
            // 
            btnSkillAll.Location = new Point(606, 12);
            btnSkillAll.Name = "btnSkillAll";
            btnSkillAll.Size = new Size(61, 33);
            btnSkillAll.TabIndex = 10;
            btnSkillAll.Text = "全部(&A)";
            btnSkillAll.UseVisualStyleBackColor = true;
            btnSkillAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // 
            // btnSkillType4
            // 
            btnSkillType4.Location = new Point(868, 12);
            btnSkillType4.Name = "btnSkillType4";
            btnSkillType4.Size = new Size(61, 33);
            btnSkillType4.TabIndex = 7;
            btnSkillType4.Text = "中立(&R)";
            btnSkillType4.UseVisualStyleBackColor = true;
            btnSkillType4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // 
            // frmSkills
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(941, 801);
            Controls.Add(btnSkillType4);
            Controls.Add(btnSkillType3);
            Controls.Add(btnSkillType2);
            Controls.Add(btnSkillType1);
            Controls.Add(btnSkillAll);
            Name = "frmSkills";
            Text = "传承技能一览";
            Load += frmSkills_Load;
            Resize += frmSkills_Resize;
            ResumeLayout(false);
        }

        #endregion

        private Button btnSkillType3;
        private Button btnSkillType2;
        private Button btnSkillType1;
        private Button btnSkillAll;
        private Button btnSkillType4;
    }
}