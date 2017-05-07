using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        private void btnOpret_Click(object sender, RoutedEventArgs e) //Opret Konto Event
        {
            string fNavn = txtNavn.Text;
            string eNavn = txtNavn1.Text;
            string user = txtUser.Text;
            List<BigInteger> cipherBlock = Encrypt.getCB(txtPass.Password + txtNavn.Text);//Salter og Kryptere Password med RSA Algoritme
            if (checkUser(user) == true) //Check if username is available
            {
                Bank.userlist.Add(new bruger(fNavn, eNavn, user, cipherBlock)); //Create new Bruger and add to userlist
                Bank.writeJson(); //Write to JSON/Database
                lblUserError.Visibility = Visibility.Hidden; //Hide errors that may have appeared
                MainWindow main = new MainWindow(); //Go back to new MainWindow (Log in screen)

                this.Close(); //Close account creation
                main.Show();
            }
            else
            {
                lblUserError.Visibility = Visibility.Visible; //Show error message (username taken)
            }

                 


        }

        private bool checkUser(string brugernavn) //Checks if username is taken, returns true if not
        {
            if (Bank.userlist != null)
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
            else
            {
                return true;
            }
            
        }

        private void user_gotFocus(object sender, RoutedEventArgs e) //Event on textbox Focus
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty; //Empty Textbox ("Fornavn" eller "Efternavn")
            tb.GotFocus -= user_gotFocus; //Removes event to avoid clearing user typed content
            tb.Opacity = 100; //Set normal opacity.

        }

        private void button_Click(object sender, RoutedEventArgs e) //Tilbage Event
        {
            MainWindow main = new MainWindow();

            this.Close();
            main.Show();
        }
    }
}
