using System.Security.Cryptography;

namespace InformationSecurity.Cryptography
{
    internal class AesExample
    {

        // 128-битный ключ и вектор инициализации по умолчанию
        private static readonly byte[] defaultKey_16 = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
        private static readonly byte[] defaultIv_16 = { 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26 };

        // Проверка на то, являются ли длина ключа и вектор инициализации корректными величинами
        private static bool IsKeyValid(byte[] key, byte[] iv)
        {
            if (key == null || iv == null)
                return false;

            if (key.Length == 16 || key.Length == 24 || key.Length == 32)
            {
                if (iv.Length == key.Length)
                    return true;
            }

            return false;
        }

        // Функция зашифровывает строку в массив байтов. Возвращает массив "зашифрованных" байтов.
        public static byte[] EncryptStringToBytes(string plainText, byte[] key = null, byte[] iv = null)
        {
            if (key == null)
                key = defaultKey_16;
            if (iv == null)
                iv = defaultIv_16;

            Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            byte[] cipherTextBytes;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (StreamWriter sWriter = new StreamWriter(cryptoStream))
                    {
                        sWriter.Write(plainText);
                    }
                    cipherTextBytes = memoryStream.ToArray();
                }
            }

            return cipherTextBytes;
        }

        // Функция дешифрует массив байтов в строку. Возвращает дешифрованную строку.
        public static string DecryptStringFromBytes(byte[] cipherTextBytes, byte[] key = null, byte[] iv = null)
        {
            if (key == null)
                key = defaultKey_16;
            if (iv == null)
                iv = defaultIv_16;

            Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            string text;

            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        text = streamReader.ReadToEnd();
                    }
                }
            }

            return text;
        }

        // Функция, демонстрирующая пример работы алгоритма, используя 128-битный ключ и вектор инициализации по умолчанию.
        public static void TestExample1()
        {
            string plainText = "Это секретный текст";

            if (!IsKeyValid(defaultKey_16, defaultIv_16)) return;

            byte[] cipherTextBytes = EncryptStringToBytes(plainText, defaultKey_16, defaultIv_16);

            string decryptedText = DecryptStringFromBytes(cipherTextBytes, defaultKey_16, defaultIv_16);

            Console.WriteLine("Исходный текст: " + plainText);
            Console.WriteLine("Зашифрованный текст: " + Convert.ToBase64String(cipherTextBytes));
            Console.WriteLine("Дешифрованный текст: " + decryptedText);
        }

        // Функция, выполняющая работу алгоритма AES, определенного в пространстве имен System.Security.Cryptography
        public static void Process(string plainText, byte[] key = null, byte[] iv = null)
        {
            if (plainText.Length == 0) return;

            if (!IsKeyValid(key, iv))
            {
                key = defaultKey_16;
                iv = defaultIv_16;
            }

            byte[] cipherTextBytes = EncryptStringToBytes(plainText, key, iv);

            string decryptedText = DecryptStringFromBytes(cipherTextBytes, key, iv);

            Console.WriteLine("Исходный текст: " + plainText);
            Console.WriteLine("Зашифрованный текст: " + Convert.ToBase64String(cipherTextBytes));
            Console.WriteLine("Дешифрованный текст: " + decryptedText);
        }
    }
}
