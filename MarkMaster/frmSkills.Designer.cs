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
            cboSkillUpperTypeFilter = new ComboBox();
            cboSkillLevelFilter = new ComboBox();
            txtSearch = new TextBox();
            btnSave = new Button();
            btnRestore = new Button();
            btnStartEditing = new Button();
            lblSeparator = new Label();
            lnklblShowAll = new LinkLabel();
            SuspendLayout();
            // 
            // btnSkillType3
            // 
            btnSkillType3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSkillType3.Location = new Point(851, 12);
            btnSkillType3.Name = "btnSkillType3";
            btnSkillType3.Size = new Size(61, 33);
            btnSkillType3.TabIndex = 7;
            btnSkillType3.Text = "菱形(&E)";
            btnSkillType3.UseVisualStyleBackColor = true;
            // 
            // btnSkillType2
            // 
            btnSkillType2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSkillType2.Location = new Point(786, 12);
            btnSkillType2.Name = "btnSkillType2";
            btnSkillType2.Size = new Size(61, 33);
            btnSkillType2.TabIndex = 6;
            btnSkillType2.Text = "三角(&W)";
            btnSkillType2.UseVisualStyleBackColor = true;
            // 
            // btnSkillType1
            // 
            btnSkillType1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSkillType1.Location = new Point(721, 12);
            btnSkillType1.Name = "btnSkillType1";
            btnSkillType1.Size = new Size(61, 33);
            btnSkillType1.TabIndex = 5;
            btnSkillType1.Text = "方块(&Q)";
            btnSkillType1.UseVisualStyleBackColor = true;
            // 
            // btnSkillAll
            // 
            btnSkillAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSkillAll.Location = new Point(656, 12);
            btnSkillAll.Name = "btnSkillAll";
            btnSkillAll.Size = new Size(61, 33);
            btnSkillAll.TabIndex = 4;
            btnSkillAll.Text = "全部(&A)";
            btnSkillAll.UseVisualStyleBackColor = true;
            // 
            // btnSkillType4
            // 
            btnSkillType4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSkillType4.Location = new Point(918, 12);
            btnSkillType4.Name = "btnSkillType4";
            btnSkillType4.Size = new Size(61, 33);
            btnSkillType4.TabIndex = 8;
            btnSkillType4.Text = "中立(&R)";
            btnSkillType4.UseVisualStyleBackColor = true;
            // 
            // cboSkillUpperTypeFilter
            // 
            cboSkillUpperTypeFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSkillUpperTypeFilter.FormattingEnabled = true;
            cboSkillUpperTypeFilter.Items.AddRange(new object[] { "所有技能", "核心技能", "常规技能", "常规队长技能", "常规非队长技能", "不可习得技能" });
            cboSkillUpperTypeFilter.Location = new Point(152, 15);
            cboSkillUpperTypeFilter.Name = "cboSkillUpperTypeFilter";
            cboSkillUpperTypeFilter.Size = new Size(101, 25);
            cboSkillUpperTypeFilter.TabIndex = 1;
            cboSkillUpperTypeFilter.SelectedIndexChanged += cboSkillUpperTypeFilter_SelectedIndexChanged;
            // 
            // cboSkillLevelFilter
            // 
            cboSkillLevelFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSkillLevelFilter.FormattingEnabled = true;
            cboSkillLevelFilter.Items.AddRange(new object[] { "全部", "未习得", "1级", "2级", "3级", "未3级" });
            cboSkillLevelFilter.Location = new Point(259, 15);
            cboSkillLevelFilter.Name = "cboSkillLevelFilter";
            cboSkillLevelFilter.Size = new Size(59, 25);
            cboSkillLevelFilter.TabIndex = 2;
            cboSkillLevelFilter.SelectedIndexChanged += cboSkillLevelFilter_SelectedIndexChanged;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(380, 17);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(138, 23);
            txtSearch.TabIndex = 3;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 12);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(61, 33);
            btnSave.TabIndex = 4;
            btnSave.Text = "保存(&S)";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Visible = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnRestore
            // 
            btnRestore.Location = new Point(79, 12);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(61, 33);
            btnRestore.TabIndex = 4;
            btnRestore.Text = "还原(&Z)";
            btnRestore.UseVisualStyleBackColor = true;
            btnRestore.Visible = false;
            btnRestore.Click += btnRestore_Click;
            // 
            // btnStartEditing
            // 
            btnStartEditing.Location = new Point(12, 12);
            btnStartEditing.Name = "btnStartEditing";
            btnStartEditing.Size = new Size(128, 33);
            btnStartEditing.TabIndex = 4;
            btnStartEditing.Text = "编辑模式(&M)";
            btnStartEditing.UseVisualStyleBackColor = true;
            btnStartEditing.Click += btnStartEditing_Click;
            // 
            // lblSeparator
            // 
            lblSeparator.BorderStyle = BorderStyle.Fixed3D;
            lblSeparator.Location = new Point(84, 388);
            lblSeparator.Name = "lblSeparator";
            lblSeparator.Size = new Size(50, 3);
            lblSeparator.TabIndex = 9;
            lblSeparator.Visible = false;
            // 
            // lnklblShowAll
            // 
            lnklblShowAll.AutoSize = true;
            lnklblShowAll.Location = new Point(851, 374);
            lnklblShowAll.Name = "lnklblShowAll";
            lnklblShowAll.Size = new Size(65, 17);
            lnklblShowAll.TabIndex = 10;
            lnklblShowAll.TabStop = true;
            lnklblShowAll.Text = "显示全部...";
            lnklblShowAll.Visible = false;
            lnklblShowAll.LinkClicked += lnklblShowAll_LinkClicked;
            // 
            // frmSkills
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(991, 801);
            Controls.Add(lnklblShowAll);
            Controls.Add(lblSeparator);
            Controls.Add(txtSearch);
            Controls.Add(cboSkillLevelFilter);
            Controls.Add(cboSkillUpperTypeFilter);
            Controls.Add(btnSkillType4);
            Controls.Add(btnSkillType3);
            Controls.Add(btnSkillType2);
            Controls.Add(btnSkillType1);
            Controls.Add(btnRestore);
            Controls.Add(btnSave);
            Controls.Add(btnSkillAll);
            Controls.Add(btnStartEditing);
            Name = "frmSkills";
            Text = "传承技能一览";
            FormClosing += frmSkills_FormClosing;
            Load += frmSkills_Load;
            Resize += frmSkills_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSkillType3;
        private Button btnSkillType2;
        private Button btnSkillType1;
        private Button btnSkillAll;
        private Button btnSkillType4;
        private ComboBox cboSkillUpperTypeFilter;
        private ComboBox cboSkillLevelFilter;
        private TextBox txtSearch;
        private Button btnSave;
        private Button btnRestore;
        private Button btnStartEditing;
        private Label lblSeparator;
        private LinkLabel lnklblShowAll;
    }
}