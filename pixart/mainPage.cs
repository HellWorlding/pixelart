using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelColorling
{
    public partial class Form1 : Form
    {
        private Bitmap originalImage; // 원본 이미지 저장용
        private int blockSize = 10;          // 난이도별 블럭 크기
        private int colorCount = 12;         // 난이도별 색상 수
        private Color[,] blockColors;        // 각 블럭의 색상
        private int[,] colorNumbers;         // 색상에 대한 번호 매핑
        private Dictionary<Color, int> colorMap = new Dictionary<Color, int>();// 색상 → 번호
        private Bitmap patternBitmap;        // 도안 결과 저장용
        private Color selectedColor = Color.Transparent;
        private bool[,] isFilled;           // 셀이 색칠되었는지 여부
        private Color[,] filledColors;      // 셀에 실제 색칠한 색상

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load_1;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            cmbDifficulty.SelectedIndex = 1;
        }

        //도안 생성 버튼 이벤트
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("먼저 이미지를 불러와주세요.");
                return;
            }

            switch (cmbDifficulty.SelectedItem.ToString())
            {
                case "쉬움":
                    blockSize = 14;
                    colorCount = 8;
                    break;
                case "보통":
                    blockSize = 10;
                    colorCount = 12;
                    break;
                case "어려움":
                    blockSize = 7;
                    colorCount = 18;
                    break;
            }

            int wBlocks = originalImage.Width / blockSize;
            int hBlocks = originalImage.Height / blockSize;

            // 2. 배열 초기화 (순서 중요!)
            blockColors = new Color[hBlocks, wBlocks];
            colorNumbers = new int[hBlocks, wBlocks];
            isFilled = new bool[hBlocks, wBlocks];
            filledColors = new Color[hBlocks, wBlocks];
            colorMap.Clear();

            // 3. 각 블럭의 평균 색상 계산 + 색상 번호 매핑
            for (int y = 0; y < hBlocks; y++)
            {
                for (int x = 0; x < wBlocks; x++)
                {
                    Color avgColor = GetAverageColor(x * blockSize, y * blockSize, blockSize);
                    blockColors[y, x] = avgColor;

                    Color simplified = SimplifyColor(avgColor, colorCount);
                    if (!colorMap.ContainsKey(simplified))
                        colorMap[simplified] = colorMap.Count + 1;

                    colorNumbers[y, x] = colorMap[simplified];
                }
            }

            // 4. 도안 그리기 요청 + 팔레트 생성
            panelCanvas.Invalidate();   // 다시 그리기
            CreateColorPalette();       // 색상 팔레트 생성
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
            // 도안이 없고, 이미지만 불러온 상태일 경우 원본 미리보기
            if (blockColors == null && originalImage != null)
            {
                // 비율 유지해서 그리기
                float scaleX = (float)panelCanvas.Width / originalImage.Width;
                float scaleY = (float)panelCanvas.Height / originalImage.Height;
                float scale = Math.Min(scaleX, scaleY);

                int drawWidth = (int)(originalImage.Width * scale);
                int drawHeight = (int)(originalImage.Height * scale);

                int offsetX = (panelCanvas.Width - drawWidth) / 2;
                int offsetY = (panelCanvas.Height - drawHeight) / 2;

                e.Graphics.DrawImage(originalImage, new Rectangle(offsetX, offsetY, drawWidth, drawHeight));
                return; // 더 이상 그릴 도안이 없으므로 여기서 종료
            }

            if (blockColors == null) return;

            Graphics g = e.Graphics;
            g.Clear(Color.White);

            int hBlocks = blockColors.GetLength(0);
            int wBlocks = blockColors.GetLength(1);

            Font font = new Font("Arial", 8);

            for (int y = 0; y < hBlocks; y++)
            {
                for (int x = 0; x < wBlocks; x++)
                {
                    Rectangle rect = new Rectangle(x * blockSize, y * blockSize, blockSize, blockSize);

                    // 1. 색칠된 셀이면 색상 채우기
                    if (isFilled != null && isFilled[y, x])
                    {
                        using (SolidBrush brush = new SolidBrush(filledColors[y, x]))
                        {
                            g.FillRectangle(brush, rect);
                        }
                    }

                    // 2. 테두리 그리기
                    g.DrawRectangle(Pens.Gray, rect);

                    // 3. 색칠 안 된 셀만 번호 표시
                    if (!isFilled[y, x])
                    {
                        string num = colorNumbers[y, x].ToString();
                        g.DrawString(num, font, Brushes.Black, rect.Location);
                    }

                }
            }



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (blockColors == null)
            {
                MessageBox.Show("먼저 도안을 생성해주세요.");
                return;
            }

            int hBlocks = blockColors.GetLength(0);
            int wBlocks = blockColors.GetLength(1);

            patternBitmap = new Bitmap(wBlocks * blockSize, hBlocks * blockSize);

            using (Graphics g = Graphics.FromImage(patternBitmap))
            {
                g.Clear(Color.White);
                Font font = new Font("Arial", 8);

                for (int y = 0; y < hBlocks; y++)
                {
                    for (int x = 0; x < wBlocks; x++)
                    {
                        Rectangle rect = new Rectangle(x * blockSize, y * blockSize, blockSize, blockSize);

                        // 1. 색칠된 셀은 색상으로 채우기
                        if (isFilled != null && isFilled[y, x])
                        {
                            using (SolidBrush brush = new SolidBrush(filledColors[y, x]))
                            {
                                g.FillRectangle(brush, rect);
                            }
                        }

                        // 2. 테두리 그리기
                        g.DrawRectangle(Pens.Gray, rect);

                        // 3. 번호 쓰기 (색칠 여부와 상관없이 항상 표시)
                        string num = colorNumbers[y, x].ToString();
                        g.DrawString(num, font, Brushes.Black, rect.Location);
                    }
                }
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PNG Files|*.png",
                Title = "도안 저장",
                FileName = "PixelPattern.png"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                patternBitmap.Save(sfd.FileName);
                MessageBox.Show("저장 완료!");
            }

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
            if (blockColors == null || selectedColor == Color.Transparent)
            {
                MessageBox.Show("먼저 색상을 선택해주세요.");
                return;
            }

            int x = e.X / blockSize;
            int y = e.Y / blockSize;

            if (y < 0 || y >= blockColors.GetLength(0) || x < 0 || x >= blockColors.GetLength(1))
                return;

            Color simplifiedClicked = SimplifyColor(blockColors[y, x], colorCount);
            Color simplifiedSelected = SimplifyColor(selectedColor, colorCount);

            if (colorMap.ContainsKey(simplifiedClicked) && simplifiedClicked == simplifiedSelected)
            {
                isFilled[y, x] = true;
                filledColors[y, x] = selectedColor;

                // 해당 셀만 다시 그리기
                Rectangle invalidateRect = new Rectangle(x * blockSize, y * blockSize, blockSize, blockSize);
                panelCanvas.Invalidate(invalidateRect);
            }
            else
            {
                MessageBox.Show("선택한 색상과 셀의 색상 번호가 다릅니다!");
            }
        }

    }
}
