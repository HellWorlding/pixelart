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
    public partial class explanation: Form
    {
        public explanation()
        {
            InitializeComponent();
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            startpage startForm = new startpage();
            this.Hide(); // explanation 폼 숨기기
            startForm.FormClosed += (s, args) => this.Close(); // startpage 닫히면 explanation도 같이 닫힘
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
            pixel.KmeansColoring form1 = new pixel.KmeansColoring(); // Fully qualify the namespace
            this.Hide();
            form1.FormClosed += (s, args) => this.Close();
            form1.Show();
        }
    }
}
