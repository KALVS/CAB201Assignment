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
            Splayer = player;
            Sexplosion = explosion;
            Sgravity = gravity;
            float angleRadians = (90 - angle) * (float)Math.PI / 180;
            float magnitude = power / 50;
            SxVelocity = (float)Math.Cos(angleRadians) * magnitude;
            SyVelocity = (float)Math.Sin(angleRadians) * -magnitude;

        }

        public override void Step()
        {
            for( int i = 1; i < 10; i++)
            {
                //increase Sx and Sy
                Sx += SxVelocity;
                Sy += SyVelocity;
                //increase Sx with WindSpeed
                Sx += protected_game.WindSpeed() / 1000.0f;
                //if left screen
                if (Sx <= 0 || Sx >= Battlefield.WIDTH || Sy >= Battlefield.HEIGHT || Sy <= 0)
                {
                    protected_game.CancelEffect(this);
                    return;
                }
                else
                if (protected_game.CheckCollidedTank(Sx, Sy))
                {
                    Splayer.ProjectileHit(Sx, Sy);
                    Sexplosion.Activate(Sx, Sy);
                    protected_game.AddEffect(Sexplosion);
                    protected_game.CancelEffect(this);
                }
                SyVelocity += Sgravity;
            }
        }

        public override void Render(Graphics graphics, Size size)
        {
            float x = (float)this.Sx * size.Width / Battlefield.WIDTH;
            float y = (float)this.Sy * size.Height / Battlefield.HEIGHT;
            float s = size.Width / Battlefield.WIDTH;

            RectangleF r = new RectangleF(x - s / 2.0f, y - s / 2.0f, s, s);
            Brush b = new SolidBrush(Color.WhiteSmoke);

            graphics.FillEllipse(b, r);
        }
    }
}
