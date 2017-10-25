using System;
using System.Windows;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FightClub.Presentor;


namespace FightClub.Presentor
{
    class AuthorizationPresentor //: INotifyPropertyChanged
    {
        Authorization Authorization;
        public AuthorizationPresentor(Authorization authorization)
        {
            Authorization = authorization;
            Authorization.StartGameBtn.Click += method;
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        public void method(object sender,RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Authorization.InputName.Text))
            {
                Random r = new Random();
                int i = r.Next(0, 13);
                using (StreamReader fs = new StreamReader(@"..\..\Res\nick.txt"))
                {
                    int k = 0;
                    while (k <= i)
                    {
                        string temp = fs.ReadLine();
                        if (temp == null) break;
                        if (k == i)
                        {
                            MainWindow main = new MainWindow();
                            MainWindowPresenter mainWindowPresenter = new MainWindowPresenter(main,Authorization.InputName.Text, temp);
                            main.Show();
                            Authorization.Close();
                        }
                        k++;
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter nickname!");
            }
        }
    }
}