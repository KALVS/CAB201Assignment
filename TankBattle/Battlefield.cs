﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Battlefield
    {
        public const int WIDTH = 160;
        public const int HEIGHT = 120;

        public Battlefield()
        {
            bool [,] terrain = new bool[Battlefield.HEIGHT, Battlefield.WIDTH];
            for (int y = 3; y < Battlefield.HEIGHT - 1; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH - 1; x++)
                {

                    //random true or false and if true below
                    Random rnd = new Random();
                    bool tile = rnd.Next(0, 100) % 2 == 0;
                    if (tile == false)
                    {
                        terrain[y, x] = tile;
                    }
                    if (terrain[y - 1, x] == true)
                    {
                        terrain[y, x] = tile;
                    }
                }
            }
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH - 1; x++)
                {
                    terrain[y, x] = false;
                }
            }
            for (int x = 0; x < Battlefield.WIDTH - 1; x++)
            {
                terrain[Battlefield.HEIGHT - 1, x] = true;
            }
        }

        public bool IsTileAt(int x, int y)
        {
            throw new NotImplementedException();
        }

        public bool TankFits(int x, int y)
        {
            throw new NotImplementedException();
        }

        public int TankYPosition(int x)
        {
            throw new NotImplementedException();
        }

        public void TerrainDestruction(float destroyX, float destroyY, float radius)
        {
            throw new NotImplementedException();
        }

        public bool CalculateGravity()
        {
            throw new NotImplementedException();
        }
    }
}
