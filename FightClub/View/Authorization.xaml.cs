using System.Windows;
using FightClub.Presentor;

namespace FightClub
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            AuthorizationPresentor authorizationPresentor = new AuthorizationPresentor(this);
        }   
    }
}
