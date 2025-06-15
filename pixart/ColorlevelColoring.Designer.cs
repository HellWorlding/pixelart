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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Coloring));
            this.panelPalette = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCanvas = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.numPixelSize = new System.Windows.Forms.NumericUpDown();
            this.btnColorAll = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnColorSelect = new System.Windows.Forms.ToolStripButton();
            this.btnSize = new System.Windows.Forms.ToolStripButton();
            this.panelCompare = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnColorPartition = new System.Windows.Forms.Button();
            this.btnSize5 = new System.Windows.Forms.Button();
            this.btnSize3 = new System.Windows.Forms.Button();
            this.btnSize1 = new System.Windows.Forms.Button();
            this.panelOriginalImg = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPalette
            // 
            this.panelPalette.AutoScroll = true;
            this.panelPalette.Location = new System.Drawing.Point(34, 163);
            this.panelPalette.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelPalette.Name = "panelPalette";
            this.panelPalette.Size = new System.Drawing.Size(215, 112);
            this.panelPalette.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "가로 픽셀수";
            // 
            // panelCanvas
            // 
            this.panelCanvas.Location = new System.Drawing.Point(254, 41);
            this.panelCanvas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(894, 616);
            this.panelCanvas.TabIndex = 13;
            this.panelCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCanvas_Paint);
            this.panelCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelCanvas_MouseClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(156, 109);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 21);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "이미지 저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(57, 109);
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
            this.btnLoadImage.Location = new System.Drawing.Point(57, 38);
            this.btnLoadImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(192, 44);
            this.btnLoadImage.TabIndex = 9;
            this.btnLoadImage.Text = "이미지 불러오기";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // numPixelSize
            // 
            this.numPixelSize.Location = new System.Drawing.Point(133, 87);
            this.numPixelSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numPixelSize.Name = "numPixelSize";
            this.numPixelSize.Size = new System.Drawing.Size(116, 21);
            this.numPixelSize.TabIndex = 16;
            // 
            // btnColorAll
            // 
            this.btnColorAll.Location = new System.Drawing.Point(174, 135);
            this.btnColorAll.Name = "btnColorAll";
            this.btnColorAll.Size = new System.Drawing.Size(75, 23);
            this.btnColorAll.TabIndex = 11;
            this.btnColorAll.Text = "전체 색칠";
            this.btnColorAll.UseVisualStyleBackColor = true;
            this.btnColorAll.Click += new System.EventHandler(this.btnColorAll_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnColorSelect,
            this.btnSize});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1184, 33);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnColorSelect
            // 
            this.btnColorSelect.AutoSize = false;
            this.btnColorSelect.BackColor = System.Drawing.Color.Black;
            this.btnColorSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnColorSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColorSelect.Name = "btnColorSelect";
            this.btnColorSelect.Size = new System.Drawing.Size(30, 30);
            this.btnColorSelect.Text = "toolStripButton1";
            this.btnColorSelect.Click += new System.EventHandler(this.btnColorSelect_Click);
            // 
            // btnSize
            // 
            this.btnSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSize.Image = ((System.Drawing.Image)(resources.GetObject("btnSize.Image")));
            this.btnSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSize.Name = "btnSize";
            this.btnSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSize.Size = new System.Drawing.Size(23, 30);
            this.btnSize.Text = "toolStripButton7";
            this.btnSize.Click += new System.EventHandler(this.btnSize_Click);
            // 
            // panelCompare
            // 
            this.panelCompare.Location = new System.Drawing.Point(34, 280);
            this.panelCompare.Name = "panelCompare";
            this.panelCompare.Size = new System.Drawing.Size(215, 130);
            this.panelCompare.TabIndex = 18;
            this.panelCompare.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCompare_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnColorPartition);
            this.panel1.Controls.Add(this.btnSize5);
            this.panel1.Controls.Add(this.btnSize3);
            this.panel1.Controls.Add(this.btnSize1);
            this.panel1.Location = new System.Drawing.Point(34, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(147, 26);
            this.panel1.TabIndex = 19;
            // 
            // btnColorPartition
            // 
            this.btnColorPartition.Location = new System.Drawing.Point(102, 2);
            this.btnColorPartition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnColorPartition.Name = "btnColorPartition";
            this.btnColorPartition.Size = new System.Drawing.Size(42, 21);
            this.btnColorPartition.TabIndex = 3;
            this.btnColorPartition.Text = "부분";
            this.btnColorPartition.UseVisualStyleBackColor = true;
            this.btnColorPartition.Click += new System.EventHandler(this.btnColorPartition_Click);
            // 
            // btnSize5
            // 
            this.btnSize5.Location = new System.Drawing.Point(70, 2);
            this.btnSize5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSize5.Name = "btnSize5";
            this.btnSize5.Size = new System.Drawing.Size(29, 21);
            this.btnSize5.TabIndex = 2;
            this.btnSize5.Text = "5";
            this.btnSize5.UseVisualStyleBackColor = true;
            this.btnSize5.Click += new System.EventHandler(this.btnSize5_Click);
            // 
            // btnSize3
            // 
            this.btnSize3.Location = new System.Drawing.Point(37, 2);
            this.btnSize3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSize3.Name = "btnSize3";
            this.btnSize3.Size = new System.Drawing.Size(29, 21);
            this.btnSize3.TabIndex = 1;
            this.btnSize3.Text = "3";
            this.btnSize3.UseVisualStyleBackColor = true;
            this.btnSize3.Click += new System.EventHandler(this.btnSize3_Click);
            // 
            // btnSize1
            // 
            this.btnSize1.Location = new System.Drawing.Point(3, 2);
            this.btnSize1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSize1.Name = "btnSize1";
            this.btnSize1.Size = new System.Drawing.Size(29, 21);
            this.btnSize1.TabIndex = 0;
            this.btnSize1.Text = "1";
            this.btnSize1.UseVisualStyleBackColor = true;
            this.btnSize1.Click += new System.EventHandler(this.btnSize1_Click);
            // 
            // panelOriginalImg
            // 
            this.panelOriginalImg.Location = new System.Drawing.Point(34, 416);
            this.panelOriginalImg.Name = "panelOriginalImg";
            this.panelOriginalImg.Size = new System.Drawing.Size(215, 130);
            this.panelOriginalImg.TabIndex = 19;
            // 
            // Coloring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.panelOriginalImg);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelCompare);
            this.Controls.Add(this.toolStrip1);
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
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnColorSelect;
        private System.Windows.Forms.Panel panelCompare;
        private System.Windows.Forms.ToolStripButton btnSize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSize5;
        private System.Windows.Forms.Button btnSize3;
        private System.Windows.Forms.Button btnSize1;
        private System.Windows.Forms.Button btnColorPartition;
        private System.Windows.Forms.Panel panelOriginalImg;
    }
}