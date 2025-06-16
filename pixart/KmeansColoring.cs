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
using System.Xml;
using pixart;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace pixel
{
    public partial class KmeansColoring : Form
    {
        public enum ColorMode
        {
            RGB,
            HSV,
            OKLAB,
            YCbCr
        }

        private int paintSize = 1; // 1, 3, 5 중 하나
        private bool paintPartition = false; // 분할 칠하기 여부


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

        // 색상 모드 (RGB, HSV 등)
        private ColorMode currentMode = ColorMode.RGB;

        //비교용 중심점 (픽셀화된 이미지의 중심)
        private Point? compareCenter = null;

        // 비교용 비트맵
        private Bitmap compareBitmap = null;


        public KmeansColoring()
        {
            InitializeComponent();
        }

        private Color selectedCustomColor = Color.Black;



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

        //private void btnPixelate_Click(object sender, EventArgs e)
        //{
        //    if (originalImage == null)
        //    {
        //        MessageBox.Show("이미지를 먼저 불러오세요.");
        //        return;
        //    }

        //    // 픽셀 메시지 박스 확인(하던 작업 초기화 방지)
        //    DialogResult result = MessageBox.Show("픽셀화 하시겠습니까?", "확인", MessageBoxButtons.YesNo);
        //    if (result == DialogResult.No)
        //        return;

        //    // 이전 색칠 정보 초기화
        //    pixelColors.Clear();
        //    selectedPoint = null;

        //    // 이미지 축소
        //    int desiredGridW = (int)numPixelSize.Value;       // 사용자가 지정한 가로 셀 수
        //    pixelSize = originalImage.Width / desiredGridW;   // 자동 계산된 셀 크기
        //    int smallW = desiredGridW;                        // 줄일 이미지의 너비 = 셀 개수
        //    int smallH = originalImage.Height / pixelSize;    // 셀 크기로 나눈 줄일 이미지의 높이

        //    //pixelSize = (int)numPixelSize.Value;
        //    //int smallW = originalImage.Width / pixelSize;
        //    //int smallH = originalImage.Height / pixelSize;


        //    // 줄인 이미지
        //    Bitmap smallImage = new Bitmap(originalImage, new Size(smallW, smallH));
        //    pixelatedImage = new Bitmap(smallW, smallH);  // 새 비트맵 초기화

        //    int gridW = smallImage.Width;
        //    int gridH = smallImage.Height;
        //    // 색 번호 도안
        //    numberGrid = new int[gridH, gridW];
        //    // 축소 이미지 저장용
        //    Color[,] colorGrid = new Color[gridH, gridW];

        //    // K means 위한 픽셀 리스트
        //    List<double[]> pixels = new List<double[]>();

        //    for (int y = 0; y < gridH; y++)
        //    {
        //        for (int x = 0; x < gridW; x++)
        //        {
        //            Color c = smallImage.GetPixel(x, y); //x, y 색 추출
        //            colorGrid[y, x] = c; //축소 이미지 색 저장
        //            pixels.Add(new double[] { c.R, c.G, c.B }); //rgb 리스트에 추가
        //        }
        //    }

        //    // k means 클러스터링 색상들
        //    k = (int)numKsize.Value;
        //    List<double[]> centroids = RunKMeans(pixels, k);

        //    // 대표 색상 리스트
        //    List<Color> representativeColors = centroids
        //        .Select(c => Color.FromArgb((int)c[0], (int)c[1], (int)c[2]))
        //        .ToList();
        //    // k means로 구한 대표 색상으로 색상 번호 매핑 (도안 숫자)
        //    Dictionary<Color, int> colorToNumber = new Dictionary<Color, int>();
        //    for (int i = 0; i < representativeColors.Count; i++)
        //        colorToNumber[representativeColors[i]] = i + 1;

        //    // 색상 번호와 색상 매핑
        //    numberToColor = colorToNumber.ToDictionary(kv => kv.Value, kv => kv.Key);

        //    // 축소 이미지 클러스터링 번호 매핑
        //    for (int y = 0; y < gridH; y++)
        //    {
        //        for (int x = 0; x < gridW; x++)
        //        {
        //            Color original = colorGrid[y, x];
        //            Color closest = FindClosestColor(original, representativeColors);
        //            numberGrid[y, x] = colorToNumber[closest];
        //        }
        //    }

        //    // 썸네일 표시
        //    int thumbWidth = picOriginalThumb.Width;
        //    int thumbHeight = picOriginalThumb.Height;
        //    Bitmap thumbImage = new Bitmap(originalImage, new Size(thumbWidth, thumbHeight));
        //    picOriginalThumb.Image = thumbImage;

        //    // 색상 가이드 표시 
        //    panelLegend.Controls.Clear();


        //    foreach (var pair in colorToNumber.OrderBy(p => p.Value))
        //    {
        //        // 색상과 번호 쌍을 패널에 추가
        //        Panel swatch = new Panel();
        //        swatch.BackColor = pair.Key;
        //        swatch.Size = new Size(20, 20);
        //        swatch.Margin = new Padding(2);

        //        Label label = new Label();
        //        label.Text = pair.Value.ToString();
        //        label.AutoSize = true;
        //        label.TextAlign = ContentAlignment.MiddleCenter;
        //        label.Padding = new Padding(0);

        //        FlowLayoutPanel itemPanel = new FlowLayoutPanel();
        //        itemPanel.FlowDirection = FlowDirection.TopDown;  //  세로 정렬
        //        itemPanel.WrapContents = false;
        //        itemPanel.Size = new Size(40, 40);  // 너비 제한

        //        itemPanel.Controls.Add(swatch);
        //        itemPanel.Controls.Add(label);

        //        panelLegend.Controls.Add(itemPanel);
        //    }

        //    // 이미지 새로고침
        //    picPreview.Image = null;
        //    picPreview.Invalidate();
        //}
        private void btnPixelate_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("이미지를 먼저 불러오세요.");
                return;
            }

            DialogResult result = MessageBox.Show("픽셀화 하시겠습니까?", "확인", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;

            pixelColors.Clear();
            selectedPoint = null;

            int desiredGridW = (int)numPixelSize.Value;
            pixelSize = originalImage.Width / desiredGridW;
            int smallW = desiredGridW;
            int smallH = originalImage.Height / pixelSize;

            Bitmap smallImage = new Bitmap(originalImage, new Size(smallW, smallH));
            pixelatedImage = new Bitmap(smallW, smallH);
            int gridW = smallImage.Width;
            int gridH = smallImage.Height;

            numberGrid = new int[gridH, gridW];
            Color[,] colorGrid = new Color[gridH, gridW];
            List<Color> colorList = new List<Color>();

            for (int y = 0; y < gridH; y++)
            {
                for (int x = 0; x < gridW; x++)
                {
                    Color c = smallImage.GetPixel(x, y);
                    colorGrid[y, x] = c;
                    colorList.Add(c);
                }
            }

            k = (int)numKsize.Value;
            int maxIter = (int)numKmeansIter.Value;

            List<Color> representativeColors = RunKMeans(colorList, k, currentMode, maxIter);

            Dictionary<Color, int> colorToNumber = new Dictionary<Color, int>();
            for (int i = 0; i < representativeColors.Count; i++)
                colorToNumber[representativeColors[i]] = i + 1;

            numberToColor = colorToNumber.ToDictionary(kv => kv.Value, kv => kv.Key);

            for (int y = 0; y < gridH; y++)
            {
                for (int x = 0; x < gridW; x++)
                {
                    Color original = colorGrid[y, x];
                    Color closest = FindClosestColor(original, representativeColors, currentMode);
                    numberGrid[y, x] = colorToNumber[closest];
                }
            }

            int thumbWidth = picOriginalThumb.Width;
            int thumbHeight = picOriginalThumb.Height;
            Bitmap thumbImage = new Bitmap(originalImage, new Size(thumbWidth, thumbHeight));
            picOriginalThumb.Image = thumbImage;

            panelLegend.Controls.Clear();

            foreach (var pair in colorToNumber.OrderBy(p => p.Value))
            {
                Color color = pair.Key;
                int number = pair.Value;

                Panel swatch = new Panel
                {
                    BackColor = color,
                    Size = new Size(20, 20),
                    Margin = new Padding(2),
                    Tag = color,
                    Cursor = Cursors.Hand
                };
                swatch.Click += LegendColor_Click;

                Label label = new Label
                {
                    Text = number.ToString(),
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Padding = new Padding(0)
                };

                FlowLayoutPanel itemPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    Size = new Size(40, 40)
                };

                itemPanel.Controls.Add(swatch);
                itemPanel.Controls.Add(label);

                panelLegend.Controls.Add(itemPanel);
            }

            picPreview.Image = null;
            picPreview.Invalidate();
        }

        private void LegendColor_Click(object sender, EventArgs e)
        {
            if (sender is Panel panel && panel.Tag is Color color)
            {
                selectedCustomColor = color;
                btnColorSelect.BackColor = color;
            }
        }

        double ColorDistanceSquared(Color a, Color b, ColorMode mode)
        {
            switch (mode)
            {
                case ColorMode.RGB:
                    return
                        Math.Pow((a.R - b.R) / 255.0, 2) +
                        Math.Pow((a.G - b.G) / 255.0, 2) +
                        Math.Pow((a.B - b.B) / 255.0, 2);

                case ColorMode.HSV:
                    var hsv1 = RGBtoHSV(a);
                    var hsv2 = RGBtoHSV(b);
                    double dh = (hsv1[0] - hsv2[0]) / 360.0;
                    double ds = hsv1[1] - hsv2[1]; // already 0–1
                    double dv = hsv1[2] - hsv2[2];
                    return dh * dh + ds * ds + dv * dv;

                case ColorMode.OKLAB:
                    var lab1 = RgbToOklab(a);
                    var lab2 = RgbToOklab(b);
                    return Math.Pow(lab1[0] - lab2[0], 2) + Math.Pow(lab1[1] - lab2[1], 2) + Math.Pow(lab1[2] - lab2[2], 2);

                case ColorMode.YCbCr:
                    var ycc1 = RGBtoYCbCr(a);
                    var ycc2 = RGBtoYCbCr(b);
                    double dy = (ycc1[0] - ycc2[0]) / 219.0; // 235-16
                    double dcb = (ycc1[1] - ycc2[1]) / 224.0; // 240-16
                    double dcr = (ycc1[2] - ycc2[2]) / 224.0;
                    return dy * dy + dcb * dcb + dcr * dcr;

                default:
                    throw new ArgumentException("Unknown color mode");
            }
        }


        double ColorDistanceSquared(Color a, Color b)
        {
            return
                Math.Pow((int)a.R - b.R, 2) +
                Math.Pow((int)a.G - b.G, 2) +
                Math.Pow((int)a.B - b.B, 2);
        }

        Color FindClosestColor(Color input, List<Color> palette, ColorMode mode)
        {
            Color closest = palette[0];
            double minDist = ColorDistanceSquared(input, closest, mode);

            foreach (var color in palette)
            {
                double dist = ColorDistanceSquared(input, color, mode);
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

        private List<Color> RunKMeans(List<Color> colors, int k, ColorMode mode, int maxIterations = 40)
        {
            // 1. 색상 벡터로 변환 (선택한 색상 모드에 따라)
            List<double[]> vectors = new List<double[]>();

            foreach (var c in colors)
            {
                double[] vec;
                if (mode == ColorMode.RGB)
                {
                    vec = new double[] { c.R, c.G, c.B };
                }
                else if (mode == ColorMode.HSV)
                {
                    vec = RGBtoHSV(c);
                }
                else if (mode == ColorMode.OKLAB)
                {
                    vec = RgbToOklab(c);
                }
                else if (mode == ColorMode.YCbCr)
                {
                    vec = RGBtoYCbCr(c);
                }
                else
                {
                    throw new ArgumentException("Unknown color mode");
                }

                vectors.Add(vec);
            }


            // 2. 클러스터링
            List<double[]> centroids = RunKMeans(vectors, k, maxIterations);

            // 3. 결과를 RGB로 다시 변환
            List<Color> result = new List<Color>();

            foreach (var centroid in centroids)
            {
                Color c;

                if (mode == ColorMode.RGB)
                {
                    c = Color.FromArgb(
                        ClampColorComponent((int)centroid[0]),
                        ClampColorComponent((int)centroid[1]),
                        ClampColorComponent((int)centroid[2]));
                }
                else if (mode == ColorMode.HSV)
                {
                    c = HSVtoRGB(centroid);
                }
                else if (mode == ColorMode.OKLAB)
                {
                    c = OklabToRGB(centroid);
                }
                else if (mode == ColorMode.YCbCr)
                {
                    c = YCbCrToRGB(centroid);
                }
                else
                {
                    c = Color.Black;
                }

                result.Add(c);
            }


            return result;
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

            selectedPoint = new Point(x, y);
            int targetNumber = numberGrid[y, x];

            // ✅ 고정된 사용자 색상 사용
            Color newColor = selectedCustomColor;

            if (paintPartition) // 전체 색칠 모드
            {
                for (int i = 0; i < gridH; i++)
                {
                    for (int j = 0; j < gridW; j++)
                    {
                        if (numberGrid[i, j] == targetNumber)
                        {
                            Point pt = new Point(j, i);
                            pixelColors[pt] = newColor;
                            pixelatedImage.SetPixel(j, i, newColor);
                        }
                    }
                }
            }
            else // paintSize 기반 칠하기
            {
                int half = paintSize / 2;
                for (int dy = -half; dy <= half; dy++)
                {
                    for (int dx = -half; dx <= half; dx++)
                    {
                        int nx = x + dx;
                        int ny = y + dy;

                        if (nx >= 0 && nx < gridW && ny >= 0 && ny < gridH)
                        {
                            Point pt = new Point(nx, ny);
                            pixelColors[pt] = newColor;
                            pixelatedImage.SetPixel(nx, ny, newColor);
                        }
                    }
                }
            }

            picPreview.Invalidate();
            UpdatePanelCompare(x, y);

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
            //cbxMode.Items.AddRange(new string[] { "RGB", "HSV", "OKLAB", "YCbCr" });
            panel1.Visible = false; // 시작 시 팬 두깨 숨김
            cbxMode.SelectedIndex = 0; // 기본 RGB
            currentMode = ColorMode.RGB;

            numKsize.Value = 8;
            numPixelSize.Value = 30;
            numKmeansIter.Value = 40;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            var saveForm = new SaveForm(pixelatedImage, originalImage.Size);
            saveForm.Show();

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void cbxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentMode = (ColorMode)cbxMode.SelectedIndex;
        }

        // 변환 유틸리티 함수들 추가
        private double[] RGBtoHSV(Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;
            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));
            double h, s, v = max;

            double delta = max - min;
            s = max == 0 ? 0 : delta / max;

            if (delta == 0) h = 0;
            else if (max == r) h = (g - b) / delta % 6;
            else if (max == g) h = (b - r) / delta + 2;
            else h = (r - g) / delta + 4;

            h *= 60;
            if (h < 0) h += 360;

            return new double[] { h, s, v };
        }

        private Color HSVtoRGB(double[] hsv)
        {
            double h = hsv[0], s = hsv[1], v = hsv[2];
            double c = v * s;
            double x = c * (1 - Math.Abs(h / 60 % 2 - 1));
            double m = v - c;

            double r = 0, g = 0, b = 0;
            if (h < 60) { r = c; g = x; }
            else if (h < 120) { r = x; g = c; }
            else if (h < 180) { g = c; b = x; }
            else if (h < 240) { g = x; b = c; }
            else if (h < 300) { r = x; b = c; }
            else { r = c; b = x; }

            return Color.FromArgb(
                ClampColorComponent((int)((r + m) * 255)),
                ClampColorComponent((int)((g + m) * 255)),
                ClampColorComponent((int)((b + m) * 255))
            );
        }

        private double[] RGBtoYCbCr(Color c)
        {
            double r = c.R, g = c.G, b = c.B;
            double y = 0.299 * r + 0.587 * g + 0.114 * b;
            double cb = 128 - 0.168736 * r - 0.331264 * g + 0.5 * b;
            double cr = 128 + 0.5 * r - 0.418688 * g - 0.081312 * b;
            return new double[] { y, cb, cr };
        }

        private Color YCbCrToRGB(double[] ycbcr)
        {
            double y = ycbcr[0], cb = ycbcr[1] - 128, cr = ycbcr[2] - 128;
            double r = y + 1.402 * cr;
            double g = y - 0.344136 * cb - 0.714136 * cr;
            double b = y + 1.772 * cb;
            return Color.FromArgb(
                ClampColorComponent((int)r),
                ClampColorComponent((int)g),
                ClampColorComponent((int)b)
            );
        }

        public static double[] RgbToOklab(Color color)
        {
            // 1. sRGB → linear RGB
            double r = SrgbToLinear(color.R / 255.0);
            double g = SrgbToLinear(color.G / 255.0);
            double b = SrgbToLinear(color.B / 255.0);

            // 2. linear RGB → LMS
            double l = 0.4122214708 * r + 0.5363325363 * g + 0.0514459929 * b;
            double m = 0.2119034982 * r + 0.6806995451 * g + 0.1073969566 * b;
            double s = 0.0883024619 * r + 0.2817188376 * g + 0.6299787005 * b;

            // 3. LMS → cube root
            double l_ = Math.Pow(l, 1.0 / 3);
            double m_ = Math.Pow(m, 1.0 / 3);
            double s_ = Math.Pow(s, 1.0 / 3);

            // 4. LMS → OKLAB
            double L = 0.2104542553 * l_ + 0.7936177850 * m_ - 0.0040720468 * s_;
            double a = 1.9779984951 * l_ - 2.4285922050 * m_ + 0.4505937099 * s_;
            double b_ = 0.0259040371 * l_ + 0.7827717662 * m_ - 0.8086757660 * s_;

            return new double[] { L, a, b_ };
        }

        private static double SrgbToLinear(double c)
        {
            return c <= 0.04045 ? c / 12.92 : Math.Pow((c + 0.055) / 1.055, 2.4);
        }


        public static Color OklabToRGB(double[] lab)
        {
            double L = lab[0];
            double a = lab[1];
            double b = lab[2];

            // 1. OKLAB → LMS cube roots
            double l_ = L + 0.3963377774 * a + 0.2158037573 * b;
            double m_ = L - 0.1055613458 * a - 0.0638541728 * b;
            double s_ = L - 0.0894841775 * a - 1.2914855480 * b;

            // 2. LMS → linear RGB
            double l = l_ * l_ * l_;
            double m = m_ * m_ * m_;
            double s = s_ * s_ * s_;

            double r = +4.0767416621 * l - 3.3077115913 * m + 0.2309699292 * s;
            double g = -1.2684380046 * l + 2.6097574011 * m - 0.3413193965 * s;
            double b_ = -0.0041960863 * l - 0.7034186147 * m + 1.7076147010 * s;

            return Color.FromArgb(
                ClampToByte(LinearToSrgb(r)),
                ClampToByte(LinearToSrgb(g)),
                ClampToByte(LinearToSrgb(b_))
            );
        }

        private static double LinearToSrgb(double c)
        {
            return c <= 0.0031308 ? c * 12.92 : 1.055 * Math.Pow(c, 1.0 / 2.4) - 0.055;
        }

        private static int ClampToByte(double value)
        {
            return Math.Min(255, Math.Max(0, (int)Math.Round(value * 255.0)));
        }


        public static double CubeRoot(double x)
        {
            return Math.Pow(x, 1.0 / 3.0);
        }

        private void btnColorSelect_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    selectedCustomColor = dlg.Color;
                    // 사용자에게 미리 보기로 선택 색상 보여주기 (선택사항)
                    btnColorSelect.BackColor = selectedCustomColor;
                }
            }
        }

        private void btnSize_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }

        private void btnSize1_Click(object sender, EventArgs e)
        {
            paintSize = 1;
            paintPartition = false;
            panel1.Visible = false;
        }

        private void btnSize3_Click(object sender, EventArgs e)
        {
            paintSize = 3;
            paintPartition = false;
            panel1.Visible = false;
        }

        private void btnSize5_Click(object sender, EventArgs e)
        {
            paintSize = 5;
            paintPartition = false;
            panel1.Visible = false;
        }

        private void btnColorPartition_Click(object sender, EventArgs e)
        {
            paintPartition = true;
            paintSize = 1;
            panel1.Visible = false;
        }


        private void UpdatePanelCompare(int centerX, int centerY)
        {
            compareCenter = new Point(centerX, centerY);
            panelCompare.Invalidate();  // 다시 그리기 유도
        }

        private void panelCompare_Paint(object sender, PaintEventArgs e)
        {
            if (numberGrid == null || numberToColor == null || compareCenter == null)
                return;

            Graphics g = e.Graphics;
            g.Clear(Color.White);

            int gridW = numberGrid.GetLength(1);
            int gridH = numberGrid.GetLength(0);
            int radius = paintSize / 2;

            int availableW = panelCompare.Width - 40;
            int availableH = panelCompare.Height - 40;
            int cellSize = Math.Min(availableW / (paintSize * 2 + 1), availableH / paintSize);

            int totalWidth = (paintSize * 2 + 1) * cellSize;
            int totalHeight = paintSize * cellSize;
            int offsetX = (panelCompare.Width - totalWidth) / 2;
            int offsetY = (panelCompare.Height - totalHeight) / 2;

            Font font = new Font("Arial", 8);
            Brush labelBrush = Brushes.Black;

            Point pt = compareCenter.Value;

            // ✅ Brush 캐싱
            Dictionary<Color, SolidBrush> brushCache = new Dictionary<Color, SolidBrush>();
            HatchBrush defaultHatchBrush = new HatchBrush(HatchStyle.LargeGrid, Color.LightGray, Color.White);

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
                        offsetX + (paintSize + 1 + lx) * cellSize,
                        offsetY + ly * cellSize,
                        cellSize, cellSize);

                    if (x >= 0 && x < gridW && y >= 0 && y < gridH)
                    {
                        int number = numberGrid[y, x];
                        Color originalColor = numberToColor.TryGetValue(number, out var oc) ? oc : Color.Gray;

                        // 🔄 브러시 캐시 재사용
                        if (!brushCache.TryGetValue(originalColor, out var brushOriginal))
                        {
                            brushOriginal = new SolidBrush(originalColor);
                            brushCache[originalColor] = brushOriginal;
                        }

                        g.FillRectangle(brushOriginal, rectOriginal);
                        g.DrawRectangle(Pens.Black, rectOriginal);

                        Point px = new Point(x, y);
                        if (pixelColors.TryGetValue(px, out var userColor))
                        {
                            if (!brushCache.TryGetValue(userColor, out var brushUser))
                            {
                                brushUser = new SolidBrush(userColor);
                                brushCache[userColor] = brushUser;
                            }
                            g.FillRectangle(brushUser, rectFilled);
                        }
                        else
                        {
                            g.FillRectangle(defaultHatchBrush, rectFilled);
                        }

                        g.DrawRectangle(Pens.Black, rectFilled);
                    }
                }
            }

            // 라벨
            g.DrawString("Original", font, labelBrush,
                offsetX + (paintSize * cellSize - 40) / 2,
                offsetY + paintSize * cellSize + 5);
            g.DrawString("Colored", font, labelBrush,
                offsetX + ((paintSize + 1) * cellSize) + (paintSize * cellSize - 40) / 2,
                offsetY + paintSize * cellSize + 5);

            font.Dispose();

            // 🧹 자원 해제
            foreach (var b in brushCache.Values) b.Dispose();
            defaultHatchBrush.Dispose();
        }

        private void tsmiImageLoad_Click(object sender, EventArgs e)
        {
            btnLoad.PerformClick();
        }

        private void tsmiImgSave_Click(object sender, EventArgs e)
        {
            btnSave.PerformClick();
        }

        private void tsmiGenerate_Click(object sender, EventArgs e)
        {
            btnPixelate.PerformClick();
        }

        private void tsmiPickPaletteColor_Click(object sender, EventArgs e)
        {
            btnColorSelect.PerformClick();
        }

        private void tsmiThick1x1_Click(object sender, EventArgs e)
        {
            paintPartition = false;
            paintSize = 1;
            panel1.Visible = false;
        }

        private void tsmiThick3x3_Click(object sender, EventArgs e)
        {
            paintPartition = false;
            paintSize = 3;
            panel1.Visible = false;
        }

        private void tsmiThick5x5_Click(object sender, EventArgs e)
        {
            paintPartition = false;
            paintSize = 5;
            panel1.Visible = false;
        }

        private void tsmiThickPartition_Click(object sender, EventArgs e)
        {
            paintPartition = true;
            paintSize = 1; // 부분 색칠 모드에서는 기본 굵기 사용
            panel1.Visible = false;
        }

        private void tsmiSaveGrid_Click(object sender, EventArgs e)
        {
            if (originalImage == null || numberGrid == null || numberToColor == null)
            {
                MessageBox.Show("저장할 도안이 없습니다.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Grid Coloring Save File (*.gcsave)|*.gcsave";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (BinaryWriter writer = new BinaryWriter(File.Open(sfd.FileName, FileMode.Create)))
            {
                // 1. 원본 이미지 저장
                using (MemoryStream ms = new MemoryStream())
                {
                    originalImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();
                    writer.Write(imageBytes.Length);
                    writer.Write(imageBytes);
                }

                // 2. 대표 색상 수 (K)
                writer.Write(numberToColor.Count);

                // 3. 색상 번호와 RGB 저장
                foreach (var pair in numberToColor.OrderBy(p => p.Key))
                {
                    writer.Write(pair.Key);
                    writer.Write(pair.Value.R);
                    writer.Write(pair.Value.G);
                    writer.Write(pair.Value.B);
                }

                // 4. numberGrid (행, 열, 데이터)
                int h = numberGrid.GetLength(0);
                int w = numberGrid.GetLength(1);
                writer.Write(h);
                writer.Write(w);
                for (int y = 0; y < h; y++)
                    for (int x = 0; x < w; x++)
                        writer.Write(numberGrid[y, x]);

                // 5. 사용자 색칠 데이터
                writer.Write(pixelColors.Count);
                foreach (var pair in pixelColors)
                {
                    writer.Write(pair.Key.X);
                    writer.Write(pair.Key.Y);
                    writer.Write(pair.Value.R);
                    writer.Write(pair.Value.G);
                    writer.Write(pair.Value.B);
                }
            }
        }


        private void tsmiLoadGrid_Click(object sender, EventArgs e)
        {
            // 🌟 기존 상태 안전 초기화
            originalImage?.Dispose();
            pixelatedImage?.Dispose();
            numberGrid = null;
            pixelColors.Clear();
            selectedPoint = null;
            compareCenter = null;
            numberToColor.Clear();
            panelLegend.Controls.Clear();
            picOriginalThumb.Image = null;
            picPreview.Image = null;

            // 기존처럼 파일 열기
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Grid Coloring Save File (*.gcsave)|*.gcsave"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            using (BinaryReader reader = new BinaryReader(File.Open(ofd.FileName, FileMode.Open)))
            {
                // 1. 이미지 복원
                int imageLength = reader.ReadInt32();
                byte[] imageBytes = reader.ReadBytes(imageLength);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    originalImage = new Bitmap(ms);
                }

                // 2. 픽셀 크기, 색상 수 복원
                pixelSize = reader.ReadInt32();
                k = reader.ReadInt32();

                // 3. 색상 매핑 복원
                int colorCount = reader.ReadInt32();
                numberToColor = new Dictionary<int, Color>();
                for (int i = 0; i < colorCount; i++)
                {
                    int key = reader.ReadInt32();
                    int r = reader.ReadByte();
                    int g = reader.ReadByte();
                    int b = reader.ReadByte();
                    numberToColor[key] = Color.FromArgb(r, g, b);
                }

                // 4. 그리드 복원
                int h = reader.ReadInt32();
                int w = reader.ReadInt32();
                numberGrid = new int[h, w];
                for (int y = 0; y < h; y++)
                    for (int x = 0; x < w; x++)
                        numberGrid[y, x] = reader.ReadInt32();

                // 5. 사용자 색칠 복원
                int filledCount = reader.ReadInt32();
                pixelColors = new Dictionary<Point, Color>();
                for (int i = 0; i < filledCount; i++)
                {
                    int x = reader.ReadInt32();
                    int y = reader.ReadInt32();
                    int r = reader.ReadByte();
                    int g = reader.ReadByte();
                    int b = reader.ReadByte();
                    pixelColors[new Point(x, y)] = Color.FromArgb(r, g, b);
                }

                // 6. 미리보기 이미지 재생성
                pixelatedImage = new Bitmap(w, h);
                foreach (var pair in pixelColors)
                {
                    pixelatedImage.SetPixel(pair.Key.X, pair.Key.Y, pair.Value);
                }

                // 7. 썸네일 이미지 표시
                picOriginalThumb.Image = new Bitmap(originalImage, picOriginalThumb.Size);

                // 8. 색상 범례 다시 생성
                DrawLegend();

                // 9. 그리드와 비교 화면 갱신
                picPreview.Invalidate();
                UpdatePanelCompare(0, 0);
            }
        }

        private void DrawLegend()
        {
            panelLegend.Controls.Clear();

            foreach (var pair in numberToColor.OrderBy(p => p.Key))
            {
                Color color = pair.Value;
                int number = pair.Key;

                Panel swatch = new Panel
                {
                    BackColor = color,
                    Size = new Size(20, 20),
                    Margin = new Padding(2),
                    Tag = color,
                    Cursor = Cursors.Hand
                };
                swatch.Click += LegendColor_Click;

                Label label = new Label
                {
                    Text = number.ToString(),
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Padding = new Padding(0)
                };

                FlowLayoutPanel itemPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    Size = new Size(40, 40)
                };

                itemPanel.Controls.Add(swatch);
                itemPanel.Controls.Add(label);
                panelLegend.Controls.Add(itemPanel);
            }
        }



    }
}

[Serializable]
public class GridSaveDataSimple
{
    public byte[] ImageBytes;
    public int PixelSize;
    public int K;
    public Dictionary<int, Color> NumberToColor;
    public int[,] NumberGrid;
    public Dictionary<Point, Color> PixelColors;
}

