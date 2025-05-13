namespace PixelColorling
{
    partial class Coloring
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
            this.panelPalette = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCanvas = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.numPixelSize = new System.Windows.Forms.NumericUpDown();
            this.btnColorAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).BeginInit();
            this.SuspendLayout();
            // 
            // panelPalette
            // 
            this.panelPalette.AutoScroll = true;
            this.panelPalette.Location = new System.Drawing.Point(10, 143);
            this.panelPalette.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelPalette.Name = "panelPalette";
            this.panelPalette.Size = new System.Drawing.Size(192, 485);
            this.panelPalette.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "가로 픽셀수";
            // 
            // panelCanvas
            // 
            this.panelCanvas.Location = new System.Drawing.Point(208, 12);
            this.panelCanvas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(894, 616);
            this.panelCanvas.TabIndex = 13;
            this.panelCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCanvas_Paint);
            this.panelCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelCanvas_MouseClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(109, 55);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 21);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "결과 저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(10, 55);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(94, 21);
            this.btnGenerate.TabIndex = 11;
            this.btnGenerate.Text = "도안 생성";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(10, 10);
            this.btnLoadImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(192, 18);
            this.btnLoadImage.TabIndex = 9;
            this.btnLoadImage.Text = "이미지 불러오기";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // numPixelSize
            // 
            this.numPixelSize.Location = new System.Drawing.Point(86, 33);
            this.numPixelSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numPixelSize.Name = "numPixelSize";
            this.numPixelSize.Size = new System.Drawing.Size(116, 21);
            this.numPixelSize.TabIndex = 16;
            // 
            // btnColorAll
            // 
            this.btnColorAll.Location = new System.Drawing.Point(127, 81);
            this.btnColorAll.Name = "btnColorAll";
            this.btnColorAll.Size = new System.Drawing.Size(75, 23);
            this.btnColorAll.TabIndex = 11;
            this.btnColorAll.Text = "전체 색칠";
            this.btnColorAll.UseVisualStyleBackColor = true;
            this.btnColorAll.Click += new System.EventHandler(this.btnColorAll_Click);
            // 
            // Coloring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1125, 647);
            this.Controls.Add(this.numPixelSize);
            this.Controls.Add(this.btnColorAll);
            this.Controls.Add(this.panelPalette);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelCanvas);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnLoadImage);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Coloring";
            this.Text = "Coloring";
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelPalette;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelCanvas;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.NumericUpDown numPixelSize;
        private System.Windows.Forms.Button btnColorAll;
    }
}