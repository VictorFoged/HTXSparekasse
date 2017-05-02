using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HTX_Sparekasse
{
    class bruger
    {
        public string fornavn;
        public string efternavn;
        public string username;
        //public string password; Decided to Save CipherBlock instead of CipherText password
        public List<konto> kontoListe = new List<konto>();
        public List<BigInteger> cipherBlock = new List<BigInteger>();

        public bruger(string fNavn, string eNavn, string user,  List<BigInteger> cB)
        {
            fornavn = fNavn;
            efternavn = eNavn;
            username = user;
            // password = pass;
            cipherBlock = cB;

        }

       

    }
}
