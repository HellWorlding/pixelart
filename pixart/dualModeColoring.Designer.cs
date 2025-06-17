namespace pixart
{
    partial class dualModeColoring
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dualModeColoring));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelPeer = new System.Windows.Forms.Panel();
            this.panelCanvas = new System.Windows.Forms.Panel();
            this.numPixelSize = new System.Windows.Forms.NumericUpDown();
            this.btnColorAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.numPixelSizeDual = new System.Windows.Forms.NumericUpDown();
            this.btnColorAllDual = new System.Windows.Forms.Button();
            this.panelPalette = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveDual = new System.Windows.Forms.Button();
            this.btnGenerateDual = new System.Windows.Forms.Button();
            this.btnLoadImageDual = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSizeDual)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.5F));
            this.tableLayoutPanel1.Controls.Add(this.panelPeer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelCanvas, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(234, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1085, 511);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelPeer
            // 
            this.panelPeer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPeer.Location = new System.Drawing.Point(540, 2);
            this.panelPeer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelPeer.Name = "panelPeer";
            this.panelPeer.Size = new System.Drawing.Size(542, 507);
            this.panelPeer.TabIndex = 0;
            this.panelPeer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPeer_Paint);
            // 
            // panelCanvas
            // 
            this.panelCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCanvas.Location = new System.Drawing.Point(3, 2);
            this.panelCanvas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(531, 507);
            this.panelCanvas.TabIndex = 1;
            this.panelCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCanvas_Paint);
            this.panelCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelCanvas_MouseClick);
            // 
            // numPixelSize
            // 
            this.numPixelSize.Location = new System.Drawing.Point(87, -102);
            this.numPixelSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numPixelSize.Name = "numPixelSize";
            this.numPixelSize.Size = new System.Drawing.Size(116, 21);
            this.numPixelSize.TabIndex = 23;
            // 
            // btnColorAll
            // 
            this.btnColorAll.Location = new System.Drawing.Point(128, -54);
            this.btnColorAll.Name = "btnColorAll";
            this.btnColorAll.Size = new System.Drawing.Size(75, 23);
            this.btnColorAll.TabIndex = 18;
            this.btnColorAll.Text = "전체 색칠";
            this.btnColorAll.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, -99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "가로 픽셀수";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(110, -79);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 21);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "결과 저장";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(10, -79);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(94, 21);
            this.btnGenerate.TabIndex = 19;
            this.btnGenerate.Text = "도안 생성";
            this.btnGenerate.UseVisualStyleBackColor = true;
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(10, -125);
            this.btnLoadImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(192, 18);
            this.btnLoadImage.TabIndex = 17;
            this.btnLoadImage.Text = "이미지 불러오기";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            // 
            // numPixelSizeDual
            // 
            this.numPixelSizeDual.Location = new System.Drawing.Point(96, 39);
            this.numPixelSizeDual.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numPixelSizeDual.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numPixelSizeDual.Name = "numPixelSizeDual";
            this.numPixelSizeDual.Size = new System.Drawing.Size(116, 21);
            this.numPixelSizeDual.TabIndex = 30;
            this.numPixelSizeDual.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // btnColorAllDual
            // 
            this.btnColorAllDual.Location = new System.Drawing.Point(137, 87);
            this.btnColorAllDual.Name = "btnColorAllDual";
            this.btnColorAllDual.Size = new System.Drawing.Size(75, 23);
            this.btnColorAllDual.TabIndex = 25;
            this.btnColorAllDual.Text = "전체 색칠";
            this.btnColorAllDual.Click += new System.EventHandler(this.btnColorAll_Click);
            // 
            // panelPalette
            // 
            this.panelPalette.AutoScroll = true;
            this.panelPalette.Location = new System.Drawing.Point(20, 122);
            this.panelPalette.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelPalette.Name = "panelPalette";
            this.panelPalette.Size = new System.Drawing.Size(192, 405);
            this.panelPalette.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "가로 픽셀수";
            // 
            // btnSaveDual
            // 
            this.btnSaveDual.Location = new System.Drawing.Point(120, 62);
            this.btnSaveDual.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveDual.Name = "btnSaveDual";
            this.btnSaveDual.Size = new System.Drawing.Size(94, 21);
            this.btnSaveDual.TabIndex = 27;
            this.btnSaveDual.Text = "결과 저장";
            this.btnSaveDual.UseVisualStyleBackColor = true;
            // 
            // btnGenerateDual
            // 
            this.btnGenerateDual.Location = new System.Drawing.Point(20, 62);
            this.btnGenerateDual.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerateDual.Name = "btnGenerateDual";
            this.btnGenerateDual.Size = new System.Drawing.Size(94, 21);
            this.btnGenerateDual.TabIndex = 26;
            this.btnGenerateDual.Text = "도안 생성";
            this.btnGenerateDual.UseVisualStyleBackColor = true;
            this.btnGenerateDual.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnLoadImageDual
            // 
            this.btnLoadImageDual.Location = new System.Drawing.Point(20, 16);
            this.btnLoadImageDual.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadImageDual.Name = "btnLoadImageDual";
            this.btnLoadImageDual.Size = new System.Drawing.Size(192, 18);
            this.btnLoadImageDual.TabIndex = 24;
            this.btnLoadImageDual.Text = "이미지 불러오기";
            this.btnLoadImageDual.UseVisualStyleBackColor = true;
            this.btnLoadImageDual.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // dualModeColoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1329, 541);
            this.Controls.Add(this.numPixelSizeDual);
            this.Controls.Add(this.btnColorAllDual);
            this.Controls.Add(this.panelPalette);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSaveDual);
            this.Controls.Add(this.btnGenerateDual);
            this.Controls.Add(this.btnLoadImageDual);
            this.Controls.Add(this.numPixelSize);
            this.Controls.Add(this.btnColorAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "dualModeColoring";
            this.Text = "듀얼모드 색칠하기";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSizeDual)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown numPixelSize;
        private System.Windows.Forms.Button btnColorAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.NumericUpDown numPixelSizeDual;
        private System.Windows.Forms.Button btnColorAllDual;
        private System.Windows.Forms.FlowLayoutPanel panelPalette;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveDual;
        private System.Windows.Forms.Button btnGenerateDual;
        private System.Windows.Forms.Button btnLoadImageDual;
        private System.Windows.Forms.Panel panelPeer;
        private System.Windows.Forms.Panel panelCanvas;
    }
}