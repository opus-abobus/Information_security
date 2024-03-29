using System.Text;

namespace InformationSecurity.Cryptography
{
    internal class CaesarsCipher
    {
        public static void Process(string plainText, sbyte key)
        {
            Encoding encoding = Console.OutputEncoding;
            Console.OutputEncoding = Encoding.Unicode;

            Console.WriteLine("Исходный текст: \n" + plainText);

            char[] cipherChars = EncryptStrToCharArr(plainText, key);
            Console.WriteLine("Зашифрованный текст: \n" + Convert.ToString(new string(cipherChars)));

            char[] decryptedChars = DecryptCharsToCharArr(cipherChars, key);
            string? decryptedStr = new string(decryptedChars);

            Console.WriteLine("Дешифрованный текст: \n" + decryptedStr + "\n");

            Console.OutputEncoding = encoding;
        }

        public static char[] EncryptStrToCharArr(string plainText, sbyte key)
        {
            char[] encrypted = new char[plainText.Length];

            int i = 0;
            foreach (char c in plainText)
            {
                encrypted[i++] = (char)(c + key);
            }

            return encrypted;
        }

        public static char[] DecryptCharsToCharArr(char[] encryptedChars, sbyte key)
        {
            char[] decrypted = new char[encryptedChars.Length];

            int i = 0;
            foreach (char c in encryptedChars)
            {
                decrypted[i++] = (char)(c - key);
            }

            return decrypted;
        }
    }
}
