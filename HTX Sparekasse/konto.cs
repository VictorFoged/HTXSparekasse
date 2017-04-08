using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX_Sparekasse
{
    class konto
    {
        public string navn;
        public decimal saldo = 0;
        public bool active = true;

        Dictionary<string, double> historik = new Dictionary<string, double>();

        public konto(string name)
        {
            navn = name;
        }

        public void addCash(decimal val)
        {
            saldo = Math.Round(saldo + val, 2, MidpointRounding.ToEven);
        }

        public static void transferCash(konto from, konto to, decimal val)
        {
            from.saldo = Math.Round(from.saldo - val,2, MidpointRounding.ToEven);
            to.saldo = Math.Round(to.saldo + val, 2, MidpointRounding.ToEven);
        }


    }
}
