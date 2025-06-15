using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        public Coloring()
        {
            InitializeComponent(); 
            this.Load += Coloring_Load; 
        }

        private void Coloring_Load(object sender, EventArgs e)
        {
            // 난이도 설정 제거, 가로 픽셀 수로 조정

            // 가로 셀 수 기본 설정 (예: 30)
            numPixelSize.Value = 30;
            numPixelSize.Minimum = 5;
            numPixelSize.Maximum = 100;
            panel1.Visible = !panel1.Visible;
        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("먼저 이미지를 불러와주세요.");
                return;
            }

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

                    Color simplified = SimplifyColor(avg, colorCount);
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
                    // 기존 이미지 해제
                    if (originalImage != null)
                        originalImage.Dispose();

                    // 선택한 이미지 로드
                    originalImage = new Bitmap(ofd.FileName);

                    // 이미지 사이즈 확인용 출력 (선택사항)
                    MessageBox.Show($"이미지 크기: {originalImage.Width} x {originalImage.Height}");

                    // 패널에 바로 렌더링 해보기 (테스트용)
                    panelCanvas.Invalidate(); // 패널 다시 그리기
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지 로드 실패: " + ex.Message);
                }
            }

        }
        //색상 단순화
        private Color SimplifyColor(Color color, int levels)
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
            }
        }


        //private void panelCanvas_MouseClick(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        if (blockColors == null) return;

        //        int hBlocks = blockColors.GetLength(0);
        //        int wBlocks = blockColors.GetLength(1);

        //        float scaleX = (float)panelCanvas.Width / wBlocks;
        //        float scaleY = (float)panelCanvas.Height / hBlocks;
        //        float cellSize = Math.Min(scaleX, scaleY);
        //        int totalW = (int)(cellSize * wBlocks);
        //        int totalH = (int)(cellSize * hBlocks);
        //        int offsetX = (panelCanvas.Width - totalW) / 2;
        //        int offsetY = (panelCanvas.Height - totalH) / 2;

        //        int x = (int)((e.X - offsetX) / cellSize);
        //        int y = (int)((e.Y - offsetY) / cellSize);

        //        if (x >= 0 && x < wBlocks && y >= 0 && y < hBlocks)
        //        {
        //            isFilled[y, x] = true;
        //            filledColors[y, x] = selectedColor;
        //            selectedPoint = new Point(x, y);

        //            // ✅ paintedResultBitmap에 블록 단위 색 반영
        //            if (paintedResultBitmap != null)
        //            {
        //                for (int dy = 0; dy < blockSize; dy++)
        //                {
        //                    int py = y * blockSize + dy;
        //                    if (py >= paintedResultBitmap.Height) continue;

        //                    for (int dx = 0; dx < blockSize; dx++)
        //                    {
        //                        int px = x * blockSize + dx;
        //                        if (px >= paintedResultBitmap.Width) continue;

        //                        paintedResultBitmap.SetPixel(px, py, selectedColor);
        //                    }
        //                }
        //            }

        //            // ✅ 셀만 리프레시
        //            RectangleF rect = new RectangleF(offsetX + x * cellSize, offsetY + y * cellSize, cellSize, cellSize);
        //            panelCanvas.Invalidate(Rectangle.Ceiling(rect));
        //        }
        //        paintedResultBitmap.SetPixel(x, y, selectedColor);

        //        panelCompare.Invalidate();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
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
            if (blockColors == null || colorMap.Count == 0)
            {
                MessageBox.Show("먼저 도안을 생성해주세요.");
                return;
            }

            int hBlocks = blockColors.GetLength(0);
            int wBlocks = blockColors.GetLength(1);

            isFilled = new bool[hBlocks, wBlocks];
            filledColors = new Color[hBlocks, wBlocks];

            Dictionary<int, Color> numberToColor = colorMap.ToDictionary(kv => kv.Value, kv => kv.Key);

            int imgWidth = wBlocks * blockSize;
            int imgHeight = hBlocks * blockSize;
            paintedResultBitmap = new Bitmap(imgWidth, imgHeight);

            using (Graphics g = Graphics.FromImage(paintedResultBitmap))
            {
                g.Clear(Color.White);

                for (int y = 0; y < hBlocks; y++)
                {
                    for (int x = 0; x < wBlocks; x++)
                    {
                        int num = colorNumbers[y, x];

                        if (numberToColor.TryGetValue(num, out Color color))
                        {
                            isFilled[y, x] = true;
                            filledColors[y, x] = color;

                            // 💡 셀 위치에 전체 사각형 색칠
                            Rectangle rect = new Rectangle(x * blockSize, y * blockSize, blockSize, blockSize);
                            using (Brush brush = new SolidBrush(color))
                            {
                                g.FillRectangle(brush, rect);
                            }
                        }
                    }
                }
            }

            panelCanvas.Invalidate();  // 다시 그리기
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
            penThickness=1; // 부분 색칠 모드에서는 기본 굵기 사용
            MessageBox.Show("번호 기준 색칠 모드: 색칠할 셀을 클릭하세요.");
        }


    }
}
