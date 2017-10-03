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
            throw new NotImplementedException();
        }

        public override void StartRound()
        {
            throw new NotImplementedException();
        }

        public override void BeginTurn(BattleForm gameplayForm, Gameplay currentGame)
        {
            throw new NotImplementedException();
        }

        public override void ProjectileHit(float x, float y)
        {
            throw new NotImplementedException();
        }
    }
}
