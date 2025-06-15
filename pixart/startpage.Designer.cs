namespace pixart
{
    partial class startpage
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
            this.btnExplanation.Location = new System.Drawing.Point(12, 226);
            this.btnExplanation.Name = "btnExplanation";
            this.btnExplanation.Size = new System.Drawing.Size(169, 85);
            this.btnExplanation.TabIndex = 0;
            this.btnExplanation.Text = "설명하기";
            this.btnExplanation.UseVisualStyleBackColor = true;
            this.btnExplanation.Click += new System.EventHandler(this.btnExplanation_Click);
            // 
            // btnKmeansColor
            // 
            this.btnKmeansColor.Location = new System.Drawing.Point(218, 226);
            this.btnKmeansColor.Name = "btnKmeansColor";
            this.btnKmeansColor.Size = new System.Drawing.Size(169, 85);
            this.btnKmeansColor.TabIndex = 1;
            this.btnKmeansColor.Text = "KMeans로 색칠하기";
            this.btnKmeansColor.UseVisualStyleBackColor = true;
            this.btnKmeansColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnDualMode
            // 
            this.btnDualMode.Location = new System.Drawing.Point(595, 226);
            this.btnDualMode.Name = "btnDualMode";
            this.btnDualMode.Size = new System.Drawing.Size(169, 85);
            this.btnDualMode.TabIndex = 2;
            this.btnDualMode.Text = "듀얼모드";
            this.btnDualMode.UseVisualStyleBackColor = true;
            this.btnDualMode.Click += new System.EventHandler(this.btnDualMode_Click);
            // 
            // btnColorLevel
            // 
            this.btnColorLevel.Location = new System.Drawing.Point(393, 226);
            this.btnColorLevel.Name = "btnColorLevel";
            this.btnColorLevel.Size = new System.Drawing.Size(169, 85);
            this.btnColorLevel.TabIndex = 3;
            this.btnColorLevel.Text = "색상 단계로 색칠하기";
            this.btnColorLevel.UseVisualStyleBackColor = true;
            this.btnColorLevel.Click += new System.EventHandler(this.btnColorLevel_Click);
            // 
            // startpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnColorLevel);
            this.Controls.Add(this.btnDualMode);
            this.Controls.Add(this.btnKmeansColor);
            this.Controls.Add(this.btnExplanation);
            this.Name = "startpage";
            this.Text = "Form1";
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

