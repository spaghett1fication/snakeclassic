using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace snakeclassic
{
    public partial class nastoy : Form
    {
        // ── Публичные статические поля — читаются из Form1 ────────────
        public static int SelectedSkin = 0;   // 0=Зелёная 1=Оранжевая 2=Красная 3=Синяя
        public static int SelectedFood = 0;   // 0=Банан   1=Яблоко

        // ══════════════════════════════════════════════════════════════
        //  НОВОЕ: Коллизия со стенами (true = стены убивают)
        // ══════════════════════════════════════════════════════════════
        public static bool WallCollision = true;

        // Локальные для UI
        private int selectedSkin = 0;
        private int selectedFood = 0;
        private bool wallCollision = true;

        private readonly Color colorNormal = Color.FromArgb(60, 20, 100);
        private readonly Color colorSelected = Color.FromArgb(120, 40, 200);

        private Panel[] skinPanels;
        private Panel[] foodPanels;

        // ── Новые контролы для коллизии (создаются программно) ──
        private Label collisionTitle;
        private Panel collisionPanel;
        private Label collisionLbl;

        public nastoy()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
        }

        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void nastoy_Load(object sender, EventArgs e)
        {
            skinPanels = new Panel[] { skinPanel0, skinPanel1, skinPanel2, skinPanel3 };
            foodPanels = new Panel[] { foodPanel0, foodPanel1 };

            // Восстанавливаем текущий выбор из статических полей
            selectedSkin = nastoy.SelectedSkin;
            selectedFood = nastoy.SelectedFood;
            wallCollision = nastoy.WallCollision;

            // Подписываем клики на панели И на все дочерние контролы
            foreach (Panel p in skinPanels)
            {
                p.Click += skinPanel_Click;
                p.Cursor = Cursors.Hand;
                foreach (Control child in p.Controls)
                {
                    child.Click += skinPanel_Click;
                    child.Cursor = Cursors.Hand;
                }
            }

            foreach (Panel p in foodPanels)
            {
                p.Click += foodPanel_Click;
                p.Cursor = Cursors.Hand;
                foreach (Control child in p.Controls)
                {
                    child.Click += foodPanel_Click;
                    child.Cursor = Cursors.Hand;
                }
            }

            RefreshSkinHighlight();
            RefreshFoodHighlight();

            // ══════════════════════════════════════════════════════════
            //  Создаём секцию "Коллизия" программно (Designer не нужен)
            // ══════════════════════════════════════════════════════════
            CreateCollisionSection();
            RefreshCollisionHighlight();
        }

        // ══════════════════════════════════════════════════════════════
        //  КОЛЛИЗИЯ: создание UI
        // ══════════════════════════════════════════════════════════════
        private void CreateCollisionSection()
        {
            // Находим нижнюю границу панелей еды
            int foodBottom = 0;
            foreach (Panel p in foodPanels)
            {
                if (p.Bottom > foodBottom)
                    foodBottom = p.Bottom;
            }

            // ── Заголовок секции ──
            collisionTitle = new Label();
            collisionTitle.Text = "КОЛЛИЗИЯ СО СТЕНАМИ";
            collisionTitle.Font = new Font("Arial", 11F, FontStyle.Bold);
            collisionTitle.ForeColor = Color.White;
            collisionTitle.BackColor = Color.Transparent;
            collisionTitle.Size = new Size(640, 26);
            collisionTitle.Location = new Point(0, foodBottom + 18);
            collisionTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(collisionTitle);

            // ── Панель-тогл ──
            collisionPanel = new Panel();
            collisionPanel.Size = new Size(300, 48);
            collisionPanel.Location = new Point(
                (this.ClientSize.Width - 300) / 2,
                collisionTitle.Bottom + 8);
            collisionPanel.BackColor = colorNormal;
            collisionPanel.Cursor = Cursors.Hand;
            collisionPanel.Click += collisionPanel_Click;

            collisionLbl = new Label();
            collisionLbl.Font = new Font("Segoe UI Emoji", 11F, FontStyle.Bold);
            collisionLbl.ForeColor = Color.White;
            collisionLbl.BackColor = Color.Transparent;
            collisionLbl.Dock = DockStyle.Fill;
            collisionLbl.TextAlign = ContentAlignment.MiddleCenter;
            collisionLbl.Cursor = Cursors.Hand;
            collisionLbl.Click += collisionPanel_Click;

            collisionPanel.Controls.Add(collisionLbl);
            this.Controls.Add(collisionPanel);

            // ── Сдвигаем кнопки "Готово" и "Назад" ниже если нужно ──
            int neededTop = collisionPanel.Bottom + 22;

            if (btn_gotov.Top < neededTop)
                btn_gotov.Location = new Point(btn_gotov.Left, neededTop);

            if (nazad_btn.Top < neededTop)
                nazad_btn.Location = new Point(nazad_btn.Left, neededTop);

            // Увеличиваем форму если кнопки вылезают за границы
            int neededHeight = Math.Max(btn_gotov.Bottom, nazad_btn.Bottom) + 20;
            if (this.ClientSize.Height < neededHeight)
                this.ClientSize = new Size(this.ClientSize.Width, neededHeight);
        }

        private void collisionPanel_Click(object sender, EventArgs e)
        {
            wallCollision = !wallCollision;
            RefreshCollisionHighlight();
        }

        private void RefreshCollisionHighlight()
        {
            if (collisionPanel == null) return;

            if (wallCollision)
            {
                collisionPanel.BackColor = Color.FromArgb(160, 30, 30);  // красный
                collisionLbl.Text = "🧱  СТЕНЫ УБИВАЮТ";
            }
            else
            {
                collisionPanel.BackColor = Color.FromArgb(0, 130, 70);   // зелёный
                collisionLbl.Text = "🌀  ПРОХОД СКВОЗЬ СТЕНЫ";
            }
        }

        // ── Выбор скина ───────────────────────────────────────────────
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

        // ── Выбор еды ─────────────────────────────────────────────────
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

        // ── Кнопка Готово ─────────────────────────────────────────────
        private void btn_gotov_Click(object sender, EventArgs e)
        {
            // Сохраняем выбор в статические поля
            nastoy.SelectedSkin = selectedSkin;
            nastoy.SelectedFood = selectedFood;
            nastoy.WallCollision = wallCollision;   // ← НОВОЕ

            // Показываем menu и закрываем настройки
            foreach (Form f in Application.OpenForms)
            {
                if (f is menu)
                {
                    f.Show();
                    break;
                }
            }

            this.Hide();
        }

        private void btn_gotov_MouseEnter(object sender, EventArgs e)
        {
            btn_gotov.Location = new Point(btn_gotov.Location.X, btn_gotov.Location.Y + 2);
        }

        private void btn_gotov_MouseLeave(object sender, EventArgs e)
        {
            btn_gotov.Location = new Point(btn_gotov.Location.X, btn_gotov.Location.Y - 2);
        }

        // ── Кнопка Назад ──────────────────────────────────────────────
        private void nazad_btn_Click(object sender, EventArgs e)
        {
            // НЕ сохраняем — просто возвращаемся
            foreach (Form f in Application.OpenForms)
            {
                if (f is menu)
                {
                    f.Show();
                    break;
                }
            }
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

        // ── Шапка ─────────────────────────────────────────────────────
        private void svernutbtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
