namespace pixel
{
    partial class KmeansColoring
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KmeansColoring));
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnPixelate = new System.Windows.Forms.Button();
            this.numPixelSize = new System.Windows.Forms.NumericUpDown();
            this.picOriginalThumb = new System.Windows.Forms.PictureBox();
            this.panelLegend = new System.Windows.Forms.FlowLayoutPanel();
            this.lblFixelCount = new System.Windows.Forms.Label();
            this.lblKnumber = new System.Windows.Forms.Label();
            this.numKsize = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnColoringKmeans = new System.Windows.Forms.Button();
            this.lblKmeansiter = new System.Windows.Forms.Label();
            this.numKmeansIter = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImageLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImgSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.실행ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPickPaletteColor = new System.Windows.Forms.ToolStripMenuItem();
            this.펜굵기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiThick1x1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiThick3x3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiThick5x5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiThickPartition = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.panelCompare = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxMode = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsButtonImageLoad = new System.Windows.Forms.ToolStripButton();
            this.btnColorSelect = new System.Windows.Forms.ToolStripButton();
            this.btnSize = new System.Windows.Forms.ToolStripButton();
            this.tsbtnColorAll = new System.Windows.Forms.ToolStripButton();
            this.tsButtonImgSave = new System.Windows.Forms.ToolStripButton();
            this.tsButtonGridDownload = new System.Windows.Forms.ToolStripButton();
            this.tsButtonGridLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbtnUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRedo = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnColorPartition = new System.Windows.Forms.Button();
            this.btnSize5 = new System.Windows.Forms.Button();
            this.btnSize3 = new System.Windows.Forms.Button();
            this.btnSize1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginalThumb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKsize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKmeansIter)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picPreview
            // 
            this.picPreview.Location = new System.Drawing.Point(277, 26);
            this.picPreview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(1100, 585);
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            this.picPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picPreview_Paint);
            this.picPreview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPreview_MouseClick);
            this.picPreview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picPreview_MouseMove);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(82, 24);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(133, 25);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "이미지　불러오기";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnPixelate
            // 
            this.btnPixelate.Location = new System.Drawing.Point(116, 180);
            this.btnPixelate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPixelate.Name = "btnPixelate";
            this.btnPixelate.Size = new System.Drawing.Size(150, 23);
            this.btnPixelate.TabIndex = 2;
            this.btnPixelate.Text = "픽셀화하기";
            this.btnPixelate.UseVisualStyleBackColor = true;
            this.btnPixelate.Click += new System.EventHandler(this.btnPixelate_Click);
            // 
            // numPixelSize
            // 
            this.numPixelSize.Location = new System.Drawing.Point(138, 87);
            this.numPixelSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numPixelSize.Name = "numPixelSize";
            this.numPixelSize.Size = new System.Drawing.Size(130, 21);
            this.numPixelSize.TabIndex = 3;
            // 
            // picOriginalThumb
            // 
            this.picOriginalThumb.Location = new System.Drawing.Point(32, 493);
            this.picOriginalThumb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picOriginalThumb.Name = "picOriginalThumb";
            this.picOriginalThumb.Size = new System.Drawing.Size(236, 114);
            this.picOriginalThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picOriginalThumb.TabIndex = 4;
            this.picOriginalThumb.TabStop = false;
            // 
            // panelLegend
            // 
            this.panelLegend.AutoScroll = true;
            this.panelLegend.CausesValidation = false;
            this.panelLegend.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelLegend.Location = new System.Drawing.Point(32, 267);
            this.panelLegend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(236, 109);
            this.panelLegend.TabIndex = 5;
            // 
            // lblFixelCount
            // 
            this.lblFixelCount.AutoSize = true;
            this.lblFixelCount.Location = new System.Drawing.Point(44, 87);
            this.lblFixelCount.Name = "lblFixelCount";
            this.lblFixelCount.Size = new System.Drawing.Size(73, 12);
            this.lblFixelCount.TabIndex = 7;
            this.lblFixelCount.Text = "가로 픽셀 수";
            // 
            // lblKnumber
            // 
            this.lblKnumber.AutoSize = true;
            this.lblKnumber.Location = new System.Drawing.Point(44, 124);
            this.lblKnumber.Name = "lblKnumber";
            this.lblKnumber.Size = new System.Drawing.Size(61, 12);
            this.lblKnumber.TabIndex = 9;
            this.lblKnumber.Text = "색 분할 수";
            // 
            // numKsize
            // 
            this.numKsize.Location = new System.Drawing.Point(138, 122);
            this.numKsize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numKsize.Name = "numKsize";
            this.numKsize.Size = new System.Drawing.Size(130, 21);
            this.numKsize.TabIndex = 8;
            // 
            // btnColoringKmeans
            // 
            this.btnColoringKmeans.Location = new System.Drawing.Point(116, 207);
            this.btnColoringKmeans.Name = "btnColoringKmeans";
            this.btnColoringKmeans.Size = new System.Drawing.Size(150, 25);
            this.btnColoringKmeans.TabIndex = 0;
            this.btnColoringKmeans.Text = "K-Means로 색칠하기";
            this.btnColoringKmeans.UseVisualStyleBackColor = true;
            this.btnColoringKmeans.Click += new System.EventHandler(this.btnColoringKmeans_Click);
            // 
            // lblKmeansiter
            // 
            this.lblKmeansiter.AutoSize = true;
            this.lblKmeansiter.Location = new System.Drawing.Point(43, 161);
            this.lblKmeansiter.Name = "lblKmeansiter";
            this.lblKmeansiter.Size = new System.Drawing.Size(85, 12);
            this.lblKmeansiter.TabIndex = 11;
            this.lblKmeansiter.Text = "정밀 반복 횟수";
            // 
            // numKmeansIter
            // 
            this.numKmeansIter.Location = new System.Drawing.Point(138, 157);
            this.numKmeansIter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numKmeansIter.Name = "numKmeansIter";
            this.numKmeansIter.Size = new System.Drawing.Size(130, 21);
            this.numKmeansIter.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(116, 238);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 24);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "이미지 저장하기";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem,
            this.실행ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1347, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiImageLoad,
            this.tsmiImgSave,
            this.tsmiLoadGrid,
            this.tsmiSaveGrid});
            this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            this.파일ToolStripMenuItem.Size = new System.Drawing.Size(43, 22);
            this.파일ToolStripMenuItem.Text = "파일";
            // 
            // tsmiImageLoad
            // 
            this.tsmiImageLoad.Name = "tsmiImageLoad";
            this.tsmiImageLoad.Size = new System.Drawing.Size(162, 22);
            this.tsmiImageLoad.Text = "이미지 불러오기";
            this.tsmiImageLoad.Click += new System.EventHandler(this.tsmiImageLoad_Click);
            // 
            // tsmiImgSave
            // 
            this.tsmiImgSave.Name = "tsmiImgSave";
            this.tsmiImgSave.Size = new System.Drawing.Size(162, 22);
            this.tsmiImgSave.Text = "이미지 저장";
            this.tsmiImgSave.Click += new System.EventHandler(this.tsmiImgSave_Click);
            // 
            // tsmiLoadGrid
            // 
            this.tsmiLoadGrid.Name = "tsmiLoadGrid";
            this.tsmiLoadGrid.Size = new System.Drawing.Size(162, 22);
            this.tsmiLoadGrid.Text = "도안 불러오기";
            this.tsmiLoadGrid.Click += new System.EventHandler(this.tsmiLoadGrid_Click);
            // 
            // tsmiSaveGrid
            // 
            this.tsmiSaveGrid.Name = "tsmiSaveGrid";
            this.tsmiSaveGrid.Size = new System.Drawing.Size(162, 22);
            this.tsmiSaveGrid.Text = "도안 저장";
            this.tsmiSaveGrid.Click += new System.EventHandler(this.tsmiSaveGrid_Click);
            // 
            // 실행ToolStripMenuItem
            // 
            this.실행ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGenerate,
            this.tsmiPickPaletteColor,
            this.펜굵기ToolStripMenuItem,
            this.tsmiColorAll,
            this.tsmiUndo,
            this.tsmiRedo});
            this.실행ToolStripMenuItem.Name = "실행ToolStripMenuItem";
            this.실행ToolStripMenuItem.Size = new System.Drawing.Size(43, 22);
            this.실행ToolStripMenuItem.Text = "실행";
            // 
            // tsmiGenerate
            // 
            this.tsmiGenerate.Name = "tsmiGenerate";
            this.tsmiGenerate.Size = new System.Drawing.Size(178, 22);
            this.tsmiGenerate.Text = "도안 생성하기";
            this.tsmiGenerate.Click += new System.EventHandler(this.tsmiGenerate_Click);
            // 
            // tsmiPickPaletteColor
            // 
            this.tsmiPickPaletteColor.Name = "tsmiPickPaletteColor";
            this.tsmiPickPaletteColor.Size = new System.Drawing.Size(178, 22);
            this.tsmiPickPaletteColor.Text = "색 선택하기";
            this.tsmiPickPaletteColor.Click += new System.EventHandler(this.tsmiPickPaletteColor_Click);
            // 
            // 펜굵기ToolStripMenuItem
            // 
            this.펜굵기ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiThick1x1,
            this.tsmiThick3x3,
            this.tsmiThick5x5,
            this.tsmiThickPartition});
            this.펜굵기ToolStripMenuItem.Name = "펜굵기ToolStripMenuItem";
            this.펜굵기ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.펜굵기ToolStripMenuItem.Text = "펜 굵기";
            // 
            // tsmiThick1x1
            // 
            this.tsmiThick1x1.Name = "tsmiThick1x1";
            this.tsmiThick1x1.Size = new System.Drawing.Size(150, 22);
            this.tsmiThick1x1.Text = "1X1";
            this.tsmiThick1x1.Click += new System.EventHandler(this.tsmiThick1x1_Click);
            // 
            // tsmiThick3x3
            // 
            this.tsmiThick3x3.Name = "tsmiThick3x3";
            this.tsmiThick3x3.Size = new System.Drawing.Size(150, 22);
            this.tsmiThick3x3.Text = "3X3";
            this.tsmiThick3x3.Click += new System.EventHandler(this.tsmiThick3x3_Click);
            // 
            // tsmiThick5x5
            // 
            this.tsmiThick5x5.Name = "tsmiThick5x5";
            this.tsmiThick5x5.Size = new System.Drawing.Size(150, 22);
            this.tsmiThick5x5.Text = "5X5";
            this.tsmiThick5x5.Click += new System.EventHandler(this.tsmiThick5x5_Click);
            // 
            // tsmiThickPartition
            // 
            this.tsmiThickPartition.Name = "tsmiThickPartition";
            this.tsmiThickPartition.Size = new System.Drawing.Size(150, 22);
            this.tsmiThickPartition.Text = "부분 색칠하기";
            this.tsmiThickPartition.Click += new System.EventHandler(this.tsmiThickPartition_Click);
            // 
            // tsmiColorAll
            // 
            this.tsmiColorAll.Name = "tsmiColorAll";
            this.tsmiColorAll.Size = new System.Drawing.Size(178, 22);
            this.tsmiColorAll.Text = "도안 전체 색칠하기";
            this.tsmiColorAll.Click += new System.EventHandler(this.tsmiColorAll_Click);
            // 
            // tsmiUndo
            // 
            this.tsmiUndo.Name = "tsmiUndo";
            this.tsmiUndo.Size = new System.Drawing.Size(178, 22);
            this.tsmiUndo.Text = "되돌리기";
            this.tsmiUndo.Click += new System.EventHandler(this.tsmiUndo_Click);
            // 
            // tsmiRedo
            // 
            this.tsmiRedo.Name = "tsmiRedo";
            this.tsmiRedo.Size = new System.Drawing.Size(178, 22);
            this.tsmiRedo.Text = "다시하기";
            this.tsmiRedo.Click += new System.EventHandler(this.tsmiRedo_Click);
            // 
            // panelCompare
            // 
            this.panelCompare.Location = new System.Drawing.Point(32, 380);
            this.panelCompare.Name = "panelCompare";
            this.panelCompare.Size = new System.Drawing.Size(235, 111);
            this.panelCompare.TabIndex = 14;
            this.panelCompare.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCompare_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "도안 생성 방식";
            // 
            // cbxMode
            // 
            this.cbxMode.FormattingEnabled = true;
            this.cbxMode.Items.AddRange(new object[] {
            "RGB",
            "HSV",
            "OKLab",
            "YCbCr"});
            this.cbxMode.Location = new System.Drawing.Point(136, 54);
            this.cbxMode.Name = "cbxMode";
            this.cbxMode.Size = new System.Drawing.Size(131, 20);
            this.cbxMode.TabIndex = 17;
            this.cbxMode.SelectedIndexChanged += new System.EventHandler(this.cbxMode_SelectedIndexChanged);
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
            this.tsButtonImgSave,
            this.tsButtonGridDownload,
            this.tsButtonGridLoad,
            this.tsbtnUndo,
            this.tsbtnRedo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(32, 590);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsButtonImageLoad
            // 
            this.tsButtonImageLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonImageLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsButtonImageLoad.Image")));
            this.tsButtonImageLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonImageLoad.Name = "tsButtonImageLoad";
            this.tsButtonImageLoad.Size = new System.Drawing.Size(27, 28);
            this.tsButtonImageLoad.Text = "이미지 불러오기";
            this.tsButtonImageLoad.Click += new System.EventHandler(this.tsButtonImageLoad_Click);
            // 
            // btnColorSelect
            // 
            this.btnColorSelect.AutoSize = false;
            this.btnColorSelect.BackColor = System.Drawing.Color.Black;
            this.btnColorSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnColorSelect.ForeColor = System.Drawing.Color.Black;
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
            this.btnSize.Size = new System.Drawing.Size(27, 28);
            this.btnSize.Text = "펜 두께 설정하기";
            this.btnSize.Click += new System.EventHandler(this.btnSize_Click);
            // 
            // tsbtnColorAll
            // 
            this.tsbtnColorAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnColorAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnColorAll.Image")));
            this.tsbtnColorAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnColorAll.Name = "tsbtnColorAll";
            this.tsbtnColorAll.Size = new System.Drawing.Size(27, 28);
            this.tsbtnColorAll.Text = "전체 색칠하기";
            this.tsbtnColorAll.Click += new System.EventHandler(this.tsbtnColorAll_Click);
            // 
            // tsButtonImgSave
            // 
            this.tsButtonImgSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonImgSave.Image = ((System.Drawing.Image)(resources.GetObject("tsButtonImgSave.Image")));
            this.tsButtonImgSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonImgSave.Name = "tsButtonImgSave";
            this.tsButtonImgSave.Size = new System.Drawing.Size(27, 28);
            this.tsButtonImgSave.Text = "이미지 저장하기";
            this.tsButtonImgSave.Click += new System.EventHandler(this.tsButtonImgSave_Click);
            // 
            // tsButtonGridDownload
            // 
            this.tsButtonGridDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonGridDownload.Image = ((System.Drawing.Image)(resources.GetObject("tsButtonGridDownload.Image")));
            this.tsButtonGridDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonGridDownload.Name = "tsButtonGridDownload";
            this.tsButtonGridDownload.Size = new System.Drawing.Size(27, 28);
            this.tsButtonGridDownload.Text = "도안 저장하기";
            this.tsButtonGridDownload.Click += new System.EventHandler(this.tsButtonGridDownload_Click);
            // 
            // tsButtonGridLoad
            // 
            this.tsButtonGridLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonGridLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsButtonGridLoad.Image")));
            this.tsButtonGridLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonGridLoad.Name = "tsButtonGridLoad";
            this.tsButtonGridLoad.Size = new System.Drawing.Size(27, 28);
            this.tsButtonGridLoad.Text = "toolStripButton2";
            this.tsButtonGridLoad.Click += new System.EventHandler(this.tsButtonGridLoad_Click);
            // 
            // tsbtnUndo
            // 
            this.tsbtnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUndo.Image")));
            this.tsbtnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUndo.Name = "tsbtnUndo";
            this.tsbtnUndo.Size = new System.Drawing.Size(27, 28);
            this.tsbtnUndo.Text = "되돌리기";
            this.tsbtnUndo.Click += new System.EventHandler(this.tsbtnUndo_Click);
            // 
            // tsbtnRedo
            // 
            this.tsbtnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRedo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRedo.Image")));
            this.tsbtnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRedo.Name = "tsbtnRedo";
            this.tsbtnRedo.Size = new System.Drawing.Size(27, 28);
            this.tsbtnRedo.Text = "다시하기";
            this.tsbtnRedo.Click += new System.EventHandler(this.tsbtnRedo_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnColorPartition);
            this.panel1.Controls.Add(this.btnSize5);
            this.panel1.Controls.Add(this.btnSize3);
            this.panel1.Controls.Add(this.btnSize1);
            this.panel1.Location = new System.Drawing.Point(32, 100);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(147, 26);
            this.panel1.TabIndex = 20;
            // 
            // btnColorPartition
            // 
            this.btnColorPartition.Location = new System.Drawing.Point(102, 2);
            this.btnColorPartition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnColorPartition.Name = "btnColorPartition";
            this.btnColorPartition.Size = new System.Drawing.Size(42, 21);
            this.btnColorPartition.TabIndex = 3;
            this.btnColorPartition.Text = "부분";
            this.btnColorPartition.UseVisualStyleBackColor = true;
            this.btnColorPartition.Click += new System.EventHandler(this.btnColorPartition_Click);
            // 
            // btnSize5
            // 
            this.btnSize5.Location = new System.Drawing.Point(70, 2);
            this.btnSize5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSize5.Name = "btnSize5";
            this.btnSize5.Size = new System.Drawing.Size(29, 21);
            this.btnSize5.TabIndex = 2;
            this.btnSize5.Text = "5";
            this.btnSize5.UseVisualStyleBackColor = true;
            this.btnSize5.Click += new System.EventHandler(this.btnSize5_Click);
            // 
            // btnSize3
            // 
            this.btnSize3.Location = new System.Drawing.Point(37, 2);
            this.btnSize3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSize3.Name = "btnSize3";
            this.btnSize3.Size = new System.Drawing.Size(29, 21);
            this.btnSize3.TabIndex = 1;
            this.btnSize3.Text = "3";
            this.btnSize3.UseVisualStyleBackColor = true;
            this.btnSize3.Click += new System.EventHandler(this.btnSize3_Click);
            // 
            // btnSize1
            // 
            this.btnSize1.Location = new System.Drawing.Point(3, 2);
            this.btnSize1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSize1.Name = "btnSize1";
            this.btnSize1.Size = new System.Drawing.Size(29, 21);
            this.btnSize1.TabIndex = 0;
            this.btnSize1.Text = "1";
            this.btnSize1.UseVisualStyleBackColor = true;
            this.btnSize1.Click += new System.EventHandler(this.btnSize1_Click);
            // 
            // KmeansColoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 614);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cbxMode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelCompare);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblKmeansiter);
            this.Controls.Add(this.numKmeansIter);
            this.Controls.Add(this.btnColoringKmeans);
            this.Controls.Add(this.lblKnumber);
            this.Controls.Add(this.numKsize);
            this.Controls.Add(this.lblFixelCount);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.picOriginalThumb);
            this.Controls.Add(this.numPixelSize);
            this.Controls.Add(this.btnPixelate);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "KmeansColoring";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KmeansColoring_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KmeansColoring_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPixelSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginalThumb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKsize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKmeansIter)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnPixelate;
        private System.Windows.Forms.NumericUpDown numPixelSize;
        private System.Windows.Forms.PictureBox picOriginalThumb;
        private System.Windows.Forms.FlowLayoutPanel panelLegend;
        private System.Windows.Forms.Label lblFixelCount;
        private System.Windows.Forms.Label lblKnumber;
        private System.Windows.Forms.NumericUpDown numKsize;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnColoringKmeans;
        private System.Windows.Forms.Label lblKmeansiter;
        private System.Windows.Forms.NumericUpDown numKmeansIter;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panelCompare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxMode;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnColorSelect;
        private System.Windows.Forms.ToolStripButton btnSize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnColorPartition;
        private System.Windows.Forms.Button btnSize5;
        private System.Windows.Forms.Button btnSize3;
        private System.Windows.Forms.Button btnSize1;
        private System.Windows.Forms.ToolStripMenuItem 파일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 실행ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiImageLoad;
        private System.Windows.Forms.ToolStripMenuItem tsmiImgSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadGrid;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveGrid;
        private System.Windows.Forms.ToolStripMenuItem tsmiGenerate;
        private System.Windows.Forms.ToolStripMenuItem tsmiPickPaletteColor;
        private System.Windows.Forms.ToolStripMenuItem 펜굵기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiThick1x1;
        private System.Windows.Forms.ToolStripMenuItem tsmiThick3x3;
        private System.Windows.Forms.ToolStripMenuItem tsmiThick5x5;
        private System.Windows.Forms.ToolStripMenuItem tsmiThickPartition;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorAll;
        private System.Windows.Forms.ToolStripButton tsButtonImageLoad;
        private System.Windows.Forms.ToolStripButton tsbtnColorAll;
        private System.Windows.Forms.ToolStripButton tsButtonGridDownload;
        private System.Windows.Forms.ToolStripButton tsButtonImgSave;
        private System.Windows.Forms.ToolStripButton tsButtonGridLoad;
        private System.Windows.Forms.ToolStripButton tsbtnUndo;
        private System.Windows.Forms.ToolStripButton tsbtnRedo;
        private System.Windows.Forms.ToolStripMenuItem tsmiUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmiRedo;
    }
}

