using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightClub
{
    public class PlayerEventArgs
    {
        public string Name;
        public double HealthPoint;

        public PlayerEventArgs(string Name, double HealthPoint)
        {
            this.Name = Name;
            this.HealthPoint = HealthPoint;
        }
    }
}
