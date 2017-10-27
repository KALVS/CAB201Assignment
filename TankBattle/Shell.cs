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
        private float shellx;
        private float shelly;
        private float gravity;
        private Shrapnel explosion;
        private Opponent player;
        private float xVelocity;
        private float yVelocity;

        public Shell(float x, float y, float angle, float power, float gravity, Shrapnel explosion, Opponent player)
        {
            float angleRadians = (90 - angle) * (float)Math.PI / 180;

            float magnitude = power / 50;

            xVelocity = (float)Math.Cos(angleRadians) * magnitude;
            yVelocity = (float)Math.Sin(angleRadians) * -magnitude;
        }

        public override void Step()
        {
            throw new NotImplementedException();

            shellx += xVelocity;
            shelly += yVelocity;
            shellx += protected_game.WindSpeed() / 1000.0f;
            if (shellx > Battlefield.WIDTH || shellx < 0 || shelly > Battlefield.HEIGHT)
            {
                protected_game.CancelEffect(this);
                return;
            } else if (protected_game.CheckCollidedTank(shellx, shelly))
            {
                // NEED TO CALL ProjectileHit() FROM OPPONENT
            }
        }

        public override void Render(Graphics graphics, Size size)
        {
            throw new NotImplementedException();
        }
    }
}
