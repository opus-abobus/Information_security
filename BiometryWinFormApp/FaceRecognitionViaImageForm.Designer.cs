namespace BiometryWinFormApp {
    partial class FaceRecognitionViaImageForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            pictureBox1 = new PictureBox();
            buttonLoadImage = new Button();
            openFileDialog1 = new OpenFileDialog();
            comboBoxClassifFace = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            comboBoxClassifEye = new ComboBox();
            label3 = new Label();
            comboBoxClassifMouth = new ComboBox();
            buttonDetect = new Button();
            numericUpDownCFS = new NumericUpDown();
            numericUpDownCES = new NumericUpDown();
            numericUpDownCMS = new NumericUpDown();
            numericUpDownCFN = new NumericUpDown();
            numericUpDownCMN = new NumericUpDown();
            numericUpDownCEN = new NumericUpDown();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            groupBoxDetectParams = new GroupBox();
            checkBoxTrain = new CheckBox();
            buttonRecognize = new Button();
            tabControl1 = new TabControl();
            tabPageDetect = new TabPage();
            tabPageRecognize = new TabPage();
            groupBoxRecParams = new GroupBox();
            label10 = new Label();
            comboBoxRecType = new ComboBox();
            buttonOpenDataFolder = new Button();
            ((System.ComponentModel.ISupportInitialize) pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize) numericUpDownCFS).BeginInit();
            ((System.ComponentModel.ISupportInitialize) numericUpDownCES).BeginInit();
            ((System.ComponentModel.ISupportInitialize) numericUpDownCMS).BeginInit();
            ((System.ComponentModel.ISupportInitialize) numericUpDownCFN).BeginInit();
            ((System.ComponentModel.ISupportInitialize) numericUpDownCMN).BeginInit();
            ((System.ComponentModel.ISupportInitialize) numericUpDownCEN).BeginInit();
            groupBoxDetectParams.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageDetect.SuspendLayout();
            tabPageRecognize.SuspendLayout();
            groupBoxRecParams.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.InactiveCaption;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1285, 927);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // buttonLoadImage
            // 
            buttonLoadImage.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point,  204);
            buttonLoadImage.Location = new Point(1379, 867);
            buttonLoadImage.Name = "buttonLoadImage";
            buttonLoadImage.Size = new Size(268, 72);
            buttonLoadImage.TabIndex = 1;
            buttonLoadImage.Text = "Выбрать изображение";
            buttonLoadImage.UseVisualStyleBackColor = true;
            buttonLoadImage.Click += buttonLoadImage_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // comboBoxClassifFace
            // 
            comboBoxClassifFace.FormattingEnabled = true;
            comboBoxClassifFace.Location = new Point(29, 70);
            comboBoxClassifFace.Name = "comboBoxClassifFace";
            comboBoxClassifFace.Size = new Size(352, 33);
            comboBoxClassifFace.TabIndex = 2;
            comboBoxClassifFace.SelectedIndexChanged += comboBoxClassifFace_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 42);
            label1.Name = "label1";
            label1.Size = new Size(247, 25);
            label1.TabIndex = 3;
            label1.Text = "Выбор классификатора лица";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 201);
            label2.Name = "label2";
            label2.Size = new Size(241, 25);
            label2.TabIndex = 4;
            label2.Text = "Выбор классификатора глаз";
            // 
            // comboBoxClassifEye
            // 
            comboBoxClassifEye.FormattingEnabled = true;
            comboBoxClassifEye.Location = new Point(29, 229);
            comboBoxClassifEye.Name = "comboBoxClassifEye";
            comboBoxClassifEye.Size = new Size(352, 33);
            comboBoxClassifEye.TabIndex = 5;
            comboBoxClassifEye.SelectedIndexChanged += comboBoxClassifEye_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 351);
            label3.Name = "label3";
            label3.Size = new Size(235, 25);
            label3.TabIndex = 6;
            label3.Text = "Выбор классификатора рта";
            // 
            // comboBoxClassifMouth
            // 
            comboBoxClassifMouth.FormattingEnabled = true;
            comboBoxClassifMouth.Location = new Point(29, 379);
            comboBoxClassifMouth.Name = "comboBoxClassifMouth";
            comboBoxClassifMouth.Size = new Size(352, 33);
            comboBoxClassifMouth.TabIndex = 7;
            comboBoxClassifMouth.SelectedIndexChanged += comboBoxClassifMouth_SelectedIndexChanged;
            // 
            // buttonDetect
            // 
            buttonDetect.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point,  204);
            buttonDetect.Location = new Point(44, 570);
            buttonDetect.Name = "buttonDetect";
            buttonDetect.Size = new Size(321, 97);
            buttonDetect.TabIndex = 8;
            buttonDetect.Text = "Обнаружить черты лица";
            buttonDetect.UseVisualStyleBackColor = true;
            buttonDetect.Click += buttonDetect_Click;
            // 
            // numericUpDownCFS
            // 
            numericUpDownCFS.DecimalPlaces = 2;
            numericUpDownCFS.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericUpDownCFS.Location = new Point(29, 147);
            numericUpDownCFS.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            numericUpDownCFS.Minimum = new decimal(new int[] { 11, 0, 0, 65536 });
            numericUpDownCFS.Name = "numericUpDownCFS";
            numericUpDownCFS.Size = new Size(79, 31);
            numericUpDownCFS.TabIndex = 9;
            numericUpDownCFS.Value = new decimal(new int[] { 11, 0, 0, 65536 });
            // 
            // numericUpDownCES
            // 
            numericUpDownCES.DecimalPlaces = 2;
            numericUpDownCES.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericUpDownCES.Location = new Point(29, 293);
            numericUpDownCES.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            numericUpDownCES.Minimum = new decimal(new int[] { 11, 0, 0, 65536 });
            numericUpDownCES.Name = "numericUpDownCES";
            numericUpDownCES.Size = new Size(79, 31);
            numericUpDownCES.TabIndex = 10;
            numericUpDownCES.Value = new decimal(new int[] { 11, 0, 0, 65536 });
            // 
            // numericUpDownCMS
            // 
            numericUpDownCMS.DecimalPlaces = 2;
            numericUpDownCMS.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericUpDownCMS.Location = new Point(29, 443);
            numericUpDownCMS.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            numericUpDownCMS.Minimum = new decimal(new int[] { 11, 0, 0, 65536 });
            numericUpDownCMS.Name = "numericUpDownCMS";
            numericUpDownCMS.Size = new Size(79, 31);
            numericUpDownCMS.TabIndex = 11;
            numericUpDownCMS.Value = new decimal(new int[] { 11, 0, 0, 65536 });
            // 
            // numericUpDownCFN
            // 
            numericUpDownCFN.Location = new Point(280, 147);
            numericUpDownCFN.Name = "numericUpDownCFN";
            numericUpDownCFN.Size = new Size(101, 31);
            numericUpDownCFN.TabIndex = 12;
            numericUpDownCFN.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // numericUpDownCMN
            // 
            numericUpDownCMN.Location = new Point(280, 443);
            numericUpDownCMN.Name = "numericUpDownCMN";
            numericUpDownCMN.Size = new Size(101, 31);
            numericUpDownCMN.TabIndex = 13;
            numericUpDownCMN.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // numericUpDownCEN
            // 
            numericUpDownCEN.Location = new Point(280, 293);
            numericUpDownCEN.Name = "numericUpDownCEN";
            numericUpDownCEN.Size = new Size(101, 31);
            numericUpDownCEN.TabIndex = 14;
            numericUpDownCEN.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(29, 119);
            label4.Name = "label4";
            label4.Size = new Size(103, 25);
            label4.TabIndex = 15;
            label4.Text = "Scale factor";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(29, 265);
            label5.Name = "label5";
            label5.Size = new Size(103, 25);
            label5.TabIndex = 16;
            label5.Text = "Scale factor";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(29, 415);
            label6.Name = "label6";
            label6.Size = new Size(103, 25);
            label6.TabIndex = 17;
            label6.Text = "Scale factor";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(244, 119);
            label7.Name = "label7";
            label7.Size = new Size(137, 25);
            label7.TabIndex = 18;
            label7.Text = "Min neighbours";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(244, 265);
            label8.Name = "label8";
            label8.Size = new Size(137, 25);
            label8.TabIndex = 19;
            label8.Text = "Min neighbours";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(244, 415);
            label9.Name = "label9";
            label9.Size = new Size(137, 25);
            label9.TabIndex = 20;
            label9.Text = "Min neighbours";
            // 
            // groupBoxDetectParams
            // 
            groupBoxDetectParams.Controls.Add(checkBoxTrain);
            groupBoxDetectParams.Controls.Add(label1);
            groupBoxDetectParams.Controls.Add(label9);
            groupBoxDetectParams.Controls.Add(comboBoxClassifFace);
            groupBoxDetectParams.Controls.Add(label8);
            groupBoxDetectParams.Controls.Add(label7);
            groupBoxDetectParams.Controls.Add(label6);
            groupBoxDetectParams.Controls.Add(numericUpDownCFS);
            groupBoxDetectParams.Controls.Add(label5);
            groupBoxDetectParams.Controls.Add(numericUpDownCFN);
            groupBoxDetectParams.Controls.Add(numericUpDownCEN);
            groupBoxDetectParams.Controls.Add(label4);
            groupBoxDetectParams.Controls.Add(numericUpDownCMN);
            groupBoxDetectParams.Controls.Add(label2);
            groupBoxDetectParams.Controls.Add(numericUpDownCMS);
            groupBoxDetectParams.Controls.Add(comboBoxClassifEye);
            groupBoxDetectParams.Controls.Add(numericUpDownCES);
            groupBoxDetectParams.Controls.Add(label3);
            groupBoxDetectParams.Controls.Add(comboBoxClassifMouth);
            groupBoxDetectParams.Location = new Point(6, 6);
            groupBoxDetectParams.Name = "groupBoxDetectParams";
            groupBoxDetectParams.Size = new Size(387, 547);
            groupBoxDetectParams.TabIndex = 21;
            groupBoxDetectParams.TabStop = false;
            groupBoxDetectParams.Text = "Параметры обнаружения";
            // 
            // checkBoxTrain
            // 
            checkBoxTrain.AutoSize = true;
            checkBoxTrain.Enabled = false;
            checkBoxTrain.Location = new Point(29, 512);
            checkBoxTrain.Name = "checkBoxTrain";
            checkBoxTrain.Size = new Size(284, 29);
            checkBoxTrain.TabIndex = 21;
            checkBoxTrain.Text = "Сохранить лица для обучения";
            checkBoxTrain.UseVisualStyleBackColor = true;
            checkBoxTrain.CheckedChanged += checkBoxTrain_CheckedChanged;
            // 
            // buttonRecognize
            // 
            buttonRecognize.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point,  204);
            buttonRecognize.Location = new Point(81, 469);
            buttonRecognize.Name = "buttonRecognize";
            buttonRecognize.Size = new Size(257, 89);
            buttonRecognize.TabIndex = 22;
            buttonRecognize.Text = "Обнаружить и распознать лица";
            buttonRecognize.UseVisualStyleBackColor = true;
            buttonRecognize.Click += buttonRecognize_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageDetect);
            tabControl1.Controls.Add(tabPageRecognize);
            tabControl1.Location = new Point(1305, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.RightToLeft = RightToLeft.No;
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(411, 727);
            tabControl1.TabIndex = 23;
            // 
            // tabPageDetect
            // 
            tabPageDetect.Controls.Add(groupBoxDetectParams);
            tabPageDetect.Controls.Add(buttonDetect);
            tabPageDetect.Location = new Point(4, 34);
            tabPageDetect.Name = "tabPageDetect";
            tabPageDetect.Padding = new Padding(3);
            tabPageDetect.Size = new Size(403, 689);
            tabPageDetect.TabIndex = 0;
            tabPageDetect.Text = "Обнаружение";
            tabPageDetect.UseVisualStyleBackColor = true;
            // 
            // tabPageRecognize
            // 
            tabPageRecognize.Controls.Add(groupBoxRecParams);
            tabPageRecognize.Controls.Add(buttonRecognize);
            tabPageRecognize.Location = new Point(4, 34);
            tabPageRecognize.Name = "tabPageRecognize";
            tabPageRecognize.Padding = new Padding(3);
            tabPageRecognize.Size = new Size(403, 689);
            tabPageRecognize.TabIndex = 1;
            tabPageRecognize.Text = "Распознавание";
            tabPageRecognize.UseVisualStyleBackColor = true;
            // 
            // groupBoxRecParams
            // 
            groupBoxRecParams.Controls.Add(label10);
            groupBoxRecParams.Controls.Add(comboBoxRecType);
            groupBoxRecParams.Location = new Point(20, 19);
            groupBoxRecParams.Name = "groupBoxRecParams";
            groupBoxRecParams.Size = new Size(362, 288);
            groupBoxRecParams.TabIndex = 24;
            groupBoxRecParams.TabStop = false;
            groupBoxRecParams.Text = "Параметры распознавания";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(15, 49);
            label10.Name = "label10";
            label10.Size = new Size(260, 25);
            label10.TabIndex = 24;
            label10.Text = "Выбор метода распознавания";
            // 
            // comboBoxRecType
            // 
            comboBoxRecType.FormattingEnabled = true;
            comboBoxRecType.Location = new Point(15, 77);
            comboBoxRecType.Name = "comboBoxRecType";
            comboBoxRecType.Size = new Size(341, 33);
            comboBoxRecType.TabIndex = 23;
            comboBoxRecType.SelectedIndexChanged += comboBoxRecType_SelectedIndexChanged;
            // 
            // buttonOpenDataFolder
            // 
            buttonOpenDataFolder.Location = new Point(1785, 871);
            buttonOpenDataFolder.Name = "buttonOpenDataFolder";
            buttonOpenDataFolder.Size = new Size(147, 66);
            buttonOpenDataFolder.TabIndex = 24;
            buttonOpenDataFolder.Text = "Открыть папку с данными";
            buttonOpenDataFolder.UseVisualStyleBackColor = true;
            buttonOpenDataFolder.Click += buttonOpenDataFolder_Click;
            // 
            // FaceRecognitionViaImageForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1944, 951);
            Controls.Add(buttonOpenDataFolder);
            Controls.Add(pictureBox1);
            Controls.Add(tabControl1);
            Controls.Add(buttonLoadImage);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FaceRecognitionViaImageForm";
            Text = "Обнаружение лица";
            FormClosed += FaceRecognitionViaImageForm_FormClosed;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize) pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize) numericUpDownCFS).EndInit();
            ((System.ComponentModel.ISupportInitialize) numericUpDownCES).EndInit();
            ((System.ComponentModel.ISupportInitialize) numericUpDownCMS).EndInit();
            ((System.ComponentModel.ISupportInitialize) numericUpDownCFN).EndInit();
            ((System.ComponentModel.ISupportInitialize) numericUpDownCMN).EndInit();
            ((System.ComponentModel.ISupportInitialize) numericUpDownCEN).EndInit();
            groupBoxDetectParams.ResumeLayout(false);
            groupBoxDetectParams.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPageDetect.ResumeLayout(false);
            tabPageRecognize.ResumeLayout(false);
            groupBoxRecParams.ResumeLayout(false);
            groupBoxRecParams.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button buttonLoadImage;
        private OpenFileDialog openFileDialog1;
        private ComboBox comboBoxClassifFace;
        private Label label1;
        private Label label2;
        private ComboBox comboBoxClassifEye;
        private Label label3;
        private ComboBox comboBoxClassifMouth;
        private Button buttonDetect;
        private NumericUpDown numericUpDownCFS;
        private NumericUpDown numericUpDownCES;
        private NumericUpDown numericUpDownCMS;
        private NumericUpDown numericUpDownCFN;
        private NumericUpDown numericUpDownCMN;
        private NumericUpDown numericUpDownCEN;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private GroupBox groupBoxDetectParams;
        private Button buttonRecognize;
        private TabControl tabControl1;
        private TabPage tabPageDetect;
        private TabPage tabPageRecognize;
        private CheckBox checkBoxTrain;
        private Label label10;
        private ComboBox comboBoxRecType;
        private GroupBox groupBoxRecParams;
        private Button buttonOpenDataFolder;
    }
}
