using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PixelColorling;

namespace pixart
{
    public partial class explanation : Form
    {
        public explanation()
        {
            InitializeComponent();

            // 전체 폼 배경색 적용
            this.BackColor = Color.LightGray;

            // 버튼과 라벨에 레트로 스타일 적용
            ApplyRetroStyle(btnMain);
            ApplyRetroStyle(btnColorLevel);
            ApplyRetroStyle(button1);
            ApplyRetroStyle(label1);
            ApplyRetroStyle(label2);
            ApplyRetroStyle(label3);
            ApplyRetroStyle(label4);

            
        }
        


        private void btnMain_Click(object sender, EventArgs e)
        {
            startpage startForm = new startpage();
            this.Hide();
            startForm.FormClosed += (s, args) => this.Close();
            startForm.Show();
        }

        private void btnColorLevel_Click(object sender, EventArgs e)
        {
            Coloring mainForm = new Coloring();
            this.Hide();
            mainForm.FormClosed += (s, args) => this.Close();
            mainForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pixel.KmeansColoring form1 = new pixel.KmeansColoring();
            this.Hide();
            form1.FormClosed += (s, args) => this.Close();
            form1.Show();
        }

        //  범용적인 스타일 적용 함수
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

                // 3D 테두리 효과
                btn.Paint += (s, e) =>
                {
                    ControlPaint.DrawBorder(e.Graphics, btn.ClientRectangle,
                        Color.White, 2, ButtonBorderStyle.Outset,
                        Color.White, 2, ButtonBorderStyle.Outset,
                        Color.Gray, 2, ButtonBorderStyle.Inset,
                        Color.Gray, 2, ButtonBorderStyle.Inset);
                };
            }
        }
    }

}
