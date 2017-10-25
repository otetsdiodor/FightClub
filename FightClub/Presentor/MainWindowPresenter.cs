using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FightClub.Presentor
{
    class MainWindowPresenter
    {
        Player FirstPlayer = null;
        Player SecondPlayer = null;
        MainWindow MainWindow = null;
        public MediaPlayer Media = null;

        public MainWindowPresenter(MainWindow mainWindow,string firstName,string secondName)
        {
            MainWindow = mainWindow;
            FirstPlayer = new Player(firstName);
            SecondPlayer = new Player(secondName);
            Media = new MediaPlayer();
            Subscribe();
            OnOffButtons(MainWindow.Comp.Children, false);
            MainWindow.FPName.Content = firstName;
            MainWindow.SPName.Content = secondName;
            MainWindow.SPHpLabel.Content = SecondPlayer.Health + "HP";
            MainWindow.FPHpLabel.Content = FirstPlayer.Health + "HP";
            foreach (var item in MainWindow.Hero.Children)
            {
                if (item is Button b)
                {
                    b.Click += DeffendYourBodyParts;
                }
            }
            foreach (var item in MainWindow.Comp.Children)
            {
                if (item is Button b)
                {
                    b.Click += AttackBodyParts;
                }
            }
            MainWindow.Closing += Close;
        }

        private void Close(object sender, CancelEventArgs e)
        {
            DeSubcsribe();
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

            OnOffButtons(MainWindow.Hero.Children, false);
            OnOffButtons(MainWindow.Comp.Children, true);

            FirstPlayer.GetHit(ChoseBodyPaart());
            MainWindow.FPHealt.Value = FirstPlayer.Health;
            MainWindow.FPHpLabel.Content = FirstPlayer.Health + "HP";

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

            MainWindow.SPHealt.Value = SecondPlayer.Health;
            MainWindow.SPHpLabel.Content = SecondPlayer.Health + "HP";

            OnOffButtons(MainWindow.Hero.Children, true);
            OnOffButtons(MainWindow.Comp.Children, false);
        }

        // Random chose body part for bot
        private BodyParts ChoseBodyPaart()
        {
            Random r = new Random();
            int x = r.Next(0, 3);
            return (BodyParts)x;
        }

        private void DoSmth(object sender, PlayerEventArgs e)
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
                MessageBox.Show("YOU LOSE", "=(");
            }
            else
            {
                Media.Open(new Uri(@"..\..\Res\win.mp3", UriKind.Relative));
                Media.Play();
                MessageBox.Show("YOU WIN");
            }
            DeSubcsribe();
            Restart();
        }
        private void Log(object sender, PlayerEventArgs e)
        {
            MainWindow.LogBlock.Text += "Игрок " + e.Name + " " + e.Message + " - " + e.HealthPoint + "HP" + "\n";
        }
        private void Restart()
        {
            FirstPlayer = new Player(FirstPlayer.Name);
            SecondPlayer = new Player(SecondPlayer.Name);
            Subscribe();

            MainWindow.SPHpLabel.Content = SecondPlayer.Health + "HP";
            MainWindow.FPHpLabel.Content = FirstPlayer.Health + "HP";

            MainWindow.FPHealt.Value = 100;
            MainWindow.SPHealt.Value = 100;

            OnOffButtons(MainWindow.Comp.Children, false);
            OnOffButtons(MainWindow.Hero.Children, true);
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
        private void DeSubcsribe()
        {
            FirstPlayer.Wound -= DoSmth;
            FirstPlayer.Wound -= Log;
            FirstPlayer.Block -= DoSmth1;
            FirstPlayer.Block -= Log;
            FirstPlayer.Death -= Died;
            FirstPlayer.Death -= Log;
            SecondPlayer.Wound -= Log;
            SecondPlayer.Wound -= DoSmth;
            SecondPlayer.Block -= DoSmth1;
            SecondPlayer.Block -= Log;
            SecondPlayer.Death -= Died;
            SecondPlayer.Death -= Log;
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