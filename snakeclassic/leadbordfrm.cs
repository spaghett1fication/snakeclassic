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
        // ── Путь к файлу — публичный static ───────────────────────────────
        public static readonly string SavePath =
            Path.Combine(@"D:\Visual Studio\course\snakeclassic", "leaderboard.txt");

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

        // ── Drag ──────────────────────────────────────────────────────────
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

        // ── Load ──────────────────────────────────────────────────────────
        private void leadbordfrm_Load(object sender, EventArgs e)
        {
            nazad_btn.MouseEnter += (s, ev) =>
                nazad_btn.Location = new Point(nazad_btn.Location.X + 2, nazad_btn.Location.Y + 2);
            nazad_btn.MouseLeave += (s, ev) =>
                nazad_btn.Location = new Point(nazad_btn.Location.X - 2, nazad_btn.Location.Y - 2);

            this.lblTitle.Paint += new PaintEventHandler(lblTitle_Paint);
            LoadLeaderboard();
        }

        // ── Paint заголовка ───────────────────────────────────────────────
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

            // Основной текст — розовый
            using (var mainBrush = new SolidBrush(Color.FromArgb(255, 80, 220)))
                e.Graphics.DrawString(lbl.Text, lbl.Font, mainBrush,
                    new RectangleF(0, 0, lbl.Width, lbl.Height),
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        // ── Загрузка таблицы в tablePanel ────────────────────────────────
        private void LoadLeaderboard()
        {
            tablePanel.Controls.Clear();

            var list = ReadFromFile();

            if (list.Count == 0)
            {
                var emptyLbl = new Label
                {
                    Text = "Пока нет результатов...",
                    ForeColor = Color.FromArgb(180, 180, 180),
                    Font = new Font("Segoe UI", 12f, FontStyle.Italic),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(420, 50),
                    BackColor = Color.Transparent
                };
                tablePanel.Controls.Add(emptyLbl);
                return;
            }

            string[] medals = { "🥇", "🥈", "🥉" };

            for (int i = 0; i < list.Count; i++)
            {
                var entry = list[i];

                var row = new Panel
                {
                    Size = new Size(416, 36),
                    BackColor = i % 2 == 0
                        ? Color.FromArgb(60, 40, 100)
                        : Color.FromArgb(45, 25, 80),
                    Margin = new Padding(0, 0, 0, 2)
                };

                // Золото/серебро/бронза для топ-3
                if (i < 3)
                    row.BackColor = i == 0 ? Color.FromArgb(80, 60, 0)
                                 : i == 1 ? Color.FromArgb(50, 50, 60)
                                          : Color.FromArgb(60, 35, 15);

                // Место
                var lblPlace = new Label
                {
                    Text = i < 3 ? medals[i] : $"{i + 1}.",
                    Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                    ForeColor = i == 0 ? Color.Gold
                              : i == 1 ? Color.Silver
                              : i == 2 ? Color.FromArgb(205, 127, 50)
                              : Color.White,
                    Location = new Point(6, 0),
                    Size = new Size(52, 36),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                // Ник
                var lblNick = new Label
                {
                    Text = entry.Key,
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.White,
                    Location = new Point(60, 0),
                    Size = new Size(220, 36),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                // Очки
                var lblScore = new Label
                {
                    Text = entry.Value.ToString(),
                    Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(100, 255, 150),
                    Location = new Point(288, 0),
                    Size = new Size(122, 36),
                    TextAlign = ContentAlignment.MiddleRight
                };

                row.Controls.Add(lblPlace);
                row.Controls.Add(lblNick);
                row.Controls.Add(lblScore);
                tablePanel.Controls.Add(row);
            }
        }

        // ── Чтение из файла — PUBLIC STATIC ──────────────────────────────
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

        // ── Добавление результата — PUBLIC STATIC ────────────────────────
        // Сохраняет ТОЛЬКО максимальный результат для каждого ника
        public static void AddScore(string nick, int score)
        {
            if (score < 10) return; // минимум 10 очков для записи

            var list = ReadFromFile();

            int existingIndex = list.FindIndex(x =>
                string.Equals(x.Key, nick, StringComparison.OrdinalIgnoreCase));

            if (existingIndex >= 0)
            {
                // Обновляем только если новый результат ЛУЧШЕ
                if (score > list[existingIndex].Value)
                {
                    list[existingIndex] = new KeyValuePair<string, int>(nick, score);
                }
                else
                {
                    return; // Результат хуже — не сохраняем
                }
            }
            else
            {
                // Новый игрок
                list.Add(new KeyValuePair<string, int>(nick, score));
            }

            list = list.OrderByDescending(x => x.Value).Take(10).ToList();

            try
            {
                string dir = Path.GetDirectoryName(SavePath);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                File.WriteAllLines(SavePath, list.Select(x => $"{x.Key}|{x.Value}"));
            }
            catch { /* молча игнорируем */ }
        }
    }
}
