﻿using System;
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
            //If turret is upright
            if (angle >= -22.5)
            {
                if (angle < 22.5)
                {
                    LineDraw(graphic, 7, 6, 7, 1);
                }
            }
            //If turret is Left
            if (angle < -67.5)
            {
                LineDraw(graphic, 7, 6, 2, 6);
            }
            //If Turret is right
            if (angle >= 67.5)
            {
                LineDraw(graphic, 7, 6, 12, 6);
            }
            //Angled to the left
            if (angle >= -67.5)
            {
                if (angle < -22.5)
                {
                    LineDraw(graphic, 7, 6, 3, 2);
                }
            }
            //Angled to the right
            if (angle >= 22.5)
            {
                if (angle < 67.5)
                {
                    LineDraw(graphic, 7, 6, 1, 2);
                }
            }

            return graphic;
        }

        public override int GetTankHealth()
        {
            //Alex Holme N9918205
            return 100;
        }

        public override void WeaponLaunch(int weapon, PlayerTank playerTank, Gameplay currentGame)
        {
            //Alex Holm N9918205
            float x = (float)(playerTank.XPos() + Chassis.WIDTH/2);
            float y = (float)(playerTank.GetY() + Chassis.HEIGHT/2);
            Opponent player = playerTank.GetPlayer();
            Shrapnel shrap = new Shrapnel(100, 4, 4);
            Shell shock = new Shell(x, y, playerTank.GetAngle(), playerTank.GetPower(), 0.01f, shrap, playerTank.GetPlayer());
            currentGame.AddEffect(shock);

        }

        public override string[] Weapons()
        {
            //Alex Holm N9918205
            return new string[] { "Standard Shell" };
        }
    }
}
