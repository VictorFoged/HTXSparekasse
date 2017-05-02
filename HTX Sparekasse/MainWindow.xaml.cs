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
using Newtonsoft.Json;

namespace HTX_Sparekasse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        

        public MainWindow()
        {
            InitializeComponent();
            Bank HTX = new Bank(); //Create Bank Class
            Encrypt.createKeys();  //Generate Keys for RSA Encryption
            
            Bank.loadJson();       //Load Userdata from JSON/Database file
            
        }

        private void btnLog_Click(object sender, RoutedEventArgs e) //Login Click Event
        {

            foreach (bruger user in Bank.userlist) //Go Through Every User in loaded JSON file
            {
                if (user.username == txtUsername.Text) //If Username Matches, Check if password match
                {
                    
                    if (Encrypt.decrypt(user.cipherBlock) == txtPassword.Password + user.fornavn) //Decrypt Password and add Salt to input
                    {
                        Bank.currentUser = user; //If password is correct, set current user to logged in user
                        Welcome main = new Welcome(); //and load Welcome Window
                        
                        this.Close(); //Close login Window
                        main.Show();  //Show Welcome Window
                    }
                    else
                    {
                        txtErr.Content = "Forkert Password"; //If Password doesn't match username
                    }
                }
               
            }
            if (txtErr.Content.ToString() != "Forkert Password")
            {
                txtErr.Content = "Brugernavn ikke fundet"; //If loop completed without logging in or finding mathcing password, give this error.
            }
            

                       
            
            

        }

        private void brnAcc_Click(object sender, RoutedEventArgs e) //Opret Konto event
        {
            CreateAcc main = new CreateAcc(); //Create CreateAcc window and class.

            this.Close();
            main.Show();
        }

        private void enterKey(object sender, KeyEventArgs e) //Trigger log in event on enter keypress.
        {
            if (e.Key == Key.Enter)
            {
                btnLog_Click(sender, e);
            }
        }
    }
}
