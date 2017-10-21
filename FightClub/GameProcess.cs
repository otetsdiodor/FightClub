using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightClub
{
    class GameProcess
    {
        public Player FirstPlayer;
        public Player SecondPlayer;

        public int StepsCount;

        public GameProcess(Player p1,Player p2)
        {
            FirstPlayer = p1;
            SecondPlayer = p2;
        }

        public void Start()
        {
            while (true)
            {

            }
        }
    }
}
