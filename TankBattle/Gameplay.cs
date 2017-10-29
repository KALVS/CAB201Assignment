using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TankBattle
{
    public class Gameplay
    {
        private int numberOfPlayers;
        private int numberOfRounds;
        List<WeaponEffect> Weapon_effects;
        private Opponent[] opponents;
        private int current_round;
        private int startingplayer;
        private Opponent currentplayer;
        private Battlefield map;
        private int[] playerpos = new int[8];
        private PlayerTank[] Playertanks;
        private int wind;
        private PlayerTank current_tank;
        private Chassis current_chassis;
        Random rnd = new Random();

        public Gameplay(int numPlayers, int numRounds)
        {
            //<summary>
            //Alex Holm N9918205
            //</summary>
            this.numberOfPlayers = numPlayers;
            //Creates a numPlayers size array of Opponent(which is stored as a private field of Gameplay)
            this.opponents = new Opponent[numPlayers];
            //Sets another private field to the number of rounds that will be played
            this.numberOfRounds = numRounds;
            //Creates an array or list collection of WeaponEffect
            Weapon_effects = new List<WeaponEffect>();
        }

        public int NumPlayers()
        {
            //<Summary>
            //Alex Holm N9918205
            //</summary>
            if (numberOfPlayers < 2)
            {
                numberOfPlayers = 2;
            } else if (numberOfPlayers > 8)
            {
                numberOfPlayers = 8;
            }
            return numberOfPlayers;
        }

        public int CurrentRound()
        {
            //<Summary>
            //Alex Holm N9918205
            //</summary>
            current_round++;
            return current_round;
        }

        public int GetMaxRounds()
        {   
            //<Summary>
            //Alex Holm N9918205
            //</summary>
            if (numberOfRounds < 2)
            {
                numberOfRounds = 2;
            } else if (numberOfRounds > 100)
            {
                numberOfRounds = 100;
            }
            return numberOfRounds;
        }
        
        public void RegisterPlayer(int playerNum, Opponent player)
        {
            //<Summary>
            //Alex Holm N9918205
            //</summary>
            opponents[playerNum - 1] = player;
        }

        public Opponent GetPlayer(int playerNum)
        {
            //<Summary>
            //Alex Holm N9918205
            //</summary>
            return opponents[playerNum - 1];
        }

        public PlayerTank PlayerTank(int playerNum)
        {
            //<Summary>
            //Alex Holm N9918205
            //</summary>
            return Playertanks[numberOfPlayers - 1];
        }

        public static Color GetColour(int playerNum)
        {
            //<Summary>
            //Alex Holm N9918205
            //</summary>
            Color a = Color.AntiqueWhite;
            Color b = Color.Blue;
            Color c = Color.Crimson;
            Color d = Color.DeepPink;
            Color e = Color.DarkViolet;
            Color f = Color.ForestGreen;
            Color g = Color.GreenYellow;
            Color h = Color.Honeydew;

            if (playerNum == 1)
            {
                return a;
            } else 
            if (playerNum == 2)
            {
                return b;
            } else
            if (playerNum == 3)
            {
                return c;
            } else
            if (playerNum == 4)
            { 
                return d;
            } else
            if (playerNum == 5)
            {
                return e;
            } else
            if (playerNum == 6)
            {
                return f;
            } else
            if (playerNum == 7)
            {
                return g;
            } else
            if (playerNum == 8)
            {
                return h;
            } else
            if (playerNum == 0)
            {
                return Color.Black;
            } else
            {
                return Color.PaleGoldenrod;
            }
                
            
        }

        public static int[] GetPlayerPositions(int numPlayers)
        {
            //<Summary>
            //Alex Holm N9918205
            //</summary>
            int segments = numPlayers + 1;
            int dist = Battlefield.WIDTH / segments;
            int[] playerPositions = new int[numPlayers];
            
            for (int player = 0; player <= numPlayers-1; player++)
            {
                playerPositions[player] = (dist/2) + (dist * player) - (Chassis.WIDTH/2);
            }
            return playerPositions;

        }

        public static void RandomReorder(int[] array)
        {
            //<Summary>
            //Alex Holm N9918205
            //</summary>
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                int idx = random.Next(i, array.Length);

                //swap elements
                int tmp = array[i];
                array[i] = array[idx];
                array[idx] = tmp;
            }
        }

        public void CommenceGame()
        {
            //<Summary>
            //Alex Holm N9918205
            //</summary>
            current_round = 1;
            startingplayer = 0;
            BeginRound();
        }

        public void BeginRound()
        {
            //<Summary>
            //Alex Holm N9918205
            //</summary>

            //Initialising a private field of Gameplay representing the current player to the value of the starting Opponent field(see CommenceGame).
            currentplayer = opponents[startingplayer];
            //Creating a new Battlefield, which is also stored as a private field of Gameplay.
            map = new Battlefield();
            //Creating an array of Opponent positions by calling GetPlayerPositions with the number of Opponents playing the game(hint: get the length of the Opponents array
            playerpos = GetPlayerPositions(opponents.Length);
            //Looping through each Opponent and calling its StartRound method.
            for (int i = 0; i < opponents.Length; i++)
            {
                opponents[i].StartRound();
            }
            //Shuffling that array of positions with the RandomReorder method.
            RandomReorder(playerpos);
            //Creating an array of PlayerTank as a private field.There should be the same number of PlayerTanks as there are Opponents in the Opponent array.
            Playertanks = new PlayerTank[opponents.Length];

            //Initialising the array of PlayerTank by finding the horizontal position of the PlayerTank
            //(by looking up the appropriate index of the array returned by GetPlayerPositions and shuffled with the RandomReorder method)

            //the vertical position of the PlayerTank(by calling TankYPosition() on the Battlefield with the horizontal position as an argument)
            //and then calling PlayerTank's constructor to create that PlayerTank (passing in the appropriate Opponent, the horizontal position, the vertical position and a reference to this)
            for (int i = 0; i < playerpos.Length; i++)
            {
                Playertanks[i] = new PlayerTank(opponents[i], playerpos[i], map.TankYPosition(playerpos[i]), this);
            }
            
            //Initialising the wind speed, another private field of Gameplay, to a random number between -100 and 100.
            WindSpeed();
            //Creating a new BattleForm and Show()ing it.
            BattleForm Form = new BattleForm(this);
            Form.Show();
        }

        public Battlefield GetMap()
        {
            return map;
        }

        public void DrawTanks(Graphics graphics, Size displaySize)
        {
            //Alex Holm N9918205
            //Loop over each PlayerTanks in the array.
            for ( int i = 0; i < Playertanks.Length; i++)
            {
                //Check if a PlayerTank is still around by calling its Alive() method.
                if (Playertanks[i].Alive())
                {
                    //If it is, call its Render() method, passing in graphics and displaySize.
                    Playertanks[i].Render(graphics, displaySize);
                }
            };
        }

        public PlayerTank GetCurrentPlayerTank()
        {
            //Alex Holm N918205
            //This method returns the PlayerTank associated with the current player.
            //Both the current player and an array of PlayerTank are private fields of Gameplay and are also initialised in BeginRound().
            return Playertanks[startingplayer];
            

        }

        public void AddEffect(WeaponEffect weaponEffect)
        {
            //Alex Holm N9918205
            Weapon_effects.Add(weaponEffect);
            //Record Current Game on Weapon Effect. Passing THIS into reference
            weaponEffect.RecordCurrentGame(this);
        }

        public bool ProcessWeaponEffects()
        {
            int steps = 0;
            for (int i = 0; i < Weapon_effects.Count; i++)
            {
                if (Weapon_effects == null) { }
                else
                {
                    Weapon_effects[i].Step();
                    steps++;
                }
            }
            if (steps == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void DisplayEffects(Graphics graphics, Size displaySize)
        {
            for (int i = 0; i < Weapon_effects.Count; i++)
            {
                GetCurrentPlayerTank().Render(graphics, displaySize);
            }
        }

        public void CancelEffect(WeaponEffect weaponEffect)
        {
            Weapon_effects.Remove(weaponEffect);
        }

        public bool CheckCollidedTank(float projectileX, float projectileY)
        {
            bool result = false;
            if ( projectileX < 0 || projectileY < 0 || projectileX > Battlefield.WIDTH || projectileY > Battlefield.HEIGHT)
            {
                result = false;
            }
            if (map.IsTileAt((int)projectileX, (int)projectileY)){
                result = true;
            }
            current_tank = GetCurrentPlayerTank();
            int current_tankY = current_tank.GetY();
            int current_tankX = current_tank.XPos();
            if ( current_tankY == (int)projectileY && current_tankX == (int)projectileX)
            {
                if (current_tank.Alive())
                {
                    result = true;
                }
            }
            for (int i = 0; i < Playertanks.Length; i++)
            {
                if (Playertanks[i].XPos() < projectileX &&
                    Playertanks[i].XPos() + Playertanks[i].XPos() + Chassis.WIDTH > projectileX &&
                    Playertanks[i].GetY() < projectileY &&
                    Chassis.HEIGHT + Playertanks[i].GetY() > projectileY)
                {
                    if (Playertanks[i].Alive())
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public void DamagePlayer(float damageX, float damageY, float explosionDamage, float radius)
        {
            float damagedone = 0;
            for (int i = 0; i < Playertanks.Length; i++)
            {
               if (Playertanks[i].Alive())
                {
                    int tankXcenter = Playertanks[i].XPos() + (Chassis.WIDTH / 2);
                    int tankYcenter = Playertanks[i].GetY() + (Chassis.HEIGHT / 2);
                    double distance = Math.Sqrt(Math.Pow(tankXcenter - damageY, 2) + Math.Pow(tankYcenter - damageY, 2));
                    if (distance > radius) { }
                    if (distance < radius && distance > radius / 2)
                    {
                        damagedone = explosionDamage * (radius - (float)distance) / radius;
                    }
                    if (distance < radius / 2)
                    {
                        damagedone = explosionDamage;
                    }
                    DamagePlayer(tankXcenter, tankYcenter, damagedone, radius);
                }
            }
        }

        public bool CalculateGravity()
        {
            bool result = false;
            if (map.CalculateGravity())
            {
                result = true;
            }
            for (int i = 0; i < Playertanks.Length; i++)
            {
                if (Playertanks[i].CalculateGravity())
                {
                    result = true;
                }
            }
            return result;
        }

        public bool TurnOver()
        {
            bool result = true;
            int players_alive = 0;
            for (int i = 0; i < opponents.Length; i++)
            {
                if (Playertanks[i].Alive())
                {
                    players_alive++;
                }
            }
            if (players_alive >= 2)
            {
                wind = wind + rnd.Next(-10, 10);
                result = true;
            }
            else if (players_alive == 0 || players_alive == 1)
            {
                ScoreWinner();
                result = false;
                
            }
                return result;
        }

        public void ScoreWinner()
        {
            for (int i = 0; i < opponents.Length; i++)
            {
                if (Playertanks[i].Alive())
                {
                    opponents[i].AddScore();
                }
            }
        }

        public void NextRound()
        {
            current_round++;
        }
        
        public int WindSpeed()
        {
            Random rnd = new Random();
            int wind = rnd.Next(-100, 100);
            return wind;
        }
    }
}
