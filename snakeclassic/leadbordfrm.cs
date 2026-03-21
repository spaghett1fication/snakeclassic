using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            this.Hide();
        }

        private void leadbordfrm_Load(object sender, EventArgs e)
        {
            nazad_btn.MouseEnter += (s, ev) =>
                nazad_btn.Location = new Point(nazad_btn.Location.X + 2, nazad_btn.Location.Y + 2);
            nazad_btn.MouseLeave += (s, ev) =>
                nazad_btn.Location = new Point(nazad_btn.Location.X - 2, nazad_btn.Location.Y - 2);

            LoadLeaderboard();
        }

        // ── Загрузка таблицы ──────────────────────────────────────────────
        private void LoadLeaderboard()
        {
            // Читаем сохранённые результаты из Settings
            // Формат строки: "Ник:Очки;Ник:Очки;..."
            string raw = "";
            try { raw = Properties.Settings.Default.LeaderboardData ?? ""; }
            catch { raw = ""; }

            var entries = ParseLeaderboard(raw);

            // Очищаем старые строки
            tablePanel.Controls.Clear();

            if (entries.Count == 0)
            {
                // Пусто — показываем заглушку
                Label empty = new Label();
                empty.Text = "Пока нет результатов.\nСыграй в игру!";
                empty.ForeColor = Color.FromArgb(200, 180, 255);
                empty.Font = new Font("Segoe UI", 11, FontStyle.Italic);
                empty.TextAlign = ContentAlignment.MiddleCenter;
                empty.Dock = DockStyle.Fill;
                tablePanel.Controls.Add(empty);
                return;
            }

            // Рисуем строки
            for (int i = 0; i < Math.Min(entries.Count, 10); i++)
            {
                AddRow(i + 1, entries[i].Key, entries[i].Value);
            }
        }

        private void AddRow(int place, string nick, int score)
        {
            Panel row = new Panel();
            row.Size = new Size(tablePanel.Width - 10, 36);
            row.Margin = new Padding(0, 0, 0, 4);

            // Фон строки — чередование
            row.BackColor = (place % 2 == 0)
                ? Color.FromArgb(50, 255, 255, 255)
                : Color.FromArgb(30, 255, 255, 255);

            // Скруглённые углы через Paint
            row.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    row.ClientRectangle,
                    place == 1 ? Color.FromArgb(80, 255, 215, 0) :
                    place == 2 ? Color.FromArgb(60, 192, 192, 192) :
                    place == 3 ? Color.FromArgb(60, 205, 127, 50) :
                                 Color.FromArgb(40, 150, 100, 220),
                    Color.Transparent,
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, row.ClientRectangle);
                }
            };

            // Место
            Label lblPlace = new Label();
            lblPlace.Text = place == 1 ? "🥇" : place == 2 ? "🥈" : place == 3 ? "🥉" : $"#{place}";
            lblPlace.ForeColor = place <= 3 ? Color.Gold : Color.FromArgb(200, 180, 255);
            lblPlace.Font = new Font("Segoe UI", place <= 3 ? 13 : 11, FontStyle.Bold);
            lblPlace.Size = new Size(50, 36);
            lblPlace.Location = new Point(6, 0);
            lblPlace.TextAlign = ContentAlignment.MiddleCenter;

            // Ник
            Label lblNick = new Label();
            lblNick.Text = nick;
            lblNick.ForeColor = Color.White;
            lblNick.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblNick.Size = new Size(180, 36);
            lblNick.Location = new Point(62, 0);
            lblNick.TextAlign = ContentAlignment.MiddleLeft;

            // Очки
            Label lblScore = new Label();
            lblScore.Text = score.ToString("N0").Replace(",", " ");
            lblScore.ForeColor = Color.FromArgb(255, 220, 80);
            lblScore.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblScore.Size = new Size(110, 36);
            lblScore.Location = new Point(248, 0);
            lblScore.TextAlign = ContentAlignment.MiddleRight;

            row.Controls.Add(lblPlace);
            row.Controls.Add(lblNick);
            row.Controls.Add(lblScore);

            tablePanel.Controls.Add(row);
        }

        // ── Парсинг / сохранение ──────────────────────────────────────────
        private List<KeyValuePair<string, int>> ParseLeaderboard(string raw)
        {
            var list = new List<KeyValuePair<string, int>>();
            if (string.IsNullOrWhiteSpace(raw)) return list;

            foreach (var entry in raw.Split(';'))
            {
                var parts = entry.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1], out int s))
                    list.Add(new KeyValuePair<string, int>(parts[0], s));
            }

            return list.OrderByDescending(x => x.Value).ToList();
        }

        /// <summary>
        /// Вызывать из Form1 после окончания игры: LeaderboardHelper.AddScore(nick, score)
        /// </summary>
        public static void AddScore(string nick, int score)
        {
            string raw = "";
            try { raw = Properties.Settings.Default.LeaderboardData ?? ""; }
            catch { }

            var list = new List<KeyValuePair<string, int>>();
            if (!string.IsNullOrWhiteSpace(raw))
            {
                foreach (var entry in raw.Split(';'))
                {
                    var parts = entry.Split(':');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int s))
                        list.Add(new KeyValuePair<string, int>(parts[0], s));
                }
            }

            list.Add(new KeyValuePair<string, int>(nick, score));
            list = list.OrderByDescending(x => x.Value).Take(10).ToList();

            string saved = string.Join(";", list.Select(x => $"{x.Key}:{x.Value}"));
            Properties.Settings.Default.LeaderboardData = saved;
            Properties.Settings.Default.Save();
        }
    }
}
