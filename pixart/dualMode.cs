using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pixart
{
    public partial class dualMode : Form
    {
        private TcpListener server;
        private TcpClient client;
        private NetworkStream stream;

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

        private void btnHostStart_Click(object sender, EventArgs e)
        {
            // 서버 시작
            server = new TcpListener(IPAddress.Any, 9000);
            server.Start();

            // 연결 대기 (비동기)
            Task.Run(() =>
            {
                try
                {
                    client = server.AcceptTcpClient();
                    stream = client.GetStream();

                    // UI 스레드에서 폼 전환
                    this.Invoke((MethodInvoker)(() =>
                    {
                        MessageBox.Show("클라이언트 연결됨!");
                        dualModeColoring form = new dualModeColoring(stream, true); // 호스트
                        this.Hide();
                        form.FormClosed += (s, args) => this.Close();
                        form.Show();
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("서버 오류: " + ex.Message);
                }
            });
        }

        private void btnClientConnect_Click(object sender, EventArgs e)
        {
            string ip = txtIpAddress.Text.Trim();

            try
            {
                client = new TcpClient(ip, 9000);
                stream = client.GetStream();

                MessageBox.Show("서버에 연결됨!");

                dualModeColoring form = new dualModeColoring(stream, false); // 클라이언트
                this.Hide();
                form.FormClosed += (s, args) => this.Close();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("연결 실패: " + ex.Message);
            }
        }
    }
}
