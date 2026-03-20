using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snakeclassic
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
        }
        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);



        private void menu_Load(object sender, EventArgs e)
        {
            igra_button.MouseEnter += igra_button_MouseEnter;
            igra_button.MouseLeave += igra_button_MouseLeave;
            nastroy_button.MouseEnter += nastroy_button_MouseEnter;
            nastroy_button.MouseLeave += nastroy_button_MouseLeave;
            table_button.MouseEnter += table_button_MouseEnter;
            table_button.MouseLeave += table_button_MouseLeave;
            exit_button.MouseEnter += exit_button_MouseEnter;
            exit_button.MouseLeave += exit_button_MouseLeave;
            panel1.MouseDown += panel1_MouseDown;

        }
        private void igra_button_MouseEnter(object sender, EventArgs e)
        {
            igra_button.Location = new Point(igra_button.Location.X + 2, igra_button.Location.Y + 2);
        }

        private void igra_button_MouseLeave(object sender, EventArgs e)
        {
            igra_button.Location = new Point(igra_button.Location.X - 2, igra_button.Location.Y - 2);
        }

        private void nastroy_button_MouseEnter(object sender, EventArgs e)
        {
            nastroy_button.Location = new Point(nastroy_button.Location.X + 2, nastroy_button.Location.Y + 2);
        }
        private void nastroy_button_MouseLeave(object sender, EventArgs e)
        {
            nastroy_button.Location = new Point(nastroy_button.Location.X - 2, nastroy_button.Location.Y - 2);
        }

        private void table_button_MouseEnter(object sender, EventArgs e)
        {
            table_button.Location = new Point(table_button.Location.X + 2, table_button.Location.Y + 2);
        }

        private void table_button_MouseLeave(object sender, EventArgs e)
        {
            table_button.Location = new Point(table_button.Location.X - 2, table_button.Location.Y - 2);
        }
        private void exit_button_MouseEnter(object sender, EventArgs e)
        {
            exit_button.Location = new Point(exit_button.Location.X + 2, exit_button.Location.Y + 2);
        }

        private void exit_button_MouseLeave(object sender, EventArgs e)
        {
            exit_button.Location = new Point(exit_button.Location.X - 2, exit_button.Location.Y - 2);
        }

        private void nastroy_button_Click(object sender, EventArgs e)
        {
            nastoy form = new nastoy();
            form.Show();
            this.Hide(); // скрывает menu
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
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

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void igra_button_Click(object sender, EventArgs e)
        {
            nicknamefrm form = new nicknamefrm();
            form.Show();
            this.Hide(); // скрывает menu
        }

        private void table_button_Click(object sender, EventArgs e)
        {
            leadbordfrm form = new leadbordfrm();
            form.Show();
            this.Hide(); // скрывает menu
        }
    }
}
