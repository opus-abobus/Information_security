using System.Drawing;

namespace InformationSecurity.Steganography {
    internal class ImageData {

        private Image? _image;

        public uint TotalPixels { get; private set; }
        public uint TotalComponents { get; private set; }

        public Image? Image {
            get {
                return _image;
            }
            set {
                _image = value;

                if (value == null) {
                    TotalPixels = 0;
                    TotalComponents = 0;
                }
                else {
                    TotalPixels = ImageUtils.GetTotalPixels(value);
                    TotalComponents = TotalPixels * 3;
                }
            }
        }

        // Метод возвращает максимальное число символов в строке, которую можно сокрыть в изображении
        public uint GetMaxSymbolsToEncode(byte leastSignifNum) {
            if (TotalPixels == 0)
                return 0;

            return ImageUtils.GetMaxMessageLength(TotalPixels, leastSignifNum);
        }

        public ImageData() {
            Image = null;
        }

        public ImageData(Image image) {
            Image = image;
        }
    }
}
