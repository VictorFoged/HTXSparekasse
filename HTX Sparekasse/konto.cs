using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX_Sparekasse
{
    class konto
    {
        string navn;
        double saldo = 0;
        bool active = true;
        Dictionary<string, double> historik = new Dictionary<string, double>();

        public konto(string name)
        {
            navn = name;
        }

        public void addCash(double val)
        {
            saldo = saldo + val;
        }
    }
}
