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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.btnExplanation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExplanation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.btnExplanation.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnExplanation.FlatAppearance.BorderSize = 2;
            this.btnExplanation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExplanation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnExplanation.Location = new System.Drawing.Point(2, 2);
            this.btnExplanation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExplanation.Name = "btnExplanation";
            this.btnExplanation.Size = new System.Drawing.Size(126, 29);
            this.btnExplanation.TabIndex = 0;
            this.btnExplanation.Text = "설명하기";
            this.btnExplanation.UseVisualStyleBackColor = false;
            this.btnExplanation.Click += new System.EventHandler(this.btnExplanation_Click);
            // 
            // btnKmeansColor
            // 
            this.btnKmeansColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKmeansColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.btnKmeansColor.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKmeansColor.FlatAppearance.BorderSize = 2;
            this.btnKmeansColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKmeansColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnKmeansColor.Location = new System.Drawing.Point(272, 2);
            this.btnKmeansColor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnKmeansColor.Name = "btnKmeansColor";
            this.btnKmeansColor.Size = new System.Drawing.Size(145, 29);
            this.btnKmeansColor.TabIndex = 1;
            this.btnKmeansColor.Text = "색상 단계로 색칠하기";
            this.btnKmeansColor.UseVisualStyleBackColor = false;
            this.btnKmeansColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnDualMode
            // 
            this.btnDualMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDualMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.btnDualMode.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDualMode.FlatAppearance.BorderSize = 2;
            this.btnDualMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDualMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnDualMode.Location = new System.Drawing.Point(421, 2);
            this.btnDualMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDualMode.Name = "btnDualMode";
            this.btnDualMode.Size = new System.Drawing.Size(120, 29);
            this.btnDualMode.TabIndex = 2;
            this.btnDualMode.Text = "듀얼모드";
            this.btnDualMode.UseVisualStyleBackColor = false;
            this.btnDualMode.Click += new System.EventHandler(this.btnDualMode_Click);
            // 
            // btnColorLevel
            // 
            this.btnColorLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColorLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.btnColorLevel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnColorLevel.FlatAppearance.BorderSize = 2;
            this.btnColorLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColorLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnColorLevel.Location = new System.Drawing.Point(132, 2);
            this.btnColorLevel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnColorLevel.Name = "btnColorLevel";
            this.btnColorLevel.Size = new System.Drawing.Size(136, 29);
            this.btnColorLevel.TabIndex = 3;
            this.btnColorLevel.Text = "Kmeans로 색칠하기";
            this.btnColorLevel.UseVisualStyleBackColor = false;
            this.btnColorLevel.Click += new System.EventHandler(this.btnColorLevel_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.btnExplanation);
            this.flowLayoutPanel1.Controls.Add(this.btnColorLevel);
            this.flowLayoutPanel1.Controls.Add(this.btnKmeansColor);
            this.flowLayoutPanel1.Controls.Add(this.btnDualMode);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(134, 284);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(545, 33);
            this.flowLayoutPanel1.TabIndex = 4;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // startpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "startpage";
            this.Text = "FixCellArt 시작화면";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnExplanation;
        private System.Windows.Forms.Button btnKmeansColor;
        private System.Windows.Forms.Button btnDualMode;
        private System.Windows.Forms.Button btnColorLevel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
