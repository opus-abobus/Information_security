using Emgu.CV.Face;
//using FaceRecognition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiometryWinFormApp {
    public partial class FaceRecognitionViaCameraForm : Form {
        //private FaceRec _faceRec;

        public FaceRecognitionViaCameraForm() {
            InitializeComponent();

            //_faceRec = new FaceRec();
        }

        private void FaceRecognitionViaCamera_Load(object sender, EventArgs e) {

        }

        private void buttonOpenCam_Click(object sender, EventArgs e) {
            //_faceRec.openCamera(pictureBox1, pictureBox2);
        }

        private void buttonSaveImage_Click(object sender, EventArgs e) {

        }

        private void buttonDetectFaces_Click(object sender, EventArgs e) {

        }
    }
}
