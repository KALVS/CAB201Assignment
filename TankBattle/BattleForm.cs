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
        private Random rng = new Random();
        private Image backgroundImage = null;
        private int levelWidth = 160;
        private int levelHeight = 120;
        private Gameplay currentGame;

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
            Random rnd = new Random();
            int i = rnd.Next(0, 3);
            backgroundImage = Image.FromFile(imageFilenames[i]);
           
            // Select a landscape colour based upon the background image
            if (i < 3)
            {
                landscapeColour = landscapeColours[i];
            } else
            {
                landscapeColour = landscapeColours[0];
            }
            

            InitializeComponent();

            InitDisplayBuffer();
            InitDisplayBuffer();

            DrawBackground();

            DrawGameplay();
            NewTurn();
        }


        private void DrawGameplay()
        {

        }

        private void NewTurn()
        {

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
            throw new NotImplementedException();
            
            
        }

        public void AimTurret(float angle)
        {
            throw new NotImplementedException();
            
            
        }

        public void SetForce(int power)
        {
            throw new NotImplementedException();
        }
        public void ChangeWeapon(int weapon)
        {
            throw new NotImplementedException();
        }

        public void Attack()
        {
            throw new NotImplementedException();
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
    }
}
