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
using System.Diagnostics;

namespace TankBattle
{
    public partial class BattleForm : Form
    {
        //Alex Holm N9918205
        private Color landscapeColour;
        Random rnd = new Random();
        private Random rng = new Random();
        private Image backgroundImage;
        private int levelWidth = 160;
        private int levelHeight = 120;


        protected int currentplayer;
        private Gameplay currentGame;
        private Opponent current_player;
        private Chassis current_chassis;
        private PlayerTank current_tank;
        private string[] weapons;

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
            //Begun Julain
            //Completed and tested Alex Holm N9918205
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



            backgroundGraphics =  InitDisplayBuffer();
            gameplayGraphics = InitDisplayBuffer();
            
            DrawBackground();
            DrawGameplay();
            NewTurn();
        }
        
    
        protected void DrawGameplay()
        {
            //begun Julain
            //Alex Holm N9918205

            Debug.WriteLine("begin draw gameplay pt 1");
            backgroundGraphics.Render(gameplayGraphics.Graphics);
            //debug code

            Debug.WriteLine("drawgameplay pt 2");
            currentGame.DrawTanks(gameplayGraphics.Graphics, displayPanel.Size);

            Debug.WriteLine("drawgameplay pt 3");
            currentGame.DisplayEffects(gameplayGraphics.Graphics, displayPanel.Size);
            Debug.WriteLine("drawGameplay Done");
        }


        protected void NewTurn()
        {
            //<Summary>
            //Alex Holm N9918205
            //</Summary>

            Debug.WriteLine("CurrentTank Get Start");
            current_tank = currentGame.GetCurrentPlayerTank();

            Debug.WriteLine("CurrentTank Get Finalized");

            Debug.WriteLine("CurrentPlayer Get Start");
            current_player = current_tank.GetPlayer();
            Debug.WriteLine("CurrentTank Get Finalized");
            
            Debug.WriteLine("CurrentForm Title Get Start");
            string Title = ("Tank Battle - Round " + currentGame.CurrentRound() + " of " + currentGame.GetMaxRounds());
            this.Text = Title;
            Debug.WriteLine("CurrentForm Title Get Finalized");

            Debug.WriteLine("Current Player colour to control panel start");
            controlPanel.BackColor = current_player.GetColour();
            Debug.WriteLine("Current player colour to control panel done");
            PlayerLabel.Text = current_player.Identifier();
            AimTurret(current_tank.GetAngle());
            PowerIndicatorLabel.Text = current_tank.GetPower().ToString();
            SetForce(current_tank.GetPower());
            
            if (currentGame.WindSpeed() > 0)
            {
                Wind.Text = currentGame.WindSpeed().ToString() + " E";
            } else
            {
                Wind.Text = Math.Abs(currentGame.WindSpeed()).ToString() + " W";
            }
            
            weaponComboBox.Items.Clear();
            Chassis current_chassiss = current_tank.GetTank();
            foreach (String weapon in current_chassiss.Weapons())
            {
                weaponComboBox.Items.Add(weapon);
            }
            ChangeWeapon(weaponComboBox.SelectedIndex);
            current_player.BeginTurn(this, currentGame);
            Debug.WriteLine("Newturn");
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
            AngleNumericUpDown.Value =  (decimal)angle;
            current_tank.AimTurret(angle);
        }

        public void SetForce(int power)
        {
            PowerBar.Value = (int)power;
            current_tank.SetForce(power);
        }
        public void ChangeWeapon(int weapon)
        {
            weaponComboBox.SelectedValue = weapon;
        }

        public void Attack()
        {
            Debug.WriteLine("BAttleForm Attack begin");
            controlPanel.Enabled = false;

            currentGame.GetCurrentPlayerTank().Attack();
            timer1.Enabled = true;
            Debug.WriteLine("BAttleForm Attack done");

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
            AimTurret((float)AngleNumericUpDown.Value);
            DrawGameplay();
            displayPanel.Invalidate();
        }

        private void PowerBar_Scroll(object sender, EventArgs e)
        {
            PowerIndicatorLabel.Text = PowerBar.Value.ToString();
            SetForce((int)PowerBar.Value);
            DrawGameplay();
            displayPanel.Invalidate();
        }

        private void weaponComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeWeapon(weaponComboBox.SelectedIndex);

        }

        private void FireButton_Click(object sender, EventArgs e)
        {
            Attack();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            Debug.WriteLine("TimerEventProcessor start");
            
            if (!currentGame.ProcessWeaponEffects())
            {
                Debug.WriteLine("if (!currentGame.ProcessWeaponEffects())");
                currentGame.CalculateGravity();
                DrawBackground();
                DrawGameplay();
                displayPanel.Invalidate();


                if (currentGame.CalculateGravity())
                {

                    Debug.WriteLine("return calculated gravity");
                    return;
                } else
                {

                    Debug.WriteLine("disable timer");
                    timer1.Enabled = false;

                    if (currentGame.TurnOver())
                    {
                        NewTurn();

                        Debug.WriteLine("Newturn done");
                    }
                    else
                    {

                        Debug.WriteLine("turn over not done");
                        Dispose();
                        currentGame.NextRound();
                        return;
                    }
                }
            }
            else
            {

                Debug.WriteLine("else processing");
                DrawGameplay();
                displayPanel.Invalidate();
                return;
            }
            
        }
    }
}
