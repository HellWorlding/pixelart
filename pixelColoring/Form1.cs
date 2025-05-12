using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace pixel
{
    public partial class Form1 : Form
    {
        

        // 원본 이미지 비트맵
        Bitmap originalImage;
        // 픽셀화된 이미지 비트맵
        Bitmap pixelatedImage;

        // 픽셀 분할 크기
        int pixelSize;
        // 셀마다 색상 번호 저장 그리드
        int[,] numberGrid;
        // 사용자 색칠한 셀 정보(point -> color)
        Dictionary<Point, Color> pixelColors = new Dictionary<Point, Color>();
        // 사용자가 현재 클릭한 셀(색칠 대상)
        Point? selectedPoint = null;
        // 대표 색상 수 (원한다면 NumericUpDown으로 조절 가능)
        int k = 8;

        // 색상 번호와 색상 매핑
        private Dictionary<int, Color> numberToColor = new Dictionary<int, Color>();

        

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            picOriginalThumb.Image = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "이미지 파일|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(ofd.FileName);
                picPreview.Image = null;

                //int minSide = Math.Min(originalImage.Width, originalImage.Height);
                //int maxSide = Math.Max(originalImage.Width, originalImage.Height);
                //// 최소 줄이기 사이즈(픽셀화 크기) 설정, 원본의 1/2 또는 긴 셀 개수 100
                //numPixelSize.Minimum = Math.Max(2, maxSide / 100);
                //// 최대 줄이기 사이즈 (픽셀화 크기) 설정, 원본중 작은 사이드의 1 /10
                //numPixelSize.Maximum = minSide / 10;

                int imageWidth = originalImage.Width;

                // 가로 셀 수를 기준으로 설정
                numPixelSize.Minimum = 10; // 최소 가로 셀 수 (직관적 최소값)
                numPixelSize.Maximum = imageWidth / 2; // 셀 하나가 최소 2픽셀은 되도록 제한

                
                // 그리드 크기 최소 설정
                //numPixelSize.Value = Math.Min(10, numPixelSize.Maximum);

                //k means 클러스터링 수 설정
                numKsize.Minimum = 2;
                numKsize.Maximum = 100;
                numKsize.Value = 8;

                // k means 반복 횟수 설정
                numKmeansIter.Minimum = 3;
                numKmeansIter.Maximum = 200;
            }
        }

        private void btnPixelate_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("이미지를 먼저 불러오세요.");
                return;
            }

            // 픽셀 메시지 박스 확인(하던 작업 초기화 방지)
            DialogResult result = MessageBox.Show("픽셀화 하시겠습니까?", "확인", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;

            // 이전 색칠 정보 초기화
            pixelColors.Clear();
            selectedPoint = null;

            // 이미지 축소
            int desiredGridW = (int)numPixelSize.Value;       // 사용자가 지정한 가로 셀 수
            pixelSize = originalImage.Width / desiredGridW;   // 자동 계산된 셀 크기
            int smallW = desiredGridW;                        // 줄일 이미지의 너비 = 셀 개수
            int smallH = originalImage.Height / pixelSize;    // 셀 크기로 나눈 줄일 이미지의 높이

            //pixelSize = (int)numPixelSize.Value;
            //int smallW = originalImage.Width / pixelSize;
            //int smallH = originalImage.Height / pixelSize;


            // 줄인 이미지
            Bitmap smallImage = new Bitmap(originalImage, new Size(smallW, smallH));
            pixelatedImage = new Bitmap(smallW, smallH);  // 새 비트맵 초기화

            int gridW = smallImage.Width;
            int gridH = smallImage.Height;
            // 색 번호 도안
            numberGrid = new int[gridH, gridW];
            // 축소 이미지 저장용
            Color[,] colorGrid = new Color[gridH, gridW];
            
            // K means 위한 픽셀 리스트
            List<double[]> pixels = new List<double[]>();

            for (int y = 0; y < gridH; y++)
            {
                for (int x = 0; x < gridW; x++)
                {
                    Color c = smallImage.GetPixel(x, y); //x, y 색 추출
                    colorGrid[y, x] = c; //축소 이미지 색 저장
                    pixels.Add(new double[] { c.R, c.G, c.B }); //rgb 리스트에 추가
                }
            }

            // k means 클러스터링 색상들
            k = (int)numKsize.Value;
            List<double[]> centroids = RunKMeans(pixels, k);

            // 대표 색상 리스트
            List<Color> representativeColors = centroids
                .Select(c => Color.FromArgb((int)c[0], (int)c[1], (int)c[2]))
                .ToList();
            // k means로 구한 대표 색상으로 색상 번호 매핑 (도안 숫자)
            Dictionary<Color, int> colorToNumber = new Dictionary<Color, int>();
            for (int i = 0; i < representativeColors.Count; i++)
                colorToNumber[representativeColors[i]] = i + 1;

            // 색상 번호와 색상 매핑
            numberToColor = colorToNumber.ToDictionary(kv => kv.Value, kv => kv.Key);
            
            // 축소 이미지 클러스터링 번호 매핑
            for (int y = 0; y < gridH; y++)
            {
                for (int x = 0; x < gridW; x++)
                {
                    Color original = colorGrid[y, x];
                    Color closest = FindClosestColor(original, representativeColors);
                    numberGrid[y, x] = colorToNumber[closest];
                }
            }

            // 썸네일 표시
            int thumbWidth = picOriginalThumb.Width;
            int thumbHeight = picOriginalThumb.Height;
            Bitmap thumbImage = new Bitmap(originalImage, new Size(thumbWidth, thumbHeight));
            picOriginalThumb.Image = thumbImage;

            // 색상 가이드 표시 
            panelLegend.Controls.Clear();


            foreach (var pair in colorToNumber.OrderBy(p => p.Value))
            {
                // 색상과 번호 쌍을 패널에 추가
                Panel swatch = new Panel();
                swatch.BackColor = pair.Key;
                swatch.Size = new Size(20, 20);
                swatch.Margin = new Padding(2);

                Label label = new Label();
                label.Text = pair.Value.ToString();
                label.AutoSize = true;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Padding = new Padding(0);

                FlowLayoutPanel itemPanel = new FlowLayoutPanel();
                itemPanel.FlowDirection = FlowDirection.TopDown;  //  세로 정렬
                itemPanel.WrapContents = false;
                itemPanel.Size = new Size(40, 40);  // 너비 제한

                itemPanel.Controls.Add(swatch);
                itemPanel.Controls.Add(label);

                panelLegend.Controls.Add(itemPanel);
            }

            // 이미지 새로고침
            picPreview.Image = null;
            picPreview.Invalidate();
        }


        double ColorDistanceSquared(Color a, Color b)
        {
            return 
                Math.Pow((int)a.R - b.R, 2) +
                Math.Pow((int)a.G - b.G, 2) +
                Math.Pow((int)a.B - b.B, 2);
        }

        Color FindClosestColor(Color input, List<Color> palette)
        {
            Color closest = palette[0];
            double minDist = ColorDistanceSquared(input, closest);

            foreach (var color in palette)
            {
                double dist = ColorDistanceSquared(input, color);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = color;
                }
            }

            return closest;
        }

        List<double[]> RunKMeans(List<double[]> data, int k, int maxIterations = 40)
        {
            Random rand = new Random();
            List<double[]> centroids = new List<double[]>();

            // 임의의 k개 색상 선택
            for (int i = 0; i < k; i++)
                centroids.Add(data[rand.Next(data.Count)]);
            // 반복
            for (int iter = 0; iter < maxIterations; iter++)
            {
                // 클러스터 전체 저장
                var clusters = new List<List<double[]>>();
                for (int i = 0; i < k; i++)
                    clusters.Add(new List<double[]>());

                // 각 데이터 포인트에 대해 가장 가까운 K값 찾기
                foreach (var point in data)
                {
                    double minDist = double.MaxValue;
                    int bestCluster = 0;

                    for (int i = 0; i < k; i++)
                    {
                        double dist = 
                            Math.Pow(point[0] - centroids[i][0], 2) +
                            Math.Pow(point[1] - centroids[i][1], 2) +
                            Math.Pow(point[2] - centroids[i][2], 2);

                        if (dist < minDist)
                        {
                            minDist = dist;
                            bestCluster = i;
                        }
                    }
                    // 각 클러스터에 픽셀 할당
                    clusters[bestCluster].Add(point);
                }

                // 클러스터의 중심점 업데이트
                for (int i = 0; i < k; i++)
                {
                    if (clusters[i].Count == 0) continue;

                    double r = 0, g = 0, b = 0;
                    foreach (var point in clusters[i])
                    {
                        r += point[0];
                        g += point[1];
                        b += point[2];
                    }

                    int count = clusters[i].Count;
                    centroids[i] = new double[] { r / count, g / count, b / count };
                }
            }

            // 최종 클러스터 색상 반환
            return centroids;
        }

        private void picPreview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            if (originalImage == null)
                return;

            // 1. 원본 미리보기 (픽셀화 이전)
            if (numberGrid == null)
            {
                float imgRatio = (float)originalImage.Width / originalImage.Height;
                float boxRatio = (float)picPreview.Width / picPreview.Height;

                int drawW, drawH;
                if (imgRatio > boxRatio)
                {
                    drawW = picPreview.Width;
                    drawH = (int)(drawW / imgRatio);
                }
                else
                {
                    drawH = picPreview.Height;
                    drawW = (int)(drawH * imgRatio);
                }

                int offsetX = (picPreview.Width - drawW) / 2;
                int offsetY = (picPreview.Height - drawH) / 2;

                g.DrawImage(originalImage, offsetX, offsetY, drawW, drawH);
                return;
            }

            // 2. 픽셀화된 셀 표시
            int gridW = numberGrid.GetLength(1);
            int gridH = numberGrid.GetLength(0);
            float scaleX = (float)picPreview.Width / gridW;
            float scaleY = (float)picPreview.Height / gridH;
            float cellSize = Math.Min(scaleX, scaleY);
            float totalW = cellSize * gridW;
            float totalH = cellSize * gridH;
            float offsetX2 = (picPreview.Width - totalW) / 2;
            float offsetY2 = (picPreview.Height - totalH) / 2;

            Font font = new Font("Arial", Math.Max(8, (int)cellSize / 2));
            Brush textBrush = Brushes.Black;

            for (int y = 0; y < gridH; y++)
            {
                for (int x = 0; x < gridW; x++)
                {
                    float left = offsetX2 + x * cellSize;
                    float top = offsetY2 + y * cellSize;
                    RectangleF cellRect = new RectangleF(left, top, cellSize, cellSize);
                    Point pt = new Point(x, y);

                    // 색칠된 셀은 색만 표시, 숫자는 생략
                    if (pixelColors.ContainsKey(pt))
                    {
                        using (Brush b = new SolidBrush(pixelColors[pt]))
                        {
                            g.FillRectangle(b, cellRect);
                        }
                    }
                    else
                    {
                        // 색칠 안 된 셀은 숫자 표시
                        int number = numberGrid[y, x];
                        string text = number.ToString();
                        g.DrawString(text, font, textBrush, left + 4, top + 4);
                    }

                    // 테두리
                    g.DrawRectangle(Pens.Gray, left, top, cellSize, cellSize);

                    // 선택된 셀 강조 (빨간 두꺼운 테두리)
                    if (selectedPoint.HasValue && selectedPoint.Value == pt)
                    {
                        using (Pen redPen = new Pen(Color.Red, 2))
                        {
                            g.DrawRectangle(redPen, left, top, cellSize, cellSize);
                        }
                    }
                }
            }
        }

        // 랜덤 색상 생성(색상 겹쳤을 시)
        private int RandomOffset(Random rnd)
        {
            // -12 ~ +12 (2.5% of 255 ≈ 6)
            return rnd.Next(-6, 7);
        }
        // byte 범위 내에서 색상 값 설정
        private int ClampColorComponent(int value)
        {
            return Math.Max(0, Math.Min(255, value));
        }



        private void picPreview_MouseClick(object sender, MouseEventArgs e)
        {
            if (numberGrid == null) return;

            int gridW = numberGrid.GetLength(1);
            int gridH = numberGrid.GetLength(0);
            float cellSize = Math.Min((float)picPreview.Width / gridW, (float)picPreview.Height / gridH);
            float totalW = cellSize * gridW;
            float totalH = cellSize * gridH;
            float offsetX = (picPreview.Width - totalW) / 2;
            float offsetY = (picPreview.Height - totalH) / 2;

            int x = (int)((e.X - offsetX) / cellSize);
            int y = (int)((e.Y - offsetY) / cellSize);

            if (x < 0 || y < 0 || x >= gridW || y >= gridH)
                return;

            Point clickedPoint = new Point(x, y);
            selectedPoint = clickedPoint;  // 선택된 셀 저장

            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    int targetNumber = numberGrid[y, x];
                    Color baseColor = dlg.Color;
                    Color newColor = baseColor;
                    Random rnd = new Random();

                    // 같은 색이 이미 있으면 유사한 다른 색으로 변경 (±6 정도 범위)
                    while (pixelColors.Values.Contains(newColor))
                    {
                        newColor = Color.FromArgb(
                            ClampColorComponent(baseColor.R + RandomOffset(rnd)),
                            ClampColorComponent(baseColor.G + RandomOffset(rnd)),
                            ClampColorComponent(baseColor.B + RandomOffset(rnd))
                        );
                    }

                    // 전체 셀 순회
                    for (int i = 0; i < gridH; i++)
                    {
                        for (int j = 0; j < gridW; j++)
                        {
                            Point pt = new Point(j, i);
                            if (numberGrid[i, j] == targetNumber)
                            {
                                pixelColors[pt] = newColor;
                                pixelatedImage.SetPixel(j, i, newColor);  // 색상 동기화
                            }
                        }
                    }

                }
            }

            picPreview.Invalidate();
        }


        // 마우스가 그리드 위에 있을 때 번호 표시
        private void picPreview_MouseMove(object sender, MouseEventArgs e)
        {
            if (numberGrid == null) return;

            int gridW = numberGrid.GetLength(1); //가로 셀 개수
            int gridH = numberGrid.GetLength(0); //세로 셀 개수

            // 그리드 크기 계산
            float cellSize = Math.Min((float)picPreview.Width / gridW, (float)picPreview.Height / gridH);
            float totalW = cellSize * gridW;
            float totalH = cellSize * gridH;
            float offsetX = (picPreview.Width - totalW) / 2;
            float offsetY = (picPreview.Height - totalH) / 2;

            // 마우스 좌표를 셀 좌표로 변환
            int x = (int)((e.X - offsetX) / cellSize);
            int y = (int)((e.Y - offsetY) / cellSize);

            // 범위 벗어나면 무시
            if (x < 0 || y < 0 || x >= gridW || y >= gridH) return;

            int number = numberGrid[y, x]; // 해당 셀의 색상 번호
            Point pt = new Point(x, y); // 좌표를 Point로 변환

            string tip = $"번호: {number}"; // 기본 툴팁 메시지

            // K-Means 색상
            Color? kmeansColor = null;
            //사용자 직접 칠한 색상
            Color? filledColor = null;

            // K-Means 색상 정보 가져오기
            if (numberToColor.TryGetValue(number, out Color kc))
            {
                kmeansColor = kc;
                tip += $"\nK-Means RGB: ({kc.R}, {kc.G}, {kc.B})";
            }

            // 사용자 색상 정보 가져오기
            if (pixelColors.TryGetValue(pt, out Color fc))
            {
                filledColor = fc;
                tip += $"\n사용자 색상 RGB: ({fc.R}, {fc.G}, {fc.B})";
            }

            // 두 색상 모두 존재하면 유사도 표시
            if (kmeansColor.HasValue && filledColor.HasValue)
            {
                int dr = Math.Abs(kmeansColor.Value.R - filledColor.Value.R);
                int dg = Math.Abs(kmeansColor.Value.G - filledColor.Value.G);
                int db = Math.Abs(kmeansColor.Value.B - filledColor.Value.B);

                double rSim = 100.0 - (dr / 255.0 * 100.0);
                double gSim = 100.0 - (dg / 255.0 * 100.0);
                double bSim = 100.0 - (db / 255.0 * 100.0);

                tip += $"\n유사도 (R/G/B): {rSim:F1}% / {gSim:F1}% / {bSim:F1}%";
            }

            toolTip1.SetToolTip(picPreview, tip);
        }

        // 버튼 클릭 시 K-Means 색상으로 전체 셀 자동 색칠
        private void btnColoringKmeans_Click(object sender, EventArgs e)
        {
            if (numberGrid == null || numberToColor == null)
            {
                MessageBox.Show("먼저 픽셀화를 해주세요.");
                return;
            }
            DialogResult result = MessageBox.Show("K-Means 색상으로 전체 픽셀을 색칠하시겠습니까?", "확인", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            

            int gridH = numberGrid.GetLength(0);
            int gridW = numberGrid.GetLength(1);
            pixelColors.Clear(); // 기존 색칠 초기화
            pixelatedImage = new Bitmap(numberGrid.GetLength(1), numberGrid.GetLength(0)); // 새로 생성
            // 색상 번호에 해당하는 색상으로 픽셀화된 이미지 채우기
            for (int y = 0; y < gridH; y++)
            {
                for (int x = 0; x < gridW; x++)
                {
                    int number = numberGrid[y, x];
                    if (numberToColor.TryGetValue(number, out Color color))
                    {
                        pixelColors[new Point(x, y)] = color;
                        pixelatedImage.SetPixel(x, y, color);  // 반영
                    }
                }
            }

            picPreview.Invalidate(); // 다시 그리기
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numKsize.Value = 8;
            numPixelSize.Value = 30;
            numKmeansIter.Value = 40;
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            //새 폼 생성
            var saveForm = new SaveKMeansForm(
                originalImage,
                pixelatedImage,
                pixelSize,
                numberGrid,
                numberToColor,
                pixelColors,
                k
            );

            saveForm.Show();
        }
    }
}
