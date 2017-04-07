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
            checkCombo();

        }

        private void checkCombo()
        {
            List<ComboBoxItem> dropDown = new List<ComboBoxItem>();
            dropDown.Add(dKonto1);
            dropDown.Add(dKonto2);
            dropDown.Add(dKonto3);
            dropDown.Add(dKonto4);
            int index = 0;
            foreach (konto konto in Bank.currentUser.kontoListe)
            {
                dropDown[index].Content = konto.navn;
                dropDown[index].Visibility = Visibility.Visible;
                index = index + 1;
            }
            dKonto1c.Content = dKonto1.Content;
            dKonto1c.Visibility = dKonto1.Visibility;
            dKonto2c.Content = dKonto2.Content;
            dKonto2c.Visibility = dKonto2.Visibility;
            dKonto3c.Content = dKonto3.Content;
            dKonto3c.Visibility = dKonto3.Visibility;
            dKonto4c.Content = dKonto4.Content;
            dKonto4c.Visibility = dKonto4.Visibility;

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
                        lblSaldo1.Content = Bank.currentUser.kontoListe[0].saldo;
                        break;
                    case 1:
                        lblKontonavn2.Content = konto.navn;
                        lblSaldo2.Content = Bank.currentUser.kontoListe[1].saldo;
                        break;
                    case 2:
                        lblKontonavn3.Content = konto.navn;
                        lblSaldo3.Content = Bank.currentUser.kontoListe[2].saldo;
                        break;

                    case 3:
                        lblKontonavn4.Content = konto.navn;
                        lblSaldo4.Content = Bank.currentUser.kontoListe[3].saldo;
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
                checkCombo();
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

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            
            decimal val;
            if (decimal.TryParse(txtValue.Text, out val))
            {

                if (cbFrom.SelectedIndex != -1)
                {
                    if (cbTo.SelectedIndex != -1)
                    {
                        if (cbFrom.SelectedIndex == 4)
                        {
                            Bank.currentUser.kontoListe[cbTo.SelectedIndex].addCash(val);
                            successPayment();
                        }
                        else
                        {
                            konto.transferCash(Bank.currentUser.kontoListe[cbFrom.SelectedIndex], Bank.currentUser.kontoListe[cbTo.SelectedIndex], val);
                            successPayment();
                        }
                    }
                    else
                    {
                        lblTransError.Content = "Vælg en konto at overføre til";
                        lblTransError.Visibility = Visibility.Visible;
                    }
                    
                }
                else
                {
                    lblTransError.Content = "Vælg en konto at overføre fra";
                    lblTransError.Visibility = Visibility.Visible;
                }
            }
                
            else
            {
                lblTransError.Content = "Indtast et beløb";
                lblTransError.Visibility = Visibility.Visible;
            }

            checkKonto();
            Bank.writeJson();
            
        }

        private void successPayment()
        {
            txtValue.Text = "";
            cbFrom.SelectedIndex = -1;
            cbTo.SelectedIndex = -1 ;
            lblTransError.Foreground = GreenLabel.Foreground;
            lblTransError.Content = "Overførsel Gennemført";
            lblTransError.Visibility = Visibility.Visible;
        }
    }
}
