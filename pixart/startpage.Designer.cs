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
            this.btnColor = new System.Windows.Forms.Button();
            this.btnDualMode = new System.Windows.Forms.Button();
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
            this.btnExplanation.Location = new System.Drawing.Point(71, 355);
            this.btnExplanation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExplanation.Name = "btnExplanation";
            this.btnExplanation.Size = new System.Drawing.Size(193, 106);
            this.btnExplanation.TabIndex = 0;
            this.btnExplanation.Text = "설명하기";
            this.btnExplanation.UseVisualStyleBackColor = true;
            this.btnExplanation.Click += new System.EventHandler(this.btnExplanation_Click);
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(361, 355);
            this.btnColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(193, 106);
            this.btnColor.TabIndex = 1;
            this.btnColor.Text = "색칠하기";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnDualMode
            // 
            this.btnDualMode.Location = new System.Drawing.Point(638, 355);
            this.btnDualMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDualMode.Name = "btnDualMode";
            this.btnDualMode.Size = new System.Drawing.Size(193, 106);
            this.btnDualMode.TabIndex = 2;
            this.btnDualMode.Text = "듀얼모드";
            this.btnDualMode.UseVisualStyleBackColor = true;
            this.btnDualMode.Click += new System.EventHandler(this.btnDualMode_Click);
            // 
            // startpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 562);
            this.Controls.Add(this.btnDualMode);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.btnExplanation);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "startpage";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnExplanation;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button btnDualMode;
    }
}

