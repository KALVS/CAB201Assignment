using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TankBattle
{
    public class Shrapnel : WeaponEffect
    {
        private int ExDam, ExRad, EarthDestRad;
        private Battlefield BatField;
        private float lifespan;
        private float shrapX, shrapY;
        public Shrapnel(int explosionDamage, int explosionRadius, int earthDestructionRadius)
        {
            ExDam = explosionDamage;
            ExRad = explosionRadius;
            EarthDestRad = earthDestructionRadius;
        }

        public void Activate(float x, float y)
        {
            shrapX = x;
            shrapY = y;
            lifespan = 1.0f;
        }

        public override void Step()
        {
            //This method reduces the Shrapnel's lifespan by 0.05, and if it reaches 0 (or lower), does the following:

            lifespan -= 0.05f;
            if( lifespan <= 0)
            {
                lifespan = 0;
                protected_game.DamagePlayer(shrapX, shrapY, ExDam, ExRad);
                BatField = protected_game.GetMap();
                BatField.TerrainDestruction(shrapX, shrapY, ExRad);
                Debug.WriteLine("Destroy Battefield geezy");
                protected_game.CancelEffect(this);
            }
            
        }

        public override void Render(Graphics graphics, Size displaySize)
        {
            float x = (float)this.shrapX * displaySize.Width / Battlefield.WIDTH;
            float y = (float)this.shrapY * displaySize.Height / Battlefield.HEIGHT;
            float radius = displaySize.Width * (float)((1.0 - lifespan) * ExRad * 3.0 / 2.0) / Battlefield.WIDTH;

            int alpha = 0, red = 0, green = 0, blue = 0;

            if (lifespan < 1.0 / 3.0)
            {
                red = 255;
                alpha = (int)(lifespan * 3.0 * 255);
            }
            else if (lifespan < 2.0 / 3.0)
            {
                red = 255;
                alpha = 255;
                green = (int)((lifespan * 3.0 - 1.0) * 255);
            }
            else
            {
                red = 255;
                alpha = 255;
                green = 255;
                blue = (int)((lifespan * 3.0 - 2.0) * 255);
            }

            RectangleF rect = new RectangleF(x - radius, y - radius, radius * 2, radius * 2);
            Brush b = new SolidBrush(Color.FromArgb(alpha, red, green, blue));

            graphics.FillEllipse(b, rect);
        }
    }
}
