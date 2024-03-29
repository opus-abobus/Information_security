namespace InformationSecurity.Steganography {
    internal static class ByteUtils {

        // Функция возвращает значение 255 - (2 ^ n - 1), где n - количество нулей
        public static byte GetOnes(byte trailingZerosNumber) {
            if (trailingZerosNumber < 0 || trailingZerosNumber > 8)
                return 0;

            return (byte) (byte.MaxValue << trailingZerosNumber);
        }

        // Функция возвращает байт с указанным количеством бит, считая справа налево
        public static byte GetLeastBits(byte number, byte bitsNumber) {
            if (bitsNumber < 1 || bitsNumber > 8) {
                var ex = new ArgumentOutOfRangeException(nameof(bitsNumber));
                throw ex;
            }

            byte ones = (byte) ((byte) Math.Pow(2, bitsNumber) - 1);

            return (byte) (ones & number);
        }

        // Функция, возвращающая число значащих битов (общее число битов минус число лидирующих нулевых битов) в байте в наибольшем по значению символе
        public static byte GetSignificantBitsNumber(string str) {
            if (string.IsNullOrEmpty(str))
                return 0;

            char maxChar = str[0];

            for (int i = 1; i < str.Length; i++) {
                if (str[i] > maxChar)
                    maxChar = str[i];
            }

            int x = maxChar;

            byte maxBitNumber = 0;

            while (x > 0) {
                x >>= 1;
                maxBitNumber++;
            }

            return maxBitNumber;
        }
    }
}
