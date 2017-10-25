namespace FightClub
{
    // Части тела игрока
    public enum BodyParts
    {
        Head,
        Body,
        Legs
    }
    
    public class Player
    {
        private string PlayerName;
        private BodyParts Blocked; 
        private int HealthPoint = 100; 
        public const int ImpactForce = 25; // Damage that is dealt

        //Delegate for my events
        public delegate void MyDelegate(object sender, PlayerEventArgs e);
        public event MyDelegate Block; 
        public event MyDelegate Wound; 
        public event MyDelegate Death; 

        public Player(string Name)
        {
            PlayerName = Name;
        }
        public string Name
        {
            get => PlayerName;
            set => PlayerName = value;
        }
        public int Health
        {
            get => HealthPoint;
            set => HealthPoint = value;
        }
        public void GetHit(BodyParts bodyPart)
        {
            if (Blocked != bodyPart)
            {
                HealthPoint -= ImpactForce;
                Wound(this,new PlayerEventArgs(PlayerName,HealthPoint,"получил урон"));
                
                if (HealthPoint <=0) //Проверить : а не мертвы ли мы?
                {
                    Death(this,new PlayerEventArgs(PlayerName,HealthPoint,"умер"));
                }
            }
            else
            {
                Block(this, new PlayerEventArgs(PlayerName,HealthPoint,"заблокировал урон"));
            }
        }
        public void SetBlock(BodyParts bodyPart)
        {
            Blocked = bodyPart;
        }

    }
}
