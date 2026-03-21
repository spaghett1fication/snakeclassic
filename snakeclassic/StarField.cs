using System;
using System.Drawing;

namespace snakeclassic
{
    public class StarField
    {
        private int count;
        private int[] xPos;
        private int[] yPos;
        private int[] baseAlpha;
        private float[] phase;
        private float[] speed;
        private byte[] colR;
        private byte[] colG;
        private byte[] colB;
        private int[] size;       // 1 или 2 пикселя

        public StarField(int width, int height, int count = 140)
        {
            this.count = count;
            Random rnd = new Random();

            xPos = new int[count];
            yPos = new int[count];
            baseAlpha = new int[count];
            phase = new float[count];
            speed = new float[count];
            colR = new byte[count];
            colG = new byte[count];
            colB = new byte[count];
            size = new int[count];

            for (int i = 0; i < count; i++)
            {
                xPos[i] = rnd.Next(0, width);
                yPos[i] = rnd.Next(0, height);

                // Базовая яркость — ощутимая, но не кричащая
                baseAlpha[i] = rnd.Next(50, 1500);

                phase[i] = (float)(rnd.NextDouble() * Math.PI * 2);
                speed[i] = 0.02f + (float)(rnd.NextDouble() * 0.07f);

                // Большинство — 1px, ~20% — 2px для глубины
                size[i] = rnd.Next(100) < 20 ? 2 : 1;

                // Холодные оттенки: голубой / сиреневый / белый
                int colorType = rnd.Next(6);
                switch (colorType)
                {
                    case 0: colR[i] = 160; colG[i] = 185; colB[i] = 255; break; // голубая
                    case 1: colR[i] = 190; colG[i] = 170; colB[i] = 255; break; // сиреневая
                    case 2: colR[i] = 130; colG[i] = 200; colB[i] = 255; break; // ледяная
                    default: colR[i] = 200; colG[i] = 210; colB[i] = 235; break; // бело-голубая
                }
            }
        }

        public void Update()
        {
            for (int i = 0; i < count; i++)
            {
                phase[i] += speed[i];
                if (phase[i] > 6.2831853f)
                    phase[i] -= 6.2831853f;
            }
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < count; i++)
            {
                float twinkle = (float)(Math.Sin(phase[i]) * 0.5 + 0.5);

                // Альфа: от 30% базы до 100% базы
                int alpha = (int)(baseAlpha[i] * (0.3f + 0.7f * twinkle));
                if (alpha < 10) alpha = 10;
                if (alpha > 180) alpha = 180;

                using (SolidBrush brush = new SolidBrush(
                           Color.FromArgb(alpha, colR[i], colG[i], colB[i])))
                {
                    g.FillRectangle(brush, xPos[i], yPos[i], size[i], size[i]);
                }
            }
        }
    }
}
