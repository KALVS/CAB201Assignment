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
        //This most likely is wrong.
        private string name;
        private Chassis tank;
        private Color colour;
        public Human(string name, Chassis tank, Color colour) : base(name, tank, colour)
        {
            this.name = name;
            this.tank = tank;
            this.colour = colour;
        }

        public override void StartRound()
        {

        }

        public override void BeginTurn(BattleForm gameplayForm, Gameplay currentGame)
        {
            gameplayForm.EnableTankButtons();
        }

        public override void ProjectileHit(float x, float y)
        {

        }
    }
}
