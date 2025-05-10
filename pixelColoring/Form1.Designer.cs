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
            this.components = new System.ComponentModel.Container();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnPixelate = new System.Windows.Forms.Button();
            this.numPixelSize = new System.Windows.Forms.NumericUpDown();
            this.picOriginalThumb = new System.Windows.Forms.PictureBox();
            this.panelLegend = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDownsize = new System.Windows.Forms.Label();
            this.lblKnumber = new System.Windows.Forms.Label();
            this.numKsize = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnColoringKmeans = new System.Windows.Forms.Button();
            this.lblKmeansiter = new System.Windows.Forms.Label();
            this.numKmeansIter = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginalThumb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKsize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKmeansIter)).BeginInit();
            this.SuspendLayout();
            // 
            // picPreview
            // 
            this.picPreview.Location = new System.Drawing.Point(11, 10);
            this.picPreview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(1176, 600);
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            this.picPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picPreview_Paint);
            this.picPreview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPreview_MouseClick);
            this.picPreview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picPreview_MouseMove);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(1258, 11);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(66, 18);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnPixelate
            // 
            this.btnPixelate.Location = new System.Drawing.Point(1302, 176);
            this.btnPixelate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPixelate.Name = "btnPixelate";
            this.btnPixelate.Size = new System.Drawing.Size(66, 18);
            this.btnPixelate.TabIndex = 2;
            this.btnPixelate.Text = "Pixelate";
            this.btnPixelate.UseVisualStyleBackColor = true;
            this.btnPixelate.Click += new System.EventHandler(this.btnPixelate_Click);
            // 
            // numPixelSize
            // 
            this.numPixelSize.Location = new System.Drawing.Point(1235, 53);
            this.numPixelSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numPixelSize.Name = "numPixelSize";
            this.numPixelSize.Size = new System.Drawing.Size(105, 21);
            this.numPixelSize.TabIndex = 3;
            // 
            // picOriginalThumb
            // 
            this.picOriginalThumb.Location = new System.Drawing.Point(1193, 496);
            this.picOriginalThumb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picOriginalThumb.Name = "picOriginalThumb";
            this.picOriginalThumb.Size = new System.Drawing.Size(174, 114);
            this.picOriginalThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picOriginalThumb.TabIndex = 4;
            this.picOriginalThumb.TabStop = false;
            // 
            // panelLegend
            // 
            this.panelLegend.AutoScroll = true;
            this.panelLegend.CausesValidation = false;
            this.panelLegend.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelLegend.Location = new System.Drawing.Point(1193, 263);
            this.panelLegend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(175, 229);
            this.panelLegend.TabIndex = 5;
            // 
            // lblDownsize
            // 
            this.lblDownsize.AutoSize = true;
            this.lblDownsize.Location = new System.Drawing.Point(1233, 39);
            this.lblDownsize.Name = "lblDownsize";
            this.lblDownsize.Size = new System.Drawing.Size(93, 12);
            this.lblDownsize.TabIndex = 7;
            this.lblDownsize.Text = "다운사이즈 크기";
            // 
            // lblKnumber
            // 
            this.lblKnumber.AutoSize = true;
            this.lblKnumber.Location = new System.Drawing.Point(1233, 88);
            this.lblKnumber.Name = "lblKnumber";
            this.lblKnumber.Size = new System.Drawing.Size(61, 12);
            this.lblKnumber.TabIndex = 9;
            this.lblKnumber.Text = "색 분할 수";
            // 
            // numKsize
            // 
            this.numKsize.Location = new System.Drawing.Point(1235, 102);
            this.numKsize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numKsize.Name = "numKsize";
            this.numKsize.Size = new System.Drawing.Size(105, 21);
            this.numKsize.TabIndex = 8;
            // 
            // btnColoringKmeans
            // 
            this.btnColoringKmeans.Location = new System.Drawing.Point(1235, 215);
            this.btnColoringKmeans.Name = "btnColoringKmeans";
            this.btnColoringKmeans.Size = new System.Drawing.Size(133, 18);
            this.btnColoringKmeans.TabIndex = 0;
            this.btnColoringKmeans.Text = "K-Means로 색칠하기";
            this.btnColoringKmeans.UseVisualStyleBackColor = true;
            this.btnColoringKmeans.Click += new System.EventHandler(this.btnColoringKmeans_Click);
            // 
            // lblKmeansiter
            // 
            this.lblKmeansiter.AutoSize = true;
            this.lblKmeansiter.Location = new System.Drawing.Point(1233, 137);
            this.lblKmeansiter.Name = "lblKmeansiter";
            this.lblKmeansiter.Size = new System.Drawing.Size(101, 12);
            this.lblKmeansiter.TabIndex = 11;
            this.lblKmeansiter.Text = "색 정밀 반복 횟수";
            // 
            // numKmeansIter
            // 
            this.numKmeansIter.Location = new System.Drawing.Point(1235, 151);
            this.numKmeansIter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numKmeansIter.Name = "numKmeansIter";
            this.numKmeansIter.Size = new System.Drawing.Size(105, 21);
            this.numKmeansIter.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 614);
            this.Controls.Add(this.lblKmeansiter);
            this.Controls.Add(this.numKmeansIter);
            this.Controls.Add(this.btnColoringKmeans);
            this.Controls.Add(this.lblKnumber);
            this.Controls.Add(this.numKsize);
            this.Controls.Add(this.lblDownsize);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.picOriginalThumb);
            this.Controls.Add(this.numPixelSize);
            this.Controls.Add(this.btnPixelate);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.picPreview);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginalThumb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKsize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKmeansIter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnPixelate;
        private System.Windows.Forms.NumericUpDown numPixelSize;
        private System.Windows.Forms.PictureBox picOriginalThumb;
        private System.Windows.Forms.FlowLayoutPanel panelLegend;
        private System.Windows.Forms.Label lblDownsize;
        private System.Windows.Forms.Label lblKnumber;
        private System.Windows.Forms.NumericUpDown numKsize;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnColoringKmeans;
        private System.Windows.Forms.Label lblKmeansiter;
        private System.Windows.Forms.NumericUpDown numKmeansIter;
    }
}

