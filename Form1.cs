using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pixel
{
    public partial class Form1 : Form
    {
        Bitmap originalImage;
        int pixelSize;
        int[,] numberGrid;
        Dictionary<Point, Color> pixelColors = new Dictionary<Point, Color>();
        Point? selectedPoint = null;

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

                int minSide = Math.Min(originalImage.Width, originalImage.Height);
                numPixelSize.Minimum = 2;
                numPixelSize.Maximum = minSide / 10;
                numPixelSize.Value = Math.Min(10, numPixelSize.Maximum);
            }
        }

        private void btnPixelate_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("이미지를 먼저 불러오세요.");
                return;
            }

            pixelSize = (int)numPixelSize.Value;
            int smallW = originalImage.Width / pixelSize;
            int smallH = originalImage.Height / pixelSize;

            Bitmap smallImage = new Bitmap(originalImage, new Size(smallW, smallH));

            int gridW = smallImage.Width;
            int gridH = smallImage.Height;
            numberGrid = new int[gridH, gridW];

            List<double[]> pixels = new List<double[]>();
            Color[,] colorGrid = new Color[gridH, gridW];

            for (int y = 0; y < gridH; y++)
            {
                for (int x = 0; x < gridW; x++)
                {
                    Color c = smallImage.GetPixel(x, y);
                    colorGrid[y, x] = c;
                    pixels.Add(new double[] { c.R, c.G, c.B });
                }
            }

            int k = 8; // 대표 색상 수 (원한다면 NumericUpDown으로 조절 가능)
            var centroids = RunKMeans(pixels, k);

            List<Color> representativeColors = centroids
                .Select(c => Color.FromArgb((int)c[0], (int)c[1], (int)c[2]))
                .ToList();

            Dictionary<Color, int> colorToNumber = new Dictionary<Color, int>();
            for (int i = 0; i < representativeColors.Count; i++)
                colorToNumber[representativeColors[i]] = i + 1;

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


        double ColorDistance(Color a, Color b)
        {
            return Math.Sqrt(
                Math.Pow(a.R - b.R, 2) +
                Math.Pow(a.G - b.G, 2) +
                Math.Pow(a.B - b.B, 2));
        }

        Color FindClosestColor(Color input, List<Color> palette)
        {
            Color closest = palette[0];
            double minDist = ColorDistance(input, closest);

            foreach (var color in palette)
            {
                double dist = ColorDistance(input, color);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = color;
                }
            }

            return closest;
        }

        List<double[]> RunKMeans(List<double[]> data, int k, int maxIterations = 10)
        {
            Random rand = new Random();
            var centroids = new List<double[]>();

            for (int i = 0; i < k; i++)
                centroids.Add(data[rand.Next(data.Count)]);

            for (int iter = 0; iter < maxIterations; iter++)
            {
                var clusters = new List<List<double[]>>();
                for (int i = 0; i < k; i++)
                    clusters.Add(new List<double[]>());

                foreach (var point in data)
                {
                    double minDist = double.MaxValue;
                    int bestCluster = 0;

                    for (int i = 0; i < k; i++)
                    {
                        double dist = Math.Sqrt(
                            Math.Pow(point[0] - centroids[i][0], 2) +
                            Math.Pow(point[1] - centroids[i][1], 2) +
                            Math.Pow(point[2] - centroids[i][2], 2));

                        if (dist < minDist)
                        {
                            minDist = dist;
                            bestCluster = i;
                        }
                    }

                    clusters[bestCluster].Add(point);
                }

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
                    if (pixelColors.ContainsKey(clickedPoint))
                        pixelColors.Remove(clickedPoint);  // 색칠 취소
                    else
                        pixelColors[clickedPoint] = dlg.Color;  //새 색상 적용
                }
            }

            picPreview.Invalidate();
        }

    }
}
///셀 혹시 잘못 칠했으면 다시 해당 셀 누르고 확인 누르면 기존 색 지워지고 셀이랑 원래 숫자가 뜸 
//k-means로 색상을 더 군집화해야할 듯 미세하게 다른 색도 걍 다른 색이라 인지해버려서 
