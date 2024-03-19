using System.Diagnostics;
using System.Reflection;

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
            string message = "Это простой текст.";
            Console.WriteLine("Исходное сообщение: " + message);

            byte p = 71, g = 79;
            var encryptionType = DHA.EncryptionType.CaesarsCipher;
            var dha = new DHA(message, p, g, encryptionType);

            Console.WriteLine("Сессионный ключ: " + dha.Key);

            dha.Process();
        }

        //
        public static void Practice3() {
            Assembly? assembly = Assembly.GetAssembly(typeof(BiometryWinFormApp.Program));  // не работает в конфигурации Release

            string targetExe;

            if (assembly != null) {
                targetExe = assembly.Location;

                // replace "dll" with "exe"
                targetExe = targetExe.Substring(0, targetExe.Length - 3) + "exe";
            }
            else {
                Console.WriteLine("Исполняемый файл не найден. Завершение работы программы.");
                return;
            }

            Process.Start(targetExe);
        }

        #endregion

        private static void Main(string[] args)
        {
            Practice3();
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
