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
    public partial class Coloring : Form
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
        private float zoom = 1.0f; // 확대배율 추가

        public Coloring()
        {
            InitializeComponent(); 
            this.Load += Coloring_Load; 
        }

        private void Coloring_Load(object sender, EventArgs e)
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

            // 2. 배열 초기화
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

            // 불러온 그림 크기에 따라 
            panelCanvas.Size = new Size(wBlocks * blockSize, hBlocks * blockSize);

            // 4. 도안 그리기 요청 + 팔레트 생성
            panelCanvas.Invalidate();   // 다시 그리기
            CreateColorPalette();       // 색상 팔레트 생성

            // 도안 패널 크기 맞추고
            FitCanvas();
            // 그에 맞춰 폼 전체 크기 맞추기
            ResizeFormToFit();
        }

        // panelCanvas 크기에 맞춰 Form 전체 크기 조정
        private void FitCanvas()
        {
            // 도안 크기(블록수×blockSize) 만큼 panelCanvas 크기 맞추기
            int wBlocks = originalImage.Width / blockSize;
            int hBlocks = originalImage.Height / blockSize;

            panelCanvas.Size = new Size(wBlocks * blockSize, hBlocks * blockSize);
            panelCanvas.Invalidate();
        }

        // 폼을 도안 크기에 맞춰 크기 조정 (여유 공간 포함)
        private void ResizeFormToFit()
        {
            // 클라이언트(내용) 영역 너비 = palette 폭 + canvas 폭
            int clientW = panelLeft.Width + panelCanvas.Width;

            // 클라이언트 높이는 두 영역 중 큰 쪽
            int clientH = Math.Max(panelLeft.Height, panelCanvas.Height);

            // 제목표시줄/테두리 보정값
            int extraW = this.Width - this.ClientSize.Width;
            int extraH = this.Height - this.ClientSize.Height;

            // 여유 공간 (양쪽 각 20px씩)
            const int marginW = 40;
            const int marginH = 40;

            // 폼 크기 설정
            this.ClientSize = new Size(clientW + marginW, clientH + marginH);
            this.Size = new Size(clientW + extraW + marginW,
                                        clientH + extraH + marginH);
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

        //팔레트 초기화 메서드.  등장 횟수별로 색상 팔레트 정렬
        private void CreateColorPalette()
        {
            // 1) 색상 번호별 등장 횟수 카운트
            var counts = new Dictionary<int, int>();
            int rows = colorNumbers.GetLength(0);
            int cols = colorNumbers.GetLength(1);
            for (int y = 0; y < rows; y++)
                for (int x = 0; x < cols; x++)
                {
                    int key = colorNumbers[y, x];
                    if (counts.TryGetValue(key, out var existing))
                        counts[key] = existing + 1;
                    else
                        counts[key] = 1;
                }
                    

            // 2) 등장 횟수 내림차순 > 번호 순차 정렬
            var sortedNums = counts
              .OrderByDescending(kv => kv.Value)
              .ThenBy(kv => kv.Key)
              .Select(kv => kv.Key);

            panelPalette.Controls.Clear(); // 기존 팔레트 제거

            // 색상 → 번호 매핑 순서대로 정렬
            var sortedColors = colorMap.OrderBy(kv => kv.Value);

            // 4) 등장 빈도순으로 버튼 생성
            foreach (int num in sortedNums)
            {
                // num 에 매핑된 색 찾아오기
                Color c = colorMap.First(kv => kv.Value == num).Key;

                Button btn = new Button
                {
                    BackColor = c,
                    Text = num.ToString(),
                    Width = 40,
                    Height = 40,
                    Margin = new Padding(5),
                    Tag = c
                };
                btn.Click += ColorButton_Click;
                panelPalette.Controls.Add(btn);
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
            var g = e.Graphics;
            g.Clear(Color.White);

         
            // 도안이 없고 원본 이미지만 있을 때: 원본 미리보기
            if (blockColors == null && originalImage != null)
            {
                // 비율 유지해서 원본 이미지를 패널 크기에 맞춰 그림
                float scaleX = (float)panelCanvas.Width / originalImage.Width;
                float scaleY = (float)panelCanvas.Height / originalImage.Height;
                float scale = Math.Min(scaleX, scaleY);

                int drawW = (int)(originalImage.Width * scale);
                int drawH = (int)(originalImage.Height * scale);
                int offsetX = (panelCanvas.Width - drawW) / 2;
                int offsetY = (panelCanvas.Height - drawH) / 2;

                g.DrawImage(originalImage,
                            new Rectangle(offsetX, offsetY, drawW, drawH));
                return;
            }

            if (blockColors == null)
                return;  // 아무 것도 그릴 게 없으면 그냥 빠져나감

            //  도안(번호 그리기) 
            // 원하는 확대 배율(1.0 = 원래 크기, 2.0 = 두 배 등)
            float zoom = 1.5f; // 화면 확대 비율
            int renderSize = (int)(blockSize * zoom); // 실제 한 셀 크기

            int rows = blockColors.GetLength(0);
            int cols = blockColors.GetLength(1);

            Font font = new Font("Arial", renderSize * 0.4f);
            Pen pen = new Pen(Color.Gray);
            
            try
            {
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < cols; x++)
                    {
                        var rect = new Rectangle(
                            x * renderSize,
                            y * renderSize,
                            renderSize,
                            renderSize);

                        // 색칠된 셀 채우기
                        if (isFilled != null && isFilled[y, x])
                        {
                            using (var brush = new SolidBrush(filledColors[y, x]))
                                g.FillRectangle(brush, rect);
                        }

                        // 테두리
                        g.DrawRectangle(pen, rect);

                        // 번호 그리기
                        string num = colorNumbers[y, x].ToString();
                        g.DrawString(num, font, Brushes.Black, rect.Location);
                    }
                }
            }
            finally
            {
                font.Dispose();
                pen.Dispose();
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
