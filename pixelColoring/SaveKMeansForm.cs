using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pixel
{
    public partial class SaveKMeansForm: Form
    {
        private Bitmap originalImage;
        private Bitmap pixelatedImage;
        private int pixelSize;
        private int[,] numberGrid;
        private Dictionary<int, Color> numberToColor;
        private Dictionary<Point, Color> pixelColors;
        private int k;
        enum Direction
        {
            None = 0,           // x
            Horizontal = 1,     // -
            Vertical = 2,       // |
            DiagonalDown = 3,   // \\
            DiagonalUp = 4      // /
        }
        // 원래 이미지 크기로 바꾸기
        private Bitmap resizedImage;

        // 특수기호로 표현, 200개, 현재 k 최대값은 100
        private static readonly char[] allSymbols = new char[]
        {
            '★','☆','○','●','◎','◇','◆','□','■','△',
            '▲','▽','▼','※','♠','♣','♥','♡','♦','♢',
            '♬','♪','∞','◈','▦','▧','▨','▩','◐','◑',
            '◒','◓','◔','◕','◖','◗','◘','◙','◚','◛',
            '◜','◝','◞','◟','■','▣','▤','▥','▦','▧',
            '▨','▩','▰','▱','▮','▯','▭','▮','▬','▲',
            '#', '*', '@', '%', '&', '$', '+', '=', '~',
            '^', '`', '-', '_', '|', '\\', '/', ':', ';',
            '.', ',', '\'', '"', '(', ')', '[', ']', '{', '}',
            '<', '>', '¤', 'µ', '©', '®', '℗', '™', '√', 'π',
            '‣','•','‥','…','‰','‱','′','″','‵','‶',
            '‷','‸','‹','›','※','⁂','⁃','⁇','⁈','⁉',
            '⁎','⁑','⁕','⁘','⁙','⁚','⁛','⁜','⁝','⁞',
            '⟀','⟁','⟡','⟣','⟤','⟥','⟦','⟧','⟨','⟩',
            '⟪','⟫','⟬','⟭','⟮','⟯','⦀','⦁','⦂','⦃',
            '⦄','⦅','⦆','⦇','⦈','⦉','⦊','⦋','⦌','⦍',
            '⦎','⦏','⦐','⦑','⦒','⦓','⦔','⦕','⦖','⦗',
            '⦘','⧈','⧉','⧊','⧋','⧌','⧍','⧎','⧏','⧐',
            '⧑','⧒','⧓','⧔','⧕','⧖','⧗','⧘','⧙','⧚',
            '⧛','⧜','⧝','⧞','⧟','⧠','⧡','⧢','⧣','⧤'
        };
        Dictionary<int, char> clusterSymbolMap = new Dictionary<int, char>();

        int cnt = 0;
        double avgxx = 0, avgyy = 0, avgdd1 = 0, avgdd2 = 0;

        public SaveKMeansForm(Bitmap original, Bitmap pixelated, int pixelSize,
        int[,] numberGrid,
        Dictionary<int, Color> numberToColor,
        Dictionary<Point, Color> pixelColors,
        int k)
        {
            InitializeComponent();

            this.originalImage = original;
            this.pixelatedImage = pixelated;
            this.pixelSize = pixelSize;
            this.numberGrid = numberGrid;
            this.numberToColor = numberToColor;
            this.pixelColors = pixelColors;
            this.k = k;

            // clusterSymbolMap 초기화
            clusterSymbolMap = GenerateRandomSymbolMap(k);

            cmbSaveForm.SelectedIndex = 0;
            cmbSaveForm.SelectedIndexChanged += cmbSaveForm_SelectedIndexChanged;

            UpdatePreview();
        }
        private void cmbSaveForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }
        private void UpdatePreview()
        {
            string mode = cmbSaveForm.SelectedItem?.ToString();
            if (mode == "단순 비트맵 저장")
            {
                resizedImage = new Bitmap(originalImage.Width, originalImage.Height);
                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.DrawImage(pixelatedImage,
                        new Rectangle(0, 0, resizedImage.Width, resizedImage.Height),
                        new Rectangle(0, 0, pixelatedImage.Width, pixelatedImage.Height),
                        GraphicsUnit.Pixel);
                }

                // 미리보기 이미지 설정
                picSavePreview.Image = resizedImage;
            }
            else if (mode == "색상 기반 기호 표현")
            {
                int w = numberGrid.GetLength(1);
                int h = numberGrid.GetLength(0);
                int cellSize = 20;

                resizedImage = new Bitmap(w * cellSize, h * cellSize);

                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.Clear(Color.White);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                    Font font = new Font("Arial", cellSize * 0.6f, FontStyle.Bold);
                    StringFormat format = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    for (int y = 0; y < h; y++)
                    {
                        for (int x = 0; x < w; x++)
                        {
                            Point pt = new Point(x, y);

                            // 사용자가 색칠한 셀만 기호로 표시
                            if (pixelColors.TryGetValue(pt, out Color customColor))
                            {
                                int number = numberGrid[y, x];
                                Rectangle cellRect = new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize);

                                if (clusterSymbolMap.TryGetValue(number, out char symbol))
                                {
                                    using (Brush textBrush = new SolidBrush(customColor))
                                    {
                                        g.DrawString(symbol.ToString(), font, textBrush, cellRect, format);
                                    }
                                }
                            }
                            // 그렇지 않으면 아무 것도 표시하지 않음
                        }
                    }
                }

                picSavePreview.Image = resizedImage;
                picSavePreview.Invalidate();
            }
            else if (mode == "방향 기반 기호 표현")
            {
                int w = numberGrid.GetLength(1);
                int h = numberGrid.GetLength(0);
                int cellSize = 20;

                Direction[,] directionMap = AnalyzeDirections(originalImage, w, h);

                resizedImage = new Bitmap(w * cellSize, h * cellSize);
                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.Clear(Color.White);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                    Font font = new Font("Arial", cellSize * 0.6f, FontStyle.Bold);
                    StringFormat format = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    for (int y = 0; y < h; y++)
                    {
                        for (int x = 0; x < w; x++)
                        {
                            Point pt = new Point(x, y);

                            // 사용자 색칠 셀만 표시
                            if (pixelColors.TryGetValue(pt, out Color customColor))
                            {
                                Direction dir = directionMap[y, x];
                                char symbol;

                                switch (dir)
                                {
                                    case Direction.Horizontal:
                                        symbol = '⎯'; break;
                                    case Direction.Vertical:
                                        symbol = '│'; break;
                                    case Direction.DiagonalDown:
                                        symbol = '＼'; break;
                                    case Direction.DiagonalUp:
                                        symbol = '／'; break;
                                    default:
                                        symbol = 'ｘ'; break;
                                }

                                Rectangle rect = new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize);
                                using (Brush brush = new SolidBrush(customColor))
                                {
                                    g.DrawString(symbol.ToString(), font, brush, rect, format);
                                }
                            }
                            // else: 아무 것도 출력하지 않음
                        }
                    }
                }

                picSavePreview.Image = resizedImage;
                picSavePreview.Invalidate();
            }
        }

        private void btnImgSave_Click(object sender, EventArgs e)
        {
            if (resizedImage == null)
            {
                MessageBox.Show("저장할 이미지가 없습니다.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG 파일 (*.png)|*.png|JPEG 파일 (*.jpg)|*.jpg|Bitmap 파일 (*.bmp)|*.bmp";
                sfd.Title = "이미지 저장";
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
                sfd.FileName = $"pixel_output_{timestamp}.png";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat format = ImageFormat.Png;
                    if (sfd.FileName.EndsWith(".jpg")) format = ImageFormat.Jpeg;
                    else if (sfd.FileName.EndsWith(".bmp")) format = ImageFormat.Bmp;
                    resizedImage.Save(sfd.FileName, format);
                    MessageBox.Show("저장 완료");
                }
            }
        }
        private void picSavePreview_Paint(object sender, PaintEventArgs e)
        {
            if (pixelatedImage == null) return;

            Graphics g = e.Graphics;
            g.Clear(Color.White);

            int imgW = pixelatedImage.Width;
            int imgH = pixelatedImage.Height;
            int boxW = picSavePreview.Width;
            int boxH = picSavePreview.Height;

            float scale = Math.Min((float)boxW / imgW, (float)boxH / imgH);
            int drawW = (int)(imgW * scale);
            int drawH = (int)(imgH * scale);
            int offsetX = (boxW - drawW) / 2;
            int offsetY = (boxH - drawH) / 2;

            // 계단현상 없이 픽셀 느낌 유지
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            g.DrawImage(resizedImage, new Rectangle(offsetX, offsetY, drawW, drawH));
        }
        private Dictionary<int, char> GenerateRandomSymbolMap(int k)
        {
            var rnd = new Random();
            var shuffled = allSymbols.OrderBy(_ => rnd.Next()).ToList();

            var result = new Dictionary<int, char>();
            for (int i = 1; i <= k; i++)
            {
                result[i] = shuffled[i - 1];
            }
            return result;
        }
        
        private Direction[,] AnalyzeDirections(Bitmap bmp, int outW, int outH)
        {
            byte[,] gray = GetGrayscaleMatrix(bmp);

            int width = bmp.Width;
            int height = bmp.Height;
            int blockW = width / outW;
            int blockH = height / outH;

            Direction[,] result = new Direction[outH, outW];

            for (int by = 0; by < outH; by++)
            {
                for (int bx = 0; bx < outW; bx++)
                {
                    int startX = bx * blockW;
                    int startY = by * blockH;

                    double sumX = 0, sumY = 0, sumD1 = 0, sumD2 = 0;

                    for (int dy = 0; dy < blockH - 1; dy++)
                    {
                        for (int dx = 0; dx < blockW - 1; dx++)
                        {
                            int px = startX + dx;
                            int py = startY + dy;

                            if (px + 1 >= width || py + 1 >= height) continue;

                            int g1 = gray[py, px];
                            int gX = gray[py, px + 1];
                            int gY = gray[py + 1, px];
                            int gD1 = gray[py + 1, px + 1];
                            int gD2 = (py > 0) ? gray[py - 1, px + 1] : g1;

                            sumX += Math.Abs(g1 - gX);
                            sumY += Math.Abs(g1 - gY);
                            sumD1 += Math.Abs(g1 - gD1);
                            sumD2 += Math.Abs(g1 - gD2);
                        }
                    }

                    // 평균 변화량 계산
                    double avgX = sumX / (blockW * (blockH - 1));
                    double avgY = sumY / ((blockW - 1) * blockH);
                    double avgD1 = sumD1 / ((blockW - 1) * (blockH - 1));
                    double avgD2 = sumD2 / ((blockW - 1) * (blockH - 1));

                    // 대각선 감점 적용
                    avgD1 *= 0.75;
                    avgD2 *= 0.75;

                    double[] scores = { avgX, avgY, avgD1, avgD2 };
                    double max = scores.Max();
                    double min = scores.Min();
                    int maxIndex = Array.IndexOf(scores, max);

                    // 기준값 (기존보다 높임)
                    const double threshold = 2.5;
                    const double relativeGap = 1.3;

                    if (max < threshold && max / min < relativeGap)
                    {
                        result[by, bx] = Direction.None; // 'x'
                    }
                    else
                    {
                        switch (maxIndex)
                        {
                            case 0: result[by, bx] = Direction.Horizontal; break;
                            case 1: result[by, bx] = Direction.Vertical; break;
                            case 2: result[by, bx] = Direction.DiagonalDown; break;
                            case 3: result[by, bx] = Direction.DiagonalUp; break;
                        }
                    }


                }
            }

            return result;
        }


        private byte[,] GetGrayscaleMatrix(Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            int stride = bmpData.Stride;
            byte[] rawData = new byte[stride * height];
            Marshal.Copy(bmpData.Scan0, rawData, 0, rawData.Length);
            bmp.UnlockBits(bmpData);

            byte[,] gray = new byte[height, width];

            for (int y = 0; y < height; y++)
            {
                int rowOffset = y * stride;
                for (int x = 0; x < width; x++)
                {
                    int i = rowOffset + x * 3;
                    byte b = rawData[i];
                    byte g = rawData[i + 1];
                    byte r = rawData[i + 2];
                    gray[y, x] = (byte)((r + g + b) / 3);
                }
            }

            return gray;
        }


    }
}
