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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Coloring));
            this.panelPalette = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCanvas = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.numPixelSize = new System.Windows.Forms.NumericUpDown();
            this.btnColorAll = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsButtonImageLoad = new System.Windows.Forms.ToolStripButton();
            this.btnColorSelect = new System.Windows.Forms.ToolStripButton();
            this.btnSize = new System.Windows.Forms.ToolStripButton();
            this.tsbtnColorAll = new System.Windows.Forms.ToolStripButton();
            this.tsImgSave = new System.Windows.Forms.ToolStripButton();
            this.tsButtonGridDownload = new System.Windows.Forms.ToolStripButton();
            this.tsButtonGridLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbtnUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRedo = new System.Windows.Forms.ToolStripButton();
            this.panelCompare = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnColorPartition = new System.Windows.Forms.Button();
            this.btnSize5 = new System.Windows.Forms.Button();
            this.btnSize3 = new System.Windows.Forms.Button();
            this.btnSize1 = new System.Windows.Forms.Button();
            this.panelOriginalImg = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDifficulty = new System.Windows.Forms.ComboBox();
            this.cbxColorType = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImageLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImageSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.저장ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPickPaletteColor = new System.Windows.Forms.ToolStripMenuItem();
            this.펜굵기선택하기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiThick1x1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiThick3x3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiThick5x5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiThickPartition = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPalette
            // 
            this.panelPalette.AutoScroll = true;
            this.panelPalette.Location = new System.Drawing.Point(81, 350);
            this.panelPalette.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelPalette.Name = "panelPalette";
            this.panelPalette.Size = new System.Drawing.Size(334, 189);
            this.panelPalette.TabIndex = 15;
            this.panelPalette.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPalette_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 132);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "가로 픽셀수";
            // 
            // panelCanvas
            // 
            this.panelCanvas.Location = new System.Drawing.Point(424, 57);
            this.panelCanvas.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(1449, 1083);
            this.panelCanvas.TabIndex = 13;
            this.panelCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCanvas_Paint);
            this.panelCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelCanvas_MouseClick);
            this.panelCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelCanvas_MouseMove);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(277, 268);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 32);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "이미지 저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(139, 268);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(130, 74);
            this.btnGenerate.TabIndex = 11;
            this.btnGenerate.Text = "도안 생성";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(139, 57);
            this.btnLoadImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(274, 66);
            this.btnLoadImage.TabIndex = 9;
            this.btnLoadImage.Text = "이미지 불러오기";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // numPixelSize
            // 
            this.numPixelSize.Location = new System.Drawing.Point(240, 129);
            this.numPixelSize.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numPixelSize.Name = "numPixelSize";
            this.numPixelSize.Size = new System.Drawing.Size(166, 28);
            this.numPixelSize.TabIndex = 16;
            // 
            // btnColorAll
            // 
            this.btnColorAll.Location = new System.Drawing.Point(277, 308);
            this.btnColorAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnColorAll.Name = "btnColorAll";
            this.btnColorAll.Size = new System.Drawing.Size(136, 34);
            this.btnColorAll.TabIndex = 11;
            this.btnColorAll.Text = "전체 색칠";
            this.btnColorAll.UseVisualStyleBackColor = true;
            this.btnColorAll.Click += new System.EventHandler(this.btnColorAll_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsButtonImageLoad,
            this.btnColorSelect,
            this.btnSize,
            this.tsbtnColorAll,
            this.tsImgSave,
            this.tsButtonGridDownload,
            this.tsButtonGridLoad,
            this.tsbtnUndo,
            this.tsbtnRedo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 35);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(37, 1121);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsButtonImageLoad
            // 
            this.tsButtonImageLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonImageLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsButtonImageLoad.Image")));
            this.tsButtonImageLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonImageLoad.Name = "tsButtonImageLoad";
            this.tsButtonImageLoad.Size = new System.Drawing.Size(30, 28);
            this.tsButtonImageLoad.Text = "이미지 불러오기";
            this.tsButtonImageLoad.Click += new System.EventHandler(this.tsButtonImageLoad_Click);
            // 
            // btnColorSelect
            // 
            this.btnColorSelect.AutoSize = false;
            this.btnColorSelect.BackColor = System.Drawing.Color.White;
            this.btnColorSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnColorSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColorSelect.Name = "btnColorSelect";
            this.btnColorSelect.Size = new System.Drawing.Size(30, 30);
            this.btnColorSelect.Text = "펜 색 설정하기";
            this.btnColorSelect.Click += new System.EventHandler(this.btnColorSelect_Click);
            // 
            // btnSize
            // 
            this.btnSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSize.Image = ((System.Drawing.Image)(resources.GetObject("btnSize.Image")));
            this.btnSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSize.Name = "btnSize";
            this.btnSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSize.Size = new System.Drawing.Size(30, 28);
            this.btnSize.Text = "펜 두께 설정하기";
            this.btnSize.Click += new System.EventHandler(this.btnSize_Click);
            // 
            // tsbtnColorAll
            // 
            this.tsbtnColorAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnColorAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnColorAll.Image")));
            this.tsbtnColorAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnColorAll.Name = "tsbtnColorAll";
            this.tsbtnColorAll.Size = new System.Drawing.Size(30, 28);
            this.tsbtnColorAll.Text = "전체 색칠하기";
            this.tsbtnColorAll.Click += new System.EventHandler(this.tsbtnColorAll_Click);
            // 
            // tsImgSave
            // 
            this.tsImgSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsImgSave.Image = ((System.Drawing.Image)(resources.GetObject("tsImgSave.Image")));
            this.tsImgSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsImgSave.Name = "tsImgSave";
            this.tsImgSave.Size = new System.Drawing.Size(30, 28);
            this.tsImgSave.Text = "이미지 저장하기";
            this.tsImgSave.Click += new System.EventHandler(this.tsImgSave_Click);
            // 
            // tsButtonGridDownload
            // 
            this.tsButtonGridDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonGridDownload.Image = ((System.Drawing.Image)(resources.GetObject("tsButtonGridDownload.Image")));
            this.tsButtonGridDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonGridDownload.Name = "tsButtonGridDownload";
            this.tsButtonGridDownload.Size = new System.Drawing.Size(30, 28);
            this.tsButtonGridDownload.Text = "도안 저장하기";
            this.tsButtonGridDownload.Click += new System.EventHandler(this.tsButtonGridDownload_Click);
            // 
            // tsButtonGridLoad
            // 
            this.tsButtonGridLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonGridLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsButtonGridLoad.Image")));
            this.tsButtonGridLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonGridLoad.Name = "tsButtonGridLoad";
            this.tsButtonGridLoad.Size = new System.Drawing.Size(30, 28);
            this.tsButtonGridLoad.Text = "toolStripButton2";
            this.tsButtonGridLoad.Click += new System.EventHandler(this.tsButtonGridLoad_Click);
            // 
            // tsbtnUndo
            // 
            this.tsbtnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUndo.Image")));
            this.tsbtnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUndo.Name = "tsbtnUndo";
            this.tsbtnUndo.Size = new System.Drawing.Size(30, 28);
            this.tsbtnUndo.Text = "되돌리기";
            this.tsbtnUndo.Click += new System.EventHandler(this.tsbtnUndo_Click);
            // 
            // tsbtnRedo
            // 
            this.tsbtnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRedo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRedo.Image")));
            this.tsbtnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRedo.Name = "tsbtnRedo";
            this.tsbtnRedo.Size = new System.Drawing.Size(30, 28);
            this.tsbtnRedo.Text = "다시하기";
            this.tsbtnRedo.Click += new System.EventHandler(this.tsbtnRedo_Click);
            // 
            // panelCompare
            // 
            this.panelCompare.Location = new System.Drawing.Point(81, 546);
            this.panelCompare.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelCompare.Name = "panelCompare";
            this.panelCompare.Size = new System.Drawing.Size(334, 216);
            this.panelCompare.TabIndex = 18;
            this.panelCompare.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCompare_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnColorPartition);
            this.panel1.Controls.Add(this.btnSize5);
            this.panel1.Controls.Add(this.btnSize3);
            this.panel1.Controls.Add(this.btnSize1);
            this.panel1.Location = new System.Drawing.Point(47, 132);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 39);
            this.panel1.TabIndex = 19;
            // 
            // btnColorPartition
            // 
            this.btnColorPartition.Location = new System.Drawing.Point(146, 3);
            this.btnColorPartition.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnColorPartition.Name = "btnColorPartition";
            this.btnColorPartition.Size = new System.Drawing.Size(60, 32);
            this.btnColorPartition.TabIndex = 3;
            this.btnColorPartition.Text = "부분";
            this.btnColorPartition.UseVisualStyleBackColor = true;
            this.btnColorPartition.Click += new System.EventHandler(this.btnColorPartition_Click);
            // 
            // btnSize5
            // 
            this.btnSize5.Location = new System.Drawing.Point(100, 3);
            this.btnSize5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSize5.Name = "btnSize5";
            this.btnSize5.Size = new System.Drawing.Size(41, 32);
            this.btnSize5.TabIndex = 2;
            this.btnSize5.Text = "5";
            this.btnSize5.UseVisualStyleBackColor = true;
            this.btnSize5.Click += new System.EventHandler(this.btnSize5_Click);
            // 
            // btnSize3
            // 
            this.btnSize3.Location = new System.Drawing.Point(53, 3);
            this.btnSize3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSize3.Name = "btnSize3";
            this.btnSize3.Size = new System.Drawing.Size(41, 32);
            this.btnSize3.TabIndex = 1;
            this.btnSize3.Text = "3";
            this.btnSize3.UseVisualStyleBackColor = true;
            this.btnSize3.Click += new System.EventHandler(this.btnSize3_Click);
            // 
            // btnSize1
            // 
            this.btnSize1.Location = new System.Drawing.Point(4, 3);
            this.btnSize1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSize1.Name = "btnSize1";
            this.btnSize1.Size = new System.Drawing.Size(41, 32);
            this.btnSize1.TabIndex = 0;
            this.btnSize1.Text = "1";
            this.btnSize1.UseVisualStyleBackColor = true;
            this.btnSize1.Click += new System.EventHandler(this.btnSize1_Click);
            // 
            // panelOriginalImg
            // 
            this.panelOriginalImg.Location = new System.Drawing.Point(79, 771);
            this.panelOriginalImg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelOriginalImg.Name = "panelOriginalImg";
            this.panelOriginalImg.Size = new System.Drawing.Size(334, 216);
            this.panelOriginalImg.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 176);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "컬러 종류";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 222);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 22;
            this.label3.Text = "난이도";
            // 
            // cbxDifficulty
            // 
            this.cbxDifficulty.FormattingEnabled = true;
            this.cbxDifficulty.Items.AddRange(new object[] {
            "쉬움",
            "중간",
            "어려움",
            "매우 어려움"});
            this.cbxDifficulty.Location = new System.Drawing.Point(240, 218);
            this.cbxDifficulty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxDifficulty.Name = "cbxDifficulty";
            this.cbxDifficulty.Size = new System.Drawing.Size(164, 26);
            this.cbxDifficulty.TabIndex = 24;
            this.cbxDifficulty.SelectedIndexChanged += new System.EventHandler(this.cbxDifficulty_SelectedIndexChanged);
            // 
            // cbxColorType
            // 
            this.cbxColorType.FormattingEnabled = true;
            this.cbxColorType.Items.AddRange(new object[] {
            "RGB",
            "HSV",
            "OKLAB",
            "YCbCr"});
            this.cbxColorType.Location = new System.Drawing.Point(240, 173);
            this.cbxColorType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxColorType.Name = "cbxColorType";
            this.cbxColorType.Size = new System.Drawing.Size(164, 26);
            this.cbxColorType.TabIndex = 25;
            this.cbxColorType.SelectedIndexChanged += new System.EventHandler(this.cbxColorType_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem,
            this.저장ToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1873, 35);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiImageLoad,
            this.tsmiImageSave,
            this.tsmiLoadGrid,
            this.tsmiSaveGrid});
            this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            this.파일ToolStripMenuItem.Size = new System.Drawing.Size(64, 29);
            this.파일ToolStripMenuItem.Text = "파일";
            // 
            // tsmiImageLoad
            // 
            this.tsmiImageLoad.Name = "tsmiImageLoad";
            this.tsmiImageLoad.Size = new System.Drawing.Size(246, 34);
            this.tsmiImageLoad.Text = "이미지 불러오기";
            this.tsmiImageLoad.Click += new System.EventHandler(this.tsmiImageLoad_Click);
            // 
            // tsmiImageSave
            // 
            this.tsmiImageSave.Name = "tsmiImageSave";
            this.tsmiImageSave.Size = new System.Drawing.Size(246, 34);
            this.tsmiImageSave.Text = "이미지 저장";
            this.tsmiImageSave.Click += new System.EventHandler(this.tsmiImageSave_Click);
            // 
            // tsmiLoadGrid
            // 
            this.tsmiLoadGrid.Name = "tsmiLoadGrid";
            this.tsmiLoadGrid.Size = new System.Drawing.Size(246, 34);
            this.tsmiLoadGrid.Text = "도안 불러오기";
            this.tsmiLoadGrid.Click += new System.EventHandler(this.tsmiLoadGrid_Click);
            // 
            // tsmiSaveGrid
            // 
            this.tsmiSaveGrid.Name = "tsmiSaveGrid";
            this.tsmiSaveGrid.Size = new System.Drawing.Size(246, 34);
            this.tsmiSaveGrid.Text = "도안 저장";
            this.tsmiSaveGrid.Click += new System.EventHandler(this.tsmiSaveGrid_Click);
            // 
            // 저장ToolStripMenuItem1
            // 
            this.저장ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGenerate,
            this.tsmiPickPaletteColor,
            this.펜굵기선택하기ToolStripMenuItem,
            this.tsmiColorAll,
            this.tsmiUndo,
            this.tsmiRedo});
            this.저장ToolStripMenuItem1.Name = "저장ToolStripMenuItem1";
            this.저장ToolStripMenuItem1.Size = new System.Drawing.Size(64, 29);
            this.저장ToolStripMenuItem1.Text = "실행";
            // 
            // tsmiGenerate
            // 
            this.tsmiGenerate.Name = "tsmiGenerate";
            this.tsmiGenerate.Size = new System.Drawing.Size(270, 34);
            this.tsmiGenerate.Text = "도안 생성하기";
            this.tsmiGenerate.Click += new System.EventHandler(this.tsmiGenerate_Click);
            // 
            // tsmiPickPaletteColor
            // 
            this.tsmiPickPaletteColor.Name = "tsmiPickPaletteColor";
            this.tsmiPickPaletteColor.Size = new System.Drawing.Size(270, 34);
            this.tsmiPickPaletteColor.Text = "색 선택하기";
            this.tsmiPickPaletteColor.Click += new System.EventHandler(this.tsmiPickPaletteColor_Click);
            // 
            // 펜굵기선택하기ToolStripMenuItem
            // 
            this.펜굵기선택하기ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiThick1x1,
            this.tsmiThick3x3,
            this.tsmiThick5x5,
            this.tsmiThickPartition});
            this.펜굵기선택하기ToolStripMenuItem.Name = "펜굵기선택하기ToolStripMenuItem";
            this.펜굵기선택하기ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.펜굵기선택하기ToolStripMenuItem.Text = "펜 굵기";
            this.펜굵기선택하기ToolStripMenuItem.Click += new System.EventHandler(this.펜굵기선택하기ToolStripMenuItem_Click);
            // 
            // tsmiThick1x1
            // 
            this.tsmiThick1x1.Name = "tsmiThick1x1";
            this.tsmiThick1x1.Size = new System.Drawing.Size(228, 34);
            this.tsmiThick1x1.Text = "1X1";
            this.tsmiThick1x1.Click += new System.EventHandler(this.tsmiThick1x1_Click);
            // 
            // tsmiThick3x3
            // 
            this.tsmiThick3x3.Name = "tsmiThick3x3";
            this.tsmiThick3x3.Size = new System.Drawing.Size(228, 34);
            this.tsmiThick3x3.Text = "3X3";
            this.tsmiThick3x3.Click += new System.EventHandler(this.tsmiThick3x3_Click);
            // 
            // tsmiThick5x5
            // 
            this.tsmiThick5x5.Name = "tsmiThick5x5";
            this.tsmiThick5x5.Size = new System.Drawing.Size(228, 34);
            this.tsmiThick5x5.Text = "5X5";
            this.tsmiThick5x5.Click += new System.EventHandler(this.tsmiThick5x5_Click);
            // 
            // tsmiThickPartition
            // 
            this.tsmiThickPartition.Name = "tsmiThickPartition";
            this.tsmiThickPartition.Size = new System.Drawing.Size(228, 34);
            this.tsmiThickPartition.Text = "부분 색칠하기";
            this.tsmiThickPartition.Click += new System.EventHandler(this.tsmiThickPartition_Click);
            // 
            // tsmiColorAll
            // 
            this.tsmiColorAll.Name = "tsmiColorAll";
            this.tsmiColorAll.Size = new System.Drawing.Size(270, 34);
            this.tsmiColorAll.Text = "도안 전체 색칠하기";
            this.tsmiColorAll.Click += new System.EventHandler(this.tsmiColorAll_Click);
            // 
            // tsmiUndo
            // 
            this.tsmiUndo.Name = "tsmiUndo";
            this.tsmiUndo.Size = new System.Drawing.Size(270, 34);
            this.tsmiUndo.Text = "되돌리기";
            this.tsmiUndo.Click += new System.EventHandler(this.tsmiUndo_Click);
            // 
            // tsmiRedo
            // 
            this.tsmiRedo.Name = "tsmiRedo";
            this.tsmiRedo.Size = new System.Drawing.Size(270, 34);
            this.tsmiRedo.Text = "다시하기";
            this.tsmiRedo.Click += new System.EventHandler(this.tsmiRedo_Click);
            // 
            // Coloring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1873, 1156);
            this.Controls.Add(this.cbxColorType);
            this.Controls.Add(this.cbxDifficulty);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelOriginalImg);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelCompare);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.numPixelSize);
            this.Controls.Add(this.btnColorAll);
            this.Controls.Add(this.panelPalette);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelCanvas);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnLoadImage);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Coloring";
            this.Text = "Coloring";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Coloring_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Coloring_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelPalette;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelCanvas;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.NumericUpDown numPixelSize;
        private System.Windows.Forms.Button btnColorAll;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnColorSelect;
        private System.Windows.Forms.Panel panelCompare;
        private System.Windows.Forms.ToolStripButton btnSize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSize5;
        private System.Windows.Forms.Button btnSize3;
        private System.Windows.Forms.Button btnSize1;
        private System.Windows.Forms.Button btnColorPartition;
        private System.Windows.Forms.Panel panelOriginalImg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxDifficulty;
        private System.Windows.Forms.ComboBox cbxColorType;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 파일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiImageLoad;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadGrid;
        private System.Windows.Forms.ToolStripMenuItem tsmiImageSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveGrid;
        private System.Windows.Forms.ToolStripMenuItem 저장ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiPickPaletteColor;
        private System.Windows.Forms.ToolStripMenuItem 펜굵기선택하기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiGenerate;
        private System.Windows.Forms.ToolStripMenuItem tsmiThick1x1;
        private System.Windows.Forms.ToolStripMenuItem tsmiThick3x3;
        private System.Windows.Forms.ToolStripMenuItem tsmiThick5x5;
        private System.Windows.Forms.ToolStripMenuItem tsmiThickPartition;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton tsbtnColorAll;
        private System.Windows.Forms.ToolStripButton tsButtonImageLoad;
        private System.Windows.Forms.ToolStripButton tsImgSave;
        private System.Windows.Forms.ToolStripButton tsButtonGridDownload;
        private System.Windows.Forms.ToolStripButton tsButtonGridLoad;
        private System.Windows.Forms.ToolStripMenuItem tsmiUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmiRedo;
        private System.Windows.Forms.ToolStripButton tsbtnUndo;
        private System.Windows.Forms.ToolStripButton tsbtnRedo;
    }
}