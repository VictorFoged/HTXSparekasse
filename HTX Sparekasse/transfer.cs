using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX_Sparekasse
{
    public class transfer
    {
        public string Overførsel { get; set; }
        public decimal Beløb { get; set; }
        public decimal Saldo { get; set; }

    public transfer(string note, decimal val, decimal saldo)
        {
            Overførsel = note;
            Beløb = val;
            Saldo = saldo;
        }
    }
}
