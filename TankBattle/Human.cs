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
        //This most likely is wrong.
        private string name;
        private Chassis tank;
        private Color colour;
        private int rounds_won;
        public Human(string name, Chassis tank, Color colour) : base(name, tank, colour)
        {
            this.name = name;
            this.tank = tank;
            this.colour = colour;
            rounds_won = 0;
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
