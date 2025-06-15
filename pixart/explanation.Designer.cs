namespace pixart
{
    partial class explanation
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnMain = new System.Windows.Forms.Button();
            this.btnColorLevel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "설명1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "설명2:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(83, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "설명3:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "설명4:";
            // 
            // btnMain
            // 
            this.btnMain.Location = new System.Drawing.Point(86, 381);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(119, 31);
            this.btnMain.TabIndex = 4;
            this.btnMain.Text = "메인으로";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // btnColorLevel
            // 
            this.btnColorLevel.Location = new System.Drawing.Point(634, 313);
            this.btnColorLevel.Name = "btnColorLevel";
            this.btnColorLevel.Size = new System.Drawing.Size(142, 31);
            this.btnColorLevel.TabIndex = 5;
            this.btnColorLevel.Text = "색상 단계로 색칠하기";
            this.btnColorLevel.UseVisualStyleBackColor = true;
            this.btnColorLevel.Click += new System.EventHandler(this.btnColorLevel_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(634, 381);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 31);
            this.button1.TabIndex = 6;
            this.button1.Text = "KMeans로 색칠하기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // explanation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnColorLevel);
            this.Controls.Add(this.btnMain);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "explanation";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.Button btnColorLevel;
        private System.Windows.Forms.Button button1;
    }
}