using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX_Sparekasse
{
    public class konto
    {
        public string navn;
        public decimal saldo = 0;
        public bool active = true;
        public int kontoType;

        public List<transfer> oversigt = new List<transfer>();

        public konto(string name, int type)
        {
            navn = name;
            kontoType = type;
        }

        public void addCash(decimal val) //Used to add cash from National Banken
        {
            saldo = Math.Round(saldo + val, 2, MidpointRounding.ToEven);
        }

        public void removeCash(decimal val) //Withdraw Money
        {
            saldo = Math.Round(saldo - val, 2, MidpointRounding.ToEven);
        }

        public static void transferCash(konto from, konto to, decimal val) //Transfer between accounts
        {
            from.saldo = Math.Round(from.saldo - val,2, MidpointRounding.ToEven);
            to.saldo = Math.Round(to.saldo + val, 2, MidpointRounding.ToEven);
        }


    }
}
