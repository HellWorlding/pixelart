// CustomMessageBox.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace pixelart
{
    public enum CustomDialogResult
    {
        None,
        OK,
        Yes,
        No,
        Cancel
    }

    public partial class CustomMessageBox : Form
    {
        private CustomDialogResult result = CustomDialogResult.None;

        public static CustomDialogResult Show(string message, string title = "메시지", CustomMessageBoxButtons buttons = CustomMessageBoxButtons.OK)
        {
            using (var box = new CustomMessageBox(message, title, buttons))
            {
                box.ShowDialog();
                return box.result;
            }
        }

        public CustomMessageBox(string message, string title, CustomMessageBoxButtons buttons)
        {
            InitializeComponent();
            this.Text = title;
            lblMessage.Text = message;

            switch (buttons)
            {
                case CustomMessageBoxButtons.OK:
                    btnOK.Visible = true;
                    break;
                case CustomMessageBoxButtons.YesNo:
                    btnYes.Visible = true;
                    btnNo.Visible = true;
                    btnOK.Visible = false;
                    break;
                case CustomMessageBoxButtons.OKCancel:
                    btnOK.Visible = true;
                    btnCancel.Visible = true;
                    break;
                case CustomMessageBoxButtons.YesNoCancel:
                    btnYes.Visible = true;
                    btnNo.Visible = true;
                    btnCancel.Visible = true;
                    btnOK.Visible = false;
                    break;


            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            result = CustomDialogResult.OK;
            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            result = CustomDialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            result = CustomDialogResult.No;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            result = CustomDialogResult.Cancel;
            this.Close();
        }
    }

    public enum CustomMessageBoxButtons
    {
        OK,
        YesNo,
        OKCancel,
        YesNoCancel
    }
}
