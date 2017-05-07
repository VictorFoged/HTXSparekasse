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
        public KontoView(konto currentKonto) //Class Contructor
        {
            InitializeComponent();
            cKont = currentKonto; //Set Which Konto to display
            //currentKonto.oversigt.Reverse();
            transfers.ItemsSource = currentKonto.oversigt; //Set Item Source for Transaction Overview (ListView)
            
            //Set user specific variables
            lblKontoNavn.Content = currentKonto.navn; 
            lblSaldo.Content = currentKonto.saldo + " DKK";
            kontOversigt.Title = currentKonto.navn + " - Konto Oversigt";

            switch (currentKonto.kontoType) //Display Kontotype
            {
                case 1:
                    lblKontotype.Content = "Lønkonto - 0% p.a.";
                    break;
                case 2:
                    lblKontotype.Content = "Opsparingskonto - 0.5% p.a.";
                    break;
                case 3:
                    lblKontotype.Content = "Pensionskonto - 1% p.a.";
                    btnWithdraw.Visibility = Visibility.Hidden; //Can't Withdraw money from Pension konto, hide option from user.
                    txtBeløb.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
            if (cKont.active == false) //Make sure Activate/Deactivate Button has the correct state.
            {
                
                btnDeactivate.Content = "Aktiver Konto";


            }





        }

        private void btnDeactivate_Click(object sender, RoutedEventArgs e) //Activate and Deactivate Konto, set state and change button content
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
            Welcome.cWin.checkCombo(); //Update Comboboxes, removes inactive Kontoer
            Bank.writeJson(); //Write to JSON/Database
        }

        private void opaBeløb(object sender, RoutedEventArgs e) //Focus Event for withdraw field
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty; //Empty textbox

            tb.Opacity = 100;
        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e) //Withdraw money
        {
            decimal input;
            if (cKont.active == true)
            {
                if (decimal.TryParse(txtBeløb.Text, out input)) //Convert string to decimal
                {
                    if (input > 0) //Can Only withdraw positive amounts.
                    {
                        cKont.removeCash(input); //Removes Cash from saldo
                        cKont.oversigt.Add(new transfer("Beløb Hævet", -input, cKont.saldo)); //Add transfer with default note
                        transfers.Items.Refresh(); //refresh item source
                        txtBeløb.Text = "Indtast Beløb"; //Fill Withdraw textbox with placeholder text
                        txtBeløb.Opacity = 50;
                        lblSaldo.Content = cKont.saldo; //Update Saldo label

                        //Check Comboboxes and Write to JSON/Database
                        Welcome.cWin.checkKonto();
                        Bank.writeJson();
                    }


                }
            }
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) //Delete Event Click
        {
            //Create Alert/Message 
            MessageBoxResult result = MessageBox.Show("Vil du slette denne konto? Alle data vil gå tabt", "Bekræft", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Bank.currentUser.kontoListe.Remove(cKont); //Removes Konto from list
                Welcome.cWin.checkKonto(); //Closes Window and updates fields.
                this.Close();
            }
        }
    }
}
