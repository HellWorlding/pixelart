using System.Windows.Forms;

namespace pixelart
{
    public static class CustomMessageBoxHelper
    {
        public static void Show(string message)
        {
            CustomMessageBox.Show(message); // 내부에서 디폴트 인자 처리함
        }

        public static void Show(string message, string title)
        {
            CustomMessageBox.Show(message, title);
        }

        public static void Show(string message, string title, CustomMessageBoxButtons buttons)
        {
            CustomMessageBox.Show(message, title, buttons);
        }

        public static CustomDialogResult ShowWithResult(string message, string title = "메시지", CustomMessageBoxButtons buttons = CustomMessageBoxButtons.OK)
        {
            return CustomMessageBox.Show(message, title, buttons);
        }


    }



}
