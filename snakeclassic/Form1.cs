using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace snakeclassic
{
    public partial class Form1 : Form
    {
        // ── Константы ──────────────────────────────────────────────────
        private const int CellSize = 20;

        // ── Состояние игры ─────────────────────────────────────────────
        private List<Point> snake = new List<Point>();
        private Point food;
        private Direction direction = Direction.Right;
        private Direction nextDirection = Direction.Right;
        private Random rand = new Random();
        private int score = 0;
        private bool gameOver = false;
        private bool paused = false;

        private enum Direction { Up, Down, Left, Right }

        // ── Цвета змеек по скину (голова / тело) ──────────────────────
        private static readonly Color[] HeadColors = new Color[]
        {
            Color.FromArgb(0,   255,  80),   // 0 — Зелёная
            Color.FromArgb(0,   150, 255),   // 1 — Синяя
            Color.FromArgb(255, 140,   0),   // 2 — Оранжевая
            Color.FromArgb(255,  50,  50),   // 3 — Красная
        };
        private static readonly Color[] BodyColors = new Color[]
        {
            Color.FromArgb(0,   200,  50),   // 0 — Зелёная
            Color.FromArgb(0,   100, 220),   // 1 — Синяя
            Color.FromArgb(220, 100,   0),   // 2 — Оранжевая
            Color.FromArgb(200,  30,  30),   // 3 — Красная
        };

        // ── Стикеры еды ───────────────────────────────────────────────
        //    0 = Яблоко 🍎   1 = Банан 🍌
        private static readonly string[] FoodEmojis = { "🍎", "🍌" };

        // Шрифт для эмодзи-стикеров
        private Font emojiFont;

        // ── Конструктор ────────────────────────────────────────────────
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyDown += Form1_KeyDown;
            gameTimer.Tick += GameTimer_Tick;

            // Segoe UI Emoji — стандартный шрифт Windows для цветных эмодзи
            emojiFont = new Font("Segoe UI Emoji", 13f);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        // ── Запуск / рестарт игры ──────────────────────────────────────
        private void StartGame()
        {
            score = 0;
            gameOver = false;
            paused = false;
            direction = Direction.Right;
            nextDirection = Direction.Right;
            snake.Clear();

            int startX = 15 * CellSize;
            int startY = 12 * CellSize;
            for (int i = 4; i >= 0; i--)
                snake.Add(new Point(startX + i * CellSize, startY));

            GenerateFood();

            // Обновляем HUD
            try
            {
                if (File.Exists(nicknamefrm.NickPath))
                {
                    string nick = File.ReadAllText(nicknamefrm.NickPath).Trim();
                    lblNick.Text = string.IsNullOrEmpty(nick) ? "Игрок" : nick;
                }
                else lblNick.Text = "Игрок";
            }
            catch { lblNick.Text = "Игрок"; }

            lblScore.Text = "Счёт: 0";
            lblLevel.Text = "Ур. 1";
            gameTimer.Interval = 120;
            gameTimer.Start();
            btnRestart.Visible = false;
            btnMenu.Visible = false;
            this.Invalidate();
            gamePanel.Invalidate();
        }

        // ── Генерация еды ──────────────────────────────────────────────
        private void GenerateFood()
        {
            int cols = gamePanel.Width / CellSize;
            int rows = gamePanel.Height / CellSize;
            do
            {
                food = new Point(rand.Next(0, cols) * CellSize, rand.Next(0, rows) * CellSize);
            }
            while (snake.Contains(food));
        }

        // ── Таймер ─────────────────────────────────────────────────────
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (gameOver || paused) return;
            direction = nextDirection;
            MoveSnake();
            gamePanel.Invalidate();
        }

        // ── Движение змейки ────────────────────────────────────────────
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

            // Столкновение со стенами
            if (newHead.X < 0 || newHead.X >= gamePanel.Width ||
                newHead.Y < 0 || newHead.Y >= gamePanel.Height)
            { GameOver(); return; }

            // Столкновение с телом (кроме последнего сегмента — он уйдёт)
            for (int i = 0; i < snake.Count - 1; i++)
                if (snake[i] == newHead) { GameOver(); return; }

            snake.Insert(0, newHead);

            if (newHead == food)
            {
                score += 10;
                int level = score / 50 + 1;
                lblScore.Text = $"Счёт: {score}";
                lblLevel.Text = $"Ур. {level}";
                if (level > 1) gameTimer.Interval = Math.Max(40, 120 - (level - 1) * 10);
                GenerateFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        // ── Конец игры ─────────────────────────────────────────────────
        private void GameOver()
        {
            gameOver = true;
            gameTimer.Stop();

            // Сохраняем только если набрали хотя бы 10 очков
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

        // ── Управление ─────────────────────────────────────────────────
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
                    if (direction != Direction.Down) nextDirection = Direction.Up; break;
                case Keys.Down:
                case Keys.S:
                    if (direction != Direction.Up) nextDirection = Direction.Down; break;
                case Keys.Left:
                case Keys.A:
                    if (direction != Direction.Right) nextDirection = Direction.Left; break;
                case Keys.Right:
                case Keys.D:
                    if (direction != Direction.Left) nextDirection = Direction.Right; break;
                case Keys.Space:
                    paused = !paused;
                    gamePanel.Invalidate();
                    break;
                case Keys.Escape:
                    GoToMenu();
                    break;
            }
        }

        // ── Назад в меню ───────────────────────────────────────────────
        private void GoToMenu()
        {
            gameTimer.Stop();
            menu m = new menu();
            m.Show();
            this.Hide();
        }

        // ── Кнопки ─────────────────────────────────────────────────────
        private void btnRestart_Click(object sender, EventArgs e) => StartGame();
        private void btnMenu_Click(object sender, EventArgs e) => GoToMenu();

        // ── Отрисовка игрового поля ────────────────────────────────────
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
            // nastoy.SelectedFood: 0=🍎  1=🍌
            string emoji = FoodEmojis[nastoy.SelectedFood];
            g.DrawString(emoji, emojiFont, Brushes.White, food.X - 1, food.Y - 2);

            // ── Змейка — цвет зависит от nastoy.SelectedSkin ──────────
            int skin = nastoy.SelectedSkin;
            Color headColor = HeadColors[skin];
            Color bodyColor = BodyColors[skin];

            for (int i = 0; i < snake.Count; i++)
            {
                Color c = (i == 0) ? headColor : bodyColor;
                using (SolidBrush b = new SolidBrush(c))
                    g.FillRectangle(b, snake[i].X + 1, snake[i].Y + 1, CellSize - 2, CellSize - 2);

                g.DrawRectangle(Pens.Black, snake[i].X + 1, snake[i].Y + 1, CellSize - 2, CellSize - 2);

                // Глаза на голове
                if (i == 0)
                {
                    using (SolidBrush eye = new SolidBrush(Color.Black))
                    {
                        g.FillEllipse(eye, snake[i].X + 4, snake[i].Y + 4, 4, 4);
                        g.FillEllipse(eye, snake[i].X + 12, snake[i].Y + 4, 4, 4);
                    }
                    using (SolidBrush shine = new SolidBrush(Color.White))
                    {
                        g.FillEllipse(shine, snake[i].X + 5, snake[i].Y + 5, 2, 2);
                        g.FillEllipse(shine, snake[i].X + 13, snake[i].Y + 5, 2, 2);
                    }
                }
            }

            // ── Пауза ─────────────────────────────────────────────────
            if (paused && !gameOver)
            {
                using (SolidBrush dim = new SolidBrush(Color.FromArgb(140, 0, 0, 0)))
                    g.FillRectangle(dim, 0, 0, gamePanel.Width, gamePanel.Height);

                using (Font f = new Font("Arial", 28, FontStyle.Bold))
                {
                    string txt = "ПАУЗА";
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

                using (Font fBig = new Font("Arial", 30, FontStyle.Bold))
                using (Font fMed = new Font("Arial", 18, FontStyle.Bold))
                using (Font fSmall = new Font("Arial", 12))
                {
                    float cx = gamePanel.Width / 2f;
                    float cy = gamePanel.Height / 2f;

                    string txt1 = "ИГРА ОКОНЧЕНА";
                    string txt2 = $"Счёт: {score}";
                    string txt3 = (score < 10)
                        ? "Нужно набрать хотя бы 10 очков для рекорда"
                        : "Результат сохранён в таблице лидеров!";
                    string txt4 = "ПРОБЕЛ / ENTER — рестарт   ESC — меню";

                    SizeF s1 = g.MeasureString(txt1, fBig);
                    SizeF s2 = g.MeasureString(txt2, fMed);
                    SizeF s3 = g.MeasureString(txt3, fSmall);
                    SizeF s4 = g.MeasureString(txt4, fSmall);

                    g.DrawString(txt1, fBig, Brushes.Yellow, cx - s1.Width / 2f, cy - 90);
                    g.DrawString(txt2, fMed, Brushes.LightGreen, cx - s2.Width / 2f, cy - 40);
                    g.DrawString(txt3, fSmall, Brushes.Plum, cx - s3.Width / 2f, cy + 10);
                    g.DrawString(txt4, fSmall, Brushes.LightGray, cx - s4.Width / 2f, cy + 40);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            emojiFont?.Dispose();
            base.Dispose(disposing);
        }
    }
}
