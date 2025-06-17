using PixelColorling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pixart
{
    public partial class startpage : Form
    {
        public startpage()
        {
            InitializeComponent();

            // 버튼에 레트로 스타일 적용
            ApplyRetroButtonStyle(btnExplanation);
            ApplyRetroButtonStyle(btnColorLevel);
            ApplyRetroButtonStyle(btnKmeansColor);
            ApplyRetroButtonStyle(btnDualMode);

            // 테두리 스타일 적용
            btnExplanation.Paint += Draw3DBorder;
            btnColorLevel.Paint += Draw3DBorder;
            btnKmeansColor.Paint += Draw3DBorder;
            btnDualMode.Paint += Draw3DBorder;
        }

        private void ApplyRetroButtonStyle(Button btn)
        {
            // 기본 색상 스타일: 회청색 계열
            btn.BackColor = Color.FromArgb(230, 235, 240);       // 기본
            btn.ForeColor = Color.FromArgb(25, 25, 25);          // 텍스트
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.BorderColor = Color.FromArgb(120, 120, 130); // 실제 적용 안 됨 (커스텀 Paint에서 그림)

            // 시스템 기본 픽셀 느낌 글꼴
            btn.Font = new Font("Courier New", 9F, FontStyle.Bold);

            // 마우스 인터랙션
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(210, 220, 230); // Hover
            btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(230, 235, 240); // Default
            btn.MouseDown += (s, e) => btn.BackColor = Color.FromArgb(180, 190, 200);  // 눌림
            btn.MouseUp += (s, e) =>
            {
                btn.BackColor = btn.ClientRectangle.Contains(btn.PointToClient(Cursor.Position))
                    ? Color.FromArgb(210, 220, 230)
                    : Color.FromArgb(230, 235, 240);
            };
        }

        private void Draw3DBorder(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;

            ControlPaint.DrawBorder(e.Graphics, btn.ClientRectangle,
                Color.White, 2, ButtonBorderStyle.Outset,    // Top
                Color.White, 2, ButtonBorderStyle.Outset,    // Left
                Color.Gray, 2, ButtonBorderStyle.Inset,      // Bottom
                Color.Gray, 2, ButtonBorderStyle.Inset);     // Right
        }

        private void btnExplanation_Click(object sender, EventArgs e)
        {
            explanation explanationForm = new explanation();
            this.Hide();
            explanationForm.FormClosed += (s, args) => this.Close();
            explanationForm.Show();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            Coloring mainForm = new Coloring();
            this.Hide();
            mainForm.FormClosed += (s, args) => this.Close();
            mainForm.Show();
        }

        private void btnDualMode_Click(object sender, EventArgs e)
        {
            dualMode dualForm = new dualMode();
            this.Hide();
            dualForm.FormClosed += (s, args) => this.Close();
            dualForm.Show();
        }

        private void btnColorLevel_Click(object sender, EventArgs e)
        {
            pixel.KmeansColoring form1 = new pixel.KmeansColoring();
            this.Hide();
            form1.FormClosed += (s, args) => this.Close();
            form1.Show();
        }
    }
}
