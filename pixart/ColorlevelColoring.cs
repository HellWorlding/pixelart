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
            simplifiedColors = new Color[hBlocks, wBlocks]; // ✅ 추가
            colorNumbers = new int[hBlocks, wBlocks];
            isFilled = new bool[hBlocks, wBlocks];
            filledColors = new Color[hBlocks, wBlocks];
            colorMap.Clear();
            colorNumberToRGB.Clear(); // ✅ 추가

            // 전체 픽셀 캐시
            Color[,] pixelCache = new Color[originalImage.Height, originalImage.Width];
            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    pixelCache[y, x] = originalImage.GetPixel(x, y);
                }
            }

            Dictionary<Color, int> simplifiedColorToNumber = new Dictionary<Color, int>();
            int currentNumber = 1;

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
                    simplifiedColors[by, bx] = simplified; // ✅ Simplified 색상 저장

                    if (!simplifiedColorToNumber.ContainsKey(simplified))
                    {
                        simplifiedColorToNumber[simplified] = currentNumber;
                        colorMap[simplified] = currentNumber;
                        colorNumberToRGB[currentNumber] = simplified; // ✅ 번호 → 색 매핑
                        currentNumber++;
                    }

                    int colorNum = simplifiedColorToNumber[simplified];
                    colorNumbers[by, bx] = colorNum;
                }
            }


            int smallW = desiredGridW;
            int smallH = originalImage.Height / blockSize;

            // 사용자가 색칠한 결과를 저장할 비트맵 초기화
            paintedResultBitmap = new Bitmap(smallW, smallH);
            using (Graphics g = Graphics.FromImage(paintedResultBitmap))
            {
                g.Clear(Color.White);  // 또는 Transparent
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
                        SolidBrush brush = new SolidBrush(filledColors[y, x]);
                        g.FillRectangle(brush, rect);
                        brush.Dispose();
                    }

                    g.DrawRectangle(Pens.Gray, left, top, cellSize, cellSize);

                    if (!isFilled[y, x])
                    {
                        string num = colorNumbers[y, x].ToString();
                        g.DrawString(num, font, Brushes.Black, rect.Location);
                    }

                    //if (selectedPoint.HasValue && selectedPoint.Value == new Point(x, y))
                    //{
                    //    Pen redPen = new Pen(Color.Red, 2);
                    //    g.DrawRectangle(redPen, left, top, cellSize, cellSize);
                    //    redPen.Dispose();
                    //}
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

                int x = (int)((e.X - offsetX) / cellSize);
                int y = (int)((e.Y - offsetY) / cellSize);

                if (x >= 0 && x < wBlocks && y >= 0 && y < hBlocks)
                {
                    isFilled[y, x] = true;
                    filledColors[y, x] = selectedColor;
                    selectedPoint = new Point(x, y);

                    // ✅ paintedResultBitmap에 블록 단위 색 반영
                    if (paintedResultBitmap != null)
                    {
                        for (int dy = 0; dy < blockSize; dy++)
                        {
                            int py = y * blockSize + dy;
                            if (py >= paintedResultBitmap.Height) continue;

                            for (int dx = 0; dx < blockSize; dx++)
                            {
                                int px = x * blockSize + dx;
                                if (px >= paintedResultBitmap.Width) continue;

                                paintedResultBitmap.SetPixel(px, py, selectedColor);
                            }
                        }
                    }

                    // ✅ 셀만 리프레시
                    RectangleF rect = new RectangleF(offsetX + x * cellSize, offsetY + y * cellSize, cellSize, cellSize);
                    panelCanvas.Invalidate(Rectangle.Ceiling(rect));
                }
                paintedResultBitmap.SetPixel(x, y, selectedColor);

                panelCompare.Invalidate();
            }
            catch (Exception ex)
            {
                
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

            Point pt = selectedPoint.Value;
            int y = pt.Y;
            int x = pt.X;

            // 색상 정보
            int colorNum = colorNumbers[y, x];
            Color originalColor = colorNumberToRGB.ContainsKey(colorNum) ? colorNumberToRGB[colorNum] : Color.Gray;
            Color? filledColor = isFilled[y, x] ? (Color?)filledColors[y, x] : null;

            // 너비 기준 계산
            int gap = 20;
            int padding = 10;
            int cellSize = Math.Min(panelCompare.Height - 2 * padding, (panelCompare.Width - 3 * gap) / 2);

            int totalWidth = 2 * cellSize + gap;
            int offsetX = (panelCompare.Width - totalWidth) / 2;
            int offsetY = padding;

            Rectangle rectOriginal = new Rectangle(offsetX, offsetY, cellSize, cellSize);
            Rectangle rectFilled = new Rectangle(offsetX + cellSize + gap, offsetY, cellSize, cellSize);

            using (SolidBrush brush = new SolidBrush(originalColor))
            {
                g.FillRectangle(brush, rectOriginal);
            }

            g.DrawRectangle(Pens.Black, rectOriginal);
            g.DrawString("Original", DefaultFont, Brushes.Black, rectOriginal.X, rectOriginal.Bottom + 5);

            if (filledColor.HasValue)
            {
                using (SolidBrush brush = new SolidBrush(filledColor.Value))
                {
                    g.FillRectangle(brush, rectFilled);
                }
            }
            else
            {
                using (HatchBrush hatch = new HatchBrush(HatchStyle.LargeGrid, Color.LightGray, Color.White))
                {
                    g.FillRectangle(hatch, rectFilled);
                }
            }

            g.DrawRectangle(Pens.Black, rectFilled);
            g.DrawString("Colored", DefaultFont, Brushes.Black, rectFilled.X, rectFilled.Bottom + 5);
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
            penThickness = 1; // 기본 굵기
        }

        private void btnSize3_Click(object sender, EventArgs e)
        {
            penThickness = 3;
            panel1.Visible = !panel1.Visible;
        }

        private void btnSize5_Click(object sender, EventArgs e)
        {
            //penThickness = 5;
            panel1.Visible = !panel1.Visible;
        }
    }
}
