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
        private static readonly string SavePath =
            @"D:\Visual Studio\course\snakeclassic\leaderboard.txt";

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

            LoadLeaderboard();
        }

        // ── Paint заголовка (свечение) — вынесен из Designer ─────────────
        private void lblTitle_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Фиолетовое свечение (тень)
            using (var shadowBrush = new SolidBrush(Color.FromArgb(120, 180, 0, 255)))
            {
                for (int dx = -2; dx <= 2; dx++)
                    for (int dy = -2; dy <= 2; dy++)
                        if (dx != 0 || dy != 0)
                            e.Graphics.DrawString(
                                lblTitle.Text, lblTitle.Font, shadowBrush,
                                new RectangleF(dx, dy, lblTitle.Width, lblTitle.Height),
                                new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                });
            }

            // Основной текст
            using (var mainBrush = new SolidBrush(Color.FromArgb(255, 80, 220)))
            {
                e.Graphics.DrawString(
                    lblTitle.Text, lblTitle.Font, mainBrush,
                    new RectangleF(0, 0, lblTitle.Width, lblTitle.Height),
                    new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    });
            }
        }

        // ── Загрузка таблицы из txt ───────────────────────────────────────
        private void LoadLeaderboard()
        {
            var entries = ReadFromFile();

            tablePanel.Controls.Clear();

            if (entries.Count == 0)
            {
                var empty = new Label
                {
                    Text = "Пока нет результатов.\nСыграй в игру!",
                    ForeColor = Color.FromArgb(200, 180, 255),
                    Font = new Font("Segoe UI", 11, FontStyle.Italic),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(420, 60)
                };
                tablePanel.Controls.Add(empty);
                return;
            }

            for (int i = 0; i < entries.Count; i++)
            {
                int place = i + 1;
                string nick = entries[i].Key;
                int score = entries[i].Value;

                var row = new Panel
                {
                    Size = new Size(418, 34),
                    BackColor = Color.Transparent
                };

                // Фон строки
                Color rowColor = place == 1
                    ? Color.FromArgb(60, 255, 215, 0)
                    : place == 2
                        ? Color.FromArgb(50, 192, 192, 192)
                        : place == 3
                            ? Color.FromArgb(50, 205, 127, 50)
                            : i % 2 == 0
                                ? Color.FromArgb(30, 150, 100, 220)
                                : Color.FromArgb(15, 100, 60, 180);

                row.BackColor = rowColor;

                // Медаль / номер
                var lblPlace = new Label
                {
                    Text = place == 1 ? "🥇" : place == 2 ? "🥈" : place == 3 ? "🥉" : $"#{place}",
                    ForeColor = place <= 3 ? Color.FromArgb(255, 215, 0) : Color.White,
                    Font = new Font("Segoe UI", 10, place <= 3 ? FontStyle.Bold : FontStyle.Regular),
                    Location = new Point(6, 0),
                    Size = new Size(56, 34),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                // Ник
                var lblNick = new Label
                {
                    Text = nick,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    Location = new Point(62, 0),
                    Size = new Size(220, 34),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                // Очки
                var lblScore = new Label
                {
                    Text = score.ToString(),
                    ForeColor = Color.FromArgb(100, 255, 130),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(290, 0),
                    Size = new Size(124, 34),
                    TextAlign = ContentAlignment.MiddleRight
                };

                row.Controls.Add(lblPlace);
                row.Controls.Add(lblNick);
                row.Controls.Add(lblScore);
                tablePanel.Controls.Add(row);
            }
        }

        // ── Чтение из файла ───────────────────────────────────────────────
        private static List<KeyValuePair<string, int>> ReadFromFile()
        {
            var list = new List<KeyValuePair<string, int>>();

            if (!File.Exists(SavePath))
                return list;

            foreach (var line in File.ReadAllLines(SavePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 2 && int.TryParse(parts[1], out int s))
                    list.Add(new KeyValuePair<string, int>(parts[0], s));
            }

            return list.OrderByDescending(x => x.Value).ToList();
        }

        // ── Добавление результата (вызывать из Form1 после Game Over) ─────
        public static void AddScore(string nick, int score)
        {
            var list = ReadFromFile();

            list.Add(new KeyValuePair<string, int>(nick, score));
            list = list.OrderByDescending(x => x.Value).Take(10).ToList();

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(SavePath));
                File.WriteAllLines(SavePath, list.Select(x => $"{x.Key}|{x.Value}"));
            }
            catch { /* молча игнорируем ошибки записи */ }
        }
    }
}
