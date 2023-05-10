namespace TZI.Helpers
{
    internal class AtbashHelper
    {
        private const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        private static string Reverse(string inputText)
        {
            var reversedText = string.Empty;
            foreach (var s in inputText)
            {
                reversedText = s + reversedText;
            }

            return reversedText;
        }

        private static string EncryptDecrypt(string text, string symbols, string cipher)
        {
            text = text.ToLower();
            var outputText = string.Empty;
            for (var i = 0; i < text.Length; i++)
            {
                var index = symbols.IndexOf(text[i]);
                if (index >= 0)
                {
                    outputText += cipher[index].ToString();
                }
            }

            return outputText;
        }

        public static string Encipher(string plainText)
        {
            return EncryptDecrypt(plainText, alphabet, Reverse(alphabet));
        }

        public static string Decipher(string encryptedText)
        {
            return EncryptDecrypt(encryptedText, Reverse(alphabet), alphabet);
        }
    }
}
