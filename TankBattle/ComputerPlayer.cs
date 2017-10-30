using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class ComputerPlayer : Opponent
    {
        Random rnd = new Random();
        public ComputerPlayer(string name, Chassis tank, Color colour) : base(name, tank, colour)
        {
            Console.WriteLine("ComputerPlayerCalled");
            base.name = name;
            base.tank = tank;
            base.colour = colour;
        }

        public override void StartRound()
        {
            Console.WriteLine("ComputerStartRoundCalled");
        }

        public override void BeginTurn(BattleForm gameplayForm, Gameplay currentGame)
        {

            Console.WriteLine("ComputerBeginturnCalled");
            gameplayForm.ChangeWeapon(0);
            gameplayForm.AimTurret(rnd.Next(-66,66));
            gameplayForm.SetForce(rnd.Next(20,60));
        }

        public override void ProjectileHit(float x, float y)
        {

            Console.WriteLine("ProjectileHitCalled");
        }
    }
}
