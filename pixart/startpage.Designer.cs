namespace pixart
{
    partial class startpage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(startpage));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnExplanation = new System.Windows.Forms.Button();
            this.btnKmeansColor = new System.Windows.Forms.Button();
            this.btnDualMode = new System.Windows.Forms.Button();
            this.btnColorLevel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnExplanation
            // 
            this.btnExplanation.BackColor = System.Drawing.Color.FromArgb(230, 235, 240);
            this.btnExplanation.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnExplanation.FlatAppearance.BorderSize = 2;
            this.btnExplanation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExplanation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnExplanation.Location = new System.Drawing.Point(179, 486);
            this.btnExplanation.Name = "btnExplanation";
            this.btnExplanation.Size = new System.Drawing.Size(180, 44);
            this.btnExplanation.TabIndex = 0;
            this.btnExplanation.Text = "설명하기";
            this.btnExplanation.UseVisualStyleBackColor = false;
            this.btnExplanation.Click += new System.EventHandler(this.btnExplanation_Click);
            // 
            // btnKmeansColor
            // 
            this.btnKmeansColor.BackColor = System.Drawing.Color.FromArgb(230, 235, 240);
            this.btnKmeansColor.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKmeansColor.FlatAppearance.BorderSize = 2;
            this.btnKmeansColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKmeansColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnKmeansColor.Location = new System.Drawing.Point(570, 486);
            this.btnKmeansColor.Name = "btnKmeansColor";
            this.btnKmeansColor.Size = new System.Drawing.Size(207, 44);
            this.btnKmeansColor.TabIndex = 1;
            this.btnKmeansColor.Text = "색상 단계로 색칠하기";
            this.btnKmeansColor.UseVisualStyleBackColor = false;
            this.btnKmeansColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnDualMode
            // 
            this.btnDualMode.BackColor = System.Drawing.Color.FromArgb(230, 235, 240);
            this.btnDualMode.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDualMode.FlatAppearance.BorderSize = 2;
            this.btnDualMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDualMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnDualMode.Location = new System.Drawing.Point(785, 486);
            this.btnDualMode.Name = "btnDualMode";
            this.btnDualMode.Size = new System.Drawing.Size(172, 44);
            this.btnDualMode.TabIndex = 2;
            this.btnDualMode.Text = "듀얼모드";
            this.btnDualMode.UseVisualStyleBackColor = false;
            this.btnDualMode.Click += new System.EventHandler(this.btnDualMode_Click);
            // 
            // btnColorLevel
            // 
            this.btnColorLevel.BackColor = System.Drawing.Color.FromArgb(230, 235, 240);
            this.btnColorLevel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnColorLevel.FlatAppearance.BorderSize = 2;
            this.btnColorLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColorLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnColorLevel.Location = new System.Drawing.Point(367, 486);
            this.btnColorLevel.Name = "btnColorLevel";
            this.btnColorLevel.Size = new System.Drawing.Size(195, 44);
            this.btnColorLevel.TabIndex = 3;
            this.btnColorLevel.Text = "Kmeans로 색칠하기";
            this.btnColorLevel.UseVisualStyleBackColor = false;
            this.btnColorLevel.Click += new System.EventHandler(this.btnColorLevel_Click);
            // 
            // startpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1143, 675);
            this.Controls.Add(this.btnColorLevel);
            this.Controls.Add(this.btnDualMode);
            this.Controls.Add(this.btnKmeansColor);
            this.Controls.Add(this.btnExplanation);
            this.DoubleBuffered = true;
            this.Name = "startpage";
            this.Text = "FixCellArt 시작화면３";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnExplanation;
        private System.Windows.Forms.Button btnKmeansColor;
        private System.Windows.Forms.Button btnDualMode;
        private System.Windows.Forms.Button btnColorLevel;
    }
}
