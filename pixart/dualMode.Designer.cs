namespace pixart
{
    partial class dualMode
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
            this.btnHostStart = new System.Windows.Forms.Button();
            this.btnClientConnect = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnHostStart
            // 
            this.btnHostStart.Location = new System.Drawing.Point(102, 336);
            this.btnHostStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHostStart.Name = "btnHostStart";
            this.btnHostStart.Size = new System.Drawing.Size(350, 128);
            this.btnHostStart.TabIndex = 0;
            this.btnHostStart.Text = "호스트로 시작하기";
            this.btnHostStart.UseVisualStyleBackColor = true;
            this.btnHostStart.Click += new System.EventHandler(this.btnHostStart_Click);
            // 
            // btnClientConnect
            // 
            this.btnClientConnect.Location = new System.Drawing.Point(500, 336);
            this.btnClientConnect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClientConnect.Name = "btnClientConnect";
            this.btnClientConnect.Size = new System.Drawing.Size(362, 128);
            this.btnClientConnect.TabIndex = 1;
            this.btnClientConnect.Text = "게스트로 접속하기";
            this.btnClientConnect.UseVisualStyleBackColor = true;
            this.btnClientConnect.Click += new System.EventHandler(this.btnClientConnect_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(850, 487);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(135, 38);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "뒤로 가기";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(104, 133);
            this.lblIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(106, 18);
            this.lblIP.TabIndex = 3;
            this.lblIP.Text = "서버 IP 주소";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(102, 170);
            this.txtIpAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(759, 28);
            this.txtIpAddress.TabIndex = 4;
            // 
            // dualMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 540);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnClientConnect);
            this.Controls.Add(this.btnHostStart);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "dualMode";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHostStart;
        private System.Windows.Forms.Button btnClientConnect;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtIpAddress;
    }
}