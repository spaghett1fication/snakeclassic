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
        private int[] size;

        public StarField(int width, int height, int count = 180)
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

                // Базовая яркость — РЕАЛЬНО видимая
                baseAlpha[i] = rnd.Next(90, 220);

                phase[i] = (float)(rnd.NextDouble() * Math.PI * 2);
                speed[i] = 0.03f + (float)(rnd.NextDouble() * 0.08f);

                // Размеры: 55% — 1px, 35% — 2px, 10% — 3px (яркие звёзды)
                int sizeRoll = rnd.Next(100);
                if (sizeRoll < 55)
                    size[i] = 1;
                else if (sizeRoll < 90)
                    size[i] = 2;
                else
                    size[i] = 3;

                // Холодные оттенки — НЕ сольются с жёлто-красной едой
                int colorType = rnd.Next(7);
                switch (colorType)
                {
                    case 0: colR[i] = 140; colG[i] = 180; colB[i] = 255; break; // голубая
                    case 1: colR[i] = 180; colG[i] = 160; colB[i] = 255; break; // сиреневая
                    case 2: colR[i] = 100; colG[i] = 200; colB[i] = 255; break; // ледяная
                    case 3: colR[i] = 220; colG[i] = 220; colB[i] = 255; break; // бело-голубая
                    case 4: colR[i] = 160; colG[i] = 140; colB[i] = 230; break; // фиолетовая
                    case 5: colR[i] = 200; colG[i] = 230; colB[i] = 255; break; // светло-голубая
                    default: colR[i] = 255; colG[i] = 255; colB[i] = 255; break; // белая
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

                // Альфа: от 25% базы (тусклая фаза) до 100% базы (яркая фаза)
                int alpha = (int)(baseAlpha[i] * (0.25f + 0.75f * twinkle));
                if (alpha < 15) alpha = 15;
                if (alpha > 240) alpha = 240;

                using (SolidBrush brush = new SolidBrush(
                           Color.FromArgb(alpha, colR[i], colG[i], colB[i])))
                {
                    g.FillRectangle(brush, xPos[i], yPos[i], size[i], size[i]);
                }
            }
        }
    }
}
