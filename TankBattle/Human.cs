using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    //Alex Holm

    public class Human : Opponent
    {
        protected new string name;
        protected new Chassis tank;
        protected new Color colour;
        public Human(string name, Chassis tank, Color colour) : base(name, tank, colour)
        {
            this.name = name;
            this.tank = tank;
            this.colour = colour;
        }

        public override void StartRound()
        {
            return;
        }

        public override void BeginTurn(BattleForm gameplayForm, Gameplay currentGame)
        {
            gameplayForm.EnableTankButtons();
        }

        public override void ProjectileHit(float x, float y)
        {
            return;
        }
    }
}
