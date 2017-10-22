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

        //ПРОВЕРЬ А МОЖНО ЛИ РАБОТАТЬ С ОДНИМ ДЕЛЕГАТОМ ДЛЯ ВСЕ СОБЫТИЙ
        public delegate void BlockDelegate(object sender, PlayerEventArgs e);
        public event BlockDelegate Block; // Атака заблокирована

        public delegate void WoundDel(object sender, PlayerEventArgs e);
        public event WoundDel Wound; // Атака пропущена , урон получен

        public delegate void DeathDel(object sender, PlayerEventArgs e);
        public event DeathDel Death; // Вы умерли =(

        public Player(string Name)
        {
            this.Name = Name;
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
                Wound(this,new PlayerEventArgs(Name,HealthPoint));
                
                if (HealthPoint <=0) //Проверить : а не мертвы ли мы?
                {
                    Death(this,new PlayerEventArgs(Name,HealthPoint));
                }
            }
            else
            {
                Block(this, new PlayerEventArgs(Name,HealthPoint));
            }
        }
        public void SetBlock(BodyParts bodyPart)
        {
            Blocked = bodyPart;
        }

    }
}
