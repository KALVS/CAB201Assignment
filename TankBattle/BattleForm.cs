using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class BattleForm : Form
    {
        private Color landscapeColour;
        Random rnd = new Random();
        private Random rng = new Random();
        private Image backgroundImage;
        private int levelWidth = 160;
        private int levelHeight = 120;

        private Timer myTimer = new Timer();
        private Gameplay currentGame;
        private Opponent current_player;
        private Chassis current_tank;
        private PlayerTank current_playerTank;
        int second_call = 0;

        private BufferedGraphics backgroundGraphics;
        private BufferedGraphics gameplayGraphics;

        // Image array from which the background will be randomly selected
        string[] imageFilenames = { "Images\\background1.jpg",
                                    "Images\\background2.jpg",
                                    "Images\\background3.jpg",
                                    "Images\\background4.jpg" };
        // Colour array from which the colour will be randomly selected
        Color[] landscapeColours = { Color.FromArgb(255, 0, 0, 0),
                                     Color.FromArgb(255, 73, 58, 47),
                                     Color.FromArgb(255, 148, 116, 93),
                                     Color.FromArgb(255, 133, 119, 109) };



        public BattleForm(Gameplay game)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            // Pass gamme into the function
            currentGame = game;

            // Generate random numbers between 0-3
            int i = rnd.Next(0, 3);

            // Select a landscape colour based upon the background image
            landscapeColour = landscapeColours[i];
            backgroundImage = Image.FromFile(imageFilenames[i]);

            InitializeComponent();

            myTimer.Interval = (20);



            backgroundGraphics =  InitDisplayBuffer();
            gameplayGraphics = InitDisplayBuffer();
            
            //These
            DrawBackground();
            //THREE
            DrawGameplay();
            //BASTARDS
            NewTurn();
            //Are the reason Commence isnt working.
        }


        private void DrawGameplay()
        {
            backgroundGraphics.Render(gameplayGraphics.Graphics);
            currentGame.DrawTanks(gameplayGraphics.Graphics, displayPanel.Size);
            currentGame.DisplayEffects(gameplayGraphics.Graphics, displayPanel.Size);
        }


        private void NewTurn()
        {
            //<Summary>
            //Alex Holm
            //</Summary>
            
            currentGame.GetCurrentPlayerTank();
            current_playerTank.GetPlayer();
            string Title = ("Tank Battle - Round " + currentGame.CurrentRound() + " of " + currentGame.GetMaxRounds());
            this.Text = Title;
            controlPanel.BackColor = current_player.GetColour();
            PlayerLabel.Text = current_playerTank.GetPlayer().ToString();
            AimTurret(current_playerTank.GetAngle());
            SetForce(current_playerTank.GetPower());
            Wind.Text = currentGame.WindSpeed().ToString();
            weaponComboBox.SelectedIndex = -1;
            current_playerTank.GetTank().Weapons();
            for (int i = 0; i < current_tank.Weapons().Count(); i++)
            {
                weaponComboBox.Items.Add(current_tank.Weapons()[i]);
            }
            ChangeWeapon(current_playerTank.GetPlayerWeapon());
            current_player.BeginTurn(this, currentGame);
            
        }


        // From https://stackoverflow.com/questions/13999781/tearing-in-my-animation-on-winforms-c-sharp
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        public void EnableTankButtons()
        {
            controlPanel.Enabled = true;
        }

        public void AimTurret(float angle)
        {
            angle = (float)AngleNumericUpDown.Value;
        }

        public void SetForce(int power)
        {
            power = (int)PowerBar.Value;
        }
        public void ChangeWeapon(int weapon)
        {
            weapon = (int)weaponComboBox.SelectedValue;
        }

        public void Attack()
        {
            currentGame.GetCurrentPlayerTank().Attack();
            controlPanel.Enabled = false;
            myTimer.Start();

        }

        private void DrawBackground()
        {
            Graphics graphics = backgroundGraphics.Graphics;
            Image background = backgroundImage;
            graphics.DrawImage(backgroundImage, new Rectangle(0, 0, displayPanel.Width, displayPanel.Height));

            Battlefield battlefield = currentGame.GetMap();
            Brush brush = new SolidBrush(landscapeColour);

            for (int y = 0; y < Battlefield.HEIGHT; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH; x++)
                {
                    if (battlefield.IsTileAt(x, y))
                    {
                        int drawX1 = displayPanel.Width * x / levelWidth;
                        int drawY1 = displayPanel.Height * y / levelHeight;
                        int drawX2 = displayPanel.Width * (x + 1) / levelWidth;
                        int drawY2 = displayPanel.Height * (y + 1) / levelHeight;
                        graphics.FillRectangle(brush, drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1);
                    }
                }
            }
        }

        public BufferedGraphics InitDisplayBuffer()
        {
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            Graphics graphics = displayPanel.CreateGraphics();
            Rectangle dimensions = new Rectangle(0, 0, displayPanel.Width, displayPanel.Height);
            BufferedGraphics bufferedGraphics = context.Allocate(graphics, dimensions);
            return bufferedGraphics;
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = displayPanel.CreateGraphics();
            gameplayGraphics.Render(graphics);
        }

        private void controlPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        //The methods tied to each of these events should call the appropriate PlayerTank method (AimTurret(), SetForce(), ChangeWeapon()).
        private void AngleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AimTurret((int)AngleNumericUpDown.Value);
            DrawGameplay();
            displayPanel.Invalidate();
        }

        private void PowerBar_Scroll(object sender, EventArgs e)
        {
            PowerIndicatorLabel.Text = PowerBar.Value.ToString();
            SetForce(PowerBar.Value);
            DrawGameplay();
            displayPanel.Invalidate();
        }

        private void weaponComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeWeapon(weaponComboBox.SelectedIndex);

        }

        //Finally, create a Tick event for[ the Timer that you created earlier. A fair bit of logic needs to go into this Tick event as it is responsible for handling much of the animation and physics logic:
        private void TimerEventProcessor()
        {
            if (!currentGame.ProcessWeaponEffects())
            {
                currentGame.CalculateGravity();
                DrawBackground();
                DrawGameplay();
                displayPanel.Invalidate();

                if (currentGame.CalculateGravity())
                {
                    return;
                }
                if (!currentGame.CalculateGravity())
                {
                    myTimer.Enabled = false;

                    if (currentGame.TurnOver())
                    {
                        NewTurn();
                    }
                    else
                    {
                        Dispose();
                        currentGame.NextRound();
                        return;
                    }
                }
            }
            else
            {
                DrawGameplay(); displayPanel.Invalidate();
                return;
            }
        }
    }
}
