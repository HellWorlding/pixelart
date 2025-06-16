using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pixart
{
    public partial class SaveForm : Form
    {
        private Bitmap pixelatedBitmap;


        public SaveForm(Bitmap downscaledBitmap, Size originalSize)
        {
            InitializeComponent();

            // 원본 크기로 확대 (NearestNeighbor 유지)
            pixelatedBitmap = new Bitmap(originalSize.Width, originalSize.Height);
            using (Graphics g = Graphics.FromImage(pixelatedBitmap))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.DrawImage(downscaledBitmap,
                    new Rectangle(0, 0, originalSize.Width, originalSize.Height),
                    new Rectangle(0, 0, downscaledBitmap.Width, downscaledBitmap.Height),
                    GraphicsUnit.Pixel);
            }

            this.Load += SaveForm_Load;
        }


        private void SaveForm_Load(object sender, EventArgs e)
        {
            if (pixelatedBitmap != null)
            {
                int boxW = 1013;
                int boxH = 606;

                int originalW = pixelatedBitmap.Width;
                int originalH = pixelatedBitmap.Height;

                float scale = Math.Min((float)boxW / originalW, (float)boxH / originalH);
                int scaledW = (int)(originalW * scale);
                int scaledH = (int)(originalH * scale);

                // ⚠️ 계단현상 방지 + 직접 스케일
                Bitmap canvas = new Bitmap(boxW, boxH); // PictureBox 크기와 동일한 캔버스
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    g.Clear(Color.White);
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = PixelOffsetMode.Half;

                    int offsetX = (boxW - scaledW) / 2;
                    int offsetY = (boxH - scaledH) / 2;

                    g.DrawImage(pixelatedBitmap,
                        new Rectangle(offsetX, offsetY, scaledW, scaledH),
                        new Rectangle(0, 0, originalW, originalH),
                        GraphicsUnit.Pixel);
                }

                picSavePreview.Image = canvas;
                picSavePreview.Size = new Size(boxW, boxH);
                picSavePreview.SizeMode = PictureBoxSizeMode.Normal;
            }
            else
            {
                MessageBox.Show("이미지가 없습니다.");
            }
        }




        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pixelatedBitmap == null)
            {
                MessageBox.Show("저장할 이미지가 없습니다.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG 파일 (*.png)|*.png|JPEG 파일 (*.jpg)|*.jpg|Bitmap 파일 (*.bmp)|*.bmp";
                sfd.Title = "이미지 저장";
                sfd.FileName = "pixel_image"+ DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".png";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat format = ImageFormat.Png;
                    if (sfd.FileName.EndsWith(".jpg")) format = ImageFormat.Jpeg;
                    else if (sfd.FileName.EndsWith(".bmp")) format = ImageFormat.Bmp;

                    pixelatedBitmap.Save(sfd.FileName, format);
                    MessageBox.Show("저장 완료");
                }
            }
        }
    }

}
