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
            this.panelPalette = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCanvas = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.cmbDifficulty = new System.Windows.Forms.ComboBox();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelPalette
            // 
            this.panelPalette.AutoScroll = true;
            this.panelPalette.Location = new System.Drawing.Point(12, 101);
            this.panelPalette.Name = "panelPalette";
            this.panelPalette.Size = new System.Drawing.Size(219, 337);
            this.panelPalette.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "난이도";
            // 
            // panelCanvas
            // 
            this.panelCanvas.Location = new System.Drawing.Point(238, 15);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(550, 423);
            this.panelCanvas.TabIndex = 13;
            this.panelCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCanvas_Paint);
            this.panelCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelCanvas_MouseClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(125, 69);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 26);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "결과 저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(12, 69);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(107, 26);
            this.btnGenerate.TabIndex = 11;
            this.btnGenerate.Text = "도안 생성";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // cmbDifficulty
            // 
            this.cmbDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDifficulty.FormattingEnabled = true;
            this.cmbDifficulty.ItemHeight = 15;
            this.cmbDifficulty.Items.AddRange(new object[] {
            "쉬움",
            "보통",
            "어려움"});
            this.cmbDifficulty.Location = new System.Drawing.Point(66, 40);
            this.cmbDifficulty.Name = "cmbDifficulty";
            this.cmbDifficulty.Size = new System.Drawing.Size(166, 23);
            this.cmbDifficulty.TabIndex = 10;
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(12, 12);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(220, 22);
            this.btnLoadImage.TabIndex = 9;
            this.btnLoadImage.Text = "이미지 불러오기";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // Coloring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelPalette);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelCanvas);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.cmbDifficulty);
            this.Controls.Add(this.btnLoadImage);
            this.Name = "Coloring";
            this.Text = "Coloring";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelPalette;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelCanvas;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ComboBox cmbDifficulty;
        private System.Windows.Forms.Button btnLoadImage;
    }
}