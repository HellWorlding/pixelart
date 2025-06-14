using System.Windows.Forms;

namespace pixel
{
    partial class SaveKMeansForm
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
            this.picSavePreview = new System.Windows.Forms.PictureBox();
            this.cmbSaveForm = new System.Windows.Forms.ComboBox();
            this.btnImgSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picSavePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // picSavePreview
            // 
            this.picSavePreview.Location = new System.Drawing.Point(12, 11);
            this.picSavePreview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picSavePreview.Name = "picSavePreview";
            this.picSavePreview.Size = new System.Drawing.Size(638, 392);
            this.picSavePreview.TabIndex = 1;
            this.picSavePreview.TabStop = false;
            this.picSavePreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picSavePreview_Paint);
            // 
            // cmbSaveForm
            // 
            this.cmbSaveForm.FormattingEnabled = true;
            this.cmbSaveForm.Items.AddRange(new object[] {
            "단순 비트맵 저장",
            "색상 기반 기호 표현",
            "방향 기반 기호 표현",
            "콜라주 사진 만들기"});
            this.cmbSaveForm.Location = new System.Drawing.Point(667, 30);
            this.cmbSaveForm.Name = "cmbSaveForm";
            this.cmbSaveForm.Size = new System.Drawing.Size(121, 20);
            this.cmbSaveForm.TabIndex = 2;
            this.cmbSaveForm.SelectedIndexChanged += new System.EventHandler(this.cmbSaveForm_SelectedIndexChanged);
            // 
            // btnImgSave
            // 
            this.btnImgSave.Location = new System.Drawing.Point(667, 137);
            this.btnImgSave.Name = "btnImgSave";
            this.btnImgSave.Size = new System.Drawing.Size(121, 52);
            this.btnImgSave.TabIndex = 3;
            this.btnImgSave.Text = "저장하기";
            this.btnImgSave.UseVisualStyleBackColor = true;
            this.btnImgSave.Click += new System.EventHandler(this.btnImgSave_Click);
            // 
            // SaveKMeansForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnImgSave);
            this.Controls.Add(this.cmbSaveForm);
            this.Controls.Add(this.picSavePreview);
            this.Name = "SaveKMeansForm";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.picSavePreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picSavePreview;
        private ComboBox cmbSaveForm;
        private Button btnImgSave;
    }
}