namespace FightClub
{
    public class PlayerEventArgs
    {
        public string Name;
        public double HealthPoint;
        public string Message;

        public PlayerEventArgs(string Name, double HealthPoint,string Message)
        {
            this.Name = Name;
            this.HealthPoint = HealthPoint;
            this.Message = Message;
        }
    }
}
