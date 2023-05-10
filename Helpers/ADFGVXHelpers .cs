using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZI.Helpers
{
    public class ADFGVXHelper
    {
        private readonly string key;

        public ADFGVXHelper(string key)
        {
            this.key = key;
        }

        public string DecryptMessage(string cipherText)
        {
            string[] splitCipher = cipherText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var transformedCipher = TransformCipher(splitCipher);
            var returnText = ReverseStepOne(transformedCipher);
            return returnText.Replace("SPACE", " ");
        }

        private string ReverseStepOne(string transformedCipher)
        {
            var sb = new StringBuilder();

            for (int i = 1; i < transformedCipher.Length; i += 2)
            {
                sb.Append(GetPlainText(transformedCipher[i - 1], transformedCipher[i]));
            }

            return sb.ToString();
        }

        private string GetPlainText(char c, char c1)
        {
            var sb = new StringBuilder();
            var sb2 = new StringBuilder();
            sb2.Append(c);
            sb2.Append(c1);
            switch (sb2.ToString())
            {
                case "AA":
                    sb.Append("F");
                    break;
                case "AD":
                    sb.Append("U");
                    break;
                case "AF":
                    sb.Append("W");
                    break;
                case "AG":
                    sb.Append("5");
                    break;
                case "AV":
                    sb.Append("Q");
                    break;
                case "AX":
                    sb.Append("G");
                    break;
                case "DA":
                    sb.Append("M");
                    break;
                case "DD":
                    sb.Append("E");
                    break;
                case "DF":
                    sb.Append("V");
                    break;
                case "DG":
                    sb.Append("R");
                    break;
                case "DV":
                    sb.Append("H");
                    break;
                case "DX":
                    sb.Append("0");
                    break;
                case "FA":
                    sb.Append("1");
                    break;
                case "FD":
                    sb.Append("N");
                    break;
                case "FF":
                    sb.Append("D");
                    break;
                case "FG":
                    sb.Append("I");
                    break;
                case "FV":
                    sb.Append("6");
                    break;
                case "FX":
                    sb.Append("7");
                    break;
                case "GA":
                    sb.Append("2");
                    break;
                case "GD":
                    sb.Append("S");
                    break;
                case "GF":
                    sb.Append("J");
                    break;
                case "GG":
                    sb.Append("C");
                    break;
                case "GV":
                    sb.Append("X");
                    break;
                case "GX":
                    sb.Append("8");
                    break;
                case "VA":
                    sb.Append("T");
                    break;
                case "VD":
                    sb.Append("K");
                    break;
                case "VF":
                    sb.Append("#");
                    break;
                case "VG":
                    sb.Append("O");
                    break;
                case "VV":
                    sb.Append("B");
                    break;
                case "VX":
                    sb.Append("Y");
                    break;
                case "XA":
                    sb.Append("L");
                    break;
                case "XD":
                    sb.Append("Z");
                    break;
                case "XF":
                    sb.Append("4");
                    break;
                case "XG":
                    sb.Append("9");
                    break;
                case "XV":
                    sb.Append("P");
                    break;
                case "XX":
                    sb.Append("A");
                    break;
            }


            return sb.ToString();
        }

        public string TransformCipher(string[] splitCipher)
        {
            var alphabeticalString = key.ToList();
            alphabeticalString.Sort();

            var columnTransposition = key.ToDictionary(c => c, c => new List<char>());
            var counter = 0;
            foreach (var c in alphabeticalString)
            {
                columnTransposition[c].AddRange(splitCipher[counter].ToList());
                counter++;
            }
            var sb = new StringBuilder();

            int longestLength = GetLongestLength(splitCipher);

            for (int i = 0; i < longestLength; i++)
            {
                foreach (var c in key)
                {
                    if (columnTransposition[c].Count != 0)
                    {
                        sb.Append(columnTransposition[c].FirstOrDefault());
                        columnTransposition[c].RemoveAt(0);
                    }

                }
            }
            return sb.ToString();
        }

        private int GetLongestLength(string[] splitCipher)
        {
            int largestLength = int.MinValue;
            foreach (var each in splitCipher)
            {
                if (each.Length > largestLength)
                    largestLength = each.Length;
            }
            return largestLength;
        }

        public string EncryptMessage(string message)
        {
            message = message.Replace(" ", "SPACE");
            var stepOne = StepOne(message.ToLower());

            var returnText = OutputText(stepOne);
            return returnText;
        }
        private string CleanMessage(string message)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in message)
            {
                switch (c)
                {
                    case '\r':
                    case '\n':
                    case '\t':
                    case ' ':
                        continue;
                    default:
                        sb.Append(c);
                        break;

                }
            }
            return sb.ToString().ToLower();
        }

        private string OutputText(string message)
        {
            var counter = 0;
            var columnTransposition = key.ToDictionary(c => c, c => new List<char>());
            foreach (var each in message)
            {
                columnTransposition[key[counter]].Add(each);
                counter++;
                if (counter >= key.Length)
                    counter = 0;
            }

            var sb = new StringBuilder();
            var alphabeticalKey = key.ToList();
            alphabeticalKey.Sort();
            foreach (var c in alphabeticalKey)
            {
                foreach (var each in columnTransposition[c])
                {
                    sb.Append(each);
                }
                sb.Append(' ');
            }

            return sb.ToString();
        }

        private string StepOne(string cleanedMessage)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in cleanedMessage)
            {
                switch (c)
                {
                    case 'a':
                        sb.Append("XX");
                        break;
                    case 'b':
                        sb.Append("VV");
                        break;
                    case 'c':
                        sb.Append("GG");
                        break;
                    case 'd':
                        sb.Append("FF");
                        break;
                    case 'e':
                        sb.Append("DD");
                        break;
                    case 'f':
                        sb.Append("AA");
                        break;
                    case 'g':
                        sb.Append("AX");
                        break;
                    case 'h':
                        sb.Append("DV");
                        break;
                    case 'i':
                        sb.Append("FG");
                        break;
                    case 'j':
                        sb.Append("GF");
                        break;
                    case 'k':
                        sb.Append("VD");
                        break;
                    case 'l':
                        sb.Append("XA");
                        break;
                    case 'm':
                        sb.Append("DA");
                        break;
                    case 'n':
                        sb.Append("FD");
                        break;
                    case 'o':
                        sb.Append("VG");
                        break;
                    case 'p':
                        sb.Append("XV");
                        break;
                    case 'q':
                        sb.Append("AV");
                        break;
                    case 'r':
                        sb.Append("DG");
                        break;
                    case 's':
                        sb.Append("GD");
                        break;
                    case 't':
                        sb.Append("VA");
                        break;
                    case 'u':
                        sb.Append("AD");
                        break;
                    case 'v':
                        sb.Append("DF");
                        break;
                    case 'w':
                        sb.Append("AF");
                        break;
                    case 'x':
                        sb.Append("GV");
                        break;
                    case 'y':
                        sb.Append("VX");
                        break;
                    case 'z':
                        sb.Append("XD");
                        break;
                    case '0':
                        sb.Append("DX");
                        break;
                    case '1':
                        sb.Append("FA");
                        break;
                    case '2':
                        sb.Append("GA");
                        break;
                    case '3':
                        sb.Append("VF");
                        break;
                    case '4':
                        sb.Append("XF");
                        break;
                    case '5':
                        sb.Append("AG");
                        break;
                    case '6':
                        sb.Append("FV");
                        break;
                    case '7':
                        sb.Append("FX");
                        break;
                    case '8':
                        sb.Append("GX");
                        break;
                    case '9':
                        sb.Append("XG");
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
