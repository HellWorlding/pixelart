using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pixart;
using pixel;

namespace PixelColorling
{
    public partial class Coloring : Form
    {
        public enum BinningMode { RGB, HSV, OKLAB, YCbCr } // 도안 생성 색상 종류
        public enum DifficultyLevel { Easy, Medium, Hard, VeryHard } // 도안 생성 색상 난이도

        private BinningMode currentBinningMode = BinningMode.RGB; //현재 도안 생성 색상 종류
        private DifficultyLevel currentDifficulty = DifficultyLevel.Medium; // 현재 도안 생성 난이도


        private Bitmap originalImage; // 원본 이미지 저장용
        private int blockSize = 10;          // 난이도별 블럭 크기
        private int colorCount = 12;         // 난이도별 색상 수
        private Color[,] blockColors;        // 각 블럭의 색상
        private int[,] colorNumbers;         // 색상에 대한 번호 매핑
        private Dictionary<Color, int> colorMap = new Dictionary<Color, int>();// 색상 → 번호

        private Color selectedColor = Color.Transparent;
        private bool[,] isFilled;           // 셀이 색칠되었는지 여부
        private Color[,] filledColors;      // 셀에 실제 색칠한 색상



        private Color selectedCustomColor = Color.Black;
        private Bitmap quantizedBitmap;  // 양자화된 원본 비트맵 저장
        private Color[,] simplifiedColors; // 각 셀의 양자화된 색상
        private Dictionary<int, Color> numberToColorMap = new Dictionary<int, Color>(); // 번호 → RGB 색상
        private Point? selectedPoint = null; // 사용자가 클릭한 셀 좌표 (없을 수도 있음)

        Dictionary<int, Color> colorNumberToRGB = new Dictionary<int, Color>();
        private ColorDialog colorDialog1 = new ColorDialog();
        private int penThickness = 1; // 기본값, 초기 펜 굵기
        private bool colorPartitionMode = false; //부분 색칠


        private Bitmap paintedResultBitmap; // 도안 결과 저장용

        private bool isPatternGenerated = false;  // 도안 생성 여부 체크용




        public Coloring()
        {
            InitializeComponent();
            this.Load += Coloring_Load;
        }

        private void Coloring_Load(object sender, EventArgs e)
        {
            // 픽셀 수 기본값 설정
            numPixelSize.Value = 30;
            numPixelSize.Minimum = 5;
            numPixelSize.Maximum = 100;

            // 콤보박스 기본값 설정
            cbxColorType.SelectedIndex = 0; // RGB
            cbxDifficulty.SelectedIndex = 1; // Medium



            panel1.Visible = !panel1.Visible;

        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {

            if (originalImage == null)
            {
                MessageBox.Show("먼저 이미지를 불러와주세요.");
                return;
            }

            // ✅ 이미 도안이 있다면 사용자 확인
            if (isPatternGenerated)
            {
                DialogResult overwrite = MessageBox.Show(
                    "기존 도안을 새로 생성하시겠습니까?", "도안 생성 확인",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (overwrite == DialogResult.No)
                    return;

                DialogResult save = MessageBox.Show(
                    "기존 도안을 저장하시겠습니까?", "도안 저장",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (save == DialogResult.Yes)
                {
                    tsmiSaveGrid_Click(null, null);
                }
            }
            isPatternGenerated = false;

            int desiredGridW = (int)numPixelSize.Value;
            blockSize = originalImage.Width / desiredGridW;
            int wBlocks = originalImage.Width / blockSize;
            int hBlocks = originalImage.Height / blockSize;
            colorCount = 12;

            // 배열 초기화
            blockColors = new Color[hBlocks, wBlocks];
            simplifiedColors = new Color[hBlocks, wBlocks];
            colorNumbers = new int[hBlocks, wBlocks];
            isFilled = new bool[hBlocks, wBlocks];
            filledColors = new Color[hBlocks, wBlocks];
            colorMap.Clear();
            colorNumberToRGB.Clear();

            // 픽셀 캐시
            Color[,] pixelCache = new Color[originalImage.Height, originalImage.Width];
            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    pixelCache[y, x] = originalImage.GetPixel(x, y);
                }
            }

            // 색상 → 번호 매핑 (양자화된 RGB를 int 키로 변환)
            int currentNumber = 1;
            Dictionary<int, int> colorKeyToNumber = new Dictionary<int, int>();

            // RGB를 정수 키로 변환하는 함수
            int SimplifiedColorKey(Color c) => (c.R << 16) | (c.G << 8) | c.B;

            for (int by = 0; by < hBlocks; by++)
            {
                for (int bx = 0; bx < wBlocks; bx++)
                {
                    int rSum = 0, gSum = 0, bSum = 0, count = 0;

                    for (int dy = 0; dy < blockSize; dy++)
                    {
                        int y = by * blockSize + dy;
                        if (y >= originalImage.Height) continue;

                        for (int dx = 0; dx < blockSize; dx++)
                        {
                            int x = bx * blockSize + dx;
                            if (x >= originalImage.Width) continue;

                            Color c = pixelCache[y, x];
                            rSum += c.R;
                            gSum += c.G;
                            bSum += c.B;
                            count++;
                        }
                    }

                    if (count == 0) continue;

                    Color avg = Color.FromArgb(rSum / count, gSum / count, bSum / count);
                    blockColors[by, bx] = avg;
                    //int step = (currentBinningMode == BinningMode.YCbCr) ? 1 : GetStepCountPerMode(currentBinningMode, currentDifficulty);

                    int step = GetStepCountPerMode(currentBinningMode, currentDifficulty);

                    Color simplified = SimplifyColor(avg, step);

                    simplifiedColors[by, bx] = simplified;

                    int key = SimplifiedColorKey(simplified);

                    if (!colorKeyToNumber.ContainsKey(key))
                    {
                        colorKeyToNumber[key] = currentNumber;
                        colorMap[simplified] = currentNumber;
                        colorNumberToRGB[currentNumber] = simplified;
                        currentNumber++;
                    }

                    colorNumbers[by, bx] = colorKeyToNumber[key];
                }
            }

            int smallW = desiredGridW;
            int smallH = originalImage.Height / blockSize;

            paintedResultBitmap = new Bitmap(smallW, smallH);
            using (Graphics g = Graphics.FromImage(paintedResultBitmap))
            {
                g.Clear(Color.White);
            }

            panelCanvas.Invalidate();
            CreateColorPalette();
        }





        //평균 색상 계산
        private Color GetAverageColor(int startX, int startY, int size)
        {
            int r = 0, g = 0, b = 0, count = 0;

            for (int y = startY; y < startY + size && y < originalImage.Height; y++)
            {
                for (int x = startX; x < startX + size && x < originalImage.Width; x++)
                {
                    Color c = originalImage.GetPixel(x, y);
                    r += c.R; g += c.G; b += c.B;
                    count++;
                }
            }

            return Color.FromArgb(r / count, g / count, b / count);
        }

        //팔레트 초기화 메서드
        private void CreateColorPalette()
        {
            panelPalette.Controls.Clear(); // 기존 팔레트 제거

            // 색상 → 번호 매핑 순서대로 정렬
            var sortedColors = colorMap.OrderBy(kv => kv.Value);

            foreach (var kv in sortedColors)
            {
                Color color = kv.Key;
                int number = kv.Value;

                Button colorButton = new Button
                {
                    BackColor = color,
                    Width = 40,
                    Height = 40,
                    Margin = new Padding(5),
                    Text = number.ToString(),
                    ForeColor = Color.Black,
                    Tag = color
                };

                colorButton.Click += ColorButton_Click;

                panelPalette.Controls.Add(colorButton);
            }
        }


        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp",
                Title = "이미지 파일 선택"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (originalImage != null)
                        originalImage.Dispose();

                    Bitmap loaded = new Bitmap(ofd.FileName);
                    originalImage = new Bitmap(loaded); // 원본 저장
                    loaded.Dispose();

                    // 패널 크기에 맞게 리사이즈된 이미지 생성
                    Bitmap resizedCanvas = ResizeToFit(originalImage, panelCanvas.Width, panelCanvas.Height);
                    Bitmap resizedOriginal = ResizeToFit(originalImage, panelOriginalImg.Width, panelOriginalImg.Height);

                    // 배경 이미지로 설정 (만약 panelOriginalImg가 PictureBox면 .Image 사용)
                    panelCanvas.BackgroundImage = resizedCanvas;
                    panelCanvas.BackgroundImageLayout = ImageLayout.Center;

                    panelOriginalImg.BackgroundImage = resizedOriginal;
                    panelOriginalImg.BackgroundImageLayout = ImageLayout.Center;

                    panelCanvas.Invalidate();
                    panelOriginalImg.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지 로드 실패: " + ex.Message);
                }
            }
        }
        private Bitmap ResizeToFit(Bitmap src, int maxWidth, int maxHeight)
        {
            double scaleX = (double)maxWidth / src.Width;
            double scaleY = (double)maxHeight / src.Height;
            double scale = Math.Min(scaleX, scaleY);  // 비율 유지

            int newW = (int)(src.Width * scale);
            int newH = (int)(src.Height * scale);

            Bitmap resized = new Bitmap(newW, newH);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(src, 0, 0, newW, newH);
            }

            return resized;
        }

        //색상 단순화
        private Color SimplifyColor(Color color, int level)
        {
            switch (currentBinningMode)
            {
                case BinningMode.RGB:
                    return SimplifyRGB(color, level);
                case BinningMode.HSV:
                    return SimplifyHSV(color, level);
                case BinningMode.OKLAB:
                    return SimplifyOklab(color, level);
                case BinningMode.YCbCr:
                    return SimplifyYCbCr(color, level);
                default:
                    return color;
            }
        }




        private Color SimplifyHSV(Color color, int level)
        {
            // RGB → HSV
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));
            double delta = max - min;

            double h = 0;
            if (delta > 0)
            {
                if (max == r)
                    h = 60 * (((g - b) / delta) % 6);
                else if (max == g)
                    h = 60 * (((b - r) / delta) + 2);
                else
                    h = 60 * (((r - g) / delta) + 4);
            }
            if (h < 0) h += 360;

            double s = (max == 0) ? 0 : delta / max;
            double v = max;

            // HSV 양자화
            int step = level;
            h = Math.Round(h / (360.0 / step)) * (360.0 / step);
            s = Math.Round(s * step) / step;
            v = Math.Round(v * step) / step;

            // HSV → RGB
            double c = v * s;
            double x = c * (1 - Math.Abs((h / 60) % 2 - 1));
            double m = v - c;

            double r1 = 0, g1 = 0, b1 = 0;

            if (h >= 0 && h < 60) { r1 = c; g1 = x; b1 = 0; }
            else if (h >= 60 && h < 120) { r1 = x; g1 = c; b1 = 0; }
            else if (h >= 120 && h < 180) { r1 = 0; g1 = c; b1 = x; }
            else if (h >= 180 && h < 240) { r1 = 0; g1 = x; b1 = c; }
            else if (h >= 240 && h < 300) { r1 = x; g1 = 0; b1 = c; }
            else { r1 = c; g1 = 0; b1 = x; }

            int R = Clamp((int)Math.Round((r1 + m) * 255));
            int G = Clamp((int)Math.Round((g1 + m) * 255));
            int B = Clamp((int)Math.Round((b1 + m) * 255));

            return Color.FromArgb(R, G, B);
        }



        public static int Clamp(int val)
        {
            return Math.Min(255, Math.Max(0, val));
        }



        private Color SimplifyRGB(Color color, int levels)
        {
            int step = 256 / levels;
            int r = (color.R / step) * step;
            int g = (color.G / step) * step;
            int b = (color.B / step) * step;
            return Color.FromArgb(r, g, b);
        }


        private void panelCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (blockColors == null) return;
            isPatternGenerated = true; // 도안이 생성되었음을 표시
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            int hBlocks = blockColors.GetLength(0);
            int wBlocks = blockColors.GetLength(1);

            float scaleX = (float)panelCanvas.Width / wBlocks;
            float scaleY = (float)panelCanvas.Height / hBlocks;
            float cellSize = Math.Min(scaleX, scaleY);
            int totalW = (int)(cellSize * wBlocks);
            int totalH = (int)(cellSize * hBlocks);
            int offsetX = (panelCanvas.Width - totalW) / 2;
            int offsetY = (panelCanvas.Height - totalH) / 2;

            Font font = new Font("Arial", Math.Max(8, (int)(cellSize * 0.5f)));

            for (int y = 0; y < hBlocks; y++)
            {
                for (int x = 0; x < wBlocks; x++)
                {
                    float left = offsetX + x * cellSize;
                    float top = offsetY + y * cellSize;
                    RectangleF rect = new RectangleF(left, top, cellSize, cellSize);

                    if (isFilled != null && isFilled[y, x])
                    {
                        using (SolidBrush brush = new SolidBrush(filledColors[y, x]))
                            g.FillRectangle(brush, rect);
                    }

                    g.DrawRectangle(Pens.Gray, left, top, cellSize, cellSize);

                    if (!isFilled[y, x])
                    {
                        int colorNum = colorNumbers[y, x];
                        string num = colorNum.ToString();

                        if (colorNumberToRGB.TryGetValue(colorNum, out Color color))
                        {
                            using (Brush brush = new SolidBrush(color))
                                g.DrawString(num, font, brush, rect.Location);
                        }
                        else
                        {
                            g.DrawString(num, font, Brushes.Black, rect.Location);
                        }
                    }
                }
            }

            font.Dispose();
        }








        private void btnSave_Click(object sender, EventArgs e)
        {
            if (colorNumbers == null || blockColors == null || filledColors == null || isFilled == null)
            {
                MessageBox.Show("먼저 도안을 생성하고 색칠해주세요.");
                return;
            }

            if (originalImage == null)
            {
                MessageBox.Show("이미지 또는 색칠된 결과가 없습니다.");
                return;
            }
            if (paintedResultBitmap == null)
            {
                MessageBox.Show("이미지 또는 색칠x된 결과가 없습니다.");
                return;
            }

            // 축소된 비트맵은 paintedResultBitmap, 원본 크기는 originalImage.Size
            var saveForm = new SaveForm(paintedResultBitmap, originalImage.Size);
            saveForm.Show();

        }


        private void ColorButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is Color)
            {
                selectedColor = (Color)btn.Tag;


                MessageBox.Show($"색상 {btn.Text}번 선택됨");
                btnColorSelect.BackColor = selectedColor;
                btnColorSelect.ForeColor = selectedColor;
            }
        }



        private void panelCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (blockColors == null) return;

                int hBlocks = blockColors.GetLength(0);
                int wBlocks = blockColors.GetLength(1);

                float scaleX = (float)panelCanvas.Width / wBlocks;
                float scaleY = (float)panelCanvas.Height / hBlocks;
                float cellSize = Math.Min(scaleX, scaleY);
                int totalW = (int)(cellSize * wBlocks);
                int totalH = (int)(cellSize * hBlocks);
                int offsetX = (panelCanvas.Width - totalW) / 2;
                int offsetY = (panelCanvas.Height - totalH) / 2;

                int cx = (int)((e.X - offsetX) / cellSize);
                int cy = (int)((e.Y - offsetY) / cellSize);
                if (cx < 0 || cx >= wBlocks || cy < 0 || cy >= hBlocks) return;

                selectedPoint = new Point(cx, cy);

                if (colorPartitionMode)
                {
                    int targetNumber = colorNumbers[cy, cx];

                    for (int y = 0; y < hBlocks; y++)
                    {
                        for (int x = 0; x < wBlocks; x++)
                        {
                            if (colorNumbers[y, x] == targetNumber)
                            {
                                isFilled[y, x] = true;
                                filledColors[y, x] = selectedColor;

                                if (paintedResultBitmap != null &&
                                    x >= 0 && x < paintedResultBitmap.Width &&
                                    y >= 0 && y < paintedResultBitmap.Height)
                                {
                                    paintedResultBitmap.SetPixel(x, y, selectedColor);
                                }

                                RectangleF rect = new RectangleF(offsetX + x * cellSize, offsetY + y * cellSize, cellSize, cellSize);
                                panelCanvas.Invalidate(Rectangle.Ceiling(rect));
                            }
                        }
                    }

                    panelCompare.Invalidate();

                    // ❌ Partition 모드 유지 (자동 해제 제거)
                    return;
                }

                // 일반 펜 모드
                int radius = penThickness / 2;
                for (int dy = -radius; dy <= radius; dy++)
                {
                    for (int dx = -radius; dx <= radius; dx++)
                    {
                        int x = cx + dx;
                        int y = cy + dy;

                        if (x >= 0 && x < wBlocks && y >= 0 && y < hBlocks)
                        {
                            isFilled[y, x] = true;
                            filledColors[y, x] = selectedColor;

                            if (paintedResultBitmap != null &&
                                x >= 0 && x < paintedResultBitmap.Width &&
                                y >= 0 && y < paintedResultBitmap.Height)
                            {
                                paintedResultBitmap.SetPixel(x, y, selectedColor);
                            }

                            RectangleF rect = new RectangleF(offsetX + x * cellSize, offsetY + y * cellSize, cellSize, cellSize);
                            panelCanvas.Invalidate(Rectangle.Ceiling(rect));
                        }
                    }
                }

                panelCompare.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러: " + ex.Message);
            }
        }










        private void btnColorAll_Click(object sender, EventArgs e)
        {
            if (colorNumbers == null || colorMap == null || blockSize == 0)
                return;

            int height = colorNumbers.GetLength(0);
            int width = colorNumbers.GetLength(1);

            paintedResultBitmap = new Bitmap(width, height);

            foreach (var kv in colorMap)
            {
                Color col = kv.Key;
                int idx = kv.Value;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (colorNumbers[y, x] == idx)
                        {
                            isFilled[y, x] = true;
                            filledColors[y, x] = col;
                            paintedResultBitmap.SetPixel(x, y, col);
                        }
                    }
                }
            }

            RepaintCanvas();           // panelCanvas.Invalidate()
            panelCompare.Invalidate(); // 비교 패널 갱신
        }





        private void btnColorSelect_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                selectedColor = colorDialog1.Color;
            }
            //바뀐 색으로 버튼 색 변경
            btnColorSelect.ForeColor = btnColorSelect.BackColor = colorDialog1.Color;
        }

        private void panelCompare_Paint(object sender, PaintEventArgs e)
        {
            if (blockColors == null || colorNumbers == null || !selectedPoint.HasValue)
                return;

            Graphics g = e.Graphics;
            g.Clear(Color.White);

            int hBlocks = blockColors.GetLength(0);
            int wBlocks = blockColors.GetLength(1);
            int radius = penThickness / 2;

            // 1️⃣ 셀 크기 계산 (패널 안에 딱 맞게)
            int availableW = panelCompare.Width - 40;
            int availableH = panelCompare.Height - 40;
            int cellSize = Math.Min(availableW / (penThickness * 2 + 1), availableH / penThickness);

            int totalWidth = (penThickness * 2 + 1) * cellSize;
            int totalHeight = penThickness * cellSize;
            int offsetX = (panelCompare.Width - totalWidth) / 2;
            int offsetY = (panelCompare.Height - totalHeight) / 2;

            Font font = new Font("Arial", 8);
            Point pt = selectedPoint.Value;
            Brush labelBrush = Brushes.Black;

            for (int dy = -radius; dy <= radius; dy++)
            {
                for (int dx = -radius; dx <= radius; dx++)
                {
                    int x = pt.X + dx;
                    int y = pt.Y + dy;
                    int lx = dx + radius;
                    int ly = dy + radius;

                    Rectangle rectOriginal = new Rectangle(
                        offsetX + lx * cellSize,
                        offsetY + ly * cellSize,
                        cellSize, cellSize);

                    Rectangle rectFilled = new Rectangle(
                        offsetX + (penThickness + 1 + lx) * cellSize,
                        offsetY + ly * cellSize,
                        cellSize, cellSize);

                    if (x >= 0 && x < wBlocks && y >= 0 && y < hBlocks)
                    {
                        int colorNum = colorNumbers[y, x];
                        Color originalColor = colorNumberToRGB.ContainsKey(colorNum) ? colorNumberToRGB[colorNum] : Color.Gray;
                        Color? filledColor = isFilled[y, x] ? (Color?)filledColors[y, x] : null;

                        using (SolidBrush brush = new SolidBrush(originalColor))
                            g.FillRectangle(brush, rectOriginal);
                        g.DrawRectangle(Pens.Black, rectOriginal);

                        if (filledColor.HasValue)
                        {
                            using (SolidBrush brush = new SolidBrush(filledColor.Value))
                                g.FillRectangle(brush, rectFilled);
                        }
                        else
                        {
                            using (HatchBrush hatch = new HatchBrush(HatchStyle.LargeGrid, Color.LightGray, Color.White))
                                g.FillRectangle(hatch, rectFilled);
                        }
                        g.DrawRectangle(Pens.Black, rectFilled);
                    }
                }
            }

            // 라벨
            g.DrawString("Original", font, labelBrush,
                offsetX + (penThickness * cellSize - 40) / 2,
                offsetY + penThickness * cellSize + 5);
            g.DrawString("Colored", font, labelBrush,
                offsetX + ((penThickness + 1) * cellSize) + (penThickness * cellSize - 40) / 2,
                offsetY + penThickness * cellSize + 5);

            font.Dispose();
        }







        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        private void btnSize_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }

        private void btnSize1_Click(object sender, EventArgs e)
        {
            colorPartitionMode = false;
            penThickness = 1; // 기본 굵기
            panel1.Visible = !panel1.Visible;
        }

        private void btnSize3_Click(object sender, EventArgs e)
        {
            colorPartitionMode = false;
            penThickness = 3;
            panel1.Visible = !panel1.Visible;
        }

        private void btnSize5_Click(object sender, EventArgs e)
        {
            colorPartitionMode = false;
            penThickness = 5;
            panel1.Visible = !panel1.Visible;
        }

        private void btnColorPartition_Click(object sender, EventArgs e)
        {
            colorPartitionMode = true;
            penThickness = 1; // 부분 색칠 모드에서는 기본 굵기 사용
            MessageBox.Show("번호 기준 색칠 모드: 색칠할 셀을 클릭하세요.");
            panel1.Visible = !panel1.Visible;
        }


        private int GetStepCountPerMode(BinningMode mode, DifficultyLevel level)
        {
            if (mode == BinningMode.RGB)
            {
                if (level == DifficultyLevel.Easy) return 6;
                else if (level == DifficultyLevel.Medium) return 12;
                else if (level == DifficultyLevel.Hard) return 24;
                else if (level == DifficultyLevel.VeryHard) return 64; // 거의 원본
                else return 12;
            }
            else if (mode == BinningMode.HSV)
            {
                if (level == DifficultyLevel.Easy) return 4;
                else if (level == DifficultyLevel.Medium) return 8;
                else if (level == DifficultyLevel.Hard) return 16;
                else if (level == DifficultyLevel.VeryHard) return 64;
                else return 8;
            }
            else if (mode == BinningMode.OKLAB)
            {
                if (level == DifficultyLevel.Easy) return 8;
                else if (level == DifficultyLevel.Medium) return 24;
                else if (level == DifficultyLevel.Hard) return 48;
                else if (level == DifficultyLevel.VeryHard) return 96;
                else return 24;
            }
            else if (mode == BinningMode.YCbCr)
            {
                if (level == DifficultyLevel.Easy) return 12;        // 낮은 정밀도
                else if (level == DifficultyLevel.Medium) return 24;
                else if (level == DifficultyLevel.Hard) return 48;
                else if (level == DifficultyLevel.VeryHard) return 96;  // 높은 정밀도
                else return 24;
            }

            return 4; // fallback
        }





        private void cbxColorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentBinningMode = (BinningMode)cbxColorType.SelectedIndex;
        }


        private void cbxDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDifficulty = (DifficultyLevel)cbxDifficulty.SelectedIndex;
        }

        // RGB → Linear
        private static double SrgbToLinear(double c)
        {
            return (c <= 0.04045) ? (c / 12.92) : Math.Pow((c + 0.055) / 1.055, 2.4);
        }

        // Linear → sRGB
        private static double LinearToSrgb(double c)
        {
            return (c <= 0.0031308) ? (12.92 * c) : (1.055 * Math.Pow(c, 1.0 / 2.4) - 0.055);
        }
        private static double Cbrt(double x)
        {
            return Math.Sign(x) * Math.Pow(Math.Abs(x), 1.0 / 3.0);
        }

        // RGB → Oklab
        public static (double L, double a, double b) RgbToOklab(Color color)
        {
            double r = SrgbToLinear(color.R / 255.0);
            double g = SrgbToLinear(color.G / 255.0);
            double b = SrgbToLinear(color.B / 255.0);

            double l = 0.4122214708 * r + 0.5363325363 * g + 0.0514459929 * b;
            double m = 0.2119034982 * r + 0.6806995451 * g + 0.1073969566 * b;
            double s = 0.0883024619 * r + 0.2817188376 * g + 0.6299787005 * b;

            double l_ = Math.Pow(l, 1.0 / 3.0);
            double m_ = Math.Pow(m, 1.0 / 3.0);
            double s_ = Math.Pow(s, 1.0 / 3.0);

            return (
                L: 0.2104542553 * l_ + 0.7936177850 * m_ - 0.0040720468 * s_,
                a: 1.9779984951 * l_ - 2.4285922050 * m_ + 0.4505937099 * s_,
                b: 0.0259040371 * l_ + 0.7827717662 * m_ - 0.8086757660 * s_
            );
        }


        // Oklab → RGB
        public static Color OklabToRgb(double L, double a, double b)
        {
            double l_ = L + 0.3963377774 * a + 0.2158037573 * b;
            double m_ = L - 0.1055613458 * a - 0.0638541728 * b;
            double s_ = L - 0.0894841775 * a - 1.2914855480 * b;

            double l = l_ * l_ * l_;
            double m = m_ * m_ * m_;
            double s = s_ * s_ * s_;

            double r = 4.0767416621 * l - 3.3077115913 * m + 0.2309699292 * s;
            double g = -1.2684380046 * l + 2.6097574011 * m - 0.3413193965 * s;
            double b_ = -0.0041960863 * l - 0.7034186147 * m + 1.7076147010 * s;

            return Color.FromArgb(
                Clamp((int)(LinearToSrgb(r) * 255)),
                Clamp((int)(LinearToSrgb(g) * 255)),
                Clamp((int)(LinearToSrgb(b_) * 255))
            );
        }

        private Color SimplifyOklab(Color color, int level)
        {
            var (L, a, b) = RgbToOklab(color);

            double stepL = 1.0 / level;
            double stepa = 1.0 / level;
            double stepb = 1.0 / level;

            L = Math.Round(L / stepL) * stepL;
            a = Math.Round(a / stepa) * stepa;
            b = Math.Round(b / stepb) * stepb;

            return OklabToRgb(L, a, b);
        }
        private Color SimplifyYCbCr(Color color, int level)
        {
            // 채널별 스텝 계산
            int stepY = 256 / level;     // 밝기
            int stepCb = 256 / (level / 2);  // 색차
            int stepCr = 256 / (level / 2);  // 색차

            double r = color.R, g = color.G, b = color.B;

            // RGB -> YCbCr
            double y = 0.299 * r + 0.587 * g + 0.114 * b;
            double cb = -0.168736 * r - 0.331264 * g + 0.5 * b + 128;
            double cr = 0.5 * r - 0.418688 * g - 0.081312 * b + 128;

            // Quantize
            y = Math.Round(y / stepY) * stepY;
            cb = Math.Round(cb / stepCb) * stepCb;
            cr = Math.Round(cr / stepCr) * stepCr;

            // YCbCr -> RGB
            double rOut = y + 1.402 * (cr - 128);
            double gOut = y - 0.344136 * (cb - 128) - 0.714136 * (cr - 128);
            double bOut = y + 1.772 * (cb - 128);

            return Color.FromArgb(
                Clamp((int)Math.Round(rOut)),
                Clamp((int)Math.Round(gOut)),
                Clamp((int)Math.Round(bOut))
            );
        }

        private void tsmiImageLoad_Click(object sender, EventArgs e)
        {
            btnLoadImage.PerformClick();
        }

        private void tsmiImageSave_Click(object sender, EventArgs e)
        {
            btnSave.PerformClick();
        }

        private void tsmiPickPaletteColor_Click(object sender, EventArgs e)
        {
            btnColorSelect.PerformClick();  // 버튼 클릭 효과를 그대로 냄
        }

        private void tsmiGenerate_Click(object sender, EventArgs e)
        {
            btnGenerate.PerformClick();
        }



        private void tsmiColorAll_Click(object sender, EventArgs e)
        {
            btnColorAll.PerformClick();
        }

        private void tsmiThick1x1_Click(object sender, EventArgs e)
        {
            colorPartitionMode = false;
            penThickness = 1;
            panel1.Visible = false;
        }

        private void tsmiThick3x3_Click(object sender, EventArgs e)
        {
            colorPartitionMode = false;
            penThickness = 3;
            panel1.Visible = false;
        }

        private void tsmiThick5x5_Click(object sender, EventArgs e)
        {
            colorPartitionMode = false;
            penThickness = 5;
            panel1.Visible = false;
        }

        private void tsmiThickPartition_Click(object sender, EventArgs e)
        {
            colorPartitionMode = true;
            penThickness = 1; // 부분 색칠 모드에서는 기본 굵기 사용
            MessageBox.Show("번호 기준 색칠 모드: 색칠할 셀을 클릭하세요.");
            panel1.Visible = false;
        }

        private void tsmiSaveGrid_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Grid Coloring Save (*.gcsave)|*.gcsave",
                Title = "Save Coloring Project"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = null;
                BinaryWriter writer = null;

                try
                {
                    fs = File.Open(sfd.FileName, FileMode.Create);
                    writer = new BinaryWriter(fs);

                    // 1. originalImage 저장
                    using (MemoryStream ms = new MemoryStream())
                    {
                        originalImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] bytes = ms.ToArray();
                        writer.Write(bytes.Length);
                        writer.Write(bytes);
                    }

                    // 2. paintedResultBitmap 저장
                    using (MemoryStream ms = new MemoryStream())
                    {
                        paintedResultBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] bytes = ms.ToArray();
                        writer.Write(bytes.Length);
                        writer.Write(bytes);
                    }

                    // 3. 도안 정보 저장
                    int height = colorNumbers.GetLength(0);
                    int width = colorNumbers.GetLength(1);

                    writer.Write(width);
                    writer.Write(height);
                    writer.Write(blockSize);
                    writer.Write(colorMap.Count); // colorCount
                    writer.Write((int)currentBinningMode);
                    writer.Write((int)currentDifficulty);

                    for (int y = 0; y < height; y++)
                        for (int x = 0; x < width; x++)
                            writer.Write(colorNumbers[y, x]);

                    for (int y = 0; y < height; y++)
                        for (int x = 0; x < width; x++)
                        {
                            writer.Write(isFilled[y, x]);
                            writer.Write(filledColors[y, x].ToArgb());
                        }

                    // ✅ 색상과 번호 함께 저장
                    writer.Write(colorMap.Count);
                    foreach (var kvp in colorMap)
                    {
                        writer.Write(kvp.Key.ToArgb()); // Color
                        writer.Write(kvp.Value);        // Number
                    }

                    MessageBox.Show("저장 완료되었습니다.");
                }
                finally
                {
                    writer?.Close();
                    fs?.Close();
                }
            }
        }









        private void tsmiLoadGrid_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Grid Coloring Save (*.gcsave)|*.gcsave",
                Title = "Load Coloring Project"
            };
            isPatternGenerated = false; // 패턴이 새로 생성되지 않았음을 표시
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = null;
                BinaryReader reader = null;

                try
                {
                    fs = File.Open(ofd.FileName, FileMode.Open);
                    reader = new BinaryReader(fs);

                    // 1. originalImage 복원
                    int origLen = reader.ReadInt32();
                    byte[] origBytes = reader.ReadBytes(origLen);
                    using (MemoryStream ms = new MemoryStream(origBytes))
                    {
                        originalImage = new Bitmap(ms);
                    }

                    // 2. paintedResultBitmap 복원
                    int paintLen = reader.ReadInt32();
                    byte[] paintBytes = reader.ReadBytes(paintLen);
                    using (MemoryStream ms = new MemoryStream(paintBytes))
                    {
                        paintedResultBitmap = new Bitmap(ms);
                    }

                    // 3. 도안 정보 복원
                    int width = reader.ReadInt32();
                    int height = reader.ReadInt32();
                    blockSize = reader.ReadInt32();
                    colorCount = reader.ReadInt32();
                    currentBinningMode = (BinningMode)reader.ReadInt32();
                    currentDifficulty = (DifficultyLevel)reader.ReadInt32();

                    // ComboBox Index 유효성 검사
                    cbxColorType.SelectedIndex = Math.Max(0, Math.Min((int)currentBinningMode, cbxColorType.Items.Count - 1));
                    cbxDifficulty.SelectedIndex = Math.Max(0, Math.Min((int)currentDifficulty, cbxDifficulty.Items.Count - 1));

                    colorNumbers = new int[height, width];
                    isFilled = new bool[height, width];
                    filledColors = new Color[height, width];

                    for (int y = 0; y < height; y++)
                        for (int x = 0; x < width; x++)
                            colorNumbers[y, x] = reader.ReadInt32();

                    for (int y = 0; y < height; y++)
                        for (int x = 0; x < width; x++)
                        {
                            isFilled[y, x] = reader.ReadBoolean();
                            filledColors[y, x] = Color.FromArgb(reader.ReadInt32());
                        }

                    // ✅ 색상 매핑 복원 (Color + Number)
                    int colorMapCount = reader.ReadInt32();
                    colorMap = new Dictionary<Color, int>();
                    colorNumberToRGB = new Dictionary<int, Color>();
                    for (int i = 0; i < colorMapCount; i++)
                    {
                        Color col = Color.FromArgb(reader.ReadInt32());
                        int number = reader.ReadInt32();
                        colorMap[col] = number;
                        colorNumberToRGB[number] = col;
                    }

                    // 팔레트 갱신
                    CreateColorPalette();

                    // UI 반영
                    panelOriginalImg.BackgroundImage = ResizeToFit(originalImage, panelOriginalImg.Width, panelOriginalImg.Height);
                    panelOriginalImg.BackgroundImageLayout = ImageLayout.Center;

                    panelCanvas.BackgroundImage = ResizeToFit(paintedResultBitmap, panelCanvas.Width, panelCanvas.Height);
                    panelCanvas.BackgroundImageLayout = ImageLayout.Center;

                    panelCanvas.Invalidate();
                    panelCompare.Invalidate();

                    MessageBox.Show("도안 불러오기 완료!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("불러오기 실패: " + ex.Message);
                }
                finally
                {
                    reader?.Close();
                    fs?.Close();
                }
            }
        }





        private int Clamp(int val, int min, int max)
        {
            return Math.Max(min, Math.Min(val, max));
        }




        private void RepaintCanvas()
        {
            panelCanvas.Invalidate();
        }

        private void panelPalette_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (colorNumbers == null || colorNumberToRGB == null) return;

            int hBlocks = colorNumbers.GetLength(0);
            int wBlocks = colorNumbers.GetLength(1);

            float scaleX = (float)panelCanvas.Width / wBlocks;
            float scaleY = (float)panelCanvas.Height / hBlocks;
            float cellSize = Math.Min(scaleX, scaleY);
            int totalW = (int)(cellSize * wBlocks);
            int totalH = (int)(cellSize * hBlocks);
            int offsetX = (panelCanvas.Width - totalW) / 2;
            int offsetY = (panelCanvas.Height - totalH) / 2;

            int cx = (int)((e.X - offsetX) / cellSize);
            int cy = (int)((e.Y - offsetY) / cellSize);

            if (cx < 0 || cx >= wBlocks || cy < 0 || cy >= hBlocks) return;

            int colorNum = colorNumbers[cy, cx];
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"좌표: ({cx}, {cy})");
            sb.AppendLine($"번호: {colorNum}");

            if (colorNumberToRGB.TryGetValue(colorNum, out Color baseColor))
            {
                sb.AppendLine($"도안 색상: RGB({baseColor.R}, {baseColor.G}, {baseColor.B})");

                if (isFilled != null && isFilled[cy, cx])
                {
                    Color userColor = filledColors[cy, cx];
                    sb.AppendLine($"사용자 색상: RGB({userColor.R}, {userColor.G}, {userColor.B})");

                    double dr = baseColor.R - userColor.R;
                    double dg = baseColor.G - userColor.G;
                    double db = baseColor.B - userColor.B;

                    double distance = Math.Sqrt(dr * dr + dg * dg + db * db);
                    double maxDistance = Math.Sqrt(3) * 255.0; // 255 * sqrt(3)
                    double similarity = 100.0 - (distance / maxDistance * 100.0);

                    sb.AppendLine($"색상 유사도 (RGB 거리 기반): {similarity:F1}%");

                }
            }

            toolTip1.SetToolTip(panelCanvas, sb.ToString());
        }

        private void Coloring_Load_1(object sender, EventArgs e)
        {

        }
    }


    //그리드 저장하기용 클래스
    public class GridSaveData
    {
        public string FormType;
        public int BlockSize;
        public int GridWidth;
        public string BinningMode;
        public string Difficulty;

        public Color[,] FilledColors;
        public bool[,] IsFilled;
        public int[,] ColorNumbers;
        public Bitmap OriginalImage;
    }

}