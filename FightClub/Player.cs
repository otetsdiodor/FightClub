using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightClub
{
    public enum BodyParts
    {
        Head,
        Body,
        Legs
    }
    class Player
    {
        private string Name;
        private BodyParts Blocked;
        private double HealthPoint;
        public Player()
        {

        }

        public void GetHit(BodyParts bodyPart)
        {

        }
        public void SetBlock(BodyParts bodyPart)
        {
            Blocked = bodyPart;
        }

    }
}
