using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX_Sparekasse
{
    class bruger
    {
        public string fornavn;
        public string efternavn;
        public string username;
        public string password;
        public List<konto> kontoListe = new List<konto>();

        public bruger(string fNavn, string eNavn, string user, string pass)
        {
            fornavn = fNavn;
            efternavn = eNavn;
            username = user;
            password = pass;

        }

       

    }
}
