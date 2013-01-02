namespace Fourchip
{
    partial class FormFourchipScan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFourchipScan));
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.labelScanRFID = new System.Windows.Forms.Label();
            this.comboBoxCOMPorts = new System.Windows.Forms.ComboBox();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.labelBPS = new System.Windows.Forms.Label();
            this.pictureBoxRFID = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRFID)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 1;
            this.serialPort.ReadBufferSize = 16384;
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // labelScanRFID
            // 
            resources.ApplyResources(this.labelScanRFID, "labelScanRFID");
            this.labelScanRFID.Name = "labelScanRFID";
            // 
            // comboBoxCOMPorts
            // 
            this.comboBoxCOMPorts.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxCOMPorts, "comboBoxCOMPorts");
            this.comboBoxCOMPorts.Name = "comboBoxCOMPorts";
            this.comboBoxCOMPorts.SelectedIndexChanged += new System.EventHandler(this.comboBoxCOMPorts_SelectedIndexChanged);
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.FormattingEnabled = true;
            this.comboBoxBaudRate.Items.AddRange(new object[] {
            resources.GetString("comboBoxBaudRate.Items"),
            resources.GetString("comboBoxBaudRate.Items1"),
            resources.GetString("comboBoxBaudRate.Items2"),
            resources.GetString("comboBoxBaudRate.Items3"),
            resources.GetString("comboBoxBaudRate.Items4")});
            resources.ApplyResources(this.comboBoxBaudRate, "comboBoxBaudRate");
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.SelectedIndexChanged += new System.EventHandler(this.comboBoxBaudRate_SelectedIndexChanged);
            // 
            // labelBPS
            // 
            resources.ApplyResources(this.labelBPS, "labelBPS");
            this.labelBPS.Name = "labelBPS";
            // 
            // pictureBoxRFID
            // 
            this.pictureBoxRFID.Image = global::Fourchip.Properties.Resources.universal_contactless_card_symbol;
            resources.ApplyResources(this.pictureBoxRFID, "pictureBoxRFID");
            this.pictureBoxRFID.Name = "pictureBoxRFID";
            this.pictureBoxRFID.TabStop = false;
            // 
            // FormFourchipScan
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelBPS);
            this.Controls.Add(this.comboBoxBaudRate);
            this.Controls.Add(this.comboBoxCOMPorts);
            this.Controls.Add(this.labelScanRFID);
            this.Controls.Add(this.pictureBoxRFID);
            this.MaximizeBox = false;
            this.Name = "FormFourchipScan";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRFID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.PictureBox pictureBoxRFID;
        private System.Windows.Forms.Label labelScanRFID;
        private System.Windows.Forms.ComboBox comboBoxCOMPorts;
        private System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.Label labelBPS;

    }
}

