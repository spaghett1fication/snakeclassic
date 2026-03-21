using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace snakeclassic
{
    public partial class Form1 : Form
    {
        // Размер одной клетки
        private const int CellSize = 20;

        // Змейка — список сегментов (голова первая)
        private List<Point> snake = new List<Point>();
        private Point food;
        private Direction direction = Direction.Right;
        private Random rand = new Random();
        private int score = 0;
        private bool gameOver = false;

        enum Direction { Up, Down, Left, Right }

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyDown += Form1_KeyDown;
            gameTimer.Tick += GameTimer_Tick;
            this.ClientSize = new Size(40 * CellSize, 30 * CellSize); // 800x600
            StartGame();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Управление: стрелки\nПробел — пауза\nEnter или пробел — рестарт после проигрыша\n\nУдачи!",
                "Змейка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void StartGame()
        {
            score = 0;
            gameOver = false;
            direction = Direction.Right;
            snake.Clear();

            int startX = 15 * CellSize;
            int startY = 12 * CellSize;
            for (int i = 4; i >= 0; i--)
                snake.Add(new Point(startX + i * CellSize, startY));

            do { GenerateFood(); } while (snake.Contains(food));

            gameTimer.Interval = 120;
            gameTimer.Start();
        }

        private void GenerateFood()
        {
            int maxX = (this.ClientSize.Width / CellSize) - 1;
            int maxY = (this.ClientSize.Height / CellSize) - 1;
            do
            {
                food = new Point(
                    rand.Next(0, maxX) * CellSize,
                    rand.Next(0, maxY) * CellSize);
            } while (snake.Contains(food));
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (gameOver) return;
            MoveSnake();
            CheckCollisions();
            this.Invalidate();
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

            snake.Insert(0, newHead);

            if (newHead == food)
            {
                score += 10;
                GenerateFood();
                if (score % 50 == 0 && gameTimer.Interval > 30)
                    gameTimer.Interval -= 10;
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private void CheckCollisions()
        {
            Point head = snake[0];

            // Столкновение со стенами
            if (head.X < 0 || head.X >= this.ClientSize.Width ||
                head.Y < 0 || head.Y >= this.ClientSize.Height)
            {
                GameOver();
                return;
            }

            // Столкновение с собой
            for (int i = 1; i < snake.Count; i++)
            {
                if (head == snake[i])
                {
                    GameOver();
                    return;
                }
            }
        }

        private void GameOver()
        {
            gameOver = true;
            gameTimer.Stop();

            // Читаем ник из txt файла
            string nick = "Игрок";
            try
            {
                if (File.Exists(nicknamefrm.NickPath))
                    nick = File.ReadAllText(nicknamefrm.NickPath).Trim();
                if (string.IsNullOrEmpty(nick)) nick = "Игрок";
            }
            catch { }

            // Сохраняем результат в leaderboard.txt
            leadbordfrm.AddScore(nick, score);

            MessageBox.Show(
                $"Игра окончена!\nВаш счёт: {score}",
                "Змейка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameOver)
            {
                if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
                    StartGame();
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Up when direction != Direction.Down: direction = Direction.Up; break;
                case Keys.Down when direction != Direction.Up: direction = Direction.Down; break;
                case Keys.Left when direction != Direction.Right: direction = Direction.Left; break;
                case Keys.Right when direction != Direction.Left: direction = Direction.Right; break;
                case Keys.Space: gameTimer.Enabled = !gameTimer.Enabled; break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Рисуем еду
            g.FillEllipse(Brushes.Red, food.X, food.Y, CellSize, CellSize);

            // Рисуем змейку
            for (int i = 0; i < snake.Count; i++)
            {
                Brush brush = (i == 0) ? Brushes.Lime : Brushes.GreenYellow;
                g.FillRectangle(brush, snake[i].X, snake[i].Y, CellSize, CellSize);
                g.DrawRectangle(Pens.Black, snake[i].X, snake[i].Y, CellSize, CellSize);
            }

            // Счёт
            g.DrawString($"Счёт: {score}", new Font("Arial", 16), Brushes.White, 10, 10);

            if (gameOver)
            {
                string text = "ИГРА ОКОНЧЕНА\nНажмите ПРОБЕЛ или ENTER для рестарта";
                SizeF size = g.MeasureString(text, new Font("Arial", 20, FontStyle.Bold));
                g.DrawString(text,
                    new Font("Arial", 20, FontStyle.Bold),
                    Brushes.Yellow,
                    (this.ClientSize.Width - size.Width) / 2,
                    (this.ClientSize.Height - size.Height) / 2);
            }
        }
    }
}
