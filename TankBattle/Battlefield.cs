using System;
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
        bool[,] terrain = new bool[Battlefield.HEIGHT, Battlefield.WIDTH];

        public Battlefield()
        {
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
            bool result = false;
            if (terrain[y,x] == true)
            {
                result = true;
            } else if (terrain[y,x] == false)
            {
                result = false;
            }
            return result;
        }

        public bool TankFits(int x, int y)
        { 
            bool result = false;
            for (int ex = 0; ex<Chassis.WIDTH; ex++)
            {
                for (int ey = 0; ey<Chassis.HEIGHT; ey++)
                {
                    if (terrain[y + ey, x + ex] == true)
                    {
                        result = true;
                        return result;
                    }
}
            }
            return result;
        }

        public int TankYPosition(int x)
        {
            bool grounded = false;
            int ey = 0;
            while (!grounded)
            {
                if (terrain[ey, x] == false)
                {
                    ey++;
                }
                else if (terrain[ey, x] == true)
                {
                    grounded = true;
                }
            }
            return ey - Chassis.HEIGHT;
        }

        public void TerrainDestruction(float destroyX, float destroyY, float radius)
        {
            double dist;
            for (int y = 0; y < Battlefield.HEIGHT; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH; x++)
                {
                    //distance between x and y
                    float csqrt = x * x + y * y;
                    dist = Math.Sqrt(csqrt);
                    if ( dist >= radius)
                    {
                        terrain[y, x] = false;
                    }
                }
            }
        }

        public bool CalculateGravity()
        {
            for (int y = 0; y < Battlefield.HEIGHT; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH; y++)
                {
                    if (terrain[y,x] == true)
                    {
                        if (terrain[y+1,x] == false)
                        {
                            terrain[y, x] = false;
                            terrain[y + 1, x] = true;
                            return true;
                        } else if (terrain[y+1,x] == true)
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }
    }
}
