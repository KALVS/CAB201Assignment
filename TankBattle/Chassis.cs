using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TankBattle
{

    public abstract class Chassis
    {
        public const int WIDTH = 4;
        public const int HEIGHT = 3;
        public const int NUM_TANKS = 1;

        public abstract int[,] DrawTankSprite(float angle);

        public static void LineDraw(int[,] graphic, int X1, int Y1, int X2, int Y2)
        {
            //Alex Holme N99128205
            int dx = Math.Abs(X2 - X1), sx = X1 < X2 ? 1 : -1;
            int dy = Math.Abs(Y2 - Y1), sy = Y1 < Y2 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            for (;;)
            {
                graphic[X1, Y1] = 1;
                if (X1 == X2 && Y1 == Y2) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; X1 += sx; }
                if (e2 < dy) { err += dx; Y1 += sy; }
            }
        }


        public Bitmap CreateTankBitmap(Color tankColour, float angle)
        {
            int[,] tankGraphic = DrawTankSprite(angle);
            int height = tankGraphic.GetLength(0);
            int width = tankGraphic.GetLength(1);

            Bitmap bmp = new Bitmap(width, height);
            Color transparent = Color.FromArgb(0, 0, 0, 0);
            Color tankOutline = Color.FromArgb(255, 0, 0, 0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (tankGraphic[y, x] == 0)
                    {
                        bmp.SetPixel(x, y, transparent);
                    }
                    else
                    {
                        bmp.SetPixel(x, y, tankColour);
                    }
                }
            }

            // Outline each pixel
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    if (tankGraphic[y, x] != 0)
                    {
                        if (tankGraphic[y - 1, x] == 0)
                            bmp.SetPixel(x, y - 1, tankOutline);
                        if (tankGraphic[y + 1, x] == 0)
                            bmp.SetPixel(x, y + 1, tankOutline);
                        if (tankGraphic[y, x - 1] == 0)
                            bmp.SetPixel(x - 1, y, tankOutline);
                        if (tankGraphic[y, x + 1] == 0)
                            bmp.SetPixel(x + 1, y, tankOutline);
                    }
                }
            }

            return bmp;
        }

        public abstract int GetTankHealth();

        public abstract string[] Weapons();

        public abstract void WeaponLaunch(int weapon, PlayerTank playerTank, Gameplay currentGame);

        public static Chassis GetTank(int tankNumber)
        {
            return new MakeTank();
        }

    }

    public class MakeTank : Chassis
    {
        public override int[,] DrawTankSprite(float angle)
        {
            //Alex Holme N9918205
            //Schlomo can suck it. Why would you give me the incorrect numbers to draw the line?
            //Tell me to do it myself or give the right numbers, dont provide me with incorrect information.
            //It is a dog act.
            int[,] graphic = { 
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                   { 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0 },
                   { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

            if (angle >= (float)23)
            {
                if (angle > 23 && angle <= 67)
                {
                    LineDraw(graphic, 5, 6, 1, 11);
                }
                if (angle > 68 && angle <= 90)
                {
                    LineDraw(graphic, 5, 6, 5, 11);
                }
            }
            //Left
            if (angle <= (float)-23)
            {
                if (angle < -65 && angle >= -90)
                {
                    LineDraw(graphic, 5, 6, 5, 1);
                    //Angle Left
                }
                if (angle > -66 && angle < -23)
                {
                    LineDraw(graphic, 5, 6, 1, 2);
                }
            }
            if (angle > -22 && angle < 22)
            {
                LineDraw(graphic, 5, 6, 1, 6);
            }
            return graphic;
        }
        

        public override int GetTankHealth()
        {
            Debug.WriteLine("Chassis GetTankHealth Start");
            //Alex Holme N9918205
            return 100;
        }

        public override void WeaponLaunch(int weapon, PlayerTank playerTank, Gameplay currentGame)
        {
            Debug.WriteLine("Chassis WeaponLaunch Start");
            //Alex Holm N9918205
            float x = (playerTank.XPos());
            float y =(playerTank.GetY());
            x += (Chassis.WIDTH)/2;
            y += (Chassis.HEIGHT)/2;
            Opponent player = playerTank.GetPlayer();
            Shrapnel shrap = new Shrapnel(100, 4, 4);
            Shell Std_shell = new Shell(x, y, playerTank.GetAngle(), playerTank.GetPower(), 0.01f, shrap, player);
            currentGame.AddEffect(Std_shell);
            Debug.WriteLine("Chassis WeaponLaunch Complete");

        }

        public override string[] Weapons()
        {
            //Alex Holm N9918205
            return new string[] { "Standard Shell" };
        }
    }
}
