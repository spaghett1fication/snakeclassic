using System;
using System.Collections.Generic;
using System.Drawing;

namespace snakeclassic
{
    public class StarField
    {
        // ── Структура одной звезды ──────────────────────────────────
        private struct Star
        {
            public int X;
            public int Y;
            public int BaseAlpha;   // базовая яркость (очень низкая)
            public float Phase;     // текущая фаза мерцания
            public float Speed;     // скорость мерцания (у каждой своя)
            public byte R, G, B;    // цвет (чуть различается)
        }

        private Star[] stars;

        // ────────────────────────────────────────────────────────────
        //  Конструктор
        //    width, height — размер gamePanel (800, 460)
        //    count         — кол-во звёзд (по умолчанию 90, не много)
        // ────────────────────────────────────────────────────────────
        public StarField(int width, int height, int count = 90)
        {
            Random rnd = new Random();
            stars = new Star[count];

            for (int i = 0; i < count; i++)
            {
                // Случайный холодный оттенок: белый / голубоватый / сиреневатый
                int colorType = rnd.Next(5);
                byte r, g, b;
                switch (colorType)
                {
                    case 0: r = 180; g = 190; b = 255; break; // голубоватая
                    case 1: r = 200; g = 180; b = 255; break; // сиреневая
                    case 2: r = 255; g = 240; b = 200; break; // тёплая (редко)
                    default: r = 210; g = 215; b = 230; break; // почти белая
                }

                stars[i] = new Star
                {
                    X = rnd.Next(0, width),
                    Y = rnd.Next(0, height),
                    BaseAlpha = rnd.Next(12, 38),                         // очень тусклые
                    Phase = (float)(rnd.NextDouble() * Math.PI * 2),
                    Speed = 0.015f + (float)(rnd.NextDouble() * 0.06f), // медленное мерцание
                    R = r,
                    G = g,
                    B = b
                };
            }
        }

        // ────────────────────────────────────────────────────────────
        //  Update()  —  сдвигает фазу мерцания каждой звезды
        // ────────────────────────────────────────────────────────────
        public void Update()
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].Phase += stars[i].Speed;
                if (stars[i].Phase > 6.2831853f)        // 2π
                    stars[i].Phase -= 6.2831853f;
            }
        }

        // ────────────────────────────────────────────────────────────
        //  Draw()  —  рисует звёзды (1 пиксель каждая, очень тихо)
        // ────────────────────────────────────────────────────────────
        public void Draw(Graphics g)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                ref Star s = ref stars[i];

                // sin даёт плавное мерцание 0 … 1
                float twinkle = (float)(Math.Sin(s.Phase) * 0.5 + 0.5);

                // Итоговый альфа: от почти невидимого до «чуть видно»
                int alpha = (int)(s.BaseAlpha * (0.2f + 0.8f * twinkle));
                if (alpha < 2) alpha = 2;
                if (alpha > 45) alpha = 45;   // потолок — звёзды НИКОГДА не будут яркими

                using (SolidBrush brush = new SolidBrush(
                           Color.FromArgb(alpha, s.R, s.G, s.B)))
                {
                    g.FillRectangle(brush, s.X, s.Y, 1, 1);
                }
            }
        }
    }
}
