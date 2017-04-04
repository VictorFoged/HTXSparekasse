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



        }

        private void opretKonto_focus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= opretKonto_focus;
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
                    case 1:
                        lblKontonavn1.Content = konto.navn;
                        break;
                    case 2:
                        lblKontonavn2.Content = konto.navn;
                        break;
                    case 3:
                        lblKontonavn3.Content = konto.navn;
                        break;

                    case 4:
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
            }
            else
            {
                lblCreateError.Content = "Maks 4 Kontoer";
            }
            
        }
    }
}
