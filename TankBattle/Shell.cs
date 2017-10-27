using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public class Shell : WeaponEffect
    {
        //These are private fields for the X, y, gravity, explosion and player fields for Shell function below
        //Using S to represent Shell so Shell's X value and so on
        private float Sx, Sy, Sgravity, SxVelocity, SyVelocity;
        private Shrapnel Sexplosion;
        private Opponent Splayer;
        public Shell(float x, float y, float angle, float power, float gravity, Shrapnel explosion, Opponent player)
        {
            Sx = x;
            Sy = y;
            Sgravity = gravity;
            float angleRadians = (90 - angle) * (float)Math.PI / 180;
            float magnitude = power / 50;
            SxVelocity = (float) Math.Cos(angleRadians) * magnitude;
            SyVelocity = (float)Math.Sin(angleRadians) * -magnitude;

        }

        public override void Step()
        {
            throw new NotImplementedException();
        }

        public override void Render(Graphics graphics, Size size)
        {
            throw new NotImplementedException();
        }
    }
}
