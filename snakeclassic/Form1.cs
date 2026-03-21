using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace snakeclassic
{
    public partial class Form1 : Form
    {
        // ── Сетка: строго фиксированные константы ─────────────────────
        private const int CellSize = 20;
        private const int Cols = 40;   // 800 / 20
        private const int Rows = 23;   // 460 / 20
        private const int GridW = Cols * CellSize; // 800
        private const int GridH = Rows * CellSize; // 460

        private List<Point> snake = new List<Point>();
        private Point food;
        private Direction direction = Direction.Right;
        private Direction nextDir = Direction.Right;
        private Random rand = new Random();
        private int score = 0;
        private bool gameOver = false;
        private bool paused = false;

        private Font emojiFont;
        private Font scoreFont;
        private StarField starField;
        private enum Direction { Up, Down, Left, Right }

        private static readonly Color[] HeadColors =
        {
            Color.FromArgb(0,   220,  60),  // 0 Зелёная
            Color.FromArgb(255, 140,   0),  // 1 Оранжевая
            Color.FromArgb(220,  30,  30),  // 2 Красная
            Color.FromArgb(0,   140, 255),  // 3 Синяя
        };
        private static readonly Color[] BodyColors =
        {
            Color.FromArgb(0,   180,  40),  // 0 Зелёная
            Color.FromArgb(210, 110,   0),  // 1 Оранжевая
            Color.FromArgb(180,  20,  20),  // 2 Красная
            Color.FromArgb(0,   100, 220),  // 3 Синяя
        };

        private static readonly string[] FoodEmojis = { "🍌", "🍎" };

        // ══════════════════════════════════════════════════════════════
        // Алмаз 💎
        // ══════════════════════════════════════════════════════════════
        private Point diamond;
        private bool diamondVisible = false;
        private Timer diamondSpawnTimer;
        private Timer diamondDespawnTimer;
        private Font diamondFont;

        // ══════════════════════════════════════════════════════════════
        // Частицы
        // ══════════════════════════════════════════════════════════════
        private List<Particle> particles = new List<Particle>();

        private class Particle
        {
            public float X, Y;
            public float VX, VY;
            public float Life;   // 1.0 → 0.0
            public Color Color;
            public int Size;
        }

        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            this.UpdateStyles();

            emojiFont = new Font("Segoe UI Emoji", 15f);
            scoreFont = new Font("Segoe UI", 14f, FontStyle.Bold);
            diamondFont = new Font("Segoe UI Emoji", 13f);

            diamondSpawnTimer = new Timer();
            diamondSpawnTimer.Interval = 10000;
            diamondSpawnTimer.Tick += DiamondSpawnTimer_Tick;

            diamondDespawnTimer = new Timer();
            diamondDespawnTimer.Interval = 5000;
            diamondDespawnTimer.Tick += DiamondDespawnTimer_Tick;

        }

        private void Form1_Load(object sender, EventArgs e) => StartGame();

        // ── Старт ────────────────────────────────────────────────────
        private void StartGame()
        {
            score = 0;
            gameOver = false;
            paused = false;
            direction = Direction.Right;
            nextDir = Direction.Right;
            snake.Clear();
            particles.Clear();
            starField = new StarField(GridW, GridH);
            diamondVisible = false;

            // Стартовая позиция строго на сетке
            int startCol = 15;
            int startRow = Rows / 2;
            for (int i = 4; i >= 0; i--)
                snake.Add(new Point((startCol + i) * CellSize, startRow * CellSize));

            GenerateFood();

            try
            {
                if (File.Exists(nicknamefrm.NickPath))
                {
                    string n = File.ReadAllText(nicknamefrm.NickPath).Trim();
                    lblNick.Text = string.IsNullOrEmpty(n) ? "Игрок" : n;
                }
                else lblNick.Text = "Игрок";
            }
            catch { lblNick.Text = "Игрок"; }

            lblLevel.Text = "Ур.1";
            gameTimer.Interval = 120;
            gameTimer.Start();

            diamondSpawnTimer.Interval = rand.Next(8000, 16000);
            diamondSpawnTimer.Start();
            diamondDespawnTimer.Stop();

            btnRestart.Visible = false;
            btnMenu.Visible = false;
            gamePanel.Invalidate();
            hudPanel.Invalidate();
        }

        // ── Генерация еды строго на сетке ────────────────────────────
        private void GenerateFood()
        {
            Point p;
            do
            {
                p = new Point(rand.Next(0, Cols) * CellSize,
                              rand.Next(0, Rows) * CellSize);
            }
            while (snake.Contains(p));
            food = p;
        }

        // ── Алмаз ────────────────────────────────────────────────────
        private void GenerateDiamond()
        {
            Point p;
            do
            {
                p = new Point(rand.Next(0, Cols) * CellSize,
                              rand.Next(0, Rows) * CellSize);
            }
            while (snake.Contains(p) || p == food);
            diamond = p;
        }

        private void DiamondSpawnTimer_Tick(object sender, EventArgs e)
        {
            diamondSpawnTimer.Stop();
            GenerateDiamond();
            diamondVisible = true;
            diamondDespawnTimer.Interval = rand.Next(4000, 7000);
            diamondDespawnTimer.Start();
            gamePanel.Invalidate();
        }

        private void DiamondDespawnTimer_Tick(object sender, EventArgs e)
        {
            diamondDespawnTimer.Stop();
            diamondVisible = false;
            diamondSpawnTimer.Interval = rand.Next(8000, 16000);
            diamondSpawnTimer.Start();
            gamePanel.Invalidate();
        }

        // ── Частицы ──────────────────────────────────────────────────
        private void SpawnParticles(Point pos, Color color, int count = 12)
        {
            for (int i = 0; i < count; i++)
            {
                float angle = (float)(rand.NextDouble() * Math.PI * 2);
                float speed = (float)(rand.NextDouble() * 3.5 + 1.0);
                particles.Add(new Particle
                {
                    X = pos.X + CellSize / 2f,
                    Y = pos.Y + CellSize / 2f,
                    VX = (float)Math.Cos(angle) * speed,
                    VY = (float)Math.Sin(angle) * speed,
                    Life = 1.0f,
                    Color = color,
                    Size = rand.Next(4, 9)
                });
            }
        }

        private void UpdateParticles()
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                var p = particles[i];
                p.X += p.VX;
                p.Y += p.VY;
                p.Life -= 0.06f;
                if (p.Life <= 0) particles.RemoveAt(i);
            }
        }

        // ── Игровой тик ──────────────────────────────────────────────
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (gameOver || paused) return;

            UpdateParticles();
            direction = nextDir;

            Point head = snake[0];
            Point newHead = head;

            switch (direction)
            {
                case Direction.Up: newHead.Y -= CellSize; break;
                case Direction.Down: newHead.Y += CellSize; break;
                case Direction.Left: newHead.X -= CellSize; break;
                case Direction.Right: newHead.X += CellSize; break;
            }

            // ── Проверка границ через КОНСТАНТЫ (не gamePanel.Width!) ──
            if (nastoy.WallCollision)
            {
                if (newHead.X < 0 || newHead.X >= GridW ||
                    newHead.Y < 0 || newHead.Y >= GridH)
                {
                    GameOver();
                    return;
                }
            }
            else
            {
                // Тороидальный мир — тоже через константы
                if (newHead.X < 0) newHead.X = GridW - CellSize;
                if (newHead.X >= GridW) newHead.X = 0;
                if (newHead.Y < 0) newHead.Y = GridH - CellSize;
                if (newHead.Y >= GridH) newHead.Y = 0;
            }

            // ── Столкновение с собой ──────────────────────────────────
            for (int i = 0; i < snake.Count - 1; i++)
            {
                if (newHead == snake[i])
                {
                    GameOver();
                    return;
                }
            }

            snake.Insert(0, newHead);

            bool ate = false;

            // ── Еда ──────────────────────────────────────────────────
            if (newHead == food)
            {
                score++;
                ate = true;
                int skin = nastoy.SelectedSkin;
                if (skin < 0 || skin >= HeadColors.Length) skin = 0;
                SpawnParticles(food, HeadColors[skin], 12);

                int level = score / 5 + 1;
                lblLevel.Text = $"Ур.{level}";
                if (level > 1)
                    gameTimer.Interval = Math.Max(40, 120 - (level - 1) * 10);

                GenerateFood();
                hudPanel.Invalidate();
            }

            // ── Алмаз ────────────────────────────────────────────────
            if (diamondVisible && newHead == diamond)
            {
                score += 5;
                ate = true;
                diamondVisible = false;
                diamondDespawnTimer.Stop();
                SpawnParticles(diamond, Color.FromArgb(0, 200, 255), 20);
                diamondSpawnTimer.Interval = rand.Next(8000, 16000);
                diamondSpawnTimer.Start();

                int level = score / 5 + 1;
                lblLevel.Text = $"Ур.{level}";
                if (level > 1)
                    gameTimer.Interval = Math.Max(40, 120 - (level - 1) * 10);

                hudPanel.Invalidate();
            }

            if (!ate)
                snake.RemoveAt(snake.Count - 1);

            gamePanel.Invalidate();
        }

        private void GameOver()
        {
            gameOver = true;
            gameTimer.Stop();
            diamondSpawnTimer.Stop();
            diamondDespawnTimer.Stop();
            diamondVisible = false;

            if (score >= 10)
            {
                string nick = "Игрок";
                try
                {
                    if (File.Exists(nicknamefrm.NickPath))
                    {
                        string n = File.ReadAllText(nicknamefrm.NickPath).Trim();
                        if (!string.IsNullOrEmpty(n)) nick = n;
                    }
                }
                catch { }
                leadbordfrm.AddScore(nick, score);
            }

            btnRestart.Visible = true;
            btnMenu.Visible = true;
            gamePanel.Invalidate();
        }

        // ── Клавиши ──────────────────────────────────────────────────
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameOver)
            {
                if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter) StartGame();
                else if (e.KeyCode == Keys.Escape) GoToMenu();
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    if (direction != Direction.Down) nextDir = Direction.Up; break;
                case Keys.Down:
                case Keys.S:
                    if (direction != Direction.Up) nextDir = Direction.Down; break;
                case Keys.Left:
                case Keys.A:
                    if (direction != Direction.Right) nextDir = Direction.Left; break;
                case Keys.Right:
                case Keys.D:
                    if (direction != Direction.Left) nextDir = Direction.Right; break;
                case Keys.Space:
                    paused = !paused;
                    gamePanel.Invalidate();
                    break;
                case Keys.Escape:
                    GoToMenu();
                    break;
            }
        }

        private void GoToMenu()
        {
            gameTimer.Stop();
            diamondSpawnTimer.Stop();
            diamondDespawnTimer.Stop();

            foreach (Form f in Application.OpenForms)
            {
                if (f is menu) { f.Show(); this.Hide(); return; }
            }
            menu m = new menu();
            m.Show();
            this.Hide();
        }

        private void btnRestart_Click(object sender, EventArgs e) => StartGame();
        private void btnMenu_Click(object sender, EventArgs e) => GoToMenu();

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        // ── HUD ──────────────────────────────────────────────────────
        private void hudPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            string emoji = FoodEmojis[nastoy.SelectedFood];
            string scoreText = score.ToString();

            SizeF emojiSize = g.MeasureString(emoji, emojiFont);
            SizeF scoreSize = g.MeasureString(scoreText, scoreFont);

            float gap = 4f;
            float totalWidth = emojiSize.Width + gap + scoreSize.Width;
            float startX = (hudPanel.Width - totalWidth) / 2f;
            float centerY = hudPanel.Height / 2f;

            g.DrawString(emoji, emojiFont, Brushes.White,
                startX, centerY - emojiSize.Height / 2f - 1f);

            using (SolidBrush scoreBrush = new SolidBrush(Color.FromArgb(255, 230, 0)))
                g.DrawString(scoreText, scoreFont, scoreBrush,
                    startX + emojiSize.Width + gap, centerY - scoreSize.Height / 2f);

            using (Pen pen = new Pen(Color.FromArgb(180, 0, 255), 2))
                g.DrawLine(pen, 0, hudPanel.Height - 1, hudPanel.Width, hudPanel.Height - 1);
        }

        // ── Игровое поле ─────────────────────────────────────────────
        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Фон
            g.Clear(Color.FromArgb(15, 5, 30));
            if (starField != null)
            {
                starField.Update();
                starField.Draw(g);
            }

            // Сетка
            using (Pen gridPen = new Pen(Color.FromArgb(25, 255, 255, 255)))
            {
                for (int x = 0; x <= GridW; x += CellSize)
                    g.DrawLine(gridPen, x, 0, x, GridH);
                for (int y = 0; y <= GridH; y += CellSize)
                    g.DrawLine(gridPen, 0, y, GridW, y);
            }

            // Еда — центрируем эмодзи внутри клетки
            int fi = nastoy.SelectedFood;
            if (fi < 0 || fi >= FoodEmojis.Length) fi = 0;
            g.DrawString(FoodEmojis[fi], emojiFont, Brushes.White,
                food.X - 2, food.Y - 2);

            // Алмаз
            if (diamondVisible)
                g.DrawString("💎", diamondFont, Brushes.White,
                    diamond.X - 1, diamond.Y - 1);

            // Частицы
            foreach (var p in particles)
            {
                int alpha = (int)(p.Life * 255);
                if (alpha < 0) alpha = 0;
                if (alpha > 255) alpha = 255;
                float drawSize = p.Size * p.Life;
                if (drawSize < 1f) continue;
                using (SolidBrush pb = new SolidBrush(Color.FromArgb(alpha, p.Color)))
                    g.FillEllipse(pb, p.X - drawSize / 2f, p.Y - drawSize / 2f,
                        drawSize, drawSize);
            }

            // Змейка
            int skin = nastoy.SelectedSkin;
            if (skin < 0 || skin >= HeadColors.Length) skin = 0;

            for (int i = snake.Count - 1; i >= 0; i--)
            {
                bool isHead = (i == 0);
                Color c = isHead ? HeadColors[skin] : BodyColors[skin];

                // Ровный прямоугольник — 1px отступ со всех сторон
                Rectangle rect = new Rectangle(
                    snake[i].X + 1,
                    snake[i].Y + 1,
                    CellSize - 2,
                    CellSize - 2);

                using (SolidBrush b = new SolidBrush(c))
                    g.FillRectangle(b, rect);

                using (Pen p = new Pen(Color.FromArgb(50, 0, 0, 0), 1))
                    g.DrawRectangle(p, rect);

                if (isHead)
                {
                    int hx = snake[i].X;
                    int hy = snake[i].Y;

                    int e1x, e1y, e2x, e2y;
                    int p1x, p1y, p2x, p2y;

                    switch (direction)
                    {
                        case Direction.Right:
                            e1x = hx + 12; e1y = hy + 4;
                            e2x = hx + 12; e2y = hy + 11;
                            p1x = hx + 14; p1y = hy + 5;
                            p2x = hx + 14; p2y = hy + 12;
                            break;
                        case Direction.Left:
                            e1x = hx + 3; e1y = hy + 4;
                            e2x = hx + 3; e2y = hy + 11;
                            p1x = hx + 3; p1y = hy + 5;
                            p2x = hx + 3; p2y = hy + 12;
                            break;
                        case Direction.Up:
                            e1x = hx + 4; e1y = hy + 3;
                            e2x = hx + 11; e2y = hy + 3;
                            p1x = hx + 5; p1y = hy + 3;
                            p2x = hx + 12; p2y = hy + 3;
                            break;
                        default: // Down
                            e1x = hx + 4; e1y = hy + 12;
                            e2x = hx + 11; e2y = hy + 12;
                            p1x = hx + 5; p1y = hy + 14;
                            p2x = hx + 12; p2y = hy + 14;
                            break;
                    }

                    using (SolidBrush black = new SolidBrush(Color.Black))
                    {
                        g.FillEllipse(black, e1x, e1y, 5, 5);
                        g.FillEllipse(black, e2x, e2y, 5, 5);
                    }
                    using (SolidBrush white = new SolidBrush(Color.White))
                    {
                        g.FillEllipse(white, p1x, p1y, 2, 2);
                        g.FillEllipse(white, p2x, p2y, 2, 2);
                    }
                }
            }

            // Пауза
            if (paused && !gameOver)
            {
                using (SolidBrush dim = new SolidBrush(Color.FromArgb(140, 0, 0, 0)))
                    g.FillRectangle(dim, 0, 0, GridW, GridH);

                using (Font f = new Font("Segoe UI", 28, FontStyle.Bold))
                {
                    string txt = "ПАУЗА";
                    SizeF sz = g.MeasureString(txt, f);
                    g.DrawString(txt, f, Brushes.White,
                        (GridW - sz.Width) / 2f, (GridH - sz.Height) / 2f);
                }
            }

            // Game Over
            if (gameOver)
            {
                using (SolidBrush dim = new SolidBrush(Color.FromArgb(170, 0, 0, 0)))
                    g.FillRectangle(dim, 0, 0, GridW, GridH);

                using (Font fBig = new Font("Segoe UI", 30, FontStyle.Bold))
                using (Font fMed = new Font("Segoe UI", 18, FontStyle.Bold))
                using (Font fSmall = new Font("Segoe UI", 11))
                {
                    float cx = GridW / 2f;
                    float cy = GridH / 2f;

                    string t1 = "ИГРА ОКОНЧЕНА";
                    string t2 = $"Счёт: {score}";
                    string t3 = score < 10
                        ? "Нужно хотя бы 10 очков для таблицы рекордов"
                        : "Результат сохранён! 🏆";
                    string t4 = "ПРОБЕЛ / ENTER — рестарт   ESC — меню";

                    SizeF s1 = g.MeasureString(t1, fBig);
                    SizeF s2 = g.MeasureString(t2, fMed);
                    SizeF s3 = g.MeasureString(t3, fSmall);
                    SizeF s4 = g.MeasureString(t4, fSmall);

                    g.DrawString(t1, fBig, Brushes.Yellow, cx - s1.Width / 2f, cy - 95);
                    g.DrawString(t2, fMed, Brushes.LightGreen, cx - s2.Width / 2f, cy - 45);
                    g.DrawString(t3, fSmall, Brushes.Plum, cx - s3.Width / 2f, cy + 5);
                    g.DrawString(t4, fSmall, Brushes.LightGray, cx - s4.Width / 2f, cy + 35);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            emojiFont?.Dispose();
            scoreFont?.Dispose();
            diamondFont?.Dispose();
            diamondSpawnTimer?.Dispose();
            diamondDespawnTimer?.Dispose();
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
    }
}
