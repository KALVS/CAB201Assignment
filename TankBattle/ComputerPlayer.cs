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
            throw new NotImplementedException();
        }

        public override void BeginTurn(BattleForm gameplayForm, Gameplay currentGame)
        {

            Console.WriteLine("ComputerBeginturnCalled");
            gameplayForm.ChangeWeapon(currentGame.GetCurrentPlayerTank().GetPlayerWeapon());
            gameplayForm.AimTurret(currentGame.GetCurrentPlayerTank().GetAngle());
            gameplayForm.SetForce(currentGame.GetCurrentPlayerTank().GetPower());
        }

        public override void ProjectileHit(float x, float y)
        {

            Console.WriteLine("ProjectileHitCalled");
            throw new NotImplementedException();
        }
    }
}
