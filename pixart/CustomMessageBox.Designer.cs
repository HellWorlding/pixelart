namespace pixelart
{
    partial class CustomMessageBox
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Courier New", 9F);
            this.lblMessage.Location = new System.Drawing.Point(13, 20);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(400, 60);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "메시지 텍스트";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.LightGray;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnOK.FlatAppearance.BorderSize = 2;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.btnOK.Location = new System.Drawing.Point(171, 90);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(83, 40);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "확인";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.Color.LightGray;
            this.btnYes.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnYes.FlatAppearance.BorderSize = 2;
            this.btnYes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.btnYes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYes.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.btnYes.Location = new System.Drawing.Point(78, 90);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(111, 40);
            this.btnYes.TabIndex = 2;
            this.btnYes.Text = "예";
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Visible = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.LightGray;
            this.btnNo.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnNo.FlatAppearance.BorderSize = 2;
            this.btnNo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.btnNo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.btnNo.Location = new System.Drawing.Point(233, 90);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(111, 40);
            this.btnNo.TabIndex = 3;
            this.btnNo.Text = "아니오";
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Visible = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnCancel.FlatAppearance.BorderSize = 2;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(158, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(111, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CustomMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(427, 160);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FixCellArt 메시지";
            this.ResumeLayout(false);

        }
    }
}
