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
        public Opponent(string name, Chassis tank, Color colour)
        {
            throw new NotImplementedException();
        }
        public Chassis GetTank()
        {
            throw new NotImplementedException();
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
