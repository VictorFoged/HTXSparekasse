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
    /// Interaction logic for KontoView.xaml
    /// </summary>
    public partial class KontoView : Window
    {
        konto cKont;
        public KontoView(konto currentKonto)
        {
            InitializeComponent();
            cKont = currentKonto;
            //currentKonto.oversigt.Reverse();
            transfers.ItemsSource = currentKonto.oversigt;
            
            lblKontoNavn.Content = currentKonto.navn;
            lblSaldo.Content = currentKonto.saldo + " DKK";
            kontOversigt.Title = currentKonto.navn + " - Konto Oversigt";

            switch (currentKonto.kontoType)
            {
                case 1:
                    lblKontotype.Content = "Lønkonto - 0% ÅOP";
                    break;
                case 2:
                    lblKontotype.Content = "Opsparingskonto - 0.5% ÅOP";
                    break;
                case 3:
                    lblKontotype.Content = "Pensionskonto - 1% ÅOP";
                    btnWithdraw.Visibility = Visibility.Hidden;
                    txtBeløb.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
            if (cKont.active == true)
            {
                cKont.active = false;
                btnDeactivate.Content = "Aktiver Konto";


            }





        }

        private void btnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            if (cKont.active == true)
            {
                cKont.active = false;
                btnDeactivate.Content = "Aktiver Konto";
                

            }
            else
            {
                cKont.active = true;
                btnDeactivate.Content = "Deaktiver Konto";
                

            }
            Welcome.cWin.checkCombo();
            Bank.writeJson();
        }

        private void opaBeløb(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;

            tb.Opacity = 100;
        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            decimal input;

            if (decimal.TryParse(txtBeløb.Text, out input))
            {
                if (input > 0)
                {
                    cKont.removeCash(input);
                    cKont.oversigt.Add(new transfer("Beløb Hævet", -input, cKont.saldo));
                    transfers.Items.Refresh();
                    txtBeløb.Text = "Indtast Beløb";
                    txtBeløb.Opacity = 50;
                    lblSaldo.Content = cKont.saldo;
                    Welcome.cWin.checkKonto();
                    Bank.writeJson();
                }
                

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Vil du slette denne konto? Alle data vil gå tabt", "Bekræft", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Bank.currentUser.kontoListe.Remove(cKont);
                Welcome.cWin.checkKonto();
                this.Close();
            }
        }
    }
}
