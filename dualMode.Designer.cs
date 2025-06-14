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
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnHostStart
            // 
            this.btnHostStart.Location = new System.Drawing.Point(82, 280);
            this.btnHostStart.Name = "btnHostStart";
            this.btnHostStart.Size = new System.Drawing.Size(280, 107);
            this.btnHostStart.TabIndex = 0;
            this.btnHostStart.Text = "호스트로 시작하기";
            this.btnHostStart.UseVisualStyleBackColor = true;
            // 
            // btnClientConnect
            // 
            this.btnClientConnect.Location = new System.Drawing.Point(400, 280);
            this.btnClientConnect.Name = "btnClientConnect";
            this.btnClientConnect.Size = new System.Drawing.Size(290, 107);
            this.btnClientConnect.TabIndex = 1;
            this.btnClientConnect.Text = "게스트로 접속하기";
            this.btnClientConnect.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(680, 406);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(108, 32);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "뒤로 가기";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(83, 111);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(90, 15);
            this.lblIP.TabIndex = 3;
            this.lblIP.Text = "서버 IP 주소";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(82, 142);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(608, 25);
            this.txtIPAddress.TabIndex = 4;
            // 
            // dualMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnClientConnect);
            this.Controls.Add(this.btnHostStart);
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
        private System.Windows.Forms.TextBox txtIPAddress;
    }
}