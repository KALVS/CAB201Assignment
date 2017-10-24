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
        List<WeaponEffect> Weapon;
        private Opponent[] opponents;
        private int current_round;

        public Gameplay(int numPlayers, int numRounds)
        {
            this.numberOfPlayers = numPlayers;
            //Creates a numPlayers size array of Opponent(which is stored as a private field of Gameplay)
            this.opponents = new Opponent[numPlayers];
            //Sets another private field to the number of rounds that will be played
            this.numberOfRounds = numRounds;
            //Creates an array or list collection of WeaponEffect
            Weapon = new List<WeaponEffect>();
        }

        public int NumPlayers()
        {
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
            current_round++;
            return current_round;
        }

        public int GetMaxRounds()
        {
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
            opponents[playerNum - 1] = player;
        }

        public Opponent GetPlayer(int playerNum)
        {
            return opponents[playerNum - 1];
        }

        public PlayerTank PlayerTank(int playerNum)
        {
            throw new NotImplementedException();
        }

        public static Color GetColour(int playerNum)
        {
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
            {
                return Color.Black;
            }
                
            
        }

        public static int[] GetPlayerPositions(int numPlayers)
        {
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
            current_round = 1;
            numberOfPlayers = 0;
            BeginRound();
        }

        public void BeginRound()
        {
            throw new NotImplementedException();
        }

        public Battlefield GetMap()
        {
            throw new NotImplementedException();
        }

        public void DrawTanks(Graphics graphics, Size displaySize)
        {
            throw new NotImplementedException();
        }

        public PlayerTank GetCurrentPlayerTank()
        {
            throw new NotImplementedException();
        }

        public void AddEffect(WeaponEffect weaponEffect)
        {
            throw new NotImplementedException();
        }

        public bool ProcessWeaponEffects()
        {
            throw new NotImplementedException();
        }

        public void DisplayEffects(Graphics graphics, Size displaySize)
        {
            throw new NotImplementedException();
        }

        public void CancelEffect(WeaponEffect weaponEffect)
        {
            throw new NotImplementedException();
        }

        public bool CheckCollidedTank(float projectileX, float projectileY)
        {
            throw new NotImplementedException();
        }

        public void DamagePlayer(float damageX, float damageY, float explosionDamage, float radius)
        {
            throw new NotImplementedException();
        }

        public bool CalculateGravity()
        {
            throw new NotImplementedException();
        }

        public bool TurnOver()
        {
            throw new NotImplementedException();
        }

        public void ScoreWinner()
        {
            throw new NotImplementedException();
        }

        public void NextRound()
        {
            throw new NotImplementedException();
        }
        
        public int WindSpeed()
        {
            throw new NotImplementedException();
        }
    }
}
