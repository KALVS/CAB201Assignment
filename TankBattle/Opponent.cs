using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    abstract public class Opponent
    {
        //Alex Holm
        protected string name;
        protected Chassis tank;
        protected Color colour;
        private int rounds_won;
        public Opponent(string name, Chassis tank, Color colour)
        {
            this.name = name;
            this.tank = tank;
            this.colour = colour;
            rounds_won = 0;
        }
        public Chassis GetTank()
        {
            return tank;
        }
        public string Identifier()
        {
            return name;
        }
        public Color GetColour()
        {
            return colour;
        }
        public void AddScore()
        {
            rounds_won++;
        }
        public int GetPoints()
        {
            return rounds_won;
        }

        public abstract void StartRound();

        public abstract void BeginTurn(BattleForm gameplayForm, Gameplay currentGame);

        public abstract void ProjectileHit(float x, float y);
    }
}
