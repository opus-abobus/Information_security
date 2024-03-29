using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InformationSecurity.Steganography
{
    // Класс, описывающий работу алгоритма LSB (Least significant bit - Наименее значащий бит) 
    internal class LSB
    {
        private static readonly string _messageDefault = "This is an encoded text!";

        private Encoding _encoding = Encoding.ASCII;

        private ImageData? _imageData;

        public ImageData? ImageData { 
            get {
                return _imageData;
            }
            private set {
                _imageData = value;
            }
        }

        private LSB() { }

        public LSB(ImageData imageData) {
            _imageData = imageData;
        }

        public LSB(ImageData imageData, Encoding encoding) {
            _imageData = imageData;
            _encoding = encoding;
        }

        private static byte GetColorComponentValue(Color color, int component) {
            switch (component) {
                case 0:
                        return color.R;
                case 1:
                    return color.G;
                case 2:
                    return color.B;
                default:
                    return 0;
            }
        }

        private static void SetColorComponentValue(ref Color pixelColor, int component, byte newColorComponentValue) {

            Color newColor = new Color();
            switch (component) {
                case 0: {
                        newColor = Color.FromArgb(pixelColor.A, newColorComponentValue, pixelColor.G, pixelColor.B);
                        break;
                    }
                case 1: {
                        newColor = Color.FromArgb(pixelColor.A, pixelColor.R, newColorComponentValue, pixelColor.B);
                        break;
                    }
                case 2: {
                        newColor = Color.FromArgb(pixelColor.A, pixelColor.R, pixelColor.G, newColorComponentValue);
                        break;
                    }
            }

            pixelColor = newColor;
        }

        private Bitmap? Encode(string message, byte lsbNumber = 2) {
            if (ImageData == null || ImageData.Image == null || lsbNumber < 1 || lsbNumber > 8 || message.Length > ImageData.GetMaxSymbolsToEncode(lsbNumber))
                return null;

            Bitmap bitmap = new Bitmap(ImageData.Image);

            byte[] messageBytes = Encoding.ASCII.GetBytes(message);

            byte significantBitsNum = 8;
            int componentsToEncode = significantBitsNum % lsbNumber == 0 ? significantBitsNum / lsbNumber : significantBitsNum / lsbNumber + 1;
            Console.WriteLine("Компонент необходимо для сокрытия одного символа: " + componentsToEncode);

            int charIndex = 0;
            int width = bitmap.Width, height = bitmap.Height;
            int i = 0, j = 0;

            Color color = bitmap.GetPixel(0, 0);

            //Console.WriteLine("--------------------Байт " + charIndex + " символа: " + messageBytes[charIndex].ToString("b8"));

            int totalComponents = componentsToEncode * message.Length;
            byte bitShift = 8;
            for (int c = 0; c < totalComponents; c++) {

                byte colorComponentValue = GetColorComponentValue(color, c % 3);
                //Console.WriteLine("В пикселе (" + i + "," + j + ") c = " + c % 3 + ": " + colorComponentValue.ToString("b8") + "\n");

                bitShift -= lsbNumber;
                if ((byte) (bitShift + lsbNumber) == 0)
                    bitShift = 0;

                // Операция записи сокрытой в очередной компоненте пикселя части байта символа сообщения
                // Биты "записываются" слева направо (первые lsbNumber битов скрываемого символа копируются в последние lsbNumber битов компоненты цвета)
                byte newColorComponentValue;
                if (bitShift > 8) {
                    bitShift += lsbNumber;

                    newColorComponentValue = (byte) ((byte) (colorComponentValue & ByteUtils.GetOnes((byte) (lsbNumber - (lsbNumber - bitShift)))) |
                        ByteUtils.GetLeastBits((byte) (messageBytes[charIndex] >> 0), (byte) (lsbNumber - (lsbNumber - bitShift))));
                }
                else {
                    if (lsbNumber > bitShift) {
                        newColorComponentValue = (byte) ((byte) (colorComponentValue & ByteUtils.GetOnes(lsbNumber)) |
                            ByteUtils.GetLeastBits((byte) (messageBytes[charIndex] >> bitShift), lsbNumber));
                    }
                    else {
                        newColorComponentValue = (byte) ((byte) (colorComponentValue & ByteUtils.GetOnes(lsbNumber)) |
                            ByteUtils.GetLeastBits((byte) (messageBytes[charIndex] >> bitShift), lsbNumber));
                    }
                }

                SetColorComponentValue(ref color, c % 3, newColorComponentValue);
                bitmap.SetPixel(i, j, Color.FromArgb(color.ToArgb()));

                //Console.WriteLine("В пикселе (" + i + "," + j + ") c = " + c % 3 + ": " + newColorComponentValue.ToString("b8") + " - новое значение цвета\n");

                if (((c + 1) % componentsToEncode == 0) && ( (c != 0) || (componentsToEncode == 1) )) {
                    charIndex++;

                    bitShift = 8;

                    //if (charIndex < message.Length)
                        //Console.WriteLine("--------------------Байт " + charIndex + " символа: " + messageBytes[charIndex].ToString("b8"));
                }
                    

                // Переход к следующему пикселю, если компонента цвета является "последней"
                if (c % 3 == 2) {

                    j++;
                    if (j == height) {
                        i++;
                        j = 0;
                        if (i == width)
                            return null;
                    }

                    color = bitmap.GetPixel(i, j);
                }
            }

            i = width - 1;
            j = height - 1;
            color = bitmap.GetPixel(i, j);
            Color dataColor;
            if (totalComponents > 255) {
                Console.WriteLine("Не удалось записать длину сокрытого текста: cлишком много компонент.");
                dataColor = Color.FromArgb(color.A, color.R, lsbNumber, color.B);
            }
            else {
                dataColor = Color.FromArgb(color.A, totalComponents, lsbNumber, color.B);
            }
            bitmap.SetPixel(i, j, dataColor);

            Console.WriteLine("Было сокрыто " + totalComponents + " компонент.");

            return bitmap;
        }

        private string Decode(Bitmap encoded) {

            if (encoded == null)
                return string.Empty;

            int componentsEncodedTotal = encoded.GetPixel(encoded.Width - 1, encoded.Height - 1).R;
            byte lsb = encoded.GetPixel(encoded.Width - 1, encoded.Height - 1).G;

            Console.WriteLine("В первых " + componentsEncodedTotal + " компонентах сокрыты биты сообщения.\nЧисло значащих битов: " + lsb + "\n");

            List<byte> decodedByteList = new List<byte>();

            Color color = encoded.GetPixel(0, 0);

            // Индексы ширины и высоты соответственно
            int i = 0, j = 0;

            byte symbolByte = 0;

            byte bitTurple = 0;

            for (int c = 0; c < componentsEncodedTotal; c++) {
                //Console.WriteLine("colorVal = " + GetColorComponentValue(color, c % 3).ToString("b8"));

                byte lb;

                byte shift = (byte) (8 - bitTurple - lsb);
                if (shift > 8) {
                    shift = 0;
                    lb = ByteUtils.GetLeastBits(GetColorComponentValue(color, c % 3), (byte) (8 - bitTurple));
                }
                else {
                    lb = ByteUtils.GetLeastBits(GetColorComponentValue(color, c % 3), lsb);
                }

                lb <<= shift;
                symbolByte |= lb;

                bitTurple += lsb;

                if (bitTurple >= 8) {
                    decodedByteList.Add(symbolByte);
                    bitTurple = 0;
                    symbolByte = 0;
                }

                // Переход к следующему пикселю, если компонента цвета является "последней"
                if (c % 3 == 2) {

                    j++;
                    if (j == encoded.Height) {
                        j = 0;
                        i++;
                        if (i == encoded.Width)
                            return string.Empty;
                    }

                    color = encoded.GetPixel(i, j);
                }
            }

            //Console.WriteLine("Байт первого символа: " + decodedByteList[0].ToString("b8") + ", а должен быть " + Encoding.ASCII.GetBytes("I")[0].ToString("b8"));

            string result = Encoding.ASCII.GetString(decodedByteList.ToArray());

            return result;
        }


        public void TestExample(string message = "", byte lsbNumber = 2)
        {
            Encoding prevEncoding = Console.OutputEncoding;
            Console.OutputEncoding = _encoding;

            if (message == "")
                message = _messageDefault;

            Console.WriteLine("Исходное сообщение: " + message + "\n");
            Console.WriteLine("Длина сообщения: " + message.Length + ", число наименее значащих битов: " + lsbNumber);

            Console.WriteLine("\n...Шифрование...\n");
            var encodedBmp = Encode(message, lsbNumber);

            if (encodedBmp == null) {
                Console.WriteLine("Не удалось создать изображение со скрытым текстом.");
                return;
            }

            string dir = Application.StartupPath + "\\Steganography\\images\\";

            //ImageUtils.SaveImage(encodedBmp, dir, "encoded");
            encodedBmp.Save(dir + "encoded.bmp");

            Console.WriteLine("\n...Дешифровка...\n");
            string decodedMessage = Decode(encodedBmp);

            Console.WriteLine("Скрытое сообщение: " + decodedMessage + "\n");

            //ImageUtils.OpenImage(dir + "encoded.bmp");

            Console.OutputEncoding = prevEncoding;
        }
    }
}
