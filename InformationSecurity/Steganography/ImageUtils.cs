using System.Diagnostics;
using System.Drawing;

namespace InformationSecurity.Steganography {
    internal static class ImageUtils {
        public static uint GetTotalPixels(Image image) {
            return (uint) (image.Width * image.Height);
        }

        public static uint GetMaxMessageLength(uint totalPixels, byte leastSignifBitsNum) {
            byte significantBitsNum = 8;
            int componentsToEncode = significantBitsNum % leastSignifBitsNum == 0 ? significantBitsNum / leastSignifBitsNum : significantBitsNum / leastSignifBitsNum + 1;

            uint totalComponents = totalPixels * 3;

            uint length = (uint) (totalComponents / componentsToEncode);

            return length;
        }

        public static void OpenImage(string path) {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo(path);
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }

        public static void SaveImage(Image image, string dir, string name, string extension = "bmp") {
            image.Save(dir + name + "." + extension);
        }
    }
}
