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
        bool[,] terrain = new bool[Battlefield.WIDTH, Battlefield.HEIGHT];
       

        public Battlefield()
        {
            Random rnd = new Random();
            for (int x = 0; x < Battlefield.WIDTH - 1; x++)
            {
                terrain[x, Battlefield.HEIGHT - 1] = true;
            }
            for (int x = 0; x < Battlefield.WIDTH - 1; x++)
            {
                for (int y = Battlefield.HEIGHT - 2; y > 0; y--)
                {
                    terrain[x, y] = true;
                }
            }
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH - 1; x++)
                {
                    terrain[x, y] = false;
                }
            }
            for (int d = 0; d < 100; d++)
            {
                TerrainDestruction(rnd.Next(0, WIDTH), rnd.Next(0, HEIGHT - 17), rnd.Next(3, 15));
            }

            while (CalculateGravity())
            {
                CalculateGravity();

            }

        }

        public bool IsTileAt(int x, int y)
        {
            return terrain[x, y];
        }

        public bool TankFits(int x, int y)
        {
            for (int ex = 0; ex < Chassis.WIDTH; ex++)
            {
                for (int ey = 0; ey < Chassis.HEIGHT; ey++)
                {
                    if (terrain[x + ex, y + ey] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int TankYPosition(int x)
        {
            int lowPoint = 0;
            for (int y = 0; y <= HEIGHT - Chassis.HEIGHT; y++)
            {
                int hitTiles = 0;
                for (int ChasY = 0; ChasY < Chassis.HEIGHT; ChasY++)
                {
                    for (int ChasX = 0; ChasX < Chassis.WIDTH; ChasX++)
                    {
                        if(IsTileAt(x + ChasX, y + ChasY))
                        {
                            hitTiles++;
                        }
                    }
                    
                }
                if (hitTiles == 0)
                {
                    lowPoint = y;
                }
            }
            return lowPoint;
        }
            

        
        public void TerrainDestruction(float destroyX, float destroyY, float radius)
        {
            float dist = 0
                ;
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    //distance between x and y
                    float DX = x - destroyX;
                    float DY = y- destroyY;
                    float csqrt = DX * DX + DY * DY;
                    dist = (float)Math.Sqrt(csqrt);
                    if (dist < radius)
                    {
                        terrain[x, y] = false;
                    }
                }
            }
        }
        bool tile_moved;
        public bool CalculateGravity()
        {
            tile_moved = false;
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = HEIGHT - 2; y > 0; y--)
                {
                    if (IsTileAt(x, y) && (!IsTileAt(x, y + 1)))
                    {
                        terrain[x, y] = false;
                        terrain[x, y + 1] = true;
                        tile_moved = true;

                    }
                }
            }
            return tile_moved;
        } 
    }
}
