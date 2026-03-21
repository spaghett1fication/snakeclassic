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
        private const int CellSize = 20;

        private List<Point> snake = new List<Point>();
        private Point food;
        private Direction direction = Direction.Right;
        private Direction nextDir = Direction.Right;
        private Random rand = new Random();
        private int score = 0;
        private bool gameOver = false;
        private bool paused = false;

        private Font emojiFont;

        private enum Direction { Up, Down, Left, Right }

        // ── Цвета змейки по скину (голова / тело) ─────────────────────
        private static readonly Color[] HeadColors = new Color[]
        {
            Color.FromArgb(0,   255, 80),   // 0 Зелёная
            Color.FromArgb(0,   150, 255),  // 1 Синяя
            Color.FromArgb(255, 140, 0),    // 2 Оранжевая
            Color.FromArgb(255, 50,  50),   // 3 Красная
        };
        private static readonly Color[] BodyColors = new Color[]
        {
            Color.FromArgb(0,   200, 50),   // 0 Зелёная
            Color.FromArgb(0,   100, 220),  // 1 Синяя
            Color.FromArgb(220, 100, 0),    // 2 Оранжевая
            Color.FromArgb(200, 30,  30),   // 3 Красная
        };

        // ── Стикеры еды ───────────────────────────────────────────────
        private static readonly string[] FoodEmojis = { "🍎", "🍌" };

        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            emojiFont = new Font("Segoe UI Emoji", 13f);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        // ── Старт игры ────────────────────────────────────────────────
        private void StartGame()
        {
            score = 0;
            gameOver = false;
            paused = false;
            direction = Direction.Right;
            nextDir = Direction.Right;
            snake.Clear();

            int startX = 15 * CellSize;
            int startY = 12 * CellSize;
            for (int i = 4; i >= 0; i--)
                snake.Add(new Point(startX + i * CellSize, startY));

            GenerateFood();

            // Никнейм
            try
            {
                if (File.Exists(nicknamefrm.NickPath))
                {
                    string n = File.ReadAllText(nicknamefrm.NickPath).Trim();
                    lblNick.Text = string.IsNullOrEmpty(n) ? "👤 Игрок" : "👤 " + n;
                }
                else lblNick.Text = "👤 Игрок";
            }
            catch { lblNick.Text = "👤 Игрок"; }

            UpdateScoreLabel();
            lblLevel.Text = "⚡ Ур.1";

            gameTimer.Interval = 120;
            gameTimer.Start();

            btnRestart.Visible = false;
            btnMenu.Visible = false;

            gamePanel.Invalidate();
        }

        private void GenerateFood()
        {
            int cols = gamePanel.Width / CellSize;
            int rows = gamePanel.Height / CellSize;
            do
            {
                food = new Point(rand.Next(0, cols) * CellSize,
                                 rand.Next(0, rows) * CellSize);
            }
            while (snake.Contains(food));
        }

        private void UpdateScoreLabel()
        {
            string icon = (nastoy.SelectedFood == 1) ? "🍌" : "🍎";
            lblScore.Text = $"{icon} {score}";
        }

        // ── Таймер ────────────────────────────────────────────────────
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (gameOver || paused) return;
            direction = nextDir;
            MoveSnake();
            gamePanel.Invalidate();
        }

        private void MoveSnake()
        {
            Point head = snake[0];
            Point newHead = head;

            switch (direction)
            {
                case Direction.Up: newHead.Y -= CellSize; break;
                case Direction.Down: newHead.Y += CellSize; break;
                case Direction.Left: newHead.X -= CellSize; break;
                case Direction.Right: newHead.X += CellSize; break;
            }

            // Столкновение со стеной
            if (newHead.X < 0 || newHead.X >= gamePanel.Width ||
                newHead.Y < 0 || newHead.Y >= gamePanel.Height)
            { GameOver(); return; }

            // Столкновение с собой
            for (int i = 0; i < snake.Count - 1; i++)
                if (snake[i] == newHead) { GameOver(); return; }

            snake.Insert(0, newHead);

            if (newHead == food)
            {
                score += 10;
                int level = score / 50 + 1;
                UpdateScoreLabel();
                lblLevel.Text = $"⚡ Ур.{level}";
                if (level > 1)
                    gameTimer.Interval = Math.Max(40, 120 - (level - 1) * 10);
                GenerateFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private void GameOver()
        {
            gameOver = true;
            gameTimer.Stop();

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

        // ── Клавиши ───────────────────────────────────────────────────
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
            menu m = new menu();
            m.Show();
            this.Hide();
        }

        private void btnRestart_Click(object sender, EventArgs e) => StartGame();
        private void btnMenu_Click(object sender, EventArgs e) => GoToMenu();

        // ── Шапка: перетаскивание ─────────────────────────────────────
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        // ── Отрисовка ─────────────────────────────────────────────────
        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Фон
            g.Clear(Color.FromArgb(15, 5, 30));

            // Сетка
            using (Pen gridPen = new Pen(Color.FromArgb(25, 255, 255, 255)))
            {
                for (int x = 0; x < gamePanel.Width; x += CellSize)
                    g.DrawLine(gridPen, x, 0, x, gamePanel.Height);
                for (int y = 0; y < gamePanel.Height; y += CellSize)
                    g.DrawLine(gridPen, 0, y, gamePanel.Width, y);
            }

            // ── Еда — стикер эмодзи ───────────────────────────────────
            string emoji = FoodEmojis[nastoy.SelectedFood];
            g.DrawString(emoji, emojiFont, Brushes.White, food.X - 1, food.Y - 2);

            // ── Змейка ────────────────────────────────────────────────
            int skin = Math.Max(0, Math.Min(nastoy.SelectedSkin, HeadColors.Length - 1));

            for (int i = snake.Count - 1; i >= 0; i--)
            {
                bool isHead = (i == 0);
                Color c = isHead ? HeadColors[skin] : BodyColors[skin];

                // Скруглённый прямоугольник тела
                Rectangle rect = new Rectangle(
                    snake[i].X + 1, snake[i].Y + 1,
                    CellSize - 2, CellSize - 2);

                using (SolidBrush b = new SolidBrush(c))
                    g.FillRectangle(b, rect);

                // Обводка
                using (Pen p = new Pen(Color.FromArgb(50, 0, 0, 0), 1))
                    g.DrawRectangle(p, rect);

                // Глаза на голове
                if (isHead)
                {
                    using (SolidBrush black = new SolidBrush(Color.Black))
                    {
                        g.FillEllipse(black, snake[i].X + 4, snake[i].Y + 4, 5, 5);
                        g.FillEllipse(black, snake[i].X + 11, snake[i].Y + 4, 5, 5);
                    }
                    using (SolidBrush white = new SolidBrush(Color.White))
                    {
                        g.FillEllipse(white, snake[i].X + 5, snake[i].Y + 5, 2, 2);
                        g.FillEllipse(white, snake[i].X + 12, snake[i].Y + 5, 2, 2);
                    }
                }
            }

            // ── Пауза ─────────────────────────────────────────────────
            if (paused && !gameOver)
            {
                using (SolidBrush dim = new SolidBrush(Color.FromArgb(140, 0, 0, 0)))
                    g.FillRectangle(dim, 0, 0, gamePanel.Width, gamePanel.Height);
                using (Font f = new Font("Segoe UI", 28, FontStyle.Bold))
                {
                    string txt = "⏸  ПАУЗА";
                    SizeF sz = g.MeasureString(txt, f);
                    g.DrawString(txt, f, Brushes.White,
                        (gamePanel.Width - sz.Width) / 2f,
                        (gamePanel.Height - sz.Height) / 2f);
                }
            }

            // ── Game Over ─────────────────────────────────────────────
            if (gameOver)
            {
                using (SolidBrush dim = new SolidBrush(Color.FromArgb(170, 0, 0, 0)))
                    g.FillRectangle(dim, 0, 0, gamePanel.Width, gamePanel.Height);

                using (Font fBig = new Font("Segoe UI", 30, FontStyle.Bold))
                using (Font fMed = new Font("Segoe UI", 18, FontStyle.Bold))
                using (Font fSmall = new Font("Segoe UI", 11))
                {
                    float cx = gamePanel.Width / 2f;
                    float cy = gamePanel.Height / 2f;

                    string t1 = "ИГРА ОКОНЧЕНА";
                    string t2 = $"Счёт: {score}";
                    string t3 = score < 10
                        ? "Нужно хотя бы 10 очков для таблицы рекордов"
                        : "Результат сохранён! 🏆";
                    string t4 = "ПРОБЕЛ / ENTER — рестарт     ESC — меню";

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
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }
    }
}
