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
        public static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user.txt");


        public static void writeJson()
        {
            string json = JsonConvert.SerializeObject(userlist.ToArray(), Formatting.Indented);

            //write string to file
            
            System.IO.File.WriteAllText(path, json);
            
        }

        public static void loadJson()
        {

            
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                userlist = JsonConvert.DeserializeObject<List<bruger>>(json);
            }
        }
    }
}
