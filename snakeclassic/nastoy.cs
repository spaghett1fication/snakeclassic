using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace snakeclassic
{
    public partial class nastoy : Form
    {
        // ── Публичные статические поля — читаются из Form1 ────────────
        public static int SelectedSkin = 0;   // 0=Зелёная, 1=Синяя, 2=Оранжевая, 3=Красная
        public static int SelectedFood = 0;   // 0=Яблоко 🍎, 1=Банан 🍌

        // Индексы выбранного скина и еды
        private int selectedSkin = 0;
        private int selectedFood = 0;

        // Цвета карточек
        private readonly Color colorNormal = Color.FromArgb(60, 20, 100);
        private readonly Color colorSelected = Color.FromArgb(120, 40, 200);

        // Массивы панелей для удобного перебора
        private Panel[] skinPanels;
        private Panel[] foodPanels;

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
            skinPanels = new Panel[] { skinPanel0, skinPanel1, skinPanel2, skinPanel3 };
            foodPanels = new Panel[] { foodPanel0, foodPanel1 };

            // Восстановить текущий выбор при повторном открытии настроек
            selectedSkin = SelectedSkin;
            selectedFood = SelectedFood;

            RefreshSkinHighlight();
            RefreshFoodHighlight();
        }

        // ── Выбор скина ────────────────────────────────────────────────
        private void skinPanel_Click(object sender, EventArgs e)
        {
            Control src = sender as Control;
            Panel clicked = (src is Panel) ? (Panel)src : src?.Parent as Panel;
            if (clicked == null) return;

            for (int i = 0; i < skinPanels.Length; i++)
            {
                if (skinPanels[i] == clicked)
                {
                    selectedSkin = i;
                    break;
                }
            }
            RefreshSkinHighlight();
        }

        private void RefreshSkinHighlight()
        {
            for (int i = 0; i < skinPanels.Length; i++)
                skinPanels[i].BackColor = (i == selectedSkin) ? colorSelected : colorNormal;
        }

        // ── Выбор еды ──────────────────────────────────────────────────
        private void foodPanel_Click(object sender, EventArgs e)
        {
            Control src = sender as Control;
            Panel clicked = (src is Panel) ? (Panel)src : src?.Parent as Panel;
            if (clicked == null) return;

            for (int i = 0; i < foodPanels.Length; i++)
            {
                if (foodPanels[i] == clicked)
                {
                    selectedFood = i;
                    break;
                }
            }
            RefreshFoodHighlight();
        }

        private void RefreshFoodHighlight()
        {
            for (int i = 0; i < foodPanels.Length; i++)
                foodPanels[i].BackColor = (i == selectedFood) ? colorSelected : colorNormal;
        }

        // ── Кнопка Готово — сохраняет выбор в статические поля ────────
        private void btn_gotov_Click(object sender, EventArgs e)
        {
            SelectedSkin = selectedSkin;
            SelectedFood = selectedFood;
            this.Close();
        }

        private void btn_gotov_MouseEnter(object sender, EventArgs e)
        {
            btn_gotov.Location = new Point(btn_gotov.Location.X, btn_gotov.Location.Y + 2);
        }

        private void btn_gotov_MouseLeave(object sender, EventArgs e)
        {
            btn_gotov.Location = new Point(btn_gotov.Location.X, btn_gotov.Location.Y - 2);
        }

        // ── Кнопка Назад ───────────────────────────────────────────────
        private void nazad_btn_Click(object sender, EventArgs e)
        {
            menu form = new menu();
            form.Show();
            this.Hide();
        }

        private void nazad_btn_MouseEnter(object sender, EventArgs e)
        {
            nazad_btn.Location = new Point(nazad_btn.Location.X, nazad_btn.Location.Y + 2);
        }

        private void nazad_btn_MouseLeave(object sender, EventArgs e)
        {
            nazad_btn.Location = new Point(nazad_btn.Location.X, nazad_btn.Location.Y - 2);
        }

        // ── Шапка: свернуть / закрыть / перетаскивание ─────────────────
        private void svernutbtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(FindForm().Handle, 0x112, 0xf012, 0);
            }
        }
    }
}
