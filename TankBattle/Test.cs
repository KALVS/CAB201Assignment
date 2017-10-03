using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TankBattle;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace TankBattleTestSuite
{
    class RequirementException : Exception
    {
        public RequirementException()
        {
        }

        public RequirementException(string message) : base(message)
        {
        }

        public RequirementException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    class Test
    {
        #region Testing Code

        private delegate bool TestCase();

        private static string ErrorDescription = null;

        private static void SetErrorDescription(string desc)
        {
            ErrorDescription = desc;
        }

        private static bool FloatEquals(float a, float b)
        {
            if (Math.Abs(a - b) < 0.01) return true;
            return false;
        }

        private static Dictionary<string, string> unitTestResults = new Dictionary<string, string>();

        private static void Passed(string name, string comment)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[passed] ");
            Console.ResetColor();
            Console.Write("{0}", name);
            if (comment != "")
            {
                Console.Write(": {0}", comment);
            }
            if (ErrorDescription != null)
            {
                throw new Exception("ErrorDescription found for passing test case");
            }
            Console.WriteLine();
        }
        private static void Failed(string name, string comment)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[failed] ");
            Console.ResetColor();
            Console.Write("{0}", name);
            if (comment != "")
            {
                Console.Write(": {0}", comment);
            }
            if (ErrorDescription != null)
            {
                Console.Write("\n{0}", ErrorDescription);
                ErrorDescription = null;
            }
            Console.WriteLine();
        }
        private static void FailedToMeetRequirement(string name, string comment)
        {
            Console.Write("[      ] ");
            Console.Write("{0}", name);
            if (comment != "")
            {
                Console.Write(": ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("{0}", comment);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        private static void DoTest(TestCase test)
        {
            // Have we already completed this test?
            if (unitTestResults.ContainsKey(test.Method.ToString()))
            {
                return;
            }

            bool passed = false;
            bool metRequirement = true;
            string exception = "";
            try
            {
                passed = test();
            }
            catch (RequirementException e)
            {
                metRequirement = false;
                exception = e.Message;
            }
            catch (Exception e)
            {
                exception = e.GetType().ToString();
            }

            string className = test.Method.ToString().Replace("Boolean Test", "").Split('0')[0];
            string fnName = test.Method.ToString().Split('0')[1];

            if (metRequirement)
            {
                if (passed)
                {
                    unitTestResults[test.Method.ToString()] = "Passed";
                    Passed(string.Format("{0}.{1}", className, fnName), exception);
                }
                else
                {
                    unitTestResults[test.Method.ToString()] = "Failed";
                    Failed(string.Format("{0}.{1}", className, fnName), exception);
                }
            }
            else
            {
                unitTestResults[test.Method.ToString()] = "Failed";
                FailedToMeetRequirement(string.Format("{0}.{1}", className, fnName), exception);
            }
            Cleanup();
        }

        private static Stack<string> errorDescriptionStack = new Stack<string>();


        private static void Requires(TestCase test)
        {
            string result;
            bool wasTested = unitTestResults.TryGetValue(test.Method.ToString(), out result);

            if (!wasTested)
            {
                // Push the error description onto the stack (only thing that can change, not that it should)
                errorDescriptionStack.Push(ErrorDescription);

                // Do the test
                DoTest(test);

                // Pop the description off
                ErrorDescription = errorDescriptionStack.Pop();

                // Get the proper result for out
                wasTested = unitTestResults.TryGetValue(test.Method.ToString(), out result);

                if (!wasTested)
                {
                    throw new Exception("This should never happen");
                }
            }

            if (result == "Failed")
            {
                string className = test.Method.ToString().Replace("Boolean Test", "").Split('0')[0];
                string fnName = test.Method.ToString().Split('0')[1];

                throw new RequirementException(string.Format("-> {0}.{1}", className, fnName));
            }
            else if (result == "Passed")
            {
                return;
            }
            else
            {
                throw new Exception("This should never happen");
            }

        }

        #endregion

        #region Test Cases
        private static Gameplay InitialiseGame()
        {
            Requires(TestGameplay0Gameplay);
            Requires(TestChassis0GetTank);
            Requires(TestOpponent0Human);
            Requires(TestGameplay0RegisterPlayer);

            Gameplay game = new Gameplay(2, 1);
            Chassis tank = Chassis.GetTank(1);
            Opponent player1 = new Human("player1", tank, Color.Orange);
            Opponent player2 = new Human("player2", tank, Color.Purple);
            game.RegisterPlayer(1, player1);
            game.RegisterPlayer(2, player2);
            return game;
        }
        private static void Cleanup()
        {
            while (Application.OpenForms.Count > 0)
            {
                Application.OpenForms[0].Dispose();
            }
        }
        private static bool TestGameplay0Gameplay()
        {
            Gameplay game = new Gameplay(2, 1);
            return true;
        }
        private static bool TestGameplay0NumPlayers()
        {
            Requires(TestGameplay0Gameplay);

            Gameplay game = new Gameplay(2, 1);
            return game.NumPlayers() == 2;
        }
        private static bool TestGameplay0GetMaxRounds()
        {
            Requires(TestGameplay0Gameplay);

            Gameplay game = new Gameplay(3, 5);
            return game.GetMaxRounds() == 5;
        }
        private static bool TestGameplay0RegisterPlayer()
        {
            Requires(TestGameplay0Gameplay);
            Requires(TestChassis0GetTank);

            Gameplay game = new Gameplay(2, 1);
            Chassis tank = Chassis.GetTank(1);
            Opponent player = new Human("playerName", tank, Color.Orange);
            game.RegisterPlayer(1, player);
            return true;
        }
        private static bool TestGameplay0GetPlayer()
        {
            Requires(TestGameplay0Gameplay);
            Requires(TestChassis0GetTank);
            Requires(TestOpponent0Human);

            Gameplay game = new Gameplay(2, 1);
            Chassis tank = Chassis.GetTank(1);
            Opponent player = new Human("playerName", tank, Color.Orange);
            game.RegisterPlayer(1, player);
            return game.GetPlayer(1) == player;
        }
        private static bool TestGameplay0GetColour()
        {
            Color[] arrayOfColours = new Color[8];
            for (int i = 0; i < 8; i++)
            {
                arrayOfColours[i] = Gameplay.GetColour(i + 1);
                for (int j = 0; j < i; j++)
                {
                    if (arrayOfColours[j] == arrayOfColours[i]) return false;
                }
            }
            return true;
        }
        private static bool TestGameplay0GetPlayerPositions()
        {
            int[] positions = Gameplay.GetPlayerPositions(8);
            for (int i = 0; i < 8; i++)
            {
                if (positions[i] < 0) return false;
                if (positions[i] > 160) return false;
                for (int j = 0; j < i; j++)
                {
                    if (positions[j] == positions[i]) return false;
                }
            }
            return true;
        }
        private static bool TestGameplay0RandomReorder()
        {
            int[] ar = new int[100];
            for (int i = 0; i < 100; i++)
            {
                ar[i] = i;
            }
            Gameplay.RandomReorder(ar);
            for (int i = 0; i < 100; i++)
            {
                if (ar[i] != i)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool TestGameplay0CommenceGame()
        {
            Gameplay game = InitialiseGame();
            game.CommenceGame();

            foreach (Form f in Application.OpenForms)
            {
                if (f is BattleForm)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool TestGameplay0GetMap()
        {
            Requires(TestBattlefield0Battlefield);
            Gameplay game = InitialiseGame();
            game.CommenceGame();
            Battlefield battlefield = game.GetMap();
            if (battlefield != null) return true;

            return false;
        }
        private static bool TestGameplay0GetCurrentPlayerTank()
        {
            Requires(TestGameplay0Gameplay);
            Requires(TestChassis0GetTank);
            Requires(TestOpponent0Human);
            Requires(TestGameplay0RegisterPlayer);
            Requires(TestPlayerTank0GetPlayer);

            Gameplay game = new Gameplay(2, 1);
            Chassis tank = Chassis.GetTank(1);
            Opponent player1 = new Human("player1", tank, Color.Orange);
            Opponent player2 = new Human("player2", tank, Color.Purple);
            game.RegisterPlayer(1, player1);
            game.RegisterPlayer(2, player2);

            game.CommenceGame();
            PlayerTank ptank = game.GetCurrentPlayerTank();
            if (ptank.GetPlayer() != player1 && ptank.GetPlayer() != player2)
            {
                return false;
            }
            if (ptank.GetTank() != tank)
            {
                return false;
            }

            return true;
        }

        private static bool TestChassis0GetTank()
        {
            Chassis tank = Chassis.GetTank(1);
            if (tank != null) return true;
            else return false;
        }
        private static bool TestChassis0DrawTankSprite()
        {
            Requires(TestChassis0GetTank);
            Chassis tank = Chassis.GetTank(1);

            int[,] tankGraphic = tank.DrawTankSprite(45);
            if (tankGraphic.GetLength(0) != 12) return false;
            if (tankGraphic.GetLength(1) != 16) return false;
            // We don't really care what the tank looks like, but the 45 degree tank
            // should at least look different to the -45 degree tank
            int[,] tankGraphic2 = tank.DrawTankSprite(-45);
            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    if (tankGraphic2[y, x] != tankGraphic[y, x])
                    {
                        return true;
                    }
                }
            }

            SetErrorDescription("Tank with turret at -45 degrees looks the same as tank with turret at 45 degrees");

            return false;
        }
        private static void DisplayLine(int[,] array)
        {
            string report = "";
            report += "A line drawn from 3,0 to 0,3 on a 4x4 array should look like this:\n";
            report += "0001\n";
            report += "0010\n";
            report += "0100\n";
            report += "1000\n";
            report += "The one produced by Chassis.LineDraw() looks like this:\n";
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    report += array[y, x] == 1 ? "1" : "0";
                }
                report += "\n";
            }
            SetErrorDescription(report);
        }
        private static bool TestChassis0LineDraw()
        {
            int[,] ar = new int[,] { { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 } };
            Chassis.LineDraw(ar, 3, 0, 0, 3);

            // Ideally, the line we want to see here is:
            // 0001
            // 0010
            // 0100
            // 1000

            // However, as we aren't that picky, as long as they have a 1 in every row and column
            // and nothing in the top-left and bottom-right corners

            int[] rows = new int[4];
            int[] cols = new int[4];
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (ar[y, x] == 1)
                    {
                        rows[y] = 1;
                        cols[x] = 1;
                    }
                    else if (ar[y, x] > 1 || ar[y, x] < 0)
                    {
                        // Only values 0 and 1 are permitted
                        SetErrorDescription(string.Format("Somehow the number {0} got into the array.", ar[y, x]));
                        return false;
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (rows[i] == 0)
                {
                    DisplayLine(ar);
                    return false;
                }
                if (cols[i] == 0)
                {
                    DisplayLine(ar);
                    return false;
                }
            }
            if (ar[0, 0] == 1)
            {
                DisplayLine(ar);
                return false;
            }
            if (ar[3, 3] == 1)
            {
                DisplayLine(ar);
                return false;
            }

            return true;
        }
        private static bool TestChassis0GetTankHealth()
        {
            Requires(TestChassis0GetTank);
            // As long as it's > 0 we're happy
            Chassis tank = Chassis.GetTank(1);
            if (tank.GetTankHealth() > 0) return true;
            return false;
        }
        private static bool TestChassis0Weapons()
        {
            Requires(TestChassis0GetTank);
            // As long as there's at least one result and it's not null / a blank string, we're happy
            Chassis tank = Chassis.GetTank(1);
            if (tank.Weapons().Length == 0) return false;
            if (tank.Weapons()[0] == null) return false;
            if (tank.Weapons()[0] == "") return false;
            return true;
        }

        private static Opponent CreateTestingPlayer()
        {
            Requires(TestChassis0GetTank);
            Requires(TestOpponent0Human);

            Chassis tank = Chassis.GetTank(1);
            Opponent player = new Human("player1", tank, Color.Aquamarine);
            return player;
        }

        private static bool TestOpponent0Human()
        {
            Requires(TestChassis0GetTank);

            Chassis tank = Chassis.GetTank(1);
            Opponent player = new Human("player1", tank, Color.Aquamarine);
            if (player != null) return true;
            return false;
        }
        private static bool TestOpponent0GetTank()
        {
            Requires(TestChassis0GetTank);
            Requires(TestOpponent0Human);

            Chassis tank = Chassis.GetTank(1);
            Opponent p = new Human("player1", tank, Color.Aquamarine);
            if (p.GetTank() == tank) return true;
            return false;
        }
        private static bool TestOpponent0Identifier()
        {
            Requires(TestChassis0GetTank);
            Requires(TestOpponent0Human);

            const string PLAYER_NAME = "kfdsahskfdajh";
            Chassis tank = Chassis.GetTank(1);
            Opponent p = new Human(PLAYER_NAME, tank, Color.Aquamarine);
            if (p.Identifier() == PLAYER_NAME) return true;
            return false;
        }
        private static bool TestOpponent0GetColour()
        {
            Requires(TestChassis0GetTank);
            Requires(TestOpponent0Human);

            Color playerColour = Color.Chartreuse;
            Chassis tank = Chassis.GetTank(1);
            Opponent p = new Human("player1", tank, playerColour);
            if (p.GetColour() == playerColour) return true;
            return false;
        }
        private static bool TestOpponent0AddScore()
        {
            Opponent p = CreateTestingPlayer();
            p.AddScore();
            return true;
        }
        private static bool TestOpponent0GetPoints()
        {
            Requires(TestOpponent0AddScore);

            Opponent p = CreateTestingPlayer();
            int wins = p.GetPoints();
            p.AddScore();
            if (p.GetPoints() == wins + 1) return true;
            return false;
        }
        private static bool TestHuman0StartRound()
        {
            Opponent p = CreateTestingPlayer();
            p.StartRound();
            return true;
        }
        private static bool TestHuman0BeginTurn()
        {
            Requires(TestGameplay0CommenceGame);
            Requires(TestGameplay0GetPlayer);
            Gameplay game = InitialiseGame();

            game.CommenceGame();

            // Find the gameplay form
            BattleForm gameplayForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is BattleForm)
                {
                    gameplayForm = f as BattleForm;
                }
            }
            if (gameplayForm == null)
            {
                SetErrorDescription("Gameplay form was not created by Gameplay.CommenceGame()");
                return false;
            }

            // Find the control panel
            Panel controlPanel = null;
            foreach (Control c in gameplayForm.Controls)
            {
                if (c is Panel)
                {
                    foreach (Control cc in c.Controls)
                    {
                        if (cc is NumericUpDown || cc is Label || cc is TrackBar)
                        {
                            controlPanel = c as Panel;
                        }
                    }
                }
            }

            if (controlPanel == null)
            {
                SetErrorDescription("Control panel was not found in BattleForm");
                return false;
            }

            // Disable the control panel to check that NewTurn enables it
            controlPanel.Enabled = false;

            game.GetPlayer(1).BeginTurn(gameplayForm, game);

            if (!controlPanel.Enabled)
            {
                SetErrorDescription("Control panel is still disabled after HumanPlayer.NewTurn()");
                return false;
            }
            return true;

        }
        private static bool TestHuman0ProjectileHit()
        {
            Opponent p = CreateTestingPlayer();
            p.ProjectileHit(0, 0);
            return true;
        }

        private static bool TestPlayerTank0PlayerTank()
        {
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);
            return true;
        }
        private static bool TestPlayerTank0GetPlayer()
        {
            Requires(TestPlayerTank0PlayerTank);
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);
            if (playerTank.GetPlayer() == p) return true;
            return false;
        }
        private static bool TestPlayerTank0GetTank()
        {
            Requires(TestPlayerTank0PlayerTank);
            Requires(TestOpponent0GetTank);
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);
            if (playerTank.GetTank() == playerTank.GetPlayer().GetTank()) return true;
            return false;
        }
        private static bool TestPlayerTank0GetAngle()
        {
            Requires(TestPlayerTank0PlayerTank);
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);
            float angle = playerTank.GetAngle();
            if (angle >= -90 && angle <= 90) return true;
            return false;
        }
        private static bool TestPlayerTank0AimTurret()
        {
            Requires(TestPlayerTank0PlayerTank);
            Requires(TestPlayerTank0GetAngle);
            float angle = 75;
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);
            playerTank.AimTurret(angle);
            if (FloatEquals(playerTank.GetAngle(), angle)) return true;
            return false;
        }
        private static bool TestPlayerTank0GetPower()
        {
            Requires(TestPlayerTank0PlayerTank);
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);

            playerTank.GetPower();
            return true;
        }
        private static bool TestPlayerTank0SetForce()
        {
            Requires(TestPlayerTank0PlayerTank);
            Requires(TestPlayerTank0GetPower);
            int power = 65;
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);
            playerTank.SetForce(power);
            if (playerTank.GetPower() == power) return true;
            return false;
        }
        private static bool TestPlayerTank0GetPlayerWeapon()
        {
            Requires(TestPlayerTank0PlayerTank);

            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);

            playerTank.GetPlayerWeapon();
            return true;
        }
        private static bool TestPlayerTank0ChangeWeapon()
        {
            Requires(TestPlayerTank0PlayerTank);
            Requires(TestPlayerTank0GetPlayerWeapon);
            int weapon = 3;
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);
            playerTank.ChangeWeapon(weapon);
            if (playerTank.GetPlayerWeapon() == weapon) return true;
            return false;
        }
        private static bool TestPlayerTank0Render()
        {
            Requires(TestPlayerTank0PlayerTank);
            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);
            playerTank.Render(graphics, bitmapSize);
            graphics.Dispose();

            for (int y = 0; y < bitmapSize.Height; y++)
            {
                for (int x = 0; x < bitmapSize.Width; x++)
                {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0))
                    {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }
        private static bool TestPlayerTank0XPos()
        {
            Requires(TestPlayerTank0PlayerTank);

            Opponent p = CreateTestingPlayer();
            int x = 73;
            int y = 28;
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, x, y, game);
            if (playerTank.XPos() == x) return true;
            return false;
        }
        private static bool TestPlayerTank0GetY()
        {
            Requires(TestPlayerTank0PlayerTank);

            Opponent p = CreateTestingPlayer();
            int x = 73;
            int y = 28;
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, x, y, game);
            if (playerTank.GetY() == y) return true;
            return false;
        }
        private static bool TestPlayerTank0Attack()
        {
            Requires(TestPlayerTank0PlayerTank);

            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);
            playerTank.Attack();
            return true;
        }
        private static bool TestPlayerTank0DamagePlayer()
        {
            Requires(TestPlayerTank0PlayerTank);
            Opponent p = CreateTestingPlayer();

            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);
            playerTank.DamagePlayer(10);
            return true;
        }
        private static bool TestPlayerTank0Alive()
        {
            Requires(TestPlayerTank0PlayerTank);
            Requires(TestPlayerTank0DamagePlayer);

            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            PlayerTank playerTank = new PlayerTank(p, 32, 32, game);
            if (!playerTank.Alive()) return false;
            playerTank.DamagePlayer(playerTank.GetTank().GetTankHealth());
            if (playerTank.Alive()) return false;
            return true;
        }
        private static bool TestPlayerTank0CalculateGravity()
        {
            Requires(TestGameplay0GetMap);
            Requires(TestBattlefield0TerrainDestruction);
            Requires(TestPlayerTank0PlayerTank);
            Requires(TestPlayerTank0DamagePlayer);
            Requires(TestPlayerTank0Alive);
            Requires(TestPlayerTank0GetTank);
            Requires(TestChassis0GetTankHealth);

            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            game.CommenceGame();
            // Unfortunately we need to rely on DestroyTerrain() to get rid of any terrain that may be in the way
            game.GetMap().TerrainDestruction(Battlefield.WIDTH / 2.0f, Battlefield.HEIGHT / 2.0f, 20);
            PlayerTank playerTank = new PlayerTank(p, Battlefield.WIDTH / 2, Battlefield.HEIGHT / 2, game);
            int oldX = playerTank.XPos();
            int oldY = playerTank.GetY();

            playerTank.CalculateGravity();

            if (playerTank.XPos() != oldX)
            {
                SetErrorDescription("Caused X coordinate to change.");
                return false;
            }
            if (playerTank.GetY() != oldY + 1)
            {
                SetErrorDescription("Did not cause Y coordinate to increase by 1.");
                return false;
            }

            int initialArmour = playerTank.GetTank().GetTankHealth();
            // The tank should have lost 1 armour from falling 1 tile already, so do
            // (initialArmour - 2) damage to the tank then drop it again. That should kill it.

            if (!playerTank.Alive())
            {
                SetErrorDescription("Tank died before we could check that fall damage worked properly");
                return false;
            }
            playerTank.DamagePlayer(initialArmour - 2);
            if (!playerTank.Alive())
            {
                SetErrorDescription("Tank died before we could check that fall damage worked properly");
                return false;
            }
            playerTank.CalculateGravity();
            if (playerTank.Alive())
            {
                SetErrorDescription("Tank survived despite taking enough falling damage to destroy it");
                return false;
            }

            return true;
        }
        private static bool TestBattlefield0Battlefield()
        {
            Battlefield battlefield = new Battlefield();
            return true;
        }
        private static bool TestBattlefield0IsTileAt()
        {
            Requires(TestBattlefield0Battlefield);

            bool foundTrue = false;
            bool foundFalse = false;
            Battlefield battlefield = new Battlefield();
            for (int y = 0; y < Battlefield.HEIGHT; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH; x++)
                {
                    if (battlefield.IsTileAt(x, y))
                    {
                        foundTrue = true;
                    }
                    else
                    {
                        foundFalse = true;
                    }
                }
            }

            if (!foundTrue)
            {
                SetErrorDescription("IsTileAt() did not return true for any tile.");
                return false;
            }

            if (!foundFalse)
            {
                SetErrorDescription("IsTileAt() did not return false for any tile.");
                return false;
            }

            return true;
        }
        private static bool TestBattlefield0TankFits()
        {
            Requires(TestBattlefield0Battlefield);
            Requires(TestBattlefield0IsTileAt);

            Battlefield battlefield = new Battlefield();
            for (int y = 0; y <= Battlefield.HEIGHT - Chassis.HEIGHT; y++)
            {
                for (int x = 0; x <= Battlefield.WIDTH - Chassis.WIDTH; x++)
                {
                    int colTiles = 0;
                    for (int iy = 0; iy < Chassis.HEIGHT; iy++)
                    {
                        for (int ix = 0; ix < Chassis.WIDTH; ix++)
                        {

                            if (battlefield.IsTileAt(x + ix, y + iy))
                            {
                                colTiles++;
                            }
                        }
                    }
                    if (colTiles == 0)
                    {
                        if (battlefield.TankFits(x, y))
                        {
                            SetErrorDescription("Found collision where there shouldn't be one");
                            return false;
                        }
                    }
                    else
                    {
                        if (!battlefield.TankFits(x, y))
                        {
                            SetErrorDescription("Didn't find collision where there should be one");
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        private static bool TestBattlefield0TankYPosition()
        {
            Requires(TestBattlefield0Battlefield);
            Requires(TestBattlefield0IsTileAt);

            Battlefield battlefield = new Battlefield();
            for (int x = 0; x <= Battlefield.WIDTH - Chassis.WIDTH; x++)
            {
                int lowestValid = 0;
                for (int y = 0; y <= Battlefield.HEIGHT - Chassis.HEIGHT; y++)
                {
                    int colTiles = 0;
                    for (int iy = 0; iy < Chassis.HEIGHT; iy++)
                    {
                        for (int ix = 0; ix < Chassis.WIDTH; ix++)
                        {

                            if (battlefield.IsTileAt(x + ix, y + iy))
                            {
                                colTiles++;
                            }
                        }
                    }
                    if (colTiles == 0)
                    {
                        lowestValid = y;
                    }
                }

                int placedY = battlefield.TankYPosition(x);
                if (placedY != lowestValid)
                {
                    SetErrorDescription(string.Format("Tank was placed at {0},{1} when it should have been placed at {0},{2}", x, placedY, lowestValid));
                    return false;
                }
            }
            return true;
        }
        private static bool TestBattlefield0TerrainDestruction()
        {
            Requires(TestBattlefield0Battlefield);
            Requires(TestBattlefield0IsTileAt);

            Battlefield battlefield = new Battlefield();
            for (int y = 0; y < Battlefield.HEIGHT; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH; x++)
                {
                    if (battlefield.IsTileAt(x, y))
                    {
                        battlefield.TerrainDestruction(x, y, 0.5f);
                        if (battlefield.IsTileAt(x, y))
                        {
                            SetErrorDescription("Attempted to destroy terrain but it still exists");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            SetErrorDescription("Did not find any terrain to destroy");
            return false;
        }
        private static bool TestBattlefield0CalculateGravity()
        {
            Requires(TestBattlefield0Battlefield);
            Requires(TestBattlefield0IsTileAt);
            Requires(TestBattlefield0TerrainDestruction);

            Battlefield battlefield = new Battlefield();
            for (int x = 0; x < Battlefield.WIDTH; x++)
            {
                if (battlefield.IsTileAt(x, Battlefield.HEIGHT - 1))
                {
                    if (battlefield.IsTileAt(x, Battlefield.HEIGHT - 2))
                    {
                        // Seek up and find the first non-set tile
                        for (int y = Battlefield.HEIGHT - 2; y >= 0; y--)
                        {
                            if (!battlefield.IsTileAt(x, y))
                            {
                                // Do a gravity step and make sure it doesn't slip down
                                battlefield.CalculateGravity();
                                if (!battlefield.IsTileAt(x, y + 1))
                                {
                                    SetErrorDescription("Moved down terrain even though there was no room");
                                    return false;
                                }

                                // Destroy the bottom-most tile
                                battlefield.TerrainDestruction(x, Battlefield.HEIGHT - 1, 0.5f);

                                // Do a gravity step and make sure it does slip down
                                battlefield.CalculateGravity();

                                if (battlefield.IsTileAt(x, y + 1))
                                {
                                    SetErrorDescription("Terrain didn't fall");
                                    return false;
                                }

                                // Otherwise this seems to have worked
                                return true;
                            }
                        }


                    }
                }
            }
            SetErrorDescription("Did not find any appropriate terrain to test");
            return false;
        }
        private static bool TestWeaponEffect0RecordCurrentGame()
        {
            Requires(TestShrapnel0Shrapnel);
            Requires(TestGameplay0Gameplay);

            WeaponEffect weaponEffect = new Shrapnel(1, 1, 1);
            Gameplay game = new Gameplay(2, 1);
            weaponEffect.RecordCurrentGame(game);
            return true;
        }
        private static bool TestShell0Shell()
        {
            Requires(TestShrapnel0Shrapnel);
            Opponent player = CreateTestingPlayer();
            Shrapnel explosion = new Shrapnel(1, 1, 1);
            Shell projectile = new Shell(25, 25, 45, 30, 0.02f, explosion, player);
            return true;
        }
        private static bool TestShell0Step()
        {
            Requires(TestGameplay0CommenceGame);
            Requires(TestShrapnel0Shrapnel);
            Requires(TestShell0Shell);
            Requires(TestWeaponEffect0RecordCurrentGame);
            Gameplay game = InitialiseGame();
            game.CommenceGame();
            Opponent player = game.GetPlayer(1);
            Shrapnel explosion = new Shrapnel(1, 1, 1);

            Shell projectile = new Shell(25, 25, 45, 100, 0.01f, explosion, player);
            projectile.RecordCurrentGame(game);
            projectile.Step();

            // We can't really test this one without a substantial framework,
            // so we just call it and hope that everything works out

            return true;
        }
        private static bool TestShell0Render()
        {
            Requires(TestGameplay0CommenceGame);
            Requires(TestGameplay0GetPlayer);
            Requires(TestShrapnel0Shrapnel);
            Requires(TestShell0Shell);
            Requires(TestWeaponEffect0RecordCurrentGame);

            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Black); // Blacken out the image so we can see the projectile
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            game.CommenceGame();
            Opponent player = game.GetPlayer(1);
            Shrapnel explosion = new Shrapnel(1, 1, 1);

            Shell projectile = new Shell(25, 25, 45, 100, 0.01f, explosion, player);
            projectile.RecordCurrentGame(game);
            projectile.Render(graphics, bitmapSize);
            graphics.Dispose();

            for (int y = 0; y < bitmapSize.Height; y++)
            {
                for (int x = 0; x < bitmapSize.Width; x++)
                {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0))
                    {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }
        private static bool TestShrapnel0Shrapnel()
        {
            Opponent player = CreateTestingPlayer();
            Shrapnel explosion = new Shrapnel(1, 1, 1);

            return true;
        }
        private static bool TestShrapnel0Activate()
        {
            Requires(TestShrapnel0Shrapnel);
            Requires(TestWeaponEffect0RecordCurrentGame);
            Requires(TestGameplay0GetPlayer);
            Requires(TestGameplay0CommenceGame);

            Gameplay game = InitialiseGame();
            game.CommenceGame();
            Opponent player = game.GetPlayer(1);
            Shrapnel explosion = new Shrapnel(1, 1, 1);
            explosion.RecordCurrentGame(game);
            explosion.Activate(25, 25);

            return true;
        }
        private static bool TestShrapnel0Step()
        {
            Requires(TestShrapnel0Shrapnel);
            Requires(TestWeaponEffect0RecordCurrentGame);
            Requires(TestGameplay0GetPlayer);
            Requires(TestGameplay0CommenceGame);
            Requires(TestShrapnel0Activate);

            Gameplay game = InitialiseGame();
            game.CommenceGame();
            Opponent player = game.GetPlayer(1);
            Shrapnel explosion = new Shrapnel(1, 1, 1);
            explosion.RecordCurrentGame(game);
            explosion.Activate(25, 25);
            explosion.Step();

            // Again, we can't really test this one without a full framework

            return true;
        }
        private static bool TestShrapnel0Render()
        {
            Requires(TestShrapnel0Shrapnel);
            Requires(TestWeaponEffect0RecordCurrentGame);
            Requires(TestGameplay0GetPlayer);
            Requires(TestGameplay0CommenceGame);
            Requires(TestShrapnel0Activate);
            Requires(TestShrapnel0Step);

            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Black); // Blacken out the image so we can see the explosion
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            game.CommenceGame();
            Opponent player = game.GetPlayer(1);
            Shrapnel explosion = new Shrapnel(10, 10, 10);
            explosion.RecordCurrentGame(game);
            explosion.Activate(25, 25);
            // Step it for a bit so we can be sure the explosion is visible
            for (int i = 0; i < 10; i++)
            {
                explosion.Step();
            }
            explosion.Render(graphics, bitmapSize);

            for (int y = 0; y < bitmapSize.Height; y++)
            {
                for (int x = 0; x < bitmapSize.Width; x++)
                {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0))
                    {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }

        private static BattleForm InitialiseBattleForm(out NumericUpDown angleCtrl, out TrackBar powerCtrl, out Button fireCtrl, out Panel controlPanel, out ListBox weaponSelect)
        {
            Requires(TestGameplay0CommenceGame);

            Gameplay game = InitialiseGame();

            angleCtrl = null;
            powerCtrl = null;
            fireCtrl = null;
            controlPanel = null;
            weaponSelect = null;

            game.CommenceGame();
            BattleForm gameplayForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is BattleForm)
                {
                    gameplayForm = f as BattleForm;
                }
            }
            if (gameplayForm == null)
            {
                SetErrorDescription("Gameplay.CommenceGame() did not create a BattleForm and that is the only way BattleForm can be tested");
                return null;
            }

            bool foundDisplayPanel = false;
            bool foundControlPanel = false;

            foreach (Control c in gameplayForm.Controls)
            {
                // The only controls should be 2 panels
                if (c is Panel)
                {
                    // Is this the control panel or the display panel?
                    Panel p = c as Panel;

                    // The display panel will have 0 controls.
                    // The control panel will have separate, of which only a few are mandatory
                    int controlsFound = 0;
                    bool foundFire = false;
                    bool foundAngle = false;
                    bool foundAngleLabel = false;
                    bool foundPower = false;
                    bool foundPowerLabel = false;


                    foreach (Control pc in p.Controls)
                    {
                        controlsFound++;

                        // Mandatory controls for the control panel are:
                        // A 'Fire!' button
                        // A NumericUpDown for controlling the angle
                        // A TrackBar for controlling the power
                        // "Power:" and "Angle:" labels

                        if (pc is Label)
                        {
                            Label lbl = pc as Label;
                            if (lbl.Text.ToLower().Contains("angle"))
                            {
                                foundAngleLabel = true;
                            }
                            else
                            if (lbl.Text.ToLower().Contains("power"))
                            {
                                foundPowerLabel = true;
                            }
                        }
                        else
                        if (pc is Button)
                        {
                            Button btn = pc as Button;
                            if (btn.Text.ToLower().Contains("fire"))
                            {
                                foundFire = true;
                                fireCtrl = btn;
                            }
                        }
                        else
                        if (pc is TrackBar)
                        {
                            foundPower = true;
                            powerCtrl = pc as TrackBar;
                        }
                        else
                        if (pc is NumericUpDown)
                        {
                            foundAngle = true;
                            angleCtrl = pc as NumericUpDown;
                        }
                        else
                        if (pc is ListBox)
                        {
                            weaponSelect = pc as ListBox;
                        }
                    }

                    if (controlsFound == 0)
                    {
                        foundDisplayPanel = true;
                    }
                    else
                    {
                        if (!foundFire)
                        {
                            SetErrorDescription("Control panel lacks a \"Fire!\" button OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundAngle)
                        {
                            SetErrorDescription("Control panel lacks an angle NumericUpDown OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundPower)
                        {
                            SetErrorDescription("Control panel lacks a power TrackBar OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundAngleLabel)
                        {
                            SetErrorDescription("Control panel lacks an \"Angle:\" label OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundPowerLabel)
                        {
                            SetErrorDescription("Control panel lacks a \"Power:\" label OR the display panel incorrectly contains controls");
                            return null;
                        }

                        foundControlPanel = true;
                        controlPanel = p;
                    }

                }
                else
                {
                    SetErrorDescription(string.Format("Unexpected control ({0}) named \"{1}\" found in BattleForm", c.GetType().FullName, c.Name));
                    return null;
                }
            }

            if (!foundDisplayPanel)
            {
                SetErrorDescription("No display panel found");
                return null;
            }
            if (!foundControlPanel)
            {
                SetErrorDescription("No control panel found");
                return null;
            }
            return gameplayForm;
        }

        private static bool TestBattleForm0BattleForm()
        {
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            BattleForm gameplayForm = InitialiseBattleForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            return true;
        }
        private static bool TestBattleForm0EnableTankButtons()
        {
            Requires(TestBattleForm0BattleForm);
            Gameplay game = InitialiseGame();
            game.CommenceGame();

            // Find the gameplay form
            BattleForm gameplayForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is BattleForm)
                {
                    gameplayForm = f as BattleForm;
                }
            }
            if (gameplayForm == null)
            {
                SetErrorDescription("Gameplay form was not created by Gameplay.CommenceGame()");
                return false;
            }

            // Find the control panel
            Panel controlPanel = null;
            foreach (Control c in gameplayForm.Controls)
            {
                if (c is Panel)
                {
                    foreach (Control cc in c.Controls)
                    {
                        if (cc is NumericUpDown || cc is Label || cc is TrackBar)
                        {
                            controlPanel = c as Panel;
                        }
                    }
                }
            }

            if (controlPanel == null)
            {
                SetErrorDescription("Control panel was not found in BattleForm");
                return false;
            }

            // Disable the control panel to check that EnableControlPanel enables it
            controlPanel.Enabled = false;

            gameplayForm.EnableTankButtons();

            if (!controlPanel.Enabled)
            {
                SetErrorDescription("Control panel is still disabled after BattleForm.EnableTankButtons()");
                return false;
            }
            return true;

        }
        private static bool TestBattleForm0AimTurret()
        {
            Requires(TestBattleForm0BattleForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            BattleForm gameplayForm = InitialiseBattleForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            float testAngle = 27;

            gameplayForm.AimTurret(testAngle);
            if (FloatEquals((float)angle.Value, testAngle)) return true;

            else
            {
                SetErrorDescription(string.Format("Attempted to set angle to {0} but angle is {1}", testAngle, (float)angle.Value));
                return false;
            }
        }
        private static bool TestBattleForm0SetForce()
        {
            Requires(TestBattleForm0BattleForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            BattleForm gameplayForm = InitialiseBattleForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            int testPower = 71;

            gameplayForm.SetForce(testPower);
            if (power.Value == testPower) return true;

            else
            {
                SetErrorDescription(string.Format("Attempted to set power to {0} but power is {1}", testPower, power.Value));
                return false;
            }
        }
        private static bool TestBattleForm0ChangeWeapon()
        {
            Requires(TestBattleForm0BattleForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            BattleForm gameplayForm = InitialiseBattleForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            gameplayForm.ChangeWeapon(0);

            // WeaponSelect is optional behaviour, so it's okay if it's not implemented here, as long as the method works.
            return true;
        }
        private static bool TestBattleForm0Attack()
        {
            Requires(TestBattleForm0BattleForm);
            // This is something we can't really test properly without a proper framework, so for now we'll just click
            // the button and make sure it disables the control panel
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            BattleForm gameplayForm = InitialiseBattleForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            controlPanel.Enabled = true;
            fire.PerformClick();
            if (controlPanel.Enabled)
            {
                SetErrorDescription("Control panel still enabled immediately after clicking fire button");
                return false;
            }

            return true;
        }
        private static void UnitTests()
        {
            DoTest(TestGameplay0Gameplay);
            DoTest(TestGameplay0NumPlayers);
            DoTest(TestGameplay0GetMaxRounds);
            DoTest(TestGameplay0RegisterPlayer);
            DoTest(TestGameplay0GetPlayer);
            DoTest(TestGameplay0GetColour);
            DoTest(TestGameplay0GetPlayerPositions);
            DoTest(TestGameplay0RandomReorder);
            DoTest(TestGameplay0CommenceGame);
            DoTest(TestGameplay0GetMap);
            DoTest(TestGameplay0GetCurrentPlayerTank);
            DoTest(TestChassis0GetTank);
            DoTest(TestChassis0DrawTankSprite);
            DoTest(TestChassis0LineDraw);
            DoTest(TestChassis0GetTankHealth);
            DoTest(TestChassis0Weapons);
            DoTest(TestOpponent0Human);
            DoTest(TestOpponent0GetTank);
            DoTest(TestOpponent0Identifier);
            DoTest(TestOpponent0GetColour);
            DoTest(TestOpponent0AddScore);
            DoTest(TestOpponent0GetPoints);
            DoTest(TestHuman0StartRound);
            DoTest(TestHuman0BeginTurn);
            DoTest(TestHuman0ProjectileHit);
            DoTest(TestPlayerTank0PlayerTank);
            DoTest(TestPlayerTank0GetPlayer);
            DoTest(TestPlayerTank0GetTank);
            DoTest(TestPlayerTank0GetAngle);
            DoTest(TestPlayerTank0AimTurret);
            DoTest(TestPlayerTank0GetPower);
            DoTest(TestPlayerTank0SetForce);
            DoTest(TestPlayerTank0GetPlayerWeapon);
            DoTest(TestPlayerTank0ChangeWeapon);
            DoTest(TestPlayerTank0Render);
            DoTest(TestPlayerTank0XPos);
            DoTest(TestPlayerTank0GetY);
            DoTest(TestPlayerTank0Attack);
            DoTest(TestPlayerTank0DamagePlayer);
            DoTest(TestPlayerTank0Alive);
            DoTest(TestPlayerTank0CalculateGravity);
            DoTest(TestBattlefield0Battlefield);
            DoTest(TestBattlefield0IsTileAt);
            DoTest(TestBattlefield0TankFits);
            DoTest(TestBattlefield0TankYPosition);
            DoTest(TestBattlefield0TerrainDestruction);
            DoTest(TestBattlefield0CalculateGravity);
            DoTest(TestWeaponEffect0RecordCurrentGame);
            DoTest(TestShell0Shell);
            DoTest(TestShell0Step);
            DoTest(TestShell0Render);
            DoTest(TestShrapnel0Shrapnel);
            DoTest(TestShrapnel0Activate);
            DoTest(TestShrapnel0Step);
            DoTest(TestShrapnel0Render);
            DoTest(TestBattleForm0BattleForm);
            DoTest(TestBattleForm0EnableTankButtons);
            DoTest(TestBattleForm0AimTurret);
            DoTest(TestBattleForm0SetForce);
            DoTest(TestBattleForm0ChangeWeapon);
            DoTest(TestBattleForm0Attack);
        }
        
        #endregion
        
        #region CheckClasses

        private static bool CheckClasses()
        {
            string[] classNames = new string[] { "Program", "ComputerPlayer", "Battlefield", "Shrapnel", "BattleForm", "Gameplay", "Human", "Shell", "Opponent", "PlayerTank", "Chassis", "WeaponEffect" };
            string[][] classFields = new string[][] {
                new string[] { "Main" }, // Program
                new string[] { }, // ComputerPlayer
                new string[] { "IsTileAt","TankFits","TankYPosition","TerrainDestruction","CalculateGravity","WIDTH","HEIGHT"}, // Battlefield
                new string[] { "Activate" }, // Shrapnel
                new string[] { "EnableTankButtons","AimTurret","SetForce","ChangeWeapon","Attack","InitDisplayBuffer"}, // BattleForm
                new string[] { "NumPlayers","CurrentRound","GetMaxRounds","RegisterPlayer","GetPlayer","PlayerTank","GetColour","GetPlayerPositions","RandomReorder","CommenceGame","BeginRound","GetMap","DrawTanks","GetCurrentPlayerTank","AddEffect","ProcessWeaponEffects","DisplayEffects","CancelEffect","CheckCollidedTank","DamagePlayer","CalculateGravity","TurnOver","ScoreWinner","NextRound","WindSpeed"}, // Gameplay
                new string[] { }, // Human
                new string[] { }, // Shell
                new string[] { "GetTank","Identifier","GetColour","AddScore","GetPoints","StartRound","BeginTurn","ProjectileHit"}, // Opponent
                new string[] { "GetPlayer","GetTank","GetAngle","AimTurret","GetPower","SetForce","GetPlayerWeapon","ChangeWeapon","Render","XPos","GetY","Attack","DamagePlayer","Alive","CalculateGravity"}, // PlayerTank
                new string[] { "DrawTankSprite","LineDraw","CreateTankBitmap","GetTankHealth","Weapons","WeaponLaunch","GetTank","WIDTH","HEIGHT","NUM_TANKS"}, // Chassis
                new string[] { "RecordCurrentGame","Step","Render"} // WeaponEffect
            };

            Assembly assembly = Assembly.GetExecutingAssembly();

            Console.WriteLine("Checking classes for public methods...");
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsPublic)
                {
                    if (type.Namespace != "TankBattle")
                    {
                        Console.WriteLine("Public type {0} is not in the TankBattle namespace.", type.FullName);
                        return false;
                    }
                    else
                    {
                        int typeIdx = -1;
                        for (int i = 0; i < classNames.Length; i++)
                        {
                            if (type.Name == classNames[i])
                            {
                                typeIdx = i;
                                classNames[typeIdx] = null;
                                break;
                            }
                        }
                        foreach (MemberInfo memberInfo in type.GetMembers())
                        {
                            string memberName = memberInfo.Name;
                            bool isInherited = false;
                            foreach (MemberInfo parentMemberInfo in type.BaseType.GetMembers())
                            {
                                if (memberInfo.Name == parentMemberInfo.Name)
                                {
                                    isInherited = true;
                                    break;
                                }
                            }
                            if (!isInherited)
                            {
                                if (typeIdx != -1)
                                {
                                    bool fieldFound = false;
                                    if (memberName[0] != '.')
                                    {
                                        foreach (string allowedFields in classFields[typeIdx])
                                        {
                                            if (memberName == allowedFields)
                                            {
                                                fieldFound = true;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        fieldFound = true;
                                    }
                                    if (!fieldFound)
                                    {
                                        Console.WriteLine("The public field \"{0}\" is not one of the authorised fields for the {1} class.\n", memberName, type.Name);
                                        Console.WriteLine("Remove it or change its access level.");
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    //Console.WriteLine("{0} passed.", type.FullName);
                }
            }
            for (int i = 0; i < classNames.Length; i++)
            {
                if (classNames[i] != null)
                {
                    Console.WriteLine("The class \"{0}\" is missing.", classNames[i]);
                    return false;
                }
            }
            Console.WriteLine("All public methods okay.");
            return true;
        }
        
        #endregion

        public static void Main()
        {
            if (CheckClasses())
            {
                UnitTests();

                int passed = 0;
                int failed = 0;
                foreach (string key in unitTestResults.Keys)
                {
                    if (unitTestResults[key] == "Passed")
                    {
                        passed++;
                    }
                    else
                    {
                        failed++;
                    }
                }

                Console.WriteLine("\n{0}/{1} unit tests passed", passed, passed + failed);
                if (failed == 0)
                {
                    Console.WriteLine("Starting up TankBattle...");
                    Program.Main();
                    return;
                }
            }

            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}
