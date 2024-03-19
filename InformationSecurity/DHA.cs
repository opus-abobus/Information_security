using InformationSecurity.MessagingService;
using System.Text;

namespace InformationSecurity {

    // Класс, описывающий алгоритм формирования ключа Диффи-Хеллмана
    internal class DHA {

        // Максимаьные значения основания степени (2^(8-1) - 1 = 127) и показателя степени (2^(8-5) - 1 = 7) соответственно.
        // Данные переменные накладывают ограничения на максимальные значения параметров в операциях возведения в степень.
        public const byte MaxBasis = byte.MaxValue >> 1;
        public const byte MaxPower = byte.MaxValue >> 5;

        private byte p, g;  // p и g - некоторые числа, не являющиеся секретными
        private EncryptionType _encryptionType;

        private List<Abonent> _abonents;

        public ushort Key { get; private set; }

        public string OriginalPlaintText { get; private set; }

        private List<Abonent> GenerateTwoSidedCommunication(string abon1Name, string abon2Name) {
            Abonent ab1 = new Abonent(abon1Name, p, g);
            Abonent ab2 = new Abonent(abon2Name, p, g);

            return new List<Abonent> { ab1, ab2 };
        }

        // Проерка числа на то, является ли оно простым
        public static bool IsPrimeNumber(uint num) {
            int x = 2;

            while (num % x != 0 && x <= Math.Sqrt(num))
                ++x;

            if (x >= Math.Sqrt(num))
                return true;

            return false;
        }
        public static byte GeneratePowerByte() {
            Random random = new Random();
            byte x = (byte) random.Next(0, MaxPower + 1);

            return x;
        }

        // Список алгоритмов шифрования при передаче сообщений через алгоритм Диффи-Хеллмана
        public enum EncryptionType {
            CaesarsCipher, AES
        }

        // Функция, выполняющая отправку одного сообщения от одного абонента к другому
        public void Process() {
            char[] encryptedChars;

            switch (_encryptionType) {
                case EncryptionType.AES: {
                        AesExample.Process(OriginalPlaintText);
                        break;
                    }
                case EncryptionType.CaesarsCipher: {
                        Encoding encoding = Console.OutputEncoding;
                        Console.OutputEncoding = Encoding.Unicode;

                        encryptedChars = CaesarsCipher.EncryptStrToCharArr(OriginalPlaintText, (sbyte) Key);

                        _abonents[0].SendEncryptedMessage(_abonents[1], encryptedChars);
                        _abonents[1].PrintEncryptedChars();

                        Console.WriteLine("Дешифрованное сообщение у " + _abonents[1].Name + ": " + 
                            new string(CaesarsCipher.DecryptCharsToCharArr(encryptedChars, (sbyte) Key)));

                        Console.OutputEncoding = encoding;

                        break;
                    }
            }
        }

        private DHA() { }

        public DHA(string plainText, byte p, byte g, EncryptionType encryptionType = EncryptionType.CaesarsCipher) {
            if (!(IsPrimeNumber(p) && IsPrimeNumber(g))) {
                Console.WriteLine("Значение переменной p или q не является простым числом." +
                    "Новые значения переменных: p = 71, g = 79");
                p = 71; g = 79;
            }

            if (String.IsNullOrEmpty(plainText))
                plainText = "Это простой текст.";


            OriginalPlaintText = plainText;

            this.p = p;
            this.g = g;

            _encryptionType = encryptionType;

            _abonents = GenerateTwoSidedCommunication(abon1Name: "Alice", abon2Name: "Bob");

            Abonent.SetKeyForAbonents(_abonents[0], _abonents[1], p);

            Key = Abonent.GetKeyFromAbonents(_abonents[0], _abonents[1]);
        }
    }
}
