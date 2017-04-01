using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX_Sparekasse
{
    class bruger
    {
        string fornavn;
        string efternavn;
        string username;
        string password; //totally safe
        List<konto> kontoListe = new List<konto>();

        public bruger(string fNavn, string eNavn, string user, string pass)
        {
            fornavn = fNavn;
            efternavn = eNavn;
            username = user;
            password = pass;

        }

    }
}
