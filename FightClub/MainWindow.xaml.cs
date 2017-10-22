﻿using System;
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
        public Player FirstPlayer = new Player("otets");
        public Player SecondPlayer = new Player("Bot228");

        public MainWindow()
        {
            FirstPlayer.Wound += DoSmth;
            FirstPlayer.Block += DoSmth1;
            SecondPlayer.Wound += DoSmth;
            SecondPlayer.Block += DoSmth1;
            FirstPlayer.Death += Died;
            SecondPlayer.Death += Died;
            InitializeComponent();
            foreach (var item in Comp.Children)
            {
                if (item is Button b1)
                {
                    b1.IsEnabled = false;
                }
            }
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
            int x = r.Next(0, 2);
            return (BodyParts)x;
        }

        private void DoSmth(object sender,PlayerEventArgs e)
        {
            MessageBox.Show("Ай как больно то");
        }
        private void DoSmth1(object sender, PlayerEventArgs e)
        {
            MessageBox.Show("хрен попадёшь!!!!");
        }
        private void Died(object sender, PlayerEventArgs e)
        {
            if (e.Name != "Bot228")
            {
                MessageBox.Show("БРАТАН ЭТО ФИАСКО");
            }
            else
            {
                MessageBox.Show("Юху ТЫ победИЛ");
            }
        }
    }
}
