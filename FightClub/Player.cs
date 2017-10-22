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
    
    public class Player
    {
        private string Name; // Имя игрока
        private BodyParts Blocked; // Часть тела которую мы блокируем
        private int HealthPoint = 100; // Текущее кол-во ХП
        public const int ImpactForce = 25; // Дамаг которые наности какждая атака

        //Delegate for my events
        public delegate void MyDelegate(object sender, PlayerEventArgs e);

        public event MyDelegate Block; // Атака заблокирована
        public event MyDelegate Wound; // Атака пропущена , урон получен
        public event MyDelegate Death; // Вы умерли =(

        public Player(string Name)
        {
            this.Name = Name;
        }
        public string GetName()
        {
            return Name;
        }
        public int GetHealth()
        {
            return HealthPoint;
        }
        public void GetHit(BodyParts bodyPart)
        {
            if (Blocked != bodyPart)
            {
                HealthPoint -= ImpactForce;
                Wound(this,new PlayerEventArgs(Name,HealthPoint,"получил урон"));
                
                if (HealthPoint <=0) //Проверить : а не мертвы ли мы?
                {
                    Death(this,new PlayerEventArgs(Name,HealthPoint,"умер"));
                }
            }
            else
            {
                Block(this, new PlayerEventArgs(Name,HealthPoint,"заблокировал урон"));
            }
        }
        public void SetBlock(BodyParts bodyPart)
        {
            Blocked = bodyPart;
        }

    }
}
