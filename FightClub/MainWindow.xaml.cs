using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FightClub
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Player FirstPlayer;
        public Player SecondPlayer;
        public MediaPlayer Media;

        public MainWindow(string NameFirstPlayer,string NameSecondPlayer)
        {
            FirstPlayer = new Player(NameFirstPlayer);
            FirstPlayer.Wound += DoSmth;
            FirstPlayer.Wound += Log;
            FirstPlayer.Block += DoSmth1;
            FirstPlayer.Block += Log;
            FirstPlayer.Death += Died;
            FirstPlayer.Death += Log;

            SecondPlayer = new Player(NameSecondPlayer);
            SecondPlayer.Wound += Log;
            SecondPlayer.Wound += DoSmth;
            SecondPlayer.Block += DoSmth1;
            SecondPlayer.Block += Log;
            SecondPlayer.Death += Died;
            SecondPlayer.Death += Log;
            InitializeComponent();

            foreach (var item in Comp.Children)
            {
                if (item is Button b1)
                {
                    b1.IsEnabled = false;
                }
            }
            FPName.Content = NameFirstPlayer;
            SPName.Content = NameSecondPlayer;
            SPHpLabel.Content = SecondPlayer.GetHealth() + "HP";
            FPHpLabel.Content = FirstPlayer.GetHealth() + "HP";
            Media = new MediaPlayer();
        }

        private void DeffendYourBodyParts(object sender, RoutedEventArgs e)
        {
            Button b1 = (Button)sender;
            if ((string)b1.Content == "Head")
            {
                FirstPlayer.SetBlock(BodyParts.Head);
            }
            if ((string)b1.Content == "Body")
            {
                FirstPlayer.SetBlock(BodyParts.Body);
            }
            if ((string)b1.Content == "Legs")
            {
                FirstPlayer.SetBlock(BodyParts.Legs);
            }

            FirstPlayer.GetHit(ChoseBodyPaart());
            FPHealt.Value = FirstPlayer.GetHealth();
            FPHpLabel.Content = FirstPlayer.GetHealth() + "HP";
            foreach (var item in Hero.Children)
            {
                if (item is Button b)
                {
                    b.IsEnabled = false;
                }
            }

            foreach (var item in Comp.Children)
            {
                if (item is Button b3)
                {
                    b3.IsEnabled = true;
                }
            }
        }
        private void AttackBodyParts(object sender, RoutedEventArgs e)
        {
            SecondPlayer.SetBlock(ChoseBodyPaart());
            Button b = (Button)sender;
            if ((string)b.Content == "Head")
            {
                SecondPlayer.GetHit(BodyParts.Head);
            }
            if ((string)b.Content == "Body")
            {
                SecondPlayer.GetHit(BodyParts.Body);
            }
            if ((string)b.Content == "Legs")
            {
                SecondPlayer.GetHit(BodyParts.Legs);
            }

            SPHealt.Value = SecondPlayer.GetHealth();
            SPHpLabel.Content = SecondPlayer.GetHealth() + "HP";

            foreach (var item in Hero.Children)
            {
                if (item is Button b2)
                {
                    b2.IsEnabled = true;
                }
            }

            foreach (var item in Comp.Children)
            {
                if (item is Button b1)
                {
                    b1.IsEnabled = false;
                }
            }
        }

        // Random chose body part for bot
        private BodyParts ChoseBodyPaart()
        {
            Random r = new Random();
            int x = r.Next(0, 3);
            return (BodyParts)x;
        }

        private void DoSmth(object sender,PlayerEventArgs e)
        {
            //MessageBox.Show("Ай как больно то");
            Media.Open(new Uri(@"damage.wav", UriKind.Relative));
            Media.Play();
        }
        private void DoSmth1(object sender, PlayerEventArgs e)
        {
            //MessageBox.Show("хрен попадёшь!!!!");
            Media.Open(new Uri(@"def.wav", UriKind.Relative));
            Media.Play();
        }
        private void Died(object sender, PlayerEventArgs e)
        {
            if (e.Name == FirstPlayer.GetName())
            {
                Media.Open(new Uri(@"lose.mp3",UriKind.Relative));
                Media.Play();
                MessageBox.Show("YOU LOSE","=(");
            }
            else
            {
                Media.Open(new Uri(@"win.mp3", UriKind.Relative));
                Media.Play();
                MessageBox.Show("YOU WIN");
            }
            Restart();
        }
        private void Log(object sender, PlayerEventArgs e)
        {
            LogBlock.Text += "Игрок " + e.Name + " " + e.Message + " - " + e.HealthPoint + "HP" + "\n";
        }
        private void Restart()
        {
            FirstPlayer = new Player(FirstPlayer.GetName());
            SecondPlayer = new Player(SecondPlayer.GetName());

            FirstPlayer.Wound += DoSmth;
            FirstPlayer.Wound += Log;
            FirstPlayer.Block += DoSmth1;
            FirstPlayer.Block += Log;
            FirstPlayer.Death += Died;
            FirstPlayer.Death += Log;

            SecondPlayer.Wound += Log;
            SecondPlayer.Wound += DoSmth;
            SecondPlayer.Block += DoSmth1;
            SecondPlayer.Block += Log;
            SecondPlayer.Death += Died;
            SecondPlayer.Death += Log;

            SPHpLabel.Content = SecondPlayer.GetHealth() + "HP";
            FPHpLabel.Content = FirstPlayer.GetHealth() + "HP";

            FPHealt.Value = 100;
            SPHealt.Value = 100;

            foreach (var item in Comp.Children)
            {
                if (item is Button b1)
                {
                    b1.IsEnabled = false;
                }
            }

            foreach (var item in Hero.Children)
            {
                if (item is Button b2)
                {
                    b2.IsEnabled = true;
                }
            }
        }
    }
}
