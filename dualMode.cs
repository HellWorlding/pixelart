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
    public partial class dualMode: Form
    {
        public dualMode()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            startpage main = new startpage();
            this.Hide();
            main.FormClosed += (s, args) => this.Close();
            main.Show();
        }
    }
}
