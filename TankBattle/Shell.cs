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

        // THis is a test commit comment, please ignore and delete at some stage.

        public Shell(float x, float y, float angle, float power, float gravity, Shrapnel explosion, Opponent player)
        {
            //throw new NotImplementedException();


            float angleRadians = (90 - angle) * (float)Math.PI / 180;

            float magnitude = power / 50;

            float xVelocity = (float)Math.Cos(angleRadians) * magnitude;
            float yVelocity = (float)Math.Sin(angleRadians) * -magnitude;
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
