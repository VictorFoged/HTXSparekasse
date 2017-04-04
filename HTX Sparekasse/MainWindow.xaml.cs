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
            Bank.loadJson();
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {

            foreach (bruger user in Bank.userlist)
            {
                if (user.username == txtUsername.Text)
                {
                    if (user.password == txtPassword.Password)
                    {
                        Bank.currentUser = user;
                        Welcome main = new Welcome();
                        
                        this.Close();
                        main.Show();
                    }
                    else
                    {
                        txtErr.Content = "Forkert Password";
                    }
                }
               
            }
                       
            
            

        }

        private void brnAcc_Click(object sender, RoutedEventArgs e)
        {
            CreateAcc main = new CreateAcc();

            this.Close();
            main.Show();
        }


    }
}
