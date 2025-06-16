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
            this.Hide(); // startpage 숨기고
            dualForm.FormClosed += (s, args) => this.Close(); // 닫히면 종료
            dualForm.Show();
        }

        private void btnColorLevel_Click(object sender, EventArgs e)
        {
            pixel.KmeansColoring form1 = new pixel.KmeansColoring(); // Fully qualify the namespace
            this.Hide();
            form1.FormClosed += (s, args) => this.Close();
            form1.Show();
        }
    }
}
