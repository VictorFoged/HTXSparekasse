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
    /// Interaction logic for KontoView.xaml
    /// </summary>
    public partial class KontoView : Window
    {
        public KontoView(konto currentKonto)
        {
            InitializeComponent();
            transfers.ItemsSource = currentKonto.oversigt;
            Console.ReadLine();
        }
    }
}
