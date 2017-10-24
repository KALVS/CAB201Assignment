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
        private string name;
        private Chassis tank;
        private Color colour;
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
            throw new NotImplementedException();
        }
        public Color GetColour()
        {
            throw new NotImplementedException();
        }
        public void AddScore()
        {
            throw new NotImplementedException();
        }
        public int GetPoints()
        {
            throw new NotImplementedException();
        }

        public abstract void StartRound();

        public abstract void BeginTurn(BattleForm gameplayForm, Gameplay currentGame);

        public abstract void ProjectileHit(float x, float y);
    }
}
