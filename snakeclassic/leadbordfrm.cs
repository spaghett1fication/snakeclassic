using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace snakeclassic
{
    public partial class leadbordfrm : Form
    {
        public leadbordfrm()
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

        private void leadbordfrm_Load(object sender, EventArgs e)
        {
            nazad_btn.MouseEnter += nazad_btn_MouseEnter;
            nazad_btn.MouseLeave += nazad_btn_MouseLeave;
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
