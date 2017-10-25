using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Shrapnel : WeaponEffect
    {
        private int ExDam, ExRad, EarthDestRad;
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
            throw new NotImplementedException();
        }

        public override void Render(Graphics graphics, Size displaySize)
        {
            throw new NotImplementedException();
        }
    }
}
