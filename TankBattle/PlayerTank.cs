using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TankBattle
{
    public class PlayerTank
    {
        private Opponent current_player;
        private Gameplay current_game;
        private Chassis current_chassis;
        private Bitmap current_tBMP;
        private int TX, TY, startingArmour, current_weapon;
        private float power, angle;
        private int armour;
        private Color colour;


        public PlayerTank(Opponent player, int tankX, int tankY, Gameplay game)
        {
            //Alex Holm N9918205
            TX = tankX;
            TY = tankY;
            current_player = player;
            current_game = game;
            current_chassis = current_player.GetTank();
            startingArmour = current_chassis.GetTankHealth();
            angle = 0;
            power = 25;
            current_weapon = GetPlayerWeapon();
            AimTurret(GetAngle());
            armour = current_chassis.GetTankHealth();
            colour = current_player.GetColour();
            current_tBMP = current_chassis.CreateTankBitmap(colour, angle);
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
            //fix me    
            return angle;
        }

        public void AimTurret(float angle)
        {
            //Alex Holm N9918205
            this.angle = angle;
            Color tankColour = current_player.GetColour();
            current_tBMP = current_chassis.CreateTankBitmap(tankColour, angle);
        }

        public int GetPower()
        {
            //Alex Holm N9918205
            Debug.WriteLine("Playertank GetPower begin");
            if (power < 0.5f)
            {
                power = 0.5f;
            } else if (power > 100f)
            {
                power = 100f;
            }

            Debug.WriteLine("Playertank GetPower End");
            return (int)power;
        }

        public void SetForce(int power)
        {
            /*<Summary> Julian Shores N9932798
             * This method sets the PlayerTank's current turret velocity
            */

            this.power = power;
            

        }

        public int GetPlayerWeapon()
        {   //Alex Holm N9918205
            return current_weapon;
        }

        public void ChangeWeapon(int newWeapon)
        {
            /* <Summary> Julian Shores N9932798
             * This method sets the PlayerTank's current weapon.
               </Summary>*/
            this.current_weapon = newWeapon;
        }

        public void Render(Graphics graphics, Size displaySize)
        {
            //Alex Holm N9918205
            int drawX1 = displaySize.Width * TX / Battlefield.WIDTH;
            int drawY1 = displaySize.Height * TY / Battlefield.HEIGHT;
            int drawX2 = displaySize.Width * (TX + Chassis.WIDTH) / Battlefield.WIDTH;
            int drawY2 = displaySize.Height * (TY + Chassis.HEIGHT) / Battlefield.HEIGHT;
            graphics.DrawImage(current_tBMP, new Rectangle(drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1));

            int drawY3 = displaySize.Height * (TY - Chassis.HEIGHT) / Battlefield.HEIGHT;
            Font font = new Font("Arial", 8);
            Brush brush = new SolidBrush(Color.White);

            int pct = armour * 100 / startingArmour;
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
            //<Summary> Julian Shores N9932798
            // This causes the PlayerTank to fire its current weapon
            //.This method should call its own GetTank() method,
            // then call WeaponLaunch() on that Chassis,
            // passing in the current weapon, 
            //the this reference and the private Gameplay field of PlayerTank. </Summary>
            Debug.WriteLine("attack from playertank");
            GetTank();
            current_chassis.WeaponLaunch(current_weapon, this, current_game);
            Debug.WriteLine("Playertank attack done");

        }

        public void DamagePlayer(int damageAmount)
        {
            //Alex Holm N9918205
            armour = armour - damageAmount;
        }

        public bool Alive()
        {
            //Alex Holm N9918205
            if (armour > 0)
            {
                return true;
            }
            return false;
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
