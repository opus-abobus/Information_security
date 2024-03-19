namespace BiometryWinFormApp {
    partial class FaceRecognitionViaCameraForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            buttonOpenCam = new Button();
            buttonSaveImage = new Button();
            buttonDetectFaces = new Button();
            ((System.ComponentModel.ISupportInitialize) pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize) pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 37);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(643, 547);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(681, 344);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(286, 240);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point,  204);
            label1.Location = new Point(12, 2);
            label1.Name = "label1";
            label1.Size = new Size(107, 32);
            label1.TabIndex = 2;
            label1.Text = "CAMERA";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point,  204);
            label2.Location = new Point(681, 309);
            label2.Name = "label2";
            label2.Size = new Size(129, 32);
            label2.TabIndex = 3;
            label2.Text = "CAPTURED";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point,  204);
            label3.Location = new Point(593, 587);
            label3.Name = "label3";
            label3.Size = new Size(81, 32);
            label3.TabIndex = 4;
            label3.Text = "NAME";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point,  204);
            textBox1.Location = new Point(681, 587);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(286, 39);
            textBox1.TabIndex = 5;
            // 
            // buttonOpenCam
            // 
            buttonOpenCam.Location = new Point(1091, 37);
            buttonOpenCam.Name = "buttonOpenCam";
            buttonOpenCam.Size = new Size(216, 75);
            buttonOpenCam.TabIndex = 6;
            buttonOpenCam.Text = "OPEN CAMERA";
            buttonOpenCam.UseVisualStyleBackColor = true;
            buttonOpenCam.Click += buttonOpenCam_Click;
            // 
            // buttonSaveImage
            // 
            buttonSaveImage.Location = new Point(1091, 234);
            buttonSaveImage.Name = "buttonSaveImage";
            buttonSaveImage.Size = new Size(216, 75);
            buttonSaveImage.TabIndex = 7;
            buttonSaveImage.Text = "SAVE IMAGE";
            buttonSaveImage.UseVisualStyleBackColor = true;
            buttonSaveImage.Click += buttonSaveImage_Click;
            // 
            // buttonDetectFaces
            // 
            buttonDetectFaces.Location = new Point(1091, 435);
            buttonDetectFaces.Name = "buttonDetectFaces";
            buttonDetectFaces.Size = new Size(216, 75);
            buttonDetectFaces.TabIndex = 8;
            buttonDetectFaces.Text = "DETECT FACES";
            buttonDetectFaces.UseVisualStyleBackColor = true;
            buttonDetectFaces.Click += buttonDetectFaces_Click;
            // 
            // FaceRecognitionViaCamera
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1319, 800);
            Controls.Add(buttonDetectFaces);
            Controls.Add(buttonSaveImage);
            Controls.Add(buttonOpenCam);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "FaceRecognitionViaCamera";
            Text = "FaceRecognitionViaCamera";
            Load += FaceRecognitionViaCamera_Load;
            ((System.ComponentModel.ISupportInitialize) pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize) pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private Button buttonOpenCam;
        private Button buttonSaveImage;
        private Button buttonDetectFaces;
    }
}