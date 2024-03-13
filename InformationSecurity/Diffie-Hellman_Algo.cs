using InformationSecurity.MessagingService;
using System.Text;

namespace InformationSecurity {
    internal static class Diffie_Hellman_Algo {

        // Список алгоритмов шифрования, выбранным элементом которого будет задействован алгоритм Диффи-Хеллмана
        public enum EncryptionType {
            CaesarsCipher, AES, MyAES
        }

        public class TwoSidedCommunication {
            private ushort p, g;  // p и g - некоторые числа, не являющиеся секретными

            private List<Abonent> _abonents;

            public ushort GetSessionKey() {
                return _abonents[0].GetKey();
            }

            private TwoSidedCommunication(ushort p, ushort g) {
                this.p = p;
                this.g = g;

                _abonents = new List<Abonent>() { new Abonent(this, "Alice"), new Abonent(this, "Bob") };

                Abonent.ExchangeValues(_abonents[0], _abonents[1]);

                Console.WriteLine("Ключи равны? - " + Abonent.IsKeyEquals(_abonents[0], _abonents[1]));
            }

            // Абонент описывает сторону (участника) сообщений.
            // Он имеет собственный закрытый ключ, а также секретный сессионный ключ (одинаковый для обоих участников)
            private class Abonent {
                public string Name { get; private set; }
                
                // Ссылка на экземпляр внешнего класса необходима для получения доступа к значениям p и g.
                private TwoSidedCommunication _session;

                // Псевдослучайное секретное за пределами абонента число
                private byte _random;

                // Число, пересылаемое другому абоненту
                public ushort Value { get; private set; }

                private ushort _receivedValue, _key;
                public ushort GetKey() { return _key; }


                private List<string> _incEncrMessages, _incDecrMessages;
                public static bool IsKeyEquals(Abonent ab1, Abonent ab2) {
                    if (ab1 == null || ab2 == null || ab1.Equals(ab2))
                        return false;

                    if (ab1._key == ab2._key) 
                        return true;

                    return false;
                }

                public static void ExchangeValues(Abonent ab1, Abonent ab2) {
                    if (ab1 == null || ab2 == null || ab1.Equals(ab2))
                        return;

                    ab1._receivedValue = ab2.Value;
                    ab2._receivedValue = ab1.Value;

                    ab1._key = (ushort) (Math.Pow(ab1._receivedValue, ab1._random) % ab1._session.p);
                    ab2._key = (ushort) (Math.Pow(ab2._receivedValue, ab2._random) % ab2._session.p);
                }

                public void SendMessage(string plainText, Abonent receiver) {
                    if (this.Equals(receiver) || receiver == null) 
                        return;

                    byte[] enc = AesExample.EncryptStringToBytes(plainText);

                    receiver._incEncrMessages.Add(Convert.ToBase64String(enc));
                }

                private void DecryptAllMessages() {
                    foreach (var message in _incEncrMessages) {
                        _incDecrMessages.Add(AesExample.DecryptStringFromBytes(Convert.FromBase64String(message)));
                    }
                }

                public void PrintAllMessages() {
                    DecryptAllMessages();

                    Console.WriteLine("Вывод дешифрованных сообщений: ");

                    foreach (var message in _incDecrMessages) 
                        Console.WriteLine(message);

                    Console.WriteLine("\n");
                }

                public Abonent(TwoSidedCommunication currentSession, string name = "default_name") {
                    _session = currentSession;

                    _random = GenerateByte();

                    Value = (ushort) (Math.Pow(_session.g, _random) % _session.p);

                    _incEncrMessages = new List<string>();
                    _incDecrMessages = new List<string>();

                    Name = name;
                }
            }

            private static byte GenerateByte(int bitShift = 2) {
                if (bitShift < 0 || bitShift > 7)
                    bitShift = 2;

                Random random = new Random();
                byte x = (byte) random.Next(0, (byte.MaxValue + 1) >> bitShift);

                return x;
            }

            public static void TestExample() {
                string plainText = "Это простой текст";

                var s = new TwoSidedCommunication(71, 79);

                Console.WriteLine(TwoSidedCommunication.Abonent.IsKeyEquals(s._abonents[0], s._abonents[1]));

                Console.WriteLine(s._abonents[0].GetKey() + "\t" + s._abonents[1].GetKey());

                s._abonents[0].SendMessage(plainText, s._abonents[1]);
                s._abonents[1].PrintAllMessages();
            }

            public static TwoSidedCommunication CreateSession(ushort p, ushort g) {
                return new TwoSidedCommunication(p, g);
            }
        }

        // Функция, выполняющая работу алгоритма Диффи-Хеллмана
        /*public static void Process(string plainText, EncryptionType encryptionType, ushort p = 71, ushort g = 79, byte numberOfAbonents = 2) {
            if (!(IsPrimeNumber(p) && IsPrimeNumber(g)) || String.IsNullOrEmpty(plainText)) {
                Console.WriteLine("Входная строка является пустой либо p или q не является простым числом. Завершение работы алгоритма.");
                return;
            }
            else {
                TwoSidedCommunication session = TwoSidedCommunication.CreateSession(p, g);

                ushort sessionKey = session.GetSessionKey();
                Console.WriteLine("Сессионный ключ: " + sessionKey);

                switch (encryptionType) {

                    case EncryptionType.AES: {
                            AesExample.Process(plainText);
                            break;
                        }

                    case EncryptionType.MyAES: {
                            MyAES.Process(plainText);
                            break;
                        }

                    case EncryptionType.CaesarsCipher: {
                            //CaesarsCipher.Process(plainText, (sbyte) sessionKey);
                            TwoSidedCommunication.TestExample();
                            break;
                        }
                }
            }
        }*/
    }

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
