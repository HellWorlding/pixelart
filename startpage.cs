using PixelColorling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pixart
{
    public partial class startpage: Form
    {
        public startpage()
        {
            InitializeComponent();
        }

        private void btnExplanation_Click(object sender, EventArgs e)
        {
            explanation explanationForm = new explanation();
            this.Hide(); // 현재 폼을 닫지 않고 숨깁니다
            explanationForm.FormClosed += (s, args) => this.Close(); // 설명 폼이 닫히면 시작 페이지도 종료
            explanationForm.Show();
        }

        //private void btnColor_Click(object sender, EventArgs e)
        //{
        //    Coloring mainForm = new Coloring();
        //    this.Hide(); // 현재 startpage를 숨깁니다
        //    mainForm.FormClosed += (s, args) => this.Close(); // mainPage 닫히면 startpage도 함께 종료
        //    mainForm.Show();
        //}
        private void btnColor_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("어떤 색칠 도구를 사용하시겠습니까?\nYes: 기본 Coloring\nNo: KMeans 기반 Form1",
                                         "색칠 선택",
                                         MessageBoxButtons.YesNoCancel,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // 기존 Coloring 사용
                Coloring mainForm = new Coloring();
                this.Hide();
                mainForm.FormClosed += (s, args) => this.Close();
                mainForm.Show();
            }
            else if (result == DialogResult.No)
            {
                // pixel에서 가져온 Form1 사용
                pixel.KmeansColoring form1 = new pixel.KmeansColoring(); // Fully qualify the namespace
                this.Hide();
                form1.FormClosed += (s, args) => this.Close();
                form1.Show();
            }
            // Cancel 누르면 아무 것도 안 함
        }


        private void btnDualMode_Click(object sender, EventArgs e)
        {
            dualMode dualForm = new dualMode();
            this.Hide(); // startpage 숨기고
            dualForm.FormClosed += (s, args) => this.Close(); // 닫히면 종료
            dualForm.Show();
        }
    }
}
