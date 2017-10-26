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
        private int TX, TY, THealth;
        private Chassis chas;
        private PlayerTank playerTank;
        private int angle, current_weapon, currentplayer;
        private float power;
        public PlayerTank(Opponent player, int tankX, int tankY, Gameplay game)
        { /*
            Color colour = Gameplay.GetColour(currentplayer) ;
            TX = tankX;
            TY = tankY;
            chas = GetTank();
            THealth = playerTank.GetTank().GetTankHealth();
            angle = 0;
            power = 25;
            current_weapon = 0;
            */
            throw new NotImplementedException();
        }

        public Opponent GetPlayer()
        {
            throw new NotImplementedException();
        }
        public Chassis GetTank()
        {
            throw new NotImplementedException();
        }

        public float GetAngle()
        {
            throw new NotImplementedException();
        }

        public void AimTurret(float angle)
        {
            throw new NotImplementedException();
        }

        public int GetPower()
        {
            throw new NotImplementedException();
        }

        public void SetForce(int power)
        {
            throw new NotImplementedException();
        }

        public int GetPlayerWeapon()
        {
            throw new NotImplementedException();
        }
        public void ChangeWeapon(int newWeapon)
        {
            throw new NotImplementedException();
        }

        public void Render(Graphics graphics, Size displaySize)
        {
            throw new NotImplementedException();
        }

        public int XPos()
        {
            throw new NotImplementedException();
        }
        public int GetY()
        {
            throw new NotImplementedException();
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void DamagePlayer(int damageAmount)
        {
            throw new NotImplementedException();
        }

        public bool Alive()
        {
            throw new NotImplementedException();
        }

        public bool CalculateGravity()
        {
            throw new NotImplementedException();
        }
    }
}
