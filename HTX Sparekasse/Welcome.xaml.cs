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
            kontoer.Add(konto1); //Add Grids to list
            kontoer.Add(konto2);
            kontoer.Add(konto3);
            kontoer.Add(konto4);
            checkKonto(); //Check Which Accounts has been created
            checkCombo(); //Check Comboboxes

        }

        public void checkCombo()
        {
            List<ComboBoxItem> dropDown = new List<ComboBoxItem>(); //Create lists of comboboxes to iterate over
            dropDown.Add(dKonto1);
            dropDown.Add(dKonto2);
            dropDown.Add(dKonto3);
            dropDown.Add(dKonto4);
            int index = 0; //dropDown list index
            foreach (konto konto in Bank.currentUser.kontoListe) //Go through each konto for each user.
            {
                dropDown[index].Content = konto.navn; //Set Name of index to the name of the account
                if (konto.active == true)
                {
                    dropDown[index].Visibility = Visibility.Visible; //Make it visible
                }
                else
                {
                    dropDown[index].Visibility = Visibility.Collapsed; //Make it invisible if the account is inactive
                }
                
                index = index + 1;
            }
            //Set all Comboboxes to have the same properties
            dKonto1c.Content = dKonto1.Content;
            dKonto1c.Visibility = dKonto1.Visibility;
            dKonto2c.Content = dKonto2.Content;
            dKonto2c.Visibility = dKonto2.Visibility;
            dKonto3c.Content = dKonto3.Content;
            dKonto3c.Visibility = dKonto3.Visibility;
            dKonto4c.Content = dKonto4.Content;
            dKonto4c.Visibility = dKonto4.Visibility;


            dKonto1cc.Content = dKonto1.Content;
            dKonto1cc.Visibility = dKonto1.Visibility;
            dKonto2cc.Content = dKonto2.Content;
            dKonto2cc.Visibility = dKonto2.Visibility;
            dKonto3cc.Content = dKonto3.Content;
            dKonto3cc.Visibility = dKonto3.Visibility;
            dKonto4cc.Content = dKonto4.Content;
            dKonto4cc.Visibility = dKonto4.Visibility;

        }

        private void opretKonto_focus(object sender, RoutedEventArgs e) //Empty textbox on textbox focus
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            
            tb.Opacity = 100;
        }

        public void checkKonto()
        {
            int index = 0;
            konto1.Visibility = Visibility.Hidden; //Hide all accounts initially (tab1)
            konto2.Visibility = Visibility.Hidden;
            konto3.Visibility = Visibility.Hidden;
            konto4.Visibility = Visibility.Hidden;
            foreach (konto konto in Bank.currentUser.kontoListe) //Foreach account in list, make it visible
            {
                
                kontoer[index].Visibility = Visibility.Visible;
                switch (index) //Set Account name and Saldo to the correct value
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

        private void button_Click(object sender, RoutedEventArgs e) //Opret Konto Event
        {
            if (Bank.currentUser.kontoListe.Count < 4) //Check if there is space for another account
            {
                if (cbKontoValg.SelectedIndex > 0) //Check if Kontotype is selected
                {
                    Bank.currentUser.kontoListe.Add(new konto(txtOpretKonto.Text, cbKontoValg.SelectedIndex)); //Add new account to list

                    checkKonto(); //Update interface
                    checkCombo();
                    Bank.writeJson(); //Write to Database/JSON
                    lblCreateError.Content = string.Empty;
                }
                else
                {
                    lblCreateError.Content = "Vælg Kontotype"; //Show error
                }
                
            }
            else
            {
                lblCreateError.Content = "Maks 4 Kontoer"; //Show error
            }
            
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e) //Log Out Event
        {
            MainWindow main = new MainWindow();
            Bank.currentUser = null; //Set current User to null
            this.Close(); //Close Window
            main.Show();  //Show login screen

        }

        public static Welcome cWin;
        private void click1Grid(object sender, MouseButtonEventArgs e)
        {
            KontoView konto1 = new KontoView(Bank.currentUser.kontoListe[0]); //Open first account in list
            cWin = this;        
            konto1.Show();
            
        }

        private void click2Grid(object sender, MouseButtonEventArgs e)
        {
            KontoView konto2 = new KontoView(Bank.currentUser.kontoListe[1]); //Open second account in list
            cWin = this;
            konto2.Show();
        }

        private void click3Grid(object sender, MouseButtonEventArgs e)
        {
            KontoView konto3 = new KontoView(Bank.currentUser.kontoListe[2]); //Open third account in list
            cWin = this;
            konto3.Show();
        }

        private void click4Grid(object sender, MouseButtonEventArgs e)
        {
            KontoView konto4 = new KontoView(Bank.currentUser.kontoListe[3]); //Open fourth account in list
            cWin = this;
            konto4.Show();
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e) 
        {
            lblTransError.Visibility = Visibility.Hidden; //Hide error label
            decimal val;
            if (decimal.TryParse(txtValue.Text, out val)) //Try to parse textbox content as decimal
            {


                if (cbFrom.SelectedIndex != -1) //Check if account selected
                {
                    if (cbTo.SelectedIndex != -1) //Check if account is selected
                    {
                        if (cbFrom.SelectedIndex == 4) //Check if account is "National Banken"
                        {
                            //Add Cash to account
                            Bank.currentUser.kontoListe[cbTo.SelectedIndex].addCash(val);
                            Bank.currentUser.kontoListe[cbTo.SelectedIndex].oversigt.Add(new transfer(txtNote.Text, val, Bank.currentUser.kontoListe[cbTo.SelectedIndex].saldo)); //Create transfer log
                            
                            successPayment();
                        }
                        else //Create Transaction
                        {
                            konto.transferCash(Bank.currentUser.kontoListe[cbFrom.SelectedIndex], Bank.currentUser.kontoListe[cbTo.SelectedIndex], val);
                            Bank.currentUser.kontoListe[cbTo.SelectedIndex].oversigt.Add(new transfer(txtNote.Text, val, Bank.currentUser.kontoListe[cbTo.SelectedIndex].saldo)); //Create transfer log for both accounts
                            Bank.currentUser.kontoListe[cbFrom.SelectedIndex].oversigt.Add(new transfer(txtNote.Text, -val, Bank.currentUser.kontoListe[cbFrom.SelectedIndex].saldo));
                            successPayment();
                        }
                    }
                    else
                    { //Error 
                        lblTransError.Content = "Vælg en konto at overføre til";
                        lblTransError.Visibility = Visibility.Visible;
                    }
                    
                }
                else
                { //Error
                    lblTransError.Content = "Vælg en konto at overføre fra";
                    lblTransError.Visibility = Visibility.Visible;
                }
            }
                
            else
            { //Error
                lblTransError.Content = "Indtast et beløb";
                lblTransError.Visibility = Visibility.Visible;
            }

            checkKonto();
            Bank.writeJson(); //Update Database
            
        }

        private void successPayment() //Update Interface when payment is successful 
        {
            txtValue.Text = "";
            cbFrom.SelectedIndex = -1;
            cbTo.SelectedIndex = -1 ;
            txtNote.Text = string.Empty;
            lblTransError.Foreground = GreenLabel.Foreground;
            lblTransError.Content = "Overførsel Gennemført";
            lblTransError.Visibility = Visibility.Visible;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //0 = DKK, 1 = USD, 2 = EUR, 3 = GBP
            Dictionary<int, List<double>> valuta = new Dictionary<int, List<double>>(); //Create Dictionary with int as key and value as a list of currency conversion rates
            valuta[0] = new List<double> { 1, 0.1419, 0.1343, 0.1151 }; //From DKK to DKK, USD, EUR, GBP.
            valuta[1] = new List<double> { 7.0216, 1, 0.9436, 0.8081 }; //From USD to DKK, USD, EUR, GBP.
            valuta[2] = new List<double> { 7.4413, 1.0598, 1, 0.8564 }; //etc.
            valuta[3] = new List<double> { 8.6891, 1.2375, 1.1677, 1 };

            double input; //declare out value

            if (Double.TryParse(txtValFrom.Text, out input)) //Try to parse string input to double
            {
                if (cbValFrom.SelectedIndex != -1 && cbValTo.SelectedIndex != -1) //Check if both boxes have selected
                {
                    double value = Math.Round(input * valuta[cbValFrom.SelectedIndex][cbValTo.SelectedIndex], 2);
                    txtValTo.Text = value.ToString();
                }
               
            }

            


        }

        private void refill(object sender, RoutedEventArgs e) //Refill Text Box
        {

            TextBox tb = (TextBox)sender;

            if (tb.Text == string.Empty)
            {
                tb.Text = "Indtast Beløb";
            }
        }
        
        private void changeColor(object sender, MouseEventArgs e) //Change Color on Hover
        {
            Grid gr = (Grid)sender;
            
            switch (gr.Name)
            {
                case "konto1": 
                    brdKonto1.Background = hovercolor.Background;
                    
                    break;
                case "konto2":
                    brdKonto2.Background = hovercolor.Background;
                    
                    break;
                case "konto3":
                    brdKonto3.Background = hovercolor.Background;
                    
                    break;
                case "konto4":
                    brdKonto4.Background = hovercolor.Background;
                    
                    break;


                default:
                    break;
            }
        }

        private void defaultColor(object sender, MouseEventArgs e) //Change to Default Color on noHover
        {
            Grid gr = (Grid)sender;

            switch (gr.Name)
            {
                case "konto1":
                    brdKonto1.Background = backgroundGrid.Background;

                    break;
                case "konto2":
                    brdKonto2.Background = backgroundGrid.Background;

                    break;
                case "konto3":
                    brdKonto3.Background = backgroundGrid.Background;

                    break;
                case "konto4":
                    brdKonto4.Background = backgroundGrid.Background;

                    break;


                default:
                    break;
            }
        }

        private void btnBeregnRenter_Click(object sender, RoutedEventArgs e)
        {
            DateTime today = DateTime.Today;
            DateTime inputDate;
            DateTime? date = datePicker.SelectedDate;
            konto valgtKonto = Bank.currentUser.kontoListe[renteKonto.SelectedIndex];
            decimal saldo = valgtKonto.saldo;
            decimal åop = 0;

            switch (valgtKonto.kontoType)
            {
                case 1:
                    åop = 0;
                    break;
                case 2:
                    åop = 0.005m;
                    break;
                case 3:
                    åop = 0.01m;
                    break;
                default:
                    break;
            }
            if (valgtKonto.saldo < 0)
            {
                åop = -0.2m;
            }




            if (date == null)
            {

            }
            else
            {
                DateTime.TryParse(date.Value.ToString(), out inputDate);
                TimeSpan tSpan = inputDate - today;

                int daysDiff = tSpan.Days;
                int yearDiff = daysDiff / 365;
                double p;
                double k;
                Double.TryParse(åop.ToString(), out p);
                Double.TryParse(valgtKonto.saldo.ToString(), out k);

                if (k < 0)
                {
                    double afterSaldo = k * Math.Pow(1 + -p / 365, daysDiff);
                    double rente = afterSaldo - k;
                    txtRente.Text = Math.Round(rente, 2).ToString();
                    txtSaldoRente.Text = Math.Round(-afterSaldo, 2).ToString();
                }
                else
                {
                    double afterSaldo = k * Math.Pow(1 + p / 365, daysDiff);
                    double rente = afterSaldo - k;
                    txtRente.Text = Math.Round(rente, 2).ToString();
                    txtSaldoRente.Text = Math.Round(afterSaldo, 2).ToString();
                }
                
                


                
                
            }


            

            

            //Console.WriteLine(tSpan.Days);
            //Console.WriteLine(inputDate);
            //Console.WriteLine(datePicker.SelectedDate);
            //Console.WriteLine(today);
        }
    }
}
