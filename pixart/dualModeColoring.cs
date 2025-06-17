using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace pixart
{
    public partial class dualModeColoring : Form
    {
        // 내 로컬 데이터 (My local data)
        private Bitmap originalImage;
        private int blockSize = 10;
        private int colorCount = 12;
        private Color[,] blockColors;
        private int[,] colorNumbers;
        private Dictionary<Color, int> colorMap = new Dictionary<Color, int>();
        private bool[,] isFilled;
        private Color[,] filledColors;
        private Color selectedColor = Color.Transparent;

        // 네트워크 및 상대방 데이터 (Network and peer data)
        private NetworkStream stream;
        private bool isHost;
        private Thread receiveThread;

        private Color[,] peerBlockColors;
        private int[,] peerColorNumbers;
        private Dictionary<Color, int> peerColorMap;
        private bool[,] peerIsFilled;
        private Color[,] peerFilledColors;

        public dualModeColoring(NetworkStream stream, bool isHost)
        {
            InitializeComponent();
            this.stream = stream;
            this.isHost = isHost;

            // 디자이너 속성 창에서 이벤트를 연결합니다.
            // (Events are subscribed in the designer's property window.)

            ApplyRetroStyle(this);  // 이 폼 내의 모든 컨트롤에 적용


            StartReceiveThread();
        }



        private void ApplyRetroStyle(Control ctrl)
        {
            ctrl.Font = new Font("Courier New", 9F, FontStyle.Bold);

            if (ctrl is Button btn)
            {
                btn.BackColor = Color.FromArgb(192, 192, 192);
                btn.ForeColor = Color.Black;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;

                btn.MouseEnter += (s, e) => btn.BackColor = Color.DarkGray;
                btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(192, 192, 192);
                btn.MouseDown += (s, e) => btn.BackColor = Color.DimGray;
                btn.MouseUp += (s, e) =>
                {
                    btn.BackColor = btn.ClientRectangle.Contains(btn.PointToClient(Cursor.Position))
                        ? Color.DarkGray : Color.FromArgb(192, 192, 192);
                };

                btn.Paint += (s, e) =>
                {
                    ControlPaint.DrawBorder(e.Graphics, btn.ClientRectangle,
                        Color.White, 2, ButtonBorderStyle.Outset,
                        Color.White, 2, ButtonBorderStyle.Outset,
                        Color.Gray, 2, ButtonBorderStyle.Inset,
                        Color.Gray, 2, ButtonBorderStyle.Inset);
                };
            }

            // 자식 컨트롤에도 재귀 적용
            foreach (Control child in ctrl.Controls)
                ApplyRetroStyle(child);
        }



        /// <summary>
        /// 네트워크 스트림으로부터 데이터를 수신하는 스레드를 시작합니다.
        /// (Starts the thread to receive data from the network stream.)
        /// </summary>
        private void StartReceiveThread()
        {
            receiveThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        // 1. 패킷 길이 수신 (4바이트)
                        byte[] lengthBuffer = new byte[4];
                        int read = stream.Read(lengthBuffer, 0, 4);
                        if (read == 0) break; // 연결 종료
                        int packetSize = BitConverter.ToInt32(lengthBuffer, 0);

                        // 2. 패킷 본문 수신
                        byte[] packetBuffer = new byte[packetSize];
                        int totalRead = 0;
                        while (totalRead < packetSize)
                        {
                            read = stream.Read(packetBuffer, totalRead, packetSize - totalRead);
                            if (read == 0) break;
                            totalRead += read;
                        }
                        if (totalRead < packetSize) break; // 비정상 종료

                        // 3. 역직렬화
                        object deserializedPacket;
                        using (MemoryStream ms = new MemoryStream(packetBuffer))
                        {
                            var formatter = new BinaryFormatter();
                            deserializedPacket = formatter.Deserialize(ms);
                        }

                        // 4. 패킷 타입에 따라 처리
                        if (deserializedPacket is PaintPacket paint)
                        {
                            this.Invoke((MethodInvoker)(() => ApplyPaintPacket(paint)));
                        }
                        else if (deserializedPacket is PatternPacket pattern)
                        {
                            this.Invoke((MethodInvoker)(() => ApplyPatternPacket(pattern)));
                        }
                        else if (deserializedPacket is ColorAllPacket) // 전체 색칠 패킷 처리
                        {
                            this.Invoke((MethodInvoker)(() => ApplyColorAllPacket()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (this.IsHandleCreated)
                    {
                        MessageBox.Show("수신 중 오류 발생: " + ex.Message);
                    }
                }
            });
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        /// <summary>
        /// 패킷을 직렬화하고 길이를 앞에 붙여 전송합니다.
        /// (Serializes a packet and sends it with a length prefix.)
        /// </summary>
        private void SendPacket(object packet)
        {
            try
            {
                byte[] data;
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, packet);
                    data = ms.ToArray();
                }

                byte[] length = BitConverter.GetBytes(data.Length);
                stream.Write(length, 0, 4);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("전송 오류: " + ex.Message);
            }
        }

        // --- 패킷 처리 로직 (Packet Handling Logic) ---

        private void ApplyPatternPacket(PatternPacket packet)
        {
            blockColors = packet.BlockColors;
            colorNumbers = packet.ColorNumbers;
            colorMap = packet.ColorMap;
            blockSize = packet.BlockSize;
            colorCount = packet.ColorCount;

            int h = blockColors.GetLength(0);
            int w = blockColors.GetLength(1);

            isFilled = new bool[h, w];
            filledColors = new Color[h, w];

            peerBlockColors = (Color[,])blockColors.Clone();
            peerColorNumbers = (int[,])colorNumbers.Clone();
            peerColorMap = new Dictionary<Color, int>(colorMap);
            peerIsFilled = new bool[h, w];
            peerFilledColors = new Color[h, w];

            CreateColorPalette();
            panelCanvas.Invalidate();
            panelPeer.Invalidate();
        }

        /// <summary>
        /// [수정됨] 상대방의 색칠 정보를 받을 때, 해당 셀만 다시 그려 깜빡임을 제거합니다.
        /// (MODIFIED: When receiving paint data from the peer, redraws only the specific cell to eliminate flickering.)
        /// </summary>
        private void ApplyPaintPacket(PaintPacket packet)
        {
            if (peerBlockColors == null || peerColorMap == null) return;

            Color paintedColor = peerColorMap.FirstOrDefault(kv => kv.Value == packet.ColorNumber).Key;

            if (paintedColor != default(Color))
            {
                peerIsFilled[packet.Y, packet.X] = true;
                peerFilledColors[packet.Y, packet.X] = paintedColor;

                // 상대방 패널의 특정 영역만 다시 그리도록 계산합니다.
                int wBlocks = peerBlockColors.GetLength(1);
                int hBlocks = peerBlockColors.GetLength(0);
                float scaleX = (float)panelPeer.Width / wBlocks;
                float scaleY = (float)panelPeer.Height / hBlocks;
                float cellSize = Math.Min(scaleX, scaleY);
                if (cellSize <= 0) return;

                float offsetX = (panelPeer.Width - (cellSize * wBlocks)) / 2;
                float offsetY = (panelPeer.Height - (cellSize * hBlocks)) / 2;

                Rectangle rectToUpdate = new Rectangle(
                    (int)Math.Floor(offsetX + packet.X * cellSize),
                    (int)Math.Floor(offsetY + packet.Y * cellSize),
                    (int)Math.Ceiling(cellSize) + 1,
                    (int)Math.Ceiling(cellSize) + 1
                );

                panelPeer.Invalidate(rectToUpdate);
            }
        }

        private void ApplyColorAllPacket()
        {
            if (peerBlockColors == null || peerColorMap == null || peerColorMap.Count == 0)
            {
                return;
            }

            int hBlocks = peerBlockColors.GetLength(0);
            int wBlocks = peerBlockColors.GetLength(1);

            Dictionary<int, Color> numberToColor = peerColorMap.ToDictionary(kv => kv.Value, kv => kv.Key);

            for (int y = 0; y < hBlocks; y++)
            {
                for (int x = 0; x < wBlocks; x++)
                {
                    int num = peerColorNumbers[y, x];
                    if (numberToColor.TryGetValue(num, out Color color))
                    {
                        peerIsFilled[y, x] = true;
                        peerFilledColors[y, x] = color;
                    }
                }
            }
            panelPeer.Invalidate();
        }


        // --- UI 이벤트 핸들러 및 로직 (UI Event Handlers and Logic) ---

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp", Title = "이미지 파일 선택" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        originalImage?.Dispose();
                        originalImage = new Bitmap(ofd.FileName);
                        panelCanvas.Invalidate();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("이미지 로드 실패: " + ex.Message);
                    }
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("먼저 이미지를 불러와주세요.");
                return;
            }

            int desiredGridW = (int)numPixelSizeDual.Value;
            if (desiredGridW <= 0) desiredGridW = 1;

            blockSize = originalImage.Width / desiredGridW;
            if (blockSize == 0) blockSize = 1;
            int wBlocks = originalImage.Width / blockSize;
            int hBlocks = originalImage.Height / blockSize;
            colorCount = 12;

            var newBlockColors = new Color[hBlocks, wBlocks];
            var newColorNumbers = new int[hBlocks, wBlocks];
            var newColorMap = new Dictionary<Color, int>();

            for (int y = 0; y < hBlocks; y++)
            {
                for (int x = 0; x < wBlocks; x++)
                {
                    Color avgColor = GetAverageColor(x * blockSize, y * blockSize, blockSize);
                    newBlockColors[y, x] = avgColor;

                    Color simplified = SimplifyColor(avgColor, colorCount);
                    if (!newColorMap.ContainsKey(simplified))
                    {
                        newColorMap[simplified] = newColorMap.Count + 1;
                    }
                    newColorNumbers[y, x] = newColorMap[simplified];
                }
            }

            var patternPacket = new PatternPacket
            {
                BlockColors = newBlockColors,
                ColorNumbers = newColorNumbers,
                ColorMap = newColorMap,
                BlockSize = blockSize,
                ColorCount = colorCount
            };

            ApplyPatternPacket(patternPacket);
            SendPacket(patternPacket);
        }

        private void panelCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (blockColors == null)
            {
                MessageBox.Show("먼저 도안을 생성해주세요.");
                return;
            }
            if (selectedColor == Color.Transparent)
            {
                MessageBox.Show("먼저 팔레트에서 색상을 선택해주세요.");
                return;
            }

            int wBlocks = blockColors.GetLength(1);
            int hBlocks = blockColors.GetLength(0);
            float scaleX = (float)panelCanvas.Width / wBlocks;
            float scaleY = (float)panelCanvas.Height / hBlocks;
            float cellSize = Math.Min(scaleX, scaleY);
            if (cellSize <= 0) return;
            float offsetX = (panelCanvas.Width - (cellSize * wBlocks)) / 2;
            float offsetY = (panelCanvas.Height - (cellSize * hBlocks)) / 2;
            int x = (int)((e.X - offsetX) / cellSize);
            int y = (int)((e.Y - offsetY) / cellSize);

            if (x < 0 || x >= wBlocks || y < 0 || y >= hBlocks) return;

            int clickedColorNumber = colorNumbers[y, x];

            if (colorMap.TryGetValue(selectedColor, out int selectedColorNumber))
            {
                if (clickedColorNumber == selectedColorNumber)
                {
                    isFilled[y, x] = true;
                    filledColors[y, x] = selectedColor;

                    Rectangle rectToUpdate = new Rectangle(
                        (int)Math.Floor(offsetX + x * cellSize),
                        (int)Math.Floor(offsetY + y * cellSize),
                        (int)Math.Ceiling(cellSize) + 1,
                        (int)Math.Ceiling(cellSize) + 1
                    );
                    panelCanvas.Invalidate(rectToUpdate);

                    PaintPacket packet = new PaintPacket { X = x, Y = y, ColorNumber = selectedColorNumber };
                    SendPacket(packet);
                }
                else
                {
                    MessageBox.Show("선택한 색상과 셀의 색상 번호가 다릅니다!");
                }
            }
            else
            {
                MessageBox.Show("오류: 선택한 색상이 팔레트의 색상 맵에 존재하지 않습니다.");
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

            Dictionary<int, Color> numberToColor = colorMap.ToDictionary(kv => kv.Value, kv => kv.Key);

            for (int y = 0; y < hBlocks; y++)
            {
                for (int x = 0; x < wBlocks; x++)
                {
                    int num = colorNumbers[y, x];
                    if (numberToColor.TryGetValue(num, out Color color))
                    {
                        isFilled[y, x] = true;
                        filledColors[y, x] = color;
                    }
                }
            }

            panelCanvas.Invalidate();

            SendPacket(new ColorAllPacket());
        }

        // --- 그리기 및 유틸리티 메서드 (이하 동일) ---

        private void panelCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (blockColors == null) return;
            // 'Paint' 이벤트의 e.Graphics 객체는 자동으로 업데이트 영역(ClipRectangle)을 알고 있습니다.
            // The e.Graphics object in the 'Paint' event automatically knows the update area (ClipRectangle).
            DrawPattern(e.Graphics, panelCanvas, blockColors, colorNumbers, isFilled, filledColors);
        }

        private void panelPeer_Paint(object sender, PaintEventArgs e)
        {
            if (peerBlockColors == null) return;
            DrawPattern(e.Graphics, panelPeer, peerBlockColors, peerColorNumbers, peerIsFilled, peerFilledColors);
        }

        private void DrawPattern(Graphics g, Panel panel, Color[,] blocks, int[,] numbers, bool[,] filledState, Color[,] filledC)
        {
            // 더블 버퍼링을 활성화하여 최종적인 깜빡임까지 최소화합니다.
            // 폼의 생성자나 Load 이벤트에서 this.DoubleBuffered = true; 를 추가하는 것이 더 좋습니다.
            // (Enable double buffering to minimize final flickering. 
            // It's better to add this.DoubleBuffered = true; in the form's constructor or Load event.)

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            // Invalidate(rect)를 사용하면 g.Clear()가 특정 영역만 지우므로 전체를 지우는 코드는 주석 처리하거나 조건부로 실행합니다.
            // (When using Invalidate(rect), g.Clear() only clears that specific area, so code for clearing the whole panel is commented out or made conditional.)
            // g.Clear(Color.White);

            int hBlocks = blocks.GetLength(0);
            int wBlocks = blocks.GetLength(1);

            float scaleX = (float)panel.Width / wBlocks;
            float scaleY = (float)panel.Height / hBlocks;
            float cellSize = Math.Min(scaleX, scaleY);
            if (cellSize <= 0) return;

            float totalW = cellSize * wBlocks;
            float totalH = cellSize * hBlocks;
            float offsetX = (panel.Width - totalW) / 2;
            float offsetY = (panel.Height - totalH) / 2;

            using (Font font = new Font("Arial", Math.Max(6, (int)(cellSize * 0.4f)), FontStyle.Bold))
            {
                for (int y = 0; y < hBlocks; y++)
                {
                    for (int x = 0; x < wBlocks; x++)
                    {
                        float left = offsetX + x * cellSize;
                        float top = offsetY + y * cellSize;
                        RectangleF rectF = new RectangleF(left, top, cellSize, cellSize);

                        // 다시 그려야 할 영역(ClipRectangle)과 현재 셀이 겹치지 않으면 그리기 건너뛰기
                        if (!g.ClipBounds.IntersectsWith(rectF))
                        {
                            continue;
                        }

                        // 배경을 먼저 흰색으로 칠합니다 (Clear를 안 쓸 경우).
                        g.FillRectangle(Brushes.White, rectF);

                        Rectangle rect = Rectangle.Round(rectF);

                        if (filledState[y, x])
                        {
                            using (SolidBrush brush = new SolidBrush(filledC[y, x]))
                                g.FillRectangle(brush, rectF);
                        }

                        g.DrawRectangle(Pens.Gray, rect);

                        if (!filledState[y, x])
                        {
                            string num = numbers[y, x].ToString();
                            TextRenderer.DrawText(g, num, font, rect, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        }
                    }
                }
            }
        }

        private void CreateColorPalette()
        {
            panelPalette.Controls.Clear();
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
                    ForeColor = (color.GetBrightness() < 0.5) ? Color.White : Color.Black,
                    Tag = color,
                    FlatStyle = FlatStyle.Flat,
                };
                colorButton.FlatAppearance.BorderColor = Color.LightGray;
                colorButton.FlatAppearance.BorderSize = 1;
                colorButton.Click += (s, e) =>
                {
                    selectedColor = (Color)((Button)s).Tag;
                    foreach (Control c in panelPalette.Controls)
                    {
                        if (c is Button b)
                        {
                            b.FlatAppearance.BorderSize = (b == s) ? 3 : 1;
                            b.FlatAppearance.BorderColor = (b == s) ? Color.DodgerBlue : Color.LightGray;
                        }
                    }
                };
                panelPalette.Controls.Add(colorButton);
            }
        }

        private Color GetAverageColor(int startX, int startY, int size)
        {
            long r = 0, g = 0, b = 0;
            int count = 0;
            int endY = startY + size;
            int endX = startX + size;

            for (int y = startY; y < endY && y < originalImage.Height; y++)
            {
                for (int x = startX; x < endX && x < originalImage.Width; x++)
                {
                    Color c = originalImage.GetPixel(x, y);
                    r += c.R;
                    g += c.G;
                    b += c.B;
                    count++;
                }
            }
            return count == 0 ? Color.Black : Color.FromArgb((int)(r / count), (int)(g / count), (int)(b / count));
        }

        private Color SimplifyColor(Color color, int levels)
        {
            if (levels <= 1) return color;
            int step = 255 / (levels - 1);
            int r = (int)Math.Round(color.R / (double)step) * step;
            int g = (int)Math.Round(color.G / (double)step) * step;
            int b = (int)Math.Round(color.B / (double)step) * step;
            return Color.FromArgb(Math.Min(255, r), Math.Min(255, g), Math.Min(255, b));
        }
    }
}
