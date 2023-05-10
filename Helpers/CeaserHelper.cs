using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZI.Helpers
{
    internal class CeaserHelper
    {
        private static char Cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }
            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);


        }

        public static string Encipher(string input, int key)
        {
            string output = string.Empty;
            foreach (char ch in input)
                output += Cipher(ch, key);
            return output;
        }

        public static string Decipher(string input, int key)
        {
            return Encipher(input, 26 - key);
        }

        public static int KeyGen(int input)
        {
            return input % 26;
        }
    }
}
