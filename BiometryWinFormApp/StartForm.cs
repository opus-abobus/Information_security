using System.Diagnostics;

namespace BiometryWinFormApp {
    public partial class StartForm : Form {

        private readonly string dataFolder = Application.StartupPath + "data\\";

        public StartForm() {
            InitializeComponent();
        }

        private void buttonImageRec_Click(object sender, EventArgs e) {
            FormManager.Instance.ShowNext(new FaceRecognitionViaImageForm());
        }

        private void buttonCamRec_Click(object sender, EventArgs e) {
            FormManager.Instance.ShowNext(new FaceRecognitionViaCameraForm());
        }

        private void StartForm_Load(object sender, EventArgs e) {

        }

        private void buttonOpenDataFolder_Click(object sender, EventArgs e) {
            Process.Start("explorer.exe", dataFolder);
        }
    }
}
