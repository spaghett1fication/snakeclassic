using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace snakeclassic
{
    public partial class leadbordfrm : Form
    {
        // ✅ Путь рядом с .exe — работает у всех, папка всегда существует
        public static readonly string SavePath =
            Path.Combine(Application.StartupPath, "leaderboard.txt");

        public leadbordfrm()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
        }

        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        // ── Drag ──────────────────────────────────────────────────────
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        private void closebtn_Click(object sender, EventArgs e) => Application.Exit();
        private void svernutbtn_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

        private void nazad_btn_Click(object sender, EventArgs e)
        {
            menu form = new menu();
            form.Show();
            this.Hide();
        }

        // ── Load ──────────────────────────────────────────────────────
        private void leadbordfrm_Load(object sender, EventArgs e)
        {
            nazad_btn.MouseEnter += (s, ev) => nazad_btn.Location = new Point(nazad_btn.Location.X + 2, nazad_btn.Location.Y + 2);
            nazad_btn.MouseLeave += (s, ev) => nazad_btn.Location = new Point(nazad_btn.Location.X - 2, nazad_btn.Location.Y - 2);
            this.lblTitle.Paint += new PaintEventHandler(lblTitle_Paint);
            LoadLeaderboard();
        }

        // ── Paint заголовка ───────────────────────────────────────────
        private void lblTitle_Paint(object sender, PaintEventArgs e)
        {
            var lbl = sender as Label;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Фиолетовое свечение
            using (var shadowBrush = new SolidBrush(Color.FromArgb(120, 180, 0, 255)))
            {
                for (int dx = -2; dx <= 2; dx++)
                    for (int dy = -2; dy <= 2; dy++)
                        if (dx != 0 || dy != 0)
                            e.Graphics.DrawString(lbl.Text, lbl.Font, shadowBrush,
                                new RectangleF(dx, dy, lbl.Width, lbl.Height),
                                new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }

            // Основной розовый текст
            using (var mainBrush = new SolidBrush(Color.FromArgb(255, 20, 200)))
            {
                e.Graphics.DrawString(lbl.Text, lbl.Font, mainBrush,
                    new RectangleF(0, 0, lbl.Width, lbl.Height),
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
        }

        // ── Загрузка и отображение ────────────────────────────────────
        private void LoadLeaderboard()
        {
            tablePanel.Controls.Clear();

            var list = ReadFromFile();

            if (list.Count == 0)
            {
                var emptyLbl = new Label
                {
                    Text = "Пока нет результатов...",
                    ForeColor = Color.Gray,
                    Font = new Font("Segoe UI", 12, FontStyle.Italic),
                    AutoSize = false,
                    Width = tablePanel.Width - 10,
                    Height = 40,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                tablePanel.Controls.Add(emptyLbl);
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                var entry = list[i];
                int rank = i + 1;

                // Цвет строки
                Color rowColor;
                if (rank == 1) rowColor = Color.FromArgb(60, 50, 30, 0);
                else if (rank == 2) rowColor = Color.FromArgb(60, 40, 40, 40);
                else if (rank == 3) rowColor = Color.FromArgb(60, 40, 20, 0);
                else rowColor = (i % 2 == 0)
                    ? Color.FromArgb(40, 255, 255, 255)
                    : Color.FromArgb(20, 255, 255, 255);

                var row = new Panel
                {
                    Width = tablePanel.Width - 10,
                    Height = 36,
                    BackColor = rowColor,
                    Margin = new System.Windows.Forms.Padding(0, 2, 0, 2)
                };

                // Медаль / номер
                string medal = rank == 1 ? "🥇" : rank == 2 ? "🥈" : rank == 3 ? "🥉" : $"#{rank}";
                Color rankColor = rank == 1 ? Color.Gold : rank == 2 ? Color.Silver : rank == 3 ? Color.Peru : Color.LightGray;

                var lblRank = new Label
                {
                    Text = medal,
                    ForeColor = rankColor,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Location = new Point(8, 8),
                    AutoSize = true
                };

                var lblNick = new Label
                {
                    Text = entry.Key,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 11),
                    Location = new Point(55, 9),
                    AutoSize = true
                };

                var lblScore = new Label
                {
                    Text = entry.Value.ToString(),
                    ForeColor = Color.LightGreen,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Width = 80,
                    TextAlign = ContentAlignment.MiddleRight,
                    Location = new Point(row.Width - 90, 9),
                    AutoSize = false
                };

                row.Controls.Add(lblRank);
                row.Controls.Add(lblNick);
                row.Controls.Add(lblScore);
                tablePanel.Controls.Add(row);
            }
        }

        // ── Чтение из файла — PUBLIC STATIC ───────────────────────────
        public static List<KeyValuePair<string, int>> ReadFromFile()
        {
            var list = new List<KeyValuePair<string, int>>();
            if (!File.Exists(SavePath)) return list;

            foreach (var line in File.ReadAllLines(SavePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 2 && int.TryParse(parts[1], out int s))
                    list.Add(new KeyValuePair<string, int>(parts[0].Trim(), s));
            }
            return list.OrderByDescending(x => x.Value).ToList();
        }

        // ── Добавление результата — PUBLIC STATIC ─────────────────────
        // Сохраняет ТОЛЬКО максимальный результат для каждого ника
        public static void AddScore(string nick, int score)
        {
            if (score <= 0) return;

            var list = ReadFromFile();

            int existingIndex = list.FindIndex(x =>
                string.Equals(x.Key, nick, StringComparison.OrdinalIgnoreCase));

            if (existingIndex >= 0)
            {
                // Обновляем только если новый результат ЛУЧШЕ
                if (score <= list[existingIndex].Value) return;
                list[existingIndex] = new KeyValuePair<string, int>(nick, score);
            }
            else
            {
                list.Add(new KeyValuePair<string, int>(nick, score));
            }

            list = list.OrderByDescending(x => x.Value).Take(10).ToList();

            try
            {
                // Application.StartupPath всегда существует — Directory.CreateDirectory не нужен
                File.WriteAllLines(SavePath, list.Select(x => $"{x.Key}|{x.Value}"));
            }
            catch { /* молча игнорируем */ }
        }
    }
}
