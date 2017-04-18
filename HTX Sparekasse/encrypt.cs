using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace HTX_Sparekasse
{
    class Encrypt
    {

        static int p;
        static int q;
        static int n;
        static int e;
        static int phi;
        static int d;

        public static string convertedText;
        public static string convertedTextBackwards;
        public static string decryptedText;
        public static List<int> cipherChars = new List<int>();
        public static List<BigInteger> cipherBlocks = new List<BigInteger>();
        public static List<string> convertedTextBlocks = new List<string>();

        public static char[] chars = "?????????? abcdefghijklmnopqrstuvwxyzæøåABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ1234567890".ToArray();

       

        public static void createKeys()
        {
            //Først sættes de to primtal p og q.
            p = 23;
            q = 31;

            //Dernæst bestemmes n
            n = p * q;

            //Herefter beregnes φ(n) = (p-1)(q-1). - φ(n) = phi
            phi = (p - 1) * (q - 1);

            //Så vælges variablen e, hvor 0 < e < φ(n) og (e, φ(n)) = 1
            e = 7;

            //Beregn d. - e * d ≡ 1 (mod φ(n))
            int RES = 0;

            for (d = 1; ; d++)
            {
                RES = (d * e) % phi;
                if (RES == 1)
                {
                    break;
                }
            }
        }

        public static string encrypt(string clearText)
        {

            //Konverter teksten til tal
            return convertToInt(clearText);

            
            

        }

        public static string decrypt(string cipher)
        {
            for (int i = 0; i < cipher.Length / 2; i++)
            {
                convertedTextBlocks.Add(convertedText.Substring(i * 2, 2));
            }
            foreach (var item in convertedTextBlocks)
            {
                BigInteger cipherBlock = BigInteger.Pow(BigInteger.Parse(item), e) % n;
                cipher += cipherBlock;
                cipherBlocks.Add(cipherBlock);
            }

            foreach (var item in cipherBlocks)
            {
                convertedTextBackwards += BigInteger.Pow(item, d) % n;
            }
            return convertToChar(convertedTextBackwards);
            
        }

        static string convertToInt(string clearText)
        {
            char[] text = clearText.ToArray();
            for (int i = 0; i < text.Length; i++)
            {
                int index = Array.IndexOf(chars, text[i]) + 1;
                if (index < 10)
                {
                    convertedText += "0" + index;
                }
                else
                {
                    convertedText += index;
                }
            }
            return convertedText;
        }

        static string convertToChar(string cipherText)
        {
            for (int i = 0; i < cipherText.Length / 2; i++)
            {
                int block = Int32.Parse(cipherText.Substring(i * 2, 2));
                cipherChars.Add(block);
            }

            foreach (var item in cipherChars)
            {
                decryptedText += chars[item - 1];
            }
            return decryptedText;

        }



    }
}
