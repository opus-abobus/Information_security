namespace BiometryWinFormApp {
    partial class StartForm {
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
            buttonImageRec = new Button();
            buttonCamRec = new Button();
            buttonOpenDataFolder = new Button();
            SuspendLayout();
            // 
            // buttonImageRec
            // 
            buttonImageRec.BackColor = SystemColors.Info;
            buttonImageRec.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point,  204);
            buttonImageRec.Location = new Point(141, 181);
            buttonImageRec.Name = "buttonImageRec";
            buttonImageRec.Size = new Size(432, 86);
            buttonImageRec.TabIndex = 0;
            buttonImageRec.Text = "Распознавание с изображения";
            buttonImageRec.UseVisualStyleBackColor = false;
            buttonImageRec.Click += buttonImageRec_Click;
            // 
            // buttonCamRec
            // 
            buttonCamRec.BackColor = SystemColors.InactiveCaption;
            buttonCamRec.Enabled = false;
            buttonCamRec.Location = new Point(259, 605);
            buttonCamRec.Name = "buttonCamRec";
            buttonCamRec.Size = new Size(195, 72);
            buttonCamRec.TabIndex = 1;
            buttonCamRec.Text = "Распознавание с веб-камеры";
            buttonCamRec.UseVisualStyleBackColor = false;
            buttonCamRec.Click += buttonCamRec_Click;
            // 
            // buttonOpenDataFolder
            // 
            buttonOpenDataFolder.Location = new Point(12, 12);
            buttonOpenDataFolder.Name = "buttonOpenDataFolder";
            buttonOpenDataFolder.Size = new Size(147, 66);
            buttonOpenDataFolder.TabIndex = 25;
            buttonOpenDataFolder.Text = "Открыть папку с данными";
            buttonOpenDataFolder.UseVisualStyleBackColor = true;
            buttonOpenDataFolder.Click += buttonOpenDataFolder_Click;
            // 
            // StartForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(733, 689);
            Controls.Add(buttonOpenDataFolder);
            Controls.Add(buttonCamRec);
            Controls.Add(buttonImageRec);
            Name = "StartForm";
            Text = "Выбор метода распознавания";
            Load += StartForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button buttonImageRec;
        private Button buttonCamRec;
        private Button buttonOpenDataFolder;
    }
}