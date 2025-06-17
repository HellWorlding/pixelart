using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using pixart;
using PixelColorling;
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

        // 스택 선언
        private Stack<CompoundAction> undoStack = new Stack<CompoundAction>();
        private Stack<CompoundAction> redoStack = new Stack<CompoundAction>();




        public KmeansColoring()
        {
            InitializeComponent();
        }

        private Color selectedCustomColor = Color.Black;



        private void btnLoad_Click(object sender, EventArgs e)
        {
            // ✅ 기존 도안이 존재하는 경우 사용자에게 확인
            if (numberGrid != null)
            {
                DialogResult overwrite = MessageBox.Show(
                    "이미 도안이 있습니다.\n이미지를 불러오시겠습니까?\n(기존 도안은 사라질 수 있습니다.)",
                    "도안 덮어쓰기 확인",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (overwrite == DialogResult.No)
                    return;

                // ✅ 저장 여부 추가 확인
                DialogResult saveConfirm = MessageBox.Show(
                    "기존 도안을 저장하시겠습니까?",
                    "도안 저장",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (saveConfirm == DialogResult.Yes)
                {
                    tsmiSaveGrid.PerformClick(); // 저장 메뉴 동작 실행
                }
            }

            // 🧹 기존 상태 완전 초기화
            originalImage?.Dispose();
            pixelatedImage?.Dispose();
            compareBitmap?.Dispose();

            originalImage = null;
            pixelatedImage = null;
            compareBitmap = null;
            numberGrid = null;
            pixelColors.Clear();
            selectedPoint = null;
            numberToColor.Clear();
            undoStack.Clear();
            redoStack.Clear();

            picOriginalThumb.Image = null;
            picPreview.Image = null;
            panelLegend.Controls.Clear();
            panelCompare.Invalidate();
            picPreview.Invalidate();

            // 이미지 선택 다이얼로그
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "이미지 파일|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "이미지 불러오기"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(ofd.FileName);

                // 썸네일 출력 (비율 유지하며 꽉 채움)
                picOriginalThumb.SizeMode = PictureBoxSizeMode.Zoom;
                picOriginalThumb.Image = originalImage;

                // 파라미터 초기화
                int imageWidth = originalImage.Width;
                numPixelSize.Minimum = 10;
                numPixelSize.Maximum = imageWidth / 2;

                numKsize.Minimum = 2;
                numKsize.Maximum = 100;
                numKsize.Value = 8;

                numKmeansIter.Minimum = 3;
                numKmeansIter.Maximum = 200;
                numKmeansIter.Value = 40;

                // 선택 색상 초기화
                selectedCustomColor = Color.Black;
                btnColorSelect.BackColor = selectedCustomColor;
            }
        }



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
                    /* if (pixelColors.ContainsKey(pt))
                    {
                        using (Brush b = new SolidBrush(pixelColors[pt]))
                        {
                            g.FillRectangle(b, cellRect);
                        }
                    }*/
                    if (pixelColors.TryGetValue(pt, out Color userColor))
                    {
                        using (Brush b = new SolidBrush(userColor))
                        {
                            g.FillRectangle(b, cellRect);
                        }

                    }
                    else
                    {
                        // 색칠 안 된 셀은 숫자 표시
                        int number = numberGrid[y, x];
                        string text = number.ToString();

                        // 번호에 해당하는 색상 얻기
                        if (numberToColor.TryGetValue(number, out Color color))
                        {
                            using (Brush numberBrush = new SolidBrush(color))
                            {
                                g.DrawString(text, font, numberBrush, left + 4, top + 4);
                            }
                        }
                        else
                        {
                            g.DrawString(text, font, Brushes.Black, left + 4, top + 4); // fallback
                        }

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

            // 새로운 작업을 시작하기 전에 redo 스택을 비움
            redoStack.Clear();

            // 마우스 클릭으로 발생하는 모든 변경을 담을 '작업 그룹'을 생성
            var compoundAction = new CompoundAction();

            if (paintPartition) // 전체 색칠 모드
            {
                for (int i = 0; i < gridH; i++)
                {
                    for (int j = 0; j < gridW; j++)
                    {
                        if (numberGrid[i, j] == targetNumber)
                        {
                            Point pt = new Point(j, i);
                            Color prevColor = pixelColors.TryGetValue(pt, out var c) ? c : Color.Transparent;

                            // 같은 색으로 칠하는 것은 작업으로 기록하지 않음
                            if (prevColor == newColor) continue;

                            // 개별 변경을 생성하여 작업 그룹에 추가합니다.
                            compoundAction.AddChange(new ColoringAction(j, i, prevColor, newColor));
                            pixelColors[pt] = newColor;
                            pixelatedImage.SetPixel(j, i, newColor);

                            /*
                            pixelColors[pt] = newColor;
                            pixelatedImage.SetPixel(j, i, newColor);
                            */
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
                            Color prevColor = pixelColors.TryGetValue(pt, out var c) ? c : Color.Transparent;

                            if (prevColor == newColor) continue;

                            // 개별 변경을 생성하여 작업 그룹에 추가
                            compoundAction.AddChange(new ColoringAction(nx, ny, prevColor, newColor));
                            pixelColors[pt] = newColor;
                            pixelatedImage.SetPixel(nx, ny, newColor);

                            /*
                            pixelColors[pt] = newColor;
                            pixelatedImage.SetPixel(nx, ny, newColor);
                            */
                        }
                    }
                }
            }

            if (compoundAction.Changes.Any())
            {
                undoStack.Push(compoundAction);
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

            // ✅ 전체 색칠 시 히스토리 초기화
            undoStack.Clear();
            redoStack.Clear();

            int gridH = numberGrid.GetLength(0);
            int gridW = numberGrid.GetLength(1);
            pixelColors.Clear(); // 기존 색칠 초기화
            pixelatedImage = new Bitmap(gridW, gridH); // 새로 생성

            for (int y = 0; y < gridH; y++)
            {
                for (int x = 0; x < gridW; x++)
                {
                    int number = numberGrid[y, x];
                    if (numberToColor.TryGetValue(number, out Color color))
                    {
                        pixelColors[new Point(x, y)] = color;
                        pixelatedImage.SetPixel(x, y, color);
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
            this.KeyPreview = true;


        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            var saveForm = new SaveForm(pixelatedImage, originalImage.Size);
            saveForm.Show();

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
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Grid Coloring Save File (*.gcsave)|*.gcsave"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (MemoryStream ms = new MemoryStream())
            {
                originalImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                var saveData = new GridSaveDataSimple
                {
                    ImageBytes = imageBytes,
                    PixelSize = pixelSize,
                    K = k,
                    Width = numberGrid.GetLength(1),
                    Height = numberGrid.GetLength(0),
                    FlatNumberGrid = numberGrid.Cast<int>().ToList(),
                    NumberToColor = numberToColor.Select(kv => ColorEntry.From(kv.Key, kv.Value)).ToList(),
                    PixelColors = pixelColors.Select(kv => CellEntry.From(kv.Key, kv.Value)).ToList()
                };

                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, saveData);
                }
            }
        }





        private void tsmiLoadGrid_Click(object sender, EventArgs e)
        {
            // 1️⃣ 기존 도안이 있는 경우 경고
            if (numberGrid != null)
            {
                DialogResult confirm = MessageBox.Show(
                    "이미 도안이 있습니다.\n도안을 불러오시겠습니까?\n(기존 도안은 사라질 수 있습니다.)",
                    "도안 덮어쓰기 확인",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.No)
                    return;

                // 2️⃣ 저장 여부 확인
                DialogResult saveConfirm = MessageBox.Show(
                    "기존 도안을 저장하시겠습니까?",
                    "도안 저장",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (saveConfirm == DialogResult.Yes)
                {
                    tsmiSaveGrid.PerformClick(); // 저장 메뉴 항목 실행
                }
            }

            // 3️⃣ Load 다이얼로그
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Grid Coloring Save File (*.gcsave)|*.gcsave"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    var loaded = (GridSaveDataSimple)bf.Deserialize(fs);

                    using (MemoryStream ms = new MemoryStream(loaded.ImageBytes))
                    {
                        originalImage = new Bitmap(ms);
                    }

                    pixelSize = loaded.PixelSize;
                    k = loaded.K;

                    numberGrid = new int[loaded.Height, loaded.Width];
                    for (int y = 0; y < loaded.Height; y++)
                        for (int x = 0; x < loaded.Width; x++)
                            numberGrid[y, x] = loaded.FlatNumberGrid[y * loaded.Width + x];

                    numberToColor = loaded.NumberToColor.ToDictionary(
                        entry => entry.Number, entry => entry.ToColor());

                    pixelColors = loaded.PixelColors.ToDictionary(
                        entry => entry.ToPoint(), entry => entry.ToColor());

                    // 이미지 초기화
                    pixelatedImage = new Bitmap(loaded.Width, loaded.Height);
                    foreach (var pair in pixelColors)
                    {
                        pixelatedImage.SetPixel(pair.Key.X, pair.Key.Y, pair.Value);
                    }

                    // UI 갱신
                    picOriginalThumb.Image = new Bitmap(originalImage, picOriginalThumb.Size);
                    DrawLegend();
                    picPreview.Invalidate();
                    UpdatePanelCompare(0, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("불러오기 중 오류 발생: " + ex.Message);
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

        private void tsbtnColorAll_Click(object sender, EventArgs e)
        {
            btnColoringKmeans.PerformClick();
        }

        private void tsButtonImageLoad_Click(object sender, EventArgs e)
        {
            btnLoad.PerformClick();
        }

        private void tsButtonGridDownload_Click(object sender, EventArgs e)
        {
            tsmiSaveGrid.PerformClick();
        }

        private void tsButtonGridLoad_Click(object sender, EventArgs e)
        {
            tsmiLoadGrid.PerformClick();
        }

        private void tsButtonImgSave_Click(object sender, EventArgs e)
        {
            btnSave.PerformClick();
        }

        private void KmeansColoring_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (numberGrid != null) // 도안이 있는 경우만 경고
            {
                DialogResult result = MessageBox.Show(
                    "도안을 저장하시겠습니까?",
                    "종료 전 저장",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    tsmiSaveGrid.PerformClick(); // 저장 호출
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // 닫기 취소
                }
                // 아니오 선택 시 그냥 닫힘
            }
        }

        private void tsbtnUndo_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                // 1. 마지막 '작업 그룹'을 undo 스택에서 꺼냄
                var compoundActionToUndo = undoStack.Pop();

                // 2. 이 작업 그룹을 redo 스택에 넣음
                redoStack.Push(compoundActionToUndo);

                // 3. 작업 그룹에 포함된 모든 픽셀 변경을 되돌림
                foreach (var action in compoundActionToUndo.Changes)
                {
                    Point pt = new Point(action.X, action.Y);
                    if (action.PreviousColor == Color.Transparent)
                    {
                        pixelColors.Remove(pt);
                        pixelatedImage.SetPixel(pt.X, pt.Y, Color.White);
                    }
                    else
                    {
                        pixelColors[pt] = action.PreviousColor;
                        pixelatedImage.SetPixel(pt.X, pt.Y, action.PreviousColor);
                    }
                }

                // 4. 모든 변경이 끝난 후 화면을 한 번만 새로 고침
                if (compoundActionToUndo.Changes.Any())
                {
                    var lastAction = compoundActionToUndo.Changes.First();
                    selectedPoint = new Point(lastAction.X, lastAction.Y);
                    UpdatePanelCompare(lastAction.X, lastAction.Y);
                }
                picPreview.Invalidate();
            }
        }

        private void tsbtnRedo_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                // 1. 취소했던 '작업 그룹'을 redo 스택에서 꺼냄
                var compoundActionToRedo = redoStack.Pop();

                // 2. 이 작업 그룹을 다시 undo 스택에 넣음
                undoStack.Push(compoundActionToRedo);

                // 3. 작업 그룹에 포함된 모든 픽셀 변경을 다시 실행함
                foreach (var action in compoundActionToRedo.Changes)
                {
                    Point pt = new Point(action.X, action.Y);
                    pixelColors[pt] = action.NewColor;
                    pixelatedImage.SetPixel(action.X, action.Y, action.NewColor);
                }

                // 4. 모든 변경이 끝난 후 화면을 한 번만 새로 고침
                if (compoundActionToRedo.Changes.Any())
                {
                    var lastAction = compoundActionToRedo.Changes.First();
                    selectedPoint = new Point(lastAction.X, lastAction.Y);
                    UpdatePanelCompare(lastAction.X, lastAction.Y);
                }
                picPreview.Invalidate();
            }
        }

        private void KmeansColoring_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                if (e.Shift)
                {
                    tsbtnRedo.PerformClick(); // Ctrl + Shift + Z → Redo
                }
                else
                {
                    tsbtnUndo.PerformClick(); // Ctrl + Z → Undo
                }

                e.Handled = true; // 이벤트 전파 방지
            }
        }

        private void tsmiColorAll_Click(object sender, EventArgs e)
        {
            btnColoringKmeans.PerformClick();
        }

        private void tsmiUndo_Click(object sender, EventArgs e)
        {
            tsbtnUndo.PerformClick();
        }

        private void tsmiRedo_Click(object sender, EventArgs e)
        {
            tsbtnRedo.PerformClick();
        }
    }
}

public class ColoringAction
{
    public int X, Y;
    public Color PreviousColor, NewColor;

    public ColoringAction(int x, int y, Color previousColor, Color newColor)
    {
        X = x;
        Y = y;
        PreviousColor = previousColor;
        NewColor = newColor;
    }
}


[Serializable]
public class GridSaveDataSimple
{
    public byte[] ImageBytes;
    public int PixelSize;
    public int K;
    public List<ColorEntry> NumberToColor;
    public int Width;
    public int Height;
    public List<int> FlatNumberGrid;
    public List<CellEntry> PixelColors;
}

[Serializable]
public class ColorEntry
{
    public int Number;
    public int R, G, B;

    public Color ToColor() => Color.FromArgb(R, G, B);
    public static ColorEntry From(int number, Color c) =>
        new ColorEntry { Number = number, R = c.R, G = c.G, B = c.B };
}

[Serializable]
public class CellEntry
{
    public int X, Y;
    public int R, G, B;

    public Point ToPoint() => new Point(X, Y);
    public Color ToColor() => Color.FromArgb(R, G, B);
    public static CellEntry From(Point pt, Color c) =>
        new CellEntry { X = pt.X, Y = pt.Y, R = c.R, G = c.G, B = c.B };
}

public class CompoundAction
{
    public List<ColoringAction> Changes { get; private set; }

    public CompoundAction()
    {
        Changes = new List<ColoringAction>();
    }

    public void AddChange(ColoringAction change)
    {
        Changes.Add(change);
    }
}


