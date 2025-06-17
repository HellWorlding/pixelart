﻿namespace pixart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dualMode));
            this.btnHostStart = new System.Windows.Forms.Button();
            this.btnClientConnect = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHostStart
            // 
            this.btnHostStart.Location = new System.Drawing.Point(71, 148);
            this.btnHostStart.Name = "btnHostStart";
            this.btnHostStart.Size = new System.Drawing.Size(245, 37);
            this.btnHostStart.TabIndex = 0;
            this.btnHostStart.Text = "호스트로 시작하기";
            this.btnHostStart.UseVisualStyleBackColor = true;
            this.btnHostStart.Click += new System.EventHandler(this.btnHostStart_Click);
            // 
            // btnClientConnect
            // 
            this.btnClientConnect.Location = new System.Drawing.Point(349, 148);
            this.btnClientConnect.Name = "btnClientConnect";
            this.btnClientConnect.Size = new System.Drawing.Size(253, 37);
            this.btnClientConnect.TabIndex = 1;
            this.btnClientConnect.Text = "게스트로 접속하기";
            this.btnClientConnect.UseVisualStyleBackColor = true;
            this.btnClientConnect.Click += new System.EventHandler(this.btnClientConnect_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(574, 312);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(94, 25);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "뒤로 가기";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(73, 89);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(72, 12);
            this.lblIP.TabIndex = 3;
            this.lblIP.Text = "서버 IP 주소";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(71, 113);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(532, 21);
            this.txtIpAddress.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(261, -7);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(141, 97);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(233, -7);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(71, 86);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(360, -7);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(71, 86);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // dualMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 360);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnClientConnect);
            this.Controls.Add(this.btnHostStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dualMode";
            this.Text = "듀얼모드 접속";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHostStart;
        private System.Windows.Forms.Button btnClientConnect;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}