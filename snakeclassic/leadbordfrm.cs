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
        // ── Путь к файлу — публичный static ─────────────────────────────
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
            nazad_btn.MouseEnter += (s, ev) => nazad_btn.Location = new Point(nazad_btn.Location.X + 2, nazad_btn.Location.Y + 2);
            nazad_btn.MouseLeave += (s, ev) => nazad_btn.Location = new Point(nazad_btn.Location.X - 2, nazad_btn.Location.Y - 2);

            this.lblTitle.Paint += new PaintEventHandler(lblTitle_Paint);

            LoadLeaderboard();
        }

        // ── Paint заголовка ─────────────────────────────────────────────
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

            // Основной текст — белый
            using (var mainBrush = new SolidBrush(Color.White))
            {
                e.Graphics.DrawString(lbl.Text, lbl.Font, mainBrush,
                    new RectangleF(0, 0, lbl.Width, lbl.Height),
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
        }

        // ── Загрузка таблицы в listView ─────────────────────────────────
        private void LoadLeaderboard()
        {
            leaderView.Items.Clear();
            var list = ReadFromFile();

            if (list.Count == 0)
            {
                var empty = new ListViewItem("—");
                empty.SubItems.Add("Пока нет результатов...");
                empty.SubItems.Add("—");
                empty.ForeColor = Color.Gray;
                leaderView.Items.Add(empty);
                return;
            }

            string[] medals = { "🥇", "🥈", "🥉" };

            for (int i = 0; i < list.Count; i++)
            {
                string place = (i < 3) ? medals[i] : $"  {i + 1}.";
                var item = new ListViewItem(place);
                item.SubItems.Add(list[i].Key);
                item.SubItems.Add(list[i].Value.ToString());

                // Топ-3 — особые цвета
                if (i == 0) item.ForeColor = Color.Gold;
                else if (i == 1) item.ForeColor = Color.Silver;
                else if (i == 2) item.ForeColor = Color.FromArgb(205, 127, 50);
                else item.ForeColor = Color.LightCyan;

                // Чередующийся фон
                item.BackColor = (i % 2 == 0)
                    ? Color.FromArgb(40, 15, 80)
                    : Color.FromArgb(55, 20, 100);

                leaderView.Items.Add(item);
            }
        }

        // ── Чтение файла — PUBLIC STATIC ────────────────────────────────
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

        // ── Добавление результата — PUBLIC STATIC ───────────────────────
        // Сохраняет ТОЛЬКО максимальный результат для каждого ника
        public static void AddScore(string nick, int score)
        {
            if (score <= 0) return;

            var list = ReadFromFile();

            // Ищем запись с таким же ником
            int existingIndex = list.FindIndex(x =>
                string.Equals(x.Key, nick, StringComparison.OrdinalIgnoreCase));

            if (existingIndex >= 0)
            {
                // Обновляем только если новый результат ЛУЧШЕ
                if (score <= list[existingIndex].Value) return;
                list.RemoveAt(existingIndex);
            }

            list.Add(new KeyValuePair<string, int>(nick, score));
            list = list.OrderByDescending(x => x.Value).Take(10).ToList();

            try
            {
                string dir = Path.GetDirectoryName(SavePath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                File.WriteAllLines(SavePath, list.Select(x => $"{x.Key}|{x.Value}"));
            }
            catch { /* молча игнорируем */ }
        }
    }
}
