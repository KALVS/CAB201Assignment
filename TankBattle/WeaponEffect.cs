using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public abstract class WeaponEffect
    {
        protected Gameplay protected_game;
        public void RecordCurrentGame(Gameplay game)
        {
            protected_game = game;
        }

        public abstract void Step();
        public abstract void Render(Graphics graphics, Size displaySize);
    }
}
