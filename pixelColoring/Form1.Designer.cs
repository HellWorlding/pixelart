namespace pixel
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnPixelate = new System.Windows.Forms.Button();
            this.numPixelSize = new System.Windows.Forms.NumericUpDown();
            this.picOriginalThumb = new System.Windows.Forms.PictureBox();
            this.panelLegend = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginalThumb)).BeginInit();
            this.SuspendLayout();
            // 
            // picPreview
            // 
            this.picPreview.Location = new System.Drawing.Point(13, 13);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(1344, 750);
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            this.picPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picPreview_Paint);
            this.picPreview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPreview_MouseClick);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(1438, 39);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnPixelate
            // 
            this.btnPixelate.Location = new System.Drawing.Point(1438, 105);
            this.btnPixelate.Name = "btnPixelate";
            this.btnPixelate.Size = new System.Drawing.Size(75, 23);
            this.btnPixelate.TabIndex = 2;
            this.btnPixelate.Text = "Pixelate";
            this.btnPixelate.UseVisualStyleBackColor = true;
            this.btnPixelate.Click += new System.EventHandler(this.btnPixelate_Click);
            // 
            // numPixelSize
            // 
            this.numPixelSize.Location = new System.Drawing.Point(1413, 162);
            this.numPixelSize.Name = "numPixelSize";
            this.numPixelSize.Size = new System.Drawing.Size(120, 25);
            this.numPixelSize.TabIndex = 3;
            // 
            // picOriginalThumb
            // 
            this.picOriginalThumb.Location = new System.Drawing.Point(1363, 620);
            this.picOriginalThumb.Name = "picOriginalThumb";
            this.picOriginalThumb.Size = new System.Drawing.Size(199, 143);
            this.picOriginalThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picOriginalThumb.TabIndex = 4;
            this.picOriginalThumb.TabStop = false;
            // 
            // panelLegend
            // 
            this.panelLegend.AutoScroll = true;
            this.panelLegend.CausesValidation = false;
            this.panelLegend.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelLegend.Location = new System.Drawing.Point(1363, 212);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(200, 402);
            this.panelLegend.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1574, 768);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.picOriginalThumb);
            this.Controls.Add(this.numPixelSize);
            this.Controls.Add(this.btnPixelate);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.picPreview);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginalThumb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnPixelate;
        private System.Windows.Forms.NumericUpDown numPixelSize;
        private System.Windows.Forms.PictureBox picOriginalThumb;
        private System.Windows.Forms.FlowLayoutPanel panelLegend;
    }
}

