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
        // Объекты двух игроков
        public Player FirstPlayer;
        public Player SecondPlayer;
        // Плеер для воспроизведения звуков
        public MediaPlayer Media;

        public MainWindow(string NameFirstPlayer,string NameSecondPlayer)
        {
            FirstPlayer = new Player(NameFirstPlayer);
            SecondPlayer = new Player(NameSecondPlayer);
            Subscribe();
            InitializeComponent();
            OnOffButtons(Comp.Children, false);
            FPName.Content = NameFirstPlayer;
            SPName.Content = NameSecondPlayer;
            SPHpLabel.Content = SecondPlayer.Health + "HP";
            FPHpLabel.Content = FirstPlayer.Health + "HP";
            Media = new MediaPlayer();
        }
        // Защита от врага
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

            OnOffButtons(Hero.Children, false);
            OnOffButtons(Comp.Children, true);

            FirstPlayer.GetHit(ChoseBodyPaart());
            FPHealt.Value = FirstPlayer.Health;
            FPHpLabel.Content = FirstPlayer.Health + "HP";

        }
        //Атакуем врага
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

            SPHealt.Value = SecondPlayer.Health;
            SPHpLabel.Content = SecondPlayer.Health + "HP";

            OnOffButtons(Hero.Children, true);
            OnOffButtons(Comp.Children, false);
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
            Media.Open(new Uri(@"..\..\Res\damage.wav", UriKind.Relative));
            Media.Play();
        }
        private void DoSmth1(object sender, PlayerEventArgs e)
        {
            Media.Open(new Uri(@"..\..\Res\def.wav", UriKind.Relative));
            Media.Play();
        }
        private void Died(object sender, PlayerEventArgs e)
        {
            if (e.Name == FirstPlayer.Name)
            {
                Media.Open(new Uri(@"..\..\Res\lose.mp3", UriKind.Relative));
                Media.Play();
                MessageBox.Show("YOU LOSE","=(");
            }
            else
            {
                Media.Open(new Uri(@"..\..\Res\win.mp3", UriKind.Relative));
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
            FirstPlayer = new Player(FirstPlayer.Name);
            SecondPlayer = new Player(SecondPlayer.Name);
            Subscribe();

            SPHpLabel.Content = SecondPlayer.Health + "HP";
            FPHpLabel.Content = FirstPlayer.Health + "HP";

            FPHealt.Value = 100;
            SPHealt.Value = 100;

            OnOffButtons(Comp.Children, false);
            OnOffButtons(Hero.Children, true);
        }
        // Подключаем методы к событиям
        private void Subscribe()
        {
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
        }
        private void OnOffButtons(UIElementCollection col, bool flag)
        {
            foreach (var item in col)
            {
                if (item is Button b1)
                {
                    b1.IsEnabled = flag;
                }
            }
        }
    }
}
