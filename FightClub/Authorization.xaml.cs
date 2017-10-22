using System;
using System.IO;
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
using System.Windows.Shapes;

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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (InputName.Text!="")
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
                        if (k==i)
                        {
                            MainWindow main = new MainWindow(InputName.Text,temp);
                            main.Show();
                            this.Close();
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
