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
using System.Windows.Shapes;

namespace HTX_Sparekasse
{
    /// <summary>
    /// Interaction logic for CreateAcc.xaml
    /// </summary>
    public partial class CreateAcc : Window
    {
        public CreateAcc()
        {
            InitializeComponent();
        }

        private void btnOpret_Click(object sender, RoutedEventArgs e)
        {
            string fNavn = txtNavn.Text;
            string eNavn = txtNavn1.Text;
            string user = txtUser.Text;
            string pass = txtPass.Text;
            
            if(checkUser(user) == true)
            {
                Bank.userlist.Add(new bruger(fNavn, eNavn, user, pass));
                Bank.writeJson();
                lblUserError.Visibility = Visibility.Hidden;
                MainWindow main = new MainWindow();

                this.Close();
                main.Show();
            }
            else
            {
                lblUserError.Visibility = Visibility.Visible;
            }

                 


        }

        private bool checkUser(string brugernavn)
        {
            foreach (bruger u in Bank.userlist)
            {
                if (u.username == brugernavn)
                {
                    return false;
                }
            }
            return true;
        }

        private void user_gotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= user_gotFocus;
            tb.Opacity = 100;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();

            this.Close();
            main.Show();
        }
    }
}
