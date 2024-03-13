using System.Text;

namespace InformationSecurity
{
    internal class Program
    {
        #region Функции практических заданий

        // Алгоритм проверки корректности номера банковской карты
        public static void Practice1() {
            LuhnAlgo.TestPositive();
            Console.WriteLine();

            LuhnAlgo.TestNegative();
            Console.WriteLine();
        }

        // Алгоритм формирования секретного ключа Диффи-Хеллмана
        public static void Practice2() {
            string message = "Текст, в котором многа букавак.";
            Console.WriteLine("Исходное сообщение: " + message);

            byte p = 71, g = 79;
            var encryptionType = DHA.EncryptionType.CaesarsCipher;
            var dha = new DHA(message, p, g, encryptionType);

            Console.WriteLine("Сессионный ключ: " + dha.Key);

            dha.Process();
        }

        #endregion

        private static void Main(string[] args)
        {
            Practice2();
        }

        public static ulong Pow(int x, int power) {
            if (power == 0)
                return 1;

            if (power == 1)
                return (ulong) x;

            if (power % 2 == 1)
                return (ulong) x * Pow(x * x, power / 2);

            return Pow(x * x, power / 2);
        }
    }
}
