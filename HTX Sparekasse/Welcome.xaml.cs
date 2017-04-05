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
using System.Windows.Shapes;

namespace HTX_Sparekasse
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        List<Grid> kontoer = new List<Grid>();
        

        public Welcome()
        {
            InitializeComponent();
            lblWelcome.Content = "Hej " + Bank.currentUser.fornavn + " " + Bank.currentUser.efternavn;
            kontoer.Add(konto1);
            kontoer.Add(konto2);
            kontoer.Add(konto3);
            kontoer.Add(konto4);
            checkKonto();



        }

        private void opretKonto_focus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            
            tb.Opacity = 100;
        }

        private void checkKonto()
        {
            int index = 0;
            foreach (konto konto in Bank.currentUser.kontoListe)
            {
                kontoer[index].Visibility = Visibility.Visible;
                switch (index)
                {
                    case 0:
                        lblKontonavn1.Content = konto.navn;
                        break;
                    case 1:
                        lblKontonavn2.Content = konto.navn;
                        break;
                    case 2:
                        lblKontonavn3.Content = konto.navn;
                        break;

                    case 3:
                        lblKontonavn4.Content = konto.navn;
                        break;
                    default:
                        break;
                }
                index = index + 1;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Bank.currentUser.kontoListe.Count < 4)
            {
                Bank.currentUser.kontoListe.Add(new konto(txtOpretKonto.Text));
                checkKonto();
                Bank.writeJson();
            }
            else
            {
                lblCreateError.Content = "Maks 4 Kontoer";
            }
            
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Bank.currentUser = null;
            this.Close();
            main.Show();

        }

        
        private void click1Grid(object sender, MouseButtonEventArgs e)
        {
            lblCreateError.Content = Bank.currentUser.kontoListe[0].navn;
        }

        private void click2Grid(object sender, MouseButtonEventArgs e)
        {
            lblCreateError.Content = Bank.currentUser.kontoListe[1].navn;
        }

        private void click3Grid(object sender, MouseButtonEventArgs e)
        {
            lblCreateError.Content = Bank.currentUser.kontoListe[2].navn;
        }

        private void click4Grid(object sender, MouseButtonEventArgs e)
        {
            lblCreateError.Content = Bank.currentUser.kontoListe[3].navn;
        }
    }
}
