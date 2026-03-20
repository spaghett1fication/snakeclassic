using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snakeclassic
{
    public partial class nastoy : Form
    {
        public nastoy()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
        }
        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void nastoy_Load(object sender, EventArgs e)
        {
            btn_gotov.MouseEnter += btn_gotov_MouseEnter;
            btn_gotov.MouseLeave += btn_gotov_MouseLeave;
            nazad_btn.MouseEnter += nazad_btn_MouseEnter;
            nazad_btn.MouseLeave += nazad_btn_MouseLeave;
        }
        private void btn_gotov_MouseEnter(object sender, EventArgs e)
        {
            btn_gotov.Location = new Point(btn_gotov.Location.X + 2, btn_gotov.Location.Y + 2);
        }

        private void btn_gotov_MouseLeave(object sender, EventArgs e)
        {
            btn_gotov.Location = new Point(btn_gotov.Location.X - 2, btn_gotov.Location.Y - 2);
        }
        private void nazad_btn_MouseLeave(object sender, EventArgs e)
        {
            nazad_btn.Location = new Point(nazad_btn.Location.X + 2, nazad_btn.Location.Y + 2);
        }

        private void nazad_btn_MouseEnter(object sender, EventArgs e)
        {
            nazad_btn.Location = new Point(nazad_btn.Location.X - 2, nazad_btn.Location.Y - 2);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(FindForm().Handle, 0x112, 0xf012, 0);
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void svernutbtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void nazad_btn_Click(object sender, EventArgs e)
        {
            menu form = new menu();
            form.Show();
            this.Hide(); // скрывает menu
        }
    }
}
