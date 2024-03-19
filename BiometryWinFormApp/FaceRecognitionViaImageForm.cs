using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Diagnostics;

namespace BiometryWinFormApp {
    public partial class FaceRecognitionViaImageForm : Form {

        private Dictionary<string, string> frontalFaceClassifiers, eyeClassifiers, mouthClassifiers;
        private CascadeClassifier? faceClassifier, eyeClassifier, mouthClassifier;

        private Image<Bgr, byte>? emguImage;

        private BasicFaceRecognizer? faceRecognizer;
        private string[] faceRecognizerMethods = new string[] {
            "Eigen", "Fisher", "LBPH"
        };

        private readonly string classifiersDir = "data\\classifiers\\";
        private readonly string dataFolder = Application.StartupPath + "data\\";
        private readonly string basicFaceRecsFolder = Application.StartupPath + "data\\basicFaceRecognizers\\";
        private readonly string preTrainedRecName = "recognizer";

        private readonly string trainedImagesFolder = Application.StartupPath + "data\\images\\trainedFaces\\";
        private readonly string trainedFacesInfoPath = Application.StartupPath + "data\\images\\trainedFaces\\Faces.txt";

        private string? lastSourcePath;

        private BasicFaceRecognizer? preTrainedRec = null;

        private readonly string originalImagesFolder = Application.StartupPath + "data\\images\\original\\samples";
        private readonly string trainedImagesFolderNew = Application.StartupPath + "data\\images\\trained\\";
        private readonly string[] sampleImagesFolderForTrain = {
            Application.StartupPath + "data\\images\\original\\samples\\NikitaBuyanov\\",
            Application.StartupPath + "data\\images\\original\\samples\\PhilSpencer\\",
            Application.StartupPath + "data\\images\\original\\samples\\GabeNewell\\",
            Application.StartupPath + "data\\images\\original\\samples\\various\\"
        };

        public FaceRecognitionViaImageForm() {
            InitializeComponent();

            InitComboBoxes();
        }

        private void Form1_Load(object sender, EventArgs e) {
            comboBoxClassifFace.SelectedIndex = 2;
            comboBoxClassifEye.SelectedIndex = 0;
            comboBoxClassifMouth.SelectedIndex = 0;

            comboBoxRecType.SelectedIndex = 0;

            if (!File.Exists(trainedFacesInfoPath))
                File.Create(trainedFacesInfoPath);

            openFileDialog1.Filter = "Image Files(*.BMP; *.JPG; *.JPEG; *.jpeg)| *.BMP; *.JPG; *.JPEG; *.jpeg | All files(*.*) | *.*";
            openFileDialog1.InitialDirectory = Application.StartupPath + "data\\images\\";

            if (!Directory.Exists(trainedImagesFolderNew))
                Directory.CreateDirectory(trainedImagesFolderNew);

            if (!Directory.Exists(basicFaceRecsFolder)) {
                Directory.CreateDirectory(basicFaceRecsFolder);
            }

            InitialSaveAndTrain();
        }

        private void buttonLoadImage_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {

                lastSourcePath = openFileDialog1.FileName;

                emguImage = new Image<Bgr, byte>(lastSourcePath);
                pictureBox1.Image = emguImage.AsBitmap();
            }
        }

        private void buttonDetect_Click(object sender, EventArgs e) {
            if (emguImage == null) {
                MessageBox.Show("Загрузите изображение!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            emguImage = new Image<Bgr, byte>(lastSourcePath);
            pictureBox1.Image = emguImage.AsBitmap();

            if (faceClassifier == null || eyeClassifier == null || mouthClassifier == null)
                MessageBox.Show("Выберите каждый классификатор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                var grayScale = ConvertToGrayScale(emguImage);

                Rectangle[]? faces = TryDetect(grayScale, faceClassifier, (double) numericUpDownCFS.Value, (int) numericUpDownCFN.Value);

                int facesCount = 0;
                if (faces != null && faces.Length > 0) {

                    foreach (Rectangle face in faces) {

                        if (checkBoxTrain.Checked)
                            SaveImageForTrain(face, facesCount.ToString(), ".bmp");

                        grayScale.ROI = face;

                        emguImage.Draw(face, new Bgr(0, 0, 255), 2);

                        Rectangle[]? eyes = TryDetect(grayScale, eyeClassifier, (double) numericUpDownCES.Value, (int) numericUpDownCEN.Value);

                        if (eyes != null && eyes.Length > 0) {
                            foreach (Rectangle eye in eyes) {
                                var rectEye = eye;
                                rectEye.X += face.X;
                                rectEye.Y += face.Y;

                                emguImage.Draw(rectEye, new Bgr(0, 255, 0), 1);
                            }

                            Rectangle[]? mouths = TryDetect(grayScale, mouthClassifier, (double) numericUpDownCMS.Value, (int) numericUpDownCMN.Value);

                            if (mouths != null && mouths.Length > 0) {
                                var rectMouth = mouths[0];
                                rectMouth.X += face.X;
                                rectMouth.Y += face.Y;

                                emguImage.Draw(rectMouth, new Bgr(255, 0, 0), 1);
                            }
                        }

                        facesCount++;
                    }

                }

                pictureBox1.Image = emguImage.AsBitmap();
            }

        }

        private void buttonRecognize_Click(object sender, EventArgs e) {
            if (emguImage is null || faceRecognizer is null || preTrainedRec is null) {
                MessageBox.Show("Изображение не выбрано или в базе отсутствуют изображения для распознавания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            emguImage = new Image<Bgr, byte>(lastSourcePath);
            pictureBox1.Image = emguImage.AsBitmap();

            var greyScale = ConvertToGrayScale(emguImage);
            Rectangle[]? detectedFaces = TryDetect(greyScale, faceClassifier, (double) numericUpDownCFS.Value, (int) numericUpDownCFN.Value);

            if (detectedFaces != null && detectedFaces.Length > 0) {

                foreach (var detectedFace in detectedFaces) {

                    greyScale.ROI = detectedFace;

                    Image<Bgr, byte> cropped = GetCroppedBitmap(lastSourcePath, detectedFace).ToImage<Bgr, byte>();
                    cropped = cropped.Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic, false);
                    var img = ConvertToGrayScale(cropped);

                    if (img.Mat.IsContinuous) {
                        var predictionResult = preTrainedRec.Predict(img);

                        emguImage.Draw(detectedFace, new Bgr(0, 255, 0), 2);

                        var bottomLeft = new Point(detectedFace.X, detectedFace.Y - 2);
                        var color = new Bgr(0, 255, 0);

                        string labelInfo = preTrainedRec.GetLabelInfo(predictionResult.Label);
                        if (labelInfo == string.Empty)
                            labelInfo = "unknown";

                        emguImage.Draw(labelInfo, bottomLeft, Emgu.CV.CvEnum.FontFace.HersheySimplex, 2, color, 2);
                    }
                    else {
                        MessageBox.Show("Матрица обрабатываемого изображения не является продолжительной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                pictureBox1.Image = emguImage.AsBitmap();
            }
        }

        #region Вспомогательные функции

        private void InitialSaveAndTrain() {
            if (File.Exists(basicFaceRecsFolder + "recognizer")) {
                preTrainedRec = new EigenFaceRecognizer(80, double.PositiveInfinity);
                preTrainedRec.Read("data\\basicFaceRecognizers\\recognizer");
            }
            else {
                MessageBox.Show("Не найдена обученная модель по умолчанию. Будет произведено обучение на существущих данных.");

                preTrainedRec = GetRecognizerFromComboBox();

                SaveDetectedResizedImages(sampleImagesFolderForTrain, trainedImagesFolderNew, preTrainedRec);

                string[] trainedImagesSrc = { trainedImagesFolderNew };
 
                if (!TryTrain(trainedImagesSrc[0], preTrainedRec, basicFaceRecsFolder))
                    MessageBox.Show("Обучение не было успешно завершено.");
            }
        }

        [Obsolete]
        private void Recognize() {
            if (emguImage is null || faceRecognizer is null) {
                MessageBox.Show("Изображение не выбрано или в базе отсутствуют изображения для распознавания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            emguImage = new Image<Bgr, byte>(lastSourcePath);
            pictureBox1.Image = emguImage.AsBitmap();

            string[] infoParts = File.ReadAllText(trainedFacesInfoPath).Split(',');
            int[] labels = new int[infoParts.Length - 1];

            Image<Gray, byte>[] grayScales = new Image<Gray, byte>[infoParts.Length - 1];
            for (int i = 1; i <= Convert.ToInt16(infoParts[0]); i++) {
                grayScales[i - 1] = ConvertToGrayScale(new Image<Bgr, byte>(trainedImagesFolder + infoParts[i] + ".bmp").Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic, false));
                labels[i - 1] = i - 1;
            }
            VectorOfMat vectorOfMat = new VectorOfMat();
            VectorOfInt vectorOfInt = new VectorOfInt();
            vectorOfMat.Push(grayScales);
            vectorOfInt.Push(labels);

            faceRecognizer.Train(vectorOfMat, vectorOfInt);
            faceRecognizer.Write(basicFaceRecsFolder);

            var grayScale = ConvertToGrayScale(emguImage);
            Rectangle[]? detectedFaces = TryDetect(grayScale, faceClassifier, (double) numericUpDownCFS.Value, (int) numericUpDownCFN.Value);

            if (detectedFaces != null && detectedFaces.Length > 0) {

                foreach (var detectedFace in detectedFaces) {

                    grayScale.ROI = detectedFace;

                    Image<Bgr, byte> cropped = GetCroppedBitmap(lastSourcePath, detectedFace).ToImage<Bgr, byte>();
                    cropped = cropped.Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic, false);
                    var img = ConvertToGrayScale(cropped);

                    if (img.Mat.IsContinuous) {
                        var predictionResult = faceRecognizer.Predict(img);

                        emguImage.Draw(detectedFace, new Bgr(0, 255, 0), 2);

                        var bottomLeft = new Point(detectedFace.X, detectedFace.Y - 2);
                        var color = new Bgr(0, 255, 0);
                        emguImage.Draw(predictionResult.Label.ToString(), bottomLeft, Emgu.CV.CvEnum.FontFace.HersheySimplex, 2, color, 2);
                    }
                    else {
                        MessageBox.Show("Матрица обрабатываемого изображения не является продолжительной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                pictureBox1.Image = emguImage.AsBitmap();
            }
        }
        private static string[] GetFilesFrom(string searchFolder, string[] filters, bool isRecursive) {
            List<string> filesFound = new List<string>();

            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            foreach (var filter in filters) {
                filesFound.AddRange(Directory.GetFiles(searchFolder, string.Format("*.{0}", filter), searchOption));
            }

            return filesFound.ToArray();
        }

        private static string GetShortFileName(string absolutePath, bool withExtension = true) {
            string result = absolutePath.Substring(absolutePath.LastIndexOf("\\")).Remove(0, 1);

            if (withExtension)
                return result;
            else
                return Path.GetFileNameWithoutExtension(absolutePath);
        }

        private static string GetFileExtension(string path) {
            string[] parts = path.Split('.');
            return '.' + parts[parts.Length - 1];
        }

        private static string GetShortDirName(string dir) {
            string result;

            string[] parts = dir.Split("\\");

            if (parts[parts.Length - 1] == "")
                result = parts[parts.Length - 2];
            else
                result = parts[parts.Length - 1];

            return result;
        }

        private void SaveDetectedResizedImages(string[] imgDirsSrc, string targetFolder, BasicFaceRecognizer faceRecognizer) {
            if (imgDirsSrc.Length == 0) {
                return;
            }

            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            string[] labels = new string[imgDirsSrc.Length];

            string[] searchPattern = { "jpg", "jpeg", "png", "bmp" };

            var faceClassifier = new CascadeClassifier(frontalFaceClassifiers["haarFrontalFaceDefault"]);

            for (int i = 0; i < imgDirsSrc.Length; i++) {

                string[] fileNames = GetFilesFrom(imgDirsSrc[i], searchPattern, true);

                string shortFNames = string.Empty;

                for (int j = 0; j < fileNames.Length; j++) {

                    if (!Directory.Exists(targetFolder + "\\" + i))
                        Directory.CreateDirectory(targetFolder + "\\" + i);

                    string shortDirName = GetShortDirName(imgDirsSrc[i]);
                    if (shortDirName == "various")
                        faceRecognizer.SetLabelInfo(i, "unknown");
                    else
                        faceRecognizer.SetLabelInfo(i, GetShortDirName(imgDirsSrc[i]));

                    Image<Bgr, byte> image = new Image<Bgr, byte>(fileNames[j]);

                    Rectangle[]? faces = TryDetect(ConvertToGrayScale(image), faceClassifier, 1.1, 5);
                    if (faces != null && faces.Length > 0) {

                        if (shortDirName != "various") {
                            var bmp = GetCroppedBitmap(fileNames[j], faces[0]);
                            if (bmp != null) {
                                image = bmp.ToImage<Bgr, byte>();
                                bmp.Dispose();
                            }
                            else
                                continue;

                            shortFNames += GetShortFileName(fileNames[j]);

                            if (j < fileNames.Length - 1)
                                shortFNames += ",";

                            image.Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic, false).Save(targetFolder + "\\" + i + "\\" + GetShortFileName(fileNames[j]));
                        }
                        else {

                            for (int k = 0; k < faces.Length; k++) {
                                var bmp = GetCroppedBitmap(fileNames[j], faces[k]);
                                if (bmp != null) {
                                    image = bmp.ToImage<Bgr, byte>();
                                    bmp.Dispose();
                                }
                                else
                                    continue;

                                shortFNames += GetShortFileName(fileNames[j]) + "_" + k;

                                if (k < faces.Length - 1)
                                    shortFNames += ",";

                                image.Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic, false).Save(targetFolder + "\\" + i + "\\" + 
                                    GetShortFileName(fileNames[j], false) + "_" + k + GetFileExtension(fileNames[j]));
                            }

                        }
                    }
                }

                labels[i] = GetShortDirName(imgDirsSrc[i]) + "|" + i + "|" + shortFNames + "\n";
            }

            string labelsPath = targetFolder + "labels.txt";
            FileStream fS = File.Create(labelsPath, 1024, FileOptions.None);
            fS.Close();

            string strToWrite = string.Empty;
            for (int i = 0; i < labels.Length - 1; i++) {
                strToWrite += labels[i];
            }
            strToWrite += labels[labels.Length - 1];

            File.WriteAllText(labelsPath, strToWrite);
        }

        private Image<Gray, byte>[] GetGreyScales(string[] imgDirs, out VectorOfInt labelsVec) {
            labelsVec = new VectorOfInt();

            if (imgDirs.Length == 0)
                return null;

            List<Image<Gray, byte>>? grays = new List<Image<Gray, byte>>();

            string[] searchPattern = { "jpg", "jpeg", "png", "bmp" };

            List<int> intLabels = new List<int>();

            for (int i = 0; i < imgDirs.Length; i++) {

                if (Directory.Exists(imgDirs[i])) {

                    string[] fileNames = GetFilesFrom(imgDirs[i], searchPattern, true);

                    for (int j = 0; j < fileNames.Length; j++) {

                        grays.Add(ConvertToGrayScale(new Image<Bgr, byte>(fileNames[j])));

                        intLabels.Add(i);
                    }
                }
            }

            labelsVec.Push(intLabels.ToArray());

            if (grays.Count == 0)
                return null;

            return grays.ToArray(); 
        }

        private bool TryTrain(string imagesDir, BasicFaceRecognizer recognizer, string recognierOutputFolder) {
            if (imagesDir.Length == 0 || recognizer == null)
                return false;

            if (!Directory.Exists(recognierOutputFolder))
                Directory.CreateDirectory(recognierOutputFolder);

            VectorOfInt vectorOfInt;
            Image<Gray, byte>[] greys = GetGreyScales(Directory.GetDirectories(imagesDir), out vectorOfInt);

            if (greys != null && greys.Length > 0) {

                VectorOfMat vectorOfMat = new VectorOfMat();
                vectorOfMat.Push(greys);

                recognizer.Train(vectorOfMat, vectorOfInt);

                try {
                    recognizer.Write("data\\basicFaceRecognizers\\" + preTrainedRecName);
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                
                return true;
            }

            return false;
        }

        [Obsolete]
        private void SaveImageForTrain(Rectangle imageRect, string postFix, string imageFormat = ".bmp") {
            Bitmap bitmap = new Bitmap(Image.FromFile(lastSourcePath));

            string path = lastSourcePath;
            path = path.Substring(path.LastIndexOf("\\")).Remove(0, 1);
            path = path.Substring(0, path.Length - (path.Length - path.LastIndexOf('.')));

            postFix = "_" + postFix;

            string label = path + postFix;

            path = trainedImagesFolder + path;

            //UpdateTrainedImagesFile(label, path + postFix + imageFormat);
            bitmap.Clone(imageRect, bitmap.PixelFormat).Save(path + postFix + imageFormat);

            bitmap.Dispose();
        }

        [Obsolete]
        private void UpdateTrainedImagesFile(string label, string fullImagePath) {
            if (File.Exists(fullImagePath))
                return;

            string facesInfo = File.ReadAllText(trainedFacesInfoPath);

            string[] infoParts = facesInfo.Split(',');

            if (infoParts.Length == 0)
                infoParts = new string[1];

            try {
                infoParts[0] = (Convert.ToInt16(infoParts[0]) + 1).ToString();
            }
            catch {
                infoParts[0] = "1";
            }

            facesInfo = string.Empty;
            int i = 0;
            while (i < infoParts.Length) {
                facesInfo += infoParts[i] + ",";
                i++;
            }
            facesInfo += label;

            File.WriteAllText(trainedFacesInfoPath, facesInfo);
        }
        private static Image<Gray, byte> ConvertToGrayScale(Image<Bgr, byte> image) {
            return image.Convert<Gray, byte>().Clone();
        }
        public static Rectangle[]? TryDetect(Image<Gray, byte> image, CascadeClassifier classifier, double scaleFactor = 1.1, int minNeighbours = 3) {
            Rectangle[]? detected = null;

            try {
                detected = classifier.DetectMultiScale(image, scaleFactor, minNeighbours);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return detected;
        }
        private Bitmap? GetCroppedBitmap(string sourcePath, Rectangle croppedArea) {
            try {
                Bitmap bitmap = new Bitmap(sourcePath);
                return bitmap.Clone(croppedArea, bitmap.PixelFormat);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n" + sourcePath);
            }

            return null;
        }

        private void InitComboBoxes() {

            // face
            frontalFaceClassifiers = new Dictionary<string, string>() {
                ["haarFrontalFaceAlt"] = classifiersDir + "haarcascade_frontalface_alt.xml",
                ["haarFrontalFaceAltTree"] = classifiersDir + "haarcascade_frontalface_alt_tree.xml",
                ["haarFrontalFaceDefault"] = classifiersDir + "haarcascade_frontalface_default.xml",
                ["lbpFrontalFace"] = classifiersDir + "lbpcascade_frontalface.xml",
                ["lbpFrontalFaceImproved"] = classifiersDir + "lbpcascade_frontalface_improved.xml"
            };

            comboBoxClassifFace.Items.Clear();
            foreach (string classifierName in frontalFaceClassifiers.Keys) {
                comboBoxClassifFace.Items.Add(classifierName);
            }
            // --face

            // eye
            eyeClassifiers = new Dictionary<string, string>() {
                ["haarEye"] = classifiersDir + "haarcascade_eye.xml"
            };

            comboBoxClassifEye.Items.Clear();
            foreach (string classifierName in eyeClassifiers.Keys) {
                comboBoxClassifEye.Items.Add(classifierName);
            }
            // --eye

            // mouth
            mouthClassifiers = new Dictionary<string, string>() {
                ["haarMcsMouth"] = classifiersDir + "haarcascade_mcs_mouth.xml",
            };

            comboBoxClassifMouth.Items.Clear();
            foreach (string classifierName in mouthClassifiers.Keys) {
                comboBoxClassifMouth.Items.Add(classifierName);
            }
            // --mouth

            // recognizerMethods
            comboBoxRecType.Items.Clear();
            comboBoxRecType.Items.AddRange(faceRecognizerMethods);
            // --recognizerMethods
        }

        private BasicFaceRecognizer GetRecognizerFromComboBox() {
            if (comboBoxRecType.SelectedItem != null) {

                switch (comboBoxRecType.SelectedItem) {

                    case "Eigen": {
                            return new EigenFaceRecognizer(80, double.PositiveInfinity);
                        }

                    case "Fisher": {
                            return new FisherFaceRecognizer(80, double.PositiveInfinity);
                        }

                    case "LBPH": {
                            //return new LBPHFaceRecognizer();
                            return new EigenFaceRecognizer(80, double.PositiveInfinity);
                        }
                    default: return new EigenFaceRecognizer(80, double.PositiveInfinity);
                }
            }
            else 
                return null;
        }

        #endregion

        private void comboBoxRecType_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBoxRecType.SelectedItem != null) {

                switch (comboBoxRecType.SelectedItem) {

                    case "Eigen": {
                            faceRecognizer = new EigenFaceRecognizer(0, double.PositiveInfinity);
                            break;
                        }

                    case "Fisher": {
                            faceRecognizer = new FisherFaceRecognizer(0, double.PositiveInfinity);
                            break;
                        }

                    case "LBPH": {
                            //faceRecognizer = new LBPHFaceRecognizer();
                            faceRecognizer = new EigenFaceRecognizer();
                            break;
                        }
                }
            }
        }
        private void checkBoxTrain_CheckedChanged(object sender, EventArgs e) {
            if (checkBoxTrain.Checked)
                buttonDetect.Text = "Обнаружить черты лица и сохранить";
            else
                buttonDetect.Text = "Обнаружить черты лица";
        }
        private void buttonOpenDataFolder_Click(object sender, EventArgs e) {
            Process.Start("explorer.exe", dataFolder);
        }
        private void FaceRecognitionViaImageForm_FormClosed(object sender, FormClosedEventArgs e) {
            //FormManager.Instance.ShowStart();
            FormManager.Instance.CloseAll();
        }
        private void comboBoxClassifFace_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBoxClassifFace.SelectedItem != null)
                faceClassifier = new CascadeClassifier(frontalFaceClassifiers[comboBoxClassifFace.SelectedItem.ToString()]);
        }
        private void comboBoxClassifEye_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBoxClassifEye.SelectedItem != null)
                eyeClassifier = new CascadeClassifier(eyeClassifiers[comboBoxClassifEye.SelectedItem.ToString()]);
        }
        private void comboBoxClassifMouth_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBoxClassifMouth.SelectedItem != null)
                mouthClassifier = new CascadeClassifier(mouthClassifiers[comboBoxClassifMouth.SelectedItem.ToString()]);
        }
    }
}
