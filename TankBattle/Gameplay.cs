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
        private int numRounds = 5;
        int numberOfPlayers;
        List<WeaponEffect> Weapon;
        int[] Opponent;
        int current_round = 0;
        int playernumber = 1;

        public Gameplay(int numPlayers, int numRounds)
        {
            this.numberOfPlayers = numPlayers;

            int[] Opponent = new int[numPlayers];
            Weapon = new List<WeaponEffect>();
            
        }

        public int NumPlayers()
        {
            if (numberOfPlayers < 2)
            {
                numberOfPlayers = 2;
            }
            if (numberOfPlayers > 8)
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
            return numRounds;
        }

        public void RegisterPlayer(int playerNum, Opponent player)
        {
            //take the player number between 1 and numver of players
            //set the appropriate field in gameplay's opponent array to player
            //note to -1 from playernumber
            for (int i = numberOfPlayers; i > 1; i--)
            {
                playerNum = i;
                int[] opponent = new int[] { playerNum };
                
            }
        }

        public Opponent GetPlayer(int playerNum)
        {
            throw new NotImplementedException();
        }

        public PlayerTank PlayerTank(int playerNum)
        {
            throw new NotImplementedException();
        }

        public static Color GetColour(int playerNum)
        {
            throw new NotImplementedException();
        }

        public static int[] GetPlayerPositions(int numPlayers)
        {
            throw new NotImplementedException();
        }

        public static void RandomReorder(int[] array)
        {
            throw new NotImplementedException();
        }

        public void CommenceGame()
        {
            throw new NotImplementedException();
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
