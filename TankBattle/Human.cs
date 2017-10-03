using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public class Human : Opponent
    {
        public Human(string name, Chassis tank, Color colour) : base(name, tank, colour)
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
