using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int dx = X2 - X1;
            int dy = Y2 - Y1;
            int y;
            for (int x = 0; x < (X1 - X2 + 1); x++)
            {
                y = Y1 + dy * (x - X1) / dx;
                graphic[y, x] = 1;

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
            /*
            This is a factory method, used to create a new object of a concrete class that inherits
            from Chassis and return it.
            This way different parts of the program can create a variety of different tanks
            without having to know anything about your concrete class.

            If you only have one type of tank your method can simply be something like:

            return new MyTank();

            If you have multiple varieties of tank,
            you will want to return a specific type of tank based on the value of tankNumber.
            The unit tests assume that tank numbers start at 1.*/
            return new CreateTank();
        }

    }

    public class CreateTank : Chassis
    {
        public override int[,] DrawTankSprite(float angle)
        {

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
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            if (angle < -67.5)
            {
                LineDraw(graphic, 6, 7, 6, 2);
            }
            if (angle >= -67.5)
            {
                if (angle < -22.5)
                {
                    LineDraw(graphic, 6, 7, 2, 3);
                }
            }
            if (angle >= -22.5)
            {
                if (angle < 22.5)
                {
                    LineDraw(graphic, 7, 6, 7, 1);
                }
            }
            if (angle >= 22.5)
            {
                if (angle < 67.5)
                {
                    LineDraw(graphic, 7, 6, 1, 2);
                }
            }
            if (angle >= 67.5)
            {
                LineDraw(graphic, 7, 6, 11, 6);
            }
            return graphic;
        }

        public override int GetTankHealth()
        {
            return 100;
        }

        public override void WeaponLaunch(int weapon, PlayerTank playerTank, Gameplay currentGame)
        {
            float x = (float)(playerTank.XPos() + Chassis.WIDTH/2);
            float y = (float)(playerTank.GetY() + Chassis.HEIGHT/2);
            Opponent player = playerTank.GetPlayer();
            Shrapnel shrap = new Shrapnel(100, 4, 4);
            Shell shock = new Shell(x, y, playerTank.GetAngle(), playerTank.GetPower(), 0.01f, shrap, playerTank.GetPlayer());
            currentGame.AddEffect(shock);

        }

        public override string[] Weapons()
        {
            return new string[] { "Standard Shell" };
        }
    }
}
