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
        // public static — доступен из Form1.cs
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

        // ── Drag ────────────────────────────────────────────────────────
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

        // ── Load ────────────────────────────────────────────────────────
        private void leadbordfrm_Load(object sender, EventArgs e)
        {
            nazad_btn.MouseEnter += (s, ev) =>
                nazad_btn.Location = new Point(nazad_btn.Location.X + 2, nazad_btn.Location.Y + 2);
            nazad_btn.MouseLeave += (s, ev) =>
                nazad_btn.Location = new Point(nazad_btn.Location.X - 2, nazad_btn.Location.Y - 2);

            lblTitle.Paint += new PaintEventHandler(lblTitle_Paint);

            LoadLeaderboard();
        }

        // ── Paint заголовка (свечение) ───────────────────────────────────
        private void lblTitle_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var shadowBrush = new SolidBrush(Color.FromArgb(120, 180, 0, 255)))
            {
                for (int dx = -2; dx <= 2; dx++)
                    for (int dy = -2; dy <= 2; dy++)
                        if (dx != 0 || dy != 0)
                            e.Graphics.DrawString(lblTitle.Text, lblTitle.Font, shadowBrush,
                                new RectangleF(dx, dy, lblTitle.Width, lblTitle.Height),
                                new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                });
            }

            using (var mainBrush = new SolidBrush(Color.FromArgb(255, 220, 255, 255)))
            {
                e.Graphics.DrawString(lblTitle.Text, lblTitle.Font, mainBrush,
                    new RectangleF(0, 0, lblTitle.Width, lblTitle.Height),
                    new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    });
            }
        }

        // ── Загрузка таблицы ────────────────────────────────────────────
        private void LoadLeaderboard()
        {
            flowPanel.Controls.Clear();

            var list = ReadFromFile();

            if (list.Count == 0)
            {
                Label empty = new Label
                {
                    Text = "Пока нет результатов...",
                    ForeColor = Color.Gray,
                    Font = new Font("Arial", 12, FontStyle.Italic),
                    AutoSize = false,
                    Width = flowPanel.Width,
                    Height = 40,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                flowPanel.Controls.Add(empty);
                return;
            }

            string[] medals = { "🥇", "🥈", "🥉" };

            for (int i = 0; i < list.Count; i++)
            {
                string medal = (i < 3) ? medals[i] : $"{i + 1}.";
                Color rowColor = (i % 2 == 0)
                    ? Color.FromArgb(40, 20, 70)
                    : Color.FromArgb(55, 30, 90);

                Panel row = new Panel
                {
                    Width = flowPanel.Width - 10,
                    Height = 36,
                    BackColor = rowColor,
                    Margin = new Padding(0, 2, 0, 2)
                };

                Label lMedal = new Label
                {
                    Text = medal,
                    Width = 50,
                    Height = 36,
                    Location = new Point(5, 0),
                    ForeColor = Color.Gold,
                    Font = new Font("Arial", 13, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lNick = new Label
                {
                    Text = list[i].Key,
                    Width = 200,
                    Height = 36,
                    Location = new Point(60, 0),
                    ForeColor = Color.White,
                    Font = new Font("Arial", 12),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                Label lScore = new Label
                {
                    Text = list[i].Value.ToString(),
                    Width = 100,
                    Height = 36,
                    Location = new Point(270, 0),
                    ForeColor = Color.LightGreen,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleRight
                };

                row.Controls.Add(lMedal);
                row.Controls.Add(lNick);
                row.Controls.Add(lScore);
                flowPanel.Controls.Add(row);
            }
        }

        // ── Чтение из файла ─────────────────────────────────────────────
        private static List<KeyValuePair<string, int>> ReadFromFile()
        {
            var list = new List<KeyValuePair<string, int>>();
            if (!File.Exists(SavePath)) return list;

            foreach (var line in File.ReadAllLines(SavePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 2 && int.TryParse(parts[1], out int s))
                    list.Add(new KeyValuePair<string, int>(parts[0], s));
            }
            return list.OrderByDescending(x => x.Value).ToList();
        }

        // ── Добавление результата ────────────────────────────────────────
        // Для каждого ника сохраняется ТОЛЬКО максимальный результат
        public static void AddScore(string nick, int score)
        {
            if (score <= 0) return;

            var list = ReadFromFile();

            // Ищем существующую запись для этого ника
            int idx = list.FindIndex(x =>
                string.Equals(x.Key, nick, StringComparison.OrdinalIgnoreCase));

            if (idx >= 0)
            {
                // Обновляем только если новый результат ЛУЧШЕ
                if (score <= list[idx].Value) return;
                list.RemoveAt(idx);
            }

            list.Add(new KeyValuePair<string, int>(nick, score));
            list = list.OrderByDescending(x => x.Value).Take(10).ToList();

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(SavePath));
                File.WriteAllLines(SavePath, list.Select(x => $"{x.Key}|{x.Value}"));
            }
            catch { }
        }
    }
}
