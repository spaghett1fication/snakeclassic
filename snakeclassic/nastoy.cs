using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace snakeclassic
{
    public partial class nastoy : Form
    {
        // ── Публичные статические поля ────────────────────────────────
        public static int SelectedSkin = 0;   // 0=Зелёная 1=Оранжевая 2=Красная 3=Синяя
        public static int SelectedFood = 0;   // 0=Банан   1=Яблоко
        public static bool WallCollision = true;

        // Локальные для UI
        private int selectedSkin = 0;
        private int selectedFood = 0;
        private bool wallCollision = true;

        private readonly Color colorNormal = Color.FromArgb(60, 20, 100);
        private readonly Color colorSelected = Color.FromArgb(120, 40, 200);

        private Panel[] skinPanels;
        private Panel[] foodPanels;

        // ── Контрол коллизии (создаётся программно) ───────────────────
        private Label collisionTitle;
        private CheckBox collisionCheck;   // ← вместо Panel-кнопки

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

            selectedSkin = nastoy.SelectedSkin;
            selectedFood = nastoy.SelectedFood;
            wallCollision = nastoy.WallCollision;

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

            CreateCollisionSection();
        }

        // ══════════════════════════════════════════════════════════════
        //  Секция «Коллизия» с кастомным CheckBox
        // ══════════════════════════════════════════════════════════════
        private void CreateCollisionSection()
        {
            // Нижний край панелей еды
            int foodBottom = 0;
            foreach (Panel p in foodPanels)
                if (p.Bottom > foodBottom) foodBottom = p.Bottom;

            // ── Заголовок ──────────────────────────────────────────
            collisionTitle = new Label
            {
                Text = "КОЛЛИЗИЯ СО СТЕНАМИ",
                Font = new Font("Arial", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Size = new Size(640, 26),
                Location = new Point(0, foodBottom + 18),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(collisionTitle);
            collisionTitle.BringToFront();

            // ── CustomCheckBox ─────────────────────────────────────
            //  Используем стандартный CheckBox с OwnerDraw — рисуем
            //  в стиле фиолетово-тёмного UI формы настроек.
            collisionCheck = new CheckBox
            {
                Text = "",            // текст рисуем сами в Paint
                Checked = wallCollision,
                FlatStyle = FlatStyle.Flat,
                Appearance = Appearance.Button,   // выглядит как кнопка-тогл
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(300, 48),
                Location = new Point((this.ClientSize.Width - 300) / 2,
                                      collisionTitle.Bottom + 8),
                Cursor = Cursors.Hand,
                BackColor = wallCollision
                    ? Color.FromArgb(160, 30, 30)
                    : Color.FromArgb(0, 130, 70),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Emoji", 11F, FontStyle.Bold),
                UseVisualStyleBackColor = false,
            };
            // Убираем рамку стандартного Button-appearance
            collisionCheck.FlatAppearance.BorderSize = 0;
            collisionCheck.FlatAppearance.CheckedBackColor = Color.FromArgb(160, 30, 30);
            collisionCheck.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 30, 140);

            // Рисуем содержимое сами через Paint
            collisionCheck.Paint += CollisionCheck_Paint;
            collisionCheck.CheckedChanged += CollisionCheck_CheckedChanged;

            this.Controls.Add(collisionCheck);
            collisionCheck.BringToFront();

            // ── Сдвигаем кнопки «Готово» / «Назад» ───────────────
            int neededTop = collisionCheck.Bottom + 22;
            if (btn_gotov.Top < neededTop)
                btn_gotov.Location = new Point(btn_gotov.Left, neededTop);
            if (nazad_btn.Top < neededTop)
                nazad_btn.Location = new Point(nazad_btn.Left, neededTop);

            int neededHeight = Math.Max(btn_gotov.Bottom, nazad_btn.Bottom) + 20;
            if (this.ClientSize.Height < neededHeight)
                this.ClientSize = new Size(this.ClientSize.Width, neededHeight);
        }

        // ── Рисуем кастомный CheckBox ─────────────────────────────────
        private void CollisionCheck_Paint(object sender, PaintEventArgs e)
        {
            var cb = (CheckBox)sender;
            bool on = cb.Checked;           // on = стены убивают
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Фон уже задан через BackColor, просто рисуем содержимое

            // ── Круглый индикатор (radio-style) слева ─────────────
            int cx = 22, cy = cb.Height / 2;
            int r = 11;
            using (Pen outerPen = new Pen(Color.White, 2f))
                g.DrawEllipse(outerPen, cx - r, cy - r, r * 2, r * 2);

            if (on)
            {
                // Красная заливка — стены убивают
                using (SolidBrush fill = new SolidBrush(Color.FromArgb(255, 80, 80)))
                    g.FillEllipse(fill, cx - r + 3, cy - r + 3, (r - 3) * 2, (r - 3) * 2);
            }
            else
            {
                // Зелёная заливка — проход сквозь стены
                using (SolidBrush fill = new SolidBrush(Color.FromArgb(60, 220, 120)))
                    g.FillEllipse(fill, cx - r + 3, cy - r + 3, (r - 3) * 2, (r - 3) * 2);
            }

            // ── Текст справа от индикатора ─────────────────────────
            string text = on ? "🧱  СТЕНЫ УБИВАЮТ" : "🌀  ПРОХОД СКВОЗЬ СТЕНЫ";
            using (var tf = new Font("Segoe UI Emoji", 11F, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(Color.White))
            {
                var sf = new StringFormat { LineAlignment = StringAlignment.Center };
                var rect = new RectangleF(cx + r + 6, 0,
                                            cb.Width - (cx + r + 6) - 4,
                                            cb.Height);
                g.DrawString(text, tf, textBrush, rect, sf);
            }
        }

        private void CollisionCheck_CheckedChanged(object sender, EventArgs e)
        {
            wallCollision = collisionCheck.Checked;
            collisionCheck.BackColor = wallCollision
                ? Color.FromArgb(160, 30, 30)
                : Color.FromArgb(0, 130, 70);
            collisionCheck.FlatAppearance.CheckedBackColor = collisionCheck.BackColor;
            collisionCheck.Invalidate();
        }

        // ── Скин ─────────────────────────────────────────────────────
        private void skinPanel_Click(object sender, EventArgs e)
        {
            Control src = sender as Control;
            Panel clicked = (src is Panel) ? (Panel)src : src?.Parent as Panel;
            if (clicked == null) return;

            for (int i = 0; i < skinPanels.Length; i++)
            {
                if (skinPanels[i] == clicked) { selectedSkin = i; break; }
            }
            RefreshSkinHighlight();
        }

        private void RefreshSkinHighlight()
        {
            for (int i = 0; i < skinPanels.Length; i++)
                skinPanels[i].BackColor = (i == selectedSkin) ? colorSelected : colorNormal;
        }

        // ── Еда ──────────────────────────────────────────────────────
        private void foodPanel_Click(object sender, EventArgs e)
        {
            Control src = sender as Control;
            Panel clicked = (src is Panel) ? (Panel)src : src?.Parent as Panel;
            if (clicked == null) return;

            for (int i = 0; i < foodPanels.Length; i++)
            {
                if (foodPanels[i] == clicked) { selectedFood = i; break; }
            }
            RefreshFoodHighlight();
        }

        private void RefreshFoodHighlight()
        {
            for (int i = 0; i < foodPanels.Length; i++)
                foodPanels[i].BackColor = (i == selectedFood) ? colorSelected : colorNormal;
        }

        // ── Готово ───────────────────────────────────────────────────
        private void btn_gotov_Click(object sender, EventArgs e)
        {
            nastoy.SelectedSkin = selectedSkin;
            nastoy.SelectedFood = selectedFood;
            nastoy.WallCollision = wallCollision;

            foreach (Form f in Application.OpenForms)
            {
                if (f is menu) { f.Show(); this.Hide(); return; }
            }
            menu m = new menu();
            m.Show();
            this.Hide();
        }

        // ── Назад ────────────────────────────────────────────────────
        private void nazad_btn_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is menu) { f.Show(); this.Hide(); return; }
            }
            menu m = new menu();
            m.Show();
            this.Hide();
        }

        // ── Закрыть / Свернуть ───────────────────────────────────────
        private void closebtn_Click(object sender, EventArgs e) => Application.Exit();
        private void svernutbtn_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }
    }
}
