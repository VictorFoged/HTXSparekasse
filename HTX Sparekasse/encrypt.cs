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

        //public static string convertedText;
        public static string convertedTextBackwards;
        //public static string decryptedText;
        public static List<int> cipherChars = new List<int>();
        //public static List<BigInteger> cipherBlocks = new List<BigInteger>();
        public static List<string> convertedTextBlocks = new List<string>();

        //Array of chars allowed in password, ??? is filler to make the min index >10. Also means you cant use question mark in passwords.
        public static char[] chars = "?????????? abcdefghijklmnopqrstuvwxyzæøåABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ1234567890".ToArray();

       

        public static void createKeys()
        {
            
            //Set prime numbers p and q
            p = 23;
            q = 31;

            //Calculate n
            n = p * q;

            
            //Calculate φ(n) = (p-1)(q-1). - φ(n) = phi
            phi = (p - 1) * (q - 1);

            //Chose e, where 0 < e < φ(n) og (e, φ(n)) = 1
            e = 7;

            //Calculate d. - e * d ≡ 1 (mod φ(n))
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

       
        public static List<BigInteger> getCB(string clearText) //Get CipherBlock
        {
            convertedTextBlocks.Clear(); //Make sure static variables are empty
            List<BigInteger> cipherBlocks = new List<BigInteger>();
            
            
            string conString = convertToInt(clearText); //Convert String to int

            for (int i = 0; i < conString.Length / 2; i++) //Split converted string into blocks
            {
                convertedTextBlocks.Add(conString.Substring(i * 2, 2));
            }
            foreach (var item in convertedTextBlocks)
            {
                BigInteger cipherBlock = BigInteger.Pow(BigInteger.Parse(item), e) % n; //Encrypt Block with RSA
                
                cipherBlocks.Add(cipherBlock); //Add Blocks to list
            }

            return cipherBlocks;
        }





        public static string decrypt(List<BigInteger> cipherBlocks)
        {
            convertedTextBackwards = ""; //Empty static variables
            foreach (var item in cipherBlocks)
            {
                convertedTextBackwards += BigInteger.Pow(item, d) % n; //Decrypt Blocks
            }
            return convertToChar(convertedTextBackwards); //Convert back to string
            
        }

        static string convertToInt(string clearText)
        {
            string conString = "";
            char[] text = clearText.ToArray();
            for (int i = 0; i < text.Length; i++)
            {
                int index = Array.IndexOf(chars, text[i]) + 1; //Using Char Array of most common chars
                if (index < 10)
                {
                    conString += "0" + index; //Make sure the block is 3 char long
                }
                else
                {
                    conString += index;
                }
            }
            return conString;
        }

        static string convertToChar(string cipherText)
        {
            string decryptedText = "";
            cipherChars.Clear();
            for (int i = 0; i < cipherText.Length / 2; i++) //Split back into blocks
            {
                int block = Int32.Parse(cipherText.Substring(i * 2, 2)); 
                cipherChars.Add(block);
            }

            foreach (var item in cipherChars)
            {
                decryptedText += chars[item - 1]; //Find equivilant char of each int
            }

            return decryptedText;

        }



    }
}
