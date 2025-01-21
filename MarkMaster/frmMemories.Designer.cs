namespace MarkMaster
{
    partial class frmMemories
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
            btnMemoryType5 = new Button();
            btnMemoryType4 = new Button();
            btnMemoryType3 = new Button();
            btnMemoryType2 = new Button();
            btnMemoryType1 = new Button();
            btnMemoryAll = new Button();
            SuspendLayout();
            // 
            // btnMemoryType5
            // 
            btnMemoryType5.Location = new Point(588, 100);
            btnMemoryType5.Name = "btnMemoryType5";
            btnMemoryType5.Size = new Size(61, 33);
            btnMemoryType5.TabIndex = 1;
            btnMemoryType5.Text = "终端(&T)";
            btnMemoryType5.UseVisualStyleBackColor = true;
            // 
            // btnMemoryType4
            // 
            btnMemoryType4.Location = new Point(523, 100);
            btnMemoryType4.Name = "btnMemoryType4";
            btnMemoryType4.Size = new Size(61, 33);
            btnMemoryType4.TabIndex = 2;
            btnMemoryType4.Text = "专精(&R)";
            btnMemoryType4.UseVisualStyleBackColor = true;
            // 
            // btnMemoryType3
            // 
            btnMemoryType3.Location = new Point(458, 100);
            btnMemoryType3.Name = "btnMemoryType3";
            btnMemoryType3.Size = new Size(61, 33);
            btnMemoryType3.TabIndex = 3;
            btnMemoryType3.Text = "攻击(&E)";
            btnMemoryType3.UseVisualStyleBackColor = true;
            // 
            // btnMemoryType2
            // 
            btnMemoryType2.Location = new Point(393, 100);
            btnMemoryType2.Name = "btnMemoryType2";
            btnMemoryType2.Size = new Size(61, 33);
            btnMemoryType2.TabIndex = 4;
            btnMemoryType2.Text = "防御(&W)";
            btnMemoryType2.UseVisualStyleBackColor = true;
            // 
            // btnMemoryType1
            // 
            btnMemoryType1.Location = new Point(328, 100);
            btnMemoryType1.Name = "btnMemoryType1";
            btnMemoryType1.Size = new Size(61, 33);
            btnMemoryType1.TabIndex = 5;
            btnMemoryType1.Text = "体质(&Q)";
            btnMemoryType1.UseVisualStyleBackColor = true;
            // 
            // btnMemoryAll
            // 
            btnMemoryAll.Location = new Point(263, 100);
            btnMemoryAll.Name = "btnMemoryAll";
            btnMemoryAll.Size = new Size(61, 33);
            btnMemoryAll.TabIndex = 6;
            btnMemoryAll.Text = "全部(&A)";
            btnMemoryAll.UseVisualStyleBackColor = true;
            // 
            // frmMemories
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnMemoryType5);
            Controls.Add(btnMemoryType4);
            Controls.Add(btnMemoryType3);
            Controls.Add(btnMemoryType2);
            Controls.Add(btnMemoryType1);
            Controls.Add(btnMemoryAll);
            Name = "frmMemories";
            Text = "漫巡启动！";
            ResumeLayout(false);
        }

        #endregion

        private Button btnMemoryType5;
        private Button btnMemoryType4;
        private Button btnMemoryType3;
        private Button btnMemoryType2;
        private Button btnMemoryType1;
        private Button btnMemoryAll;
    }
}