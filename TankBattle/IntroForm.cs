using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class IntroForm : Form
    {
        public IntroForm()
        {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            Gameplay game = new Gameplay(2, 1);
            Opponent player1 = new Human("Player 1", Chassis.GetTank(1), Gameplay.GetColour(1));
            Opponent player2 = new Human("Player 2", Chassis.GetTank(1), Gameplay.GetColour(2));
            game.RegisterPlayer(1, player1);
            game.RegisterPlayer(2, player2);
            game.CommenceGame();
        }
    }
}
