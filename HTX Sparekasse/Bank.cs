using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX_Sparekasse
{

    class Bank
    {
        public static List<bruger> userlist = new List<bruger>();
        public static  bruger currentUser;



        public static void writeJson()
        {
            string json = JsonConvert.SerializeObject(userlist.ToArray(), Formatting.Indented);

            //write string to file
            System.IO.File.WriteAllText(@"C:\Users\Victor\Pictures\Camera Roll\user.txt", json);
        }

        public static void loadJson()
        {
            using (StreamReader r = new StreamReader(@"C:\Users\Victor\Pictures\Camera Roll\user.txt"))
            {
                string json = r.ReadToEnd();
                userlist = JsonConvert.DeserializeObject<List<bruger>>(json);
            }
        }
    }
}
