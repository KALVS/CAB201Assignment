using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class PlayerTank
    {
        private Opponent current_player;
        private Gameplay current_game;
        private Chassis current_chassis;
        private int TX, TY, THealth, angle, current_weapon, current_velocity;
        private float power;
        private Bitmap current_tank;
        private int armour;
        public PlayerTank(Opponent player, int tankX, int tankY, Gameplay game)
        {
            //Alex Holm N9918205
            TX = tankX;
            TY = tankY;
            current_player = player;
            current_game = game;
            current_chassis = current_player.GetTank();
            THealth = current_chassis.GetTankHealth();
            angle = 0;
            power = 25;
            current_weapon = 0;
            current_tank = current_chassis.CreateTankBitmap(current_player.GetColour(), angle);
            armour = current_chassis.GetTankHealth();
        }

        public Opponent GetPlayer()
        {
            //Alex Holm N9918205
            return current_player;
        }
        public Chassis GetTank()
        {
            //Alex Holm N9918205
            return current_player.GetTank();
        }

        public float GetAngle()
        {
            //Alex Holm N9918205
            return angle;
        }

        public void AimTurret(float angle)
        {
            //Alex Holm N9918205
            current_tank = current_chassis.CreateTankBitmap(current_player.GetColour(), GetAngle());
        }

        public int GetPower()
        {
            //Alex Holm N9918205
            if (power < 0.5)
            {
                power = 0.5f;
            } else if (power > 100f)
            {
                power = 100f;
            }
            return (int)power;
        }

        public void SetForce(int power)
        {
            //Alex Holm N9918205
            current_velocity = power;
        }

        public int GetPlayerWeapon()
        {
            return current_weapon;
        }
        public void ChangeWeapon(int newWeapon)
        {
            throw new NotImplementedException();
        }

        public void Render(Graphics graphics, Size displaySize)
        {
            //Alex Holm N9918205
            int drawX1 = displaySize.Width * TX / Battlefield.WIDTH;
            int drawY1 = displaySize.Height * TY / Battlefield.HEIGHT;
            int drawX2 = displaySize.Width * (TX + Chassis.WIDTH) / Battlefield.WIDTH;
            int drawY2 = displaySize.Height * (TY + Chassis.HEIGHT) / Battlefield.HEIGHT;
            graphics.DrawImage(current_tank, new Rectangle(drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1));

            int drawY3 = displaySize.Height * (TY - Chassis.HEIGHT) / Battlefield.HEIGHT;
            Font font = new Font("Arial", 8);
            Brush brush = new SolidBrush(Color.White);

            int pct = armour * 100 / THealth;
            if (pct < 100)
            {
                graphics.DrawString(pct + "%", font, brush, new Point(drawX1, drawY3));
            }
        }

        public int XPos()
        {
            //Alex Holm N9918205
            return TX;
        }
        public int GetY()
        {
            //Alex Holm N9918205
            return TY;
        }

        public void Attack()
        {
            current_game.GetCurrentPlayerTank().Attack();
            //Disables Control Panel
            //Enables Timer
 
            throw new NotImplementedException();
            
        }

        public void DamagePlayer(int damageAmount)
        {
            armour = armour - damageAmount;
        }

        public bool Alive()
        {
            if (armour > 0)
            {
                return true;
            }
            else { return false; }
        
        }

        public bool CalculateGravity()
        {
            /*<Summary> N9932798 Julian Shores
             * The calculate gravity function takes multiple functions' output to return a result.
             * Firstly it checks for the boolean result of the Alive() function, then reacting accordingly.
             * Secondly it gets the map and checks to see if the tank fits. If not, false is returned.
             * Falling damage is calculated, otherwise.
             * Thridly the function does a check to see if the tank is still within the bounds of the screen.
             * If not, the tank's armour is reduced to 0.
             * </Summary>
            */

            if (Alive() == false)
            {
                return false;
            }
            current_game.GetMap();
            if (current_game.GetMap().TankFits(TX, TY))
            {
                return false;
            } else
            {
                TY += 1;
                armour -= 1;
            }

            if (TY == Battlefield.HEIGHT - Chassis.HEIGHT)
            {
                armour = 0;
            }
            return true;

        }
    }
}
