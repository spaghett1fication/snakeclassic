using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace snakeclassic
{
    public partial class nicknamefrm : Form
    {
        // Путь к файлу с ником (тот же каталог что и leaderboard.txt)
        public static readonly string NickPath =
            Path.Combine(@"D:\Visual Studio\course\snakeclassic", "nickname.txt");

        public nicknamefrm()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
        }

        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(FindForm().Handle, 0x112, 0xf012, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Сохраняем ник перед запуском игры
            string nick = nicknametextbox.Text.Trim();
            if (string.IsNullOrEmpty(nick)) nick = "Игрок";

            Directory.CreateDirectory(Path.GetDirectoryName(NickPath));
            File.WriteAllText(NickPath, nick);

            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void nazad_btn_Click(object sender, EventArgs e)
        {
            menu form = new menu();
            form.Show();
            this.Hide();
        }

        private void nicknamefrm_Load(object sender, EventArgs e)
        {
            btn_gotov.MouseEnter += btn_gotov_MouseEnter;
            btn_gotov.MouseLeave += btn_gotov_MouseLeave;
            nazad_btn.MouseEnter += nazad_btn_MouseEnter;
            nazad_btn.MouseLeave += nazad_btn_MouseLeave;

            // Загружаем сохранённый ник если есть
            if (File.Exists(NickPath))
                nicknametextbox.Text = File.ReadAllText(NickPath).Trim();
        }

        private void btn_gotov_MouseEnter(object sender, EventArgs e)
        {
            btn_gotov.Location = new Point(btn_gotov.Location.X + 2, btn_gotov.Location.Y + 2);
        }

        private void btn_gotov_MouseLeave(object sender, EventArgs e)
        {
            btn_gotov.Location = new Point(btn_gotov.Location.X - 2, btn_gotov.Location.Y - 2);
        }

        private void nazad_btn_MouseEnter(object sender, EventArgs e)
        {
            nazad_btn.Location = new Point(nazad_btn.Location.X + 2, nazad_btn.Location.Y + 2);
        }

        private void nazad_btn_MouseLeave(object sender, EventArgs e)
        {
            nazad_btn.Location = new Point(nazad_btn.Location.X - 2, nazad_btn.Location.Y - 2);
        }
    }
}
