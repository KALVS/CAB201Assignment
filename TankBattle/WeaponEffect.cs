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
        public void RecordCurrentGame(Gameplay game)
        {
            throw new NotImplementedException();
        }

        public abstract void Step();
        public abstract void Render(Graphics graphics, Size displaySize);
    }
}
