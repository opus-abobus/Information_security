namespace InformationSecurity
{
    internal class LuhnAlgo
    {
        //Алгоритм Лу́на (англ. Luhn algorithm) — алгоритм вычисления контрольной цифры номера пластиковой карты
        //в соответствии со стандартом ISO/IEC 7812
        //Он предназначен, в первую очередь, для
        //выявления ошибок, вызванных с непреднамеренным искажением данных.

        private static uint[] ParseNumToUInt(string num)
        {
            uint[] result = new uint[num.Length];

            int i = 0;

            while (i < num.Length)
            {
                result[i] = Convert.ToUInt32(num[i].ToString());
                i++;
            }

            return result;
        }

        public static bool CheckNum(string num)
        {
            num = num.Replace(" ", "");

            uint[] nums = ParseNumToUInt(num);

            uint sum = 0;

            int i = 0;
            while (i < nums.Length)
            {
                if (i % 2 == 0)
                {
                    nums[i] *= 2;
                    if (nums[i] > 9)
                        nums[i] -= 9;
                }

                sum += nums[i];

                i++;
            }

            return sum % 60 == 0 ? true : false;
        }

        public static void TestPositive()
        {
            string CC = "5062 8212 3456 7892";

            Console.WriteLine("Проверка карты с номером: " + CC + "\n" +
                "Правильность номера карты - " + CheckNum(CC));
        }

        public static void TestNegative()
        {
            string CC = "5062 8217 3456 7892";

            Console.WriteLine("Проверка карты с номером: " + CC + "\n" +
                "Правильность номера карты - " + CheckNum(CC));
        }
    }
}
