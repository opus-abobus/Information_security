namespace InformationSecurity.Cryptography.MessagingService
{

    // Абонент-класс описывает сторону (участника) системы обмена сообщений.
    internal class Abonent
    {

        // Псевдослучайное секретное за пределами абонента число
        private byte _random;

        // Число, пересылаемое другому абоненту
        public ushort Value { get; private set; }

        public string Name { get; private set; } = "";
        public ushort Key { get; private set; }

        private char[] _encryptedChars;

        public static void SetKeyForAbonents(Abonent ab1, Abonent ab2, int p)
        {
            ab1.Key = (ushort)(Program.Pow(ab2.Value, ab1._random) % (ulong)p);
            ab2.Key = (ushort)(Program.Pow(ab1.Value, ab2._random) % (ulong)p);
        }

        public void SetKeyFromAbonent(Abonent abonent, int p)
        {
            Key = (ushort)(Program.Pow(abonent.Value, _random) % (ulong)p);
        }

        public static ushort GetKeyFromAbonents(Abonent ab1, Abonent ab2)
        {
            return (ushort)(ab1.Key == ab2.Key ? ab1.Key : 0);
        }

        public void SendEncryptedMessage(Abonent receiver, char[] encryptedChars)
        {
            receiver._encryptedChars = encryptedChars;
        }

        public void PrintEncryptedChars()
        {
            if (_encryptedChars == null)
                Console.WriteLine("У абонента " + Name + " отсутствует шифросимволы");
            else
                Console.WriteLine("Шифротекст абонента " + Name + ": " + new string(_encryptedChars));
        }

        private Abonent() { }

        public Abonent(string name, int p, int g)
        {
            Name = name;

            _random = DHA.GeneratePowerByte();

            if (g > DHA.MaxBasis)
                Console.WriteLine("Значение g превышает максимальное значение основания степени, результат может быть некорректным.");

            Value = (ushort)(Program.Pow(g, _random) % (ulong)p);
        }
    }
}
