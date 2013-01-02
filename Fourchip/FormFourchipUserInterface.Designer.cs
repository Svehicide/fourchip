namespace Fourchip
{
    partial class FormFourchipUserInterface
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
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.labelNICpc = new System.Windows.Forms.Label();
            this.comboBoxNIC = new System.Windows.Forms.ComboBox();
            this.labelMask = new System.Windows.Forms.Label();
            this.labelGW = new System.Windows.Forms.Label();
            this.labelDNS1 = new System.Windows.Forms.Label();
            this.labelDNS2 = new System.Windows.Forms.Label();
            this.buttonDisconnected = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonUpdateNIC = new System.Windows.Forms.Button();
            this.labelAddress = new System.Windows.Forms.Label();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBoxTemp = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxBrightness = new System.Windows.Forms.GroupBox();
            this.ipAddressControl1 = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl2 = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl3 = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl4 = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControl5 = new IPAddressControlLib.IPAddressControl();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxTemp.SuspendLayout();
            this.groupBoxBrightness.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelNICpc
            // 
            this.labelNICpc.AutoSize = true;
            this.labelNICpc.Location = new System.Drawing.Point(5, 19);
            this.labelNICpc.Name = "labelNICpc";
            this.labelNICpc.Size = new System.Drawing.Size(74, 13);
            this.labelNICpc.TabIndex = 2;
            this.labelNICpc.Text = "NIC selected :";
            // 
            // comboBoxNIC
            // 
            this.comboBoxNIC.FormattingEnabled = true;
            this.comboBoxNIC.Location = new System.Drawing.Point(98, 16);
            this.comboBoxNIC.Name = "comboBoxNIC";
            this.comboBoxNIC.Size = new System.Drawing.Size(174, 21);
            this.comboBoxNIC.TabIndex = 3;
            // 
            // labelMask
            // 
            this.labelMask.AutoSize = true;
            this.labelMask.Location = new System.Drawing.Point(6, 81);
            this.labelMask.Name = "labelMask";
            this.labelMask.Size = new System.Drawing.Size(81, 13);
            this.labelMask.TabIndex = 5;
            this.labelMask.Text = "Network mask :";
            // 
            // labelGW
            // 
            this.labelGW.AutoSize = true;
            this.labelGW.Location = new System.Drawing.Point(6, 107);
            this.labelGW.Name = "labelGW";
            this.labelGW.Size = new System.Drawing.Size(55, 13);
            this.labelGW.TabIndex = 6;
            this.labelGW.Text = "Gateway :";
            this.labelGW.Click += new System.EventHandler(this.labelGW_Click);
            // 
            // labelDNS1
            // 
            this.labelDNS1.AutoSize = true;
            this.labelDNS1.Location = new System.Drawing.Point(6, 133);
            this.labelDNS1.Name = "labelDNS1";
            this.labelDNS1.Size = new System.Drawing.Size(73, 13);
            this.labelDNS1.TabIndex = 7;
            this.labelDNS1.Text = "Primary DNS :";
            this.labelDNS1.Click += new System.EventHandler(this.labelDNS1_Click);
            // 
            // labelDNS2
            // 
            this.labelDNS2.AutoSize = true;
            this.labelDNS2.Location = new System.Drawing.Point(6, 159);
            this.labelDNS2.Name = "labelDNS2";
            this.labelDNS2.Size = new System.Drawing.Size(90, 13);
            this.labelDNS2.TabIndex = 8;
            this.labelDNS2.Text = "Secondary DNS :";
            this.labelDNS2.Click += new System.EventHandler(this.labelDNS2_Click);
            // 
            // buttonDisconnected
            // 
            this.buttonDisconnected.Location = new System.Drawing.Point(423, 12);
            this.buttonDisconnected.Name = "buttonDisconnected";
            this.buttonDisconnected.Size = new System.Drawing.Size(142, 23);
            this.buttonDisconnected.TabIndex = 9;
            this.buttonDisconnected.Text = "Disconnect";
            this.buttonDisconnected.UseVisualStyleBackColor = true;
            this.buttonDisconnected.Click += new System.EventHandler(this.buttonDisconnected_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ipAddressControl5);
            this.groupBox1.Controls.Add(this.ipAddressControl4);
            this.groupBox1.Controls.Add(this.ipAddressControl3);
            this.groupBox1.Controls.Add(this.ipAddressControl2);
            this.groupBox1.Controls.Add(this.ipAddressControl1);
            this.groupBox1.Controls.Add(this.buttonUpdateNIC);
            this.groupBox1.Controls.Add(this.labelAddress);
            this.groupBox1.Controls.Add(this.labelMask);
            this.groupBox1.Controls.Add(this.labelGW);
            this.groupBox1.Controls.Add(this.labelDNS1);
            this.groupBox1.Controls.Add(this.labelDNS2);
            this.groupBox1.Controls.Add(this.comboBoxNIC);
            this.groupBox1.Controls.Add(this.labelNICpc);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 218);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PC address";
            // 
            // buttonUpdateNIC
            // 
            this.buttonUpdateNIC.Location = new System.Drawing.Point(9, 189);
            this.buttonUpdateNIC.Name = "buttonUpdateNIC";
            this.buttonUpdateNIC.Size = new System.Drawing.Size(263, 23);
            this.buttonUpdateNIC.TabIndex = 20;
            this.buttonUpdateNIC.Text = "Update";
            this.buttonUpdateNIC.UseVisualStyleBackColor = true;
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(6, 55);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(63, 13);
            this.labelAddress.TabIndex = 4;
            this.labelAddress.Text = "IP address :";
            this.labelAddress.Click += new System.EventHandler(this.labelAddress_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(105, 17);
            this.toolStripStatusLabel1.Text = "Connection time : ";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 559);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(577, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBoxTemp
            // 
            this.groupBoxTemp.Controls.Add(this.pictureBox2);
            this.groupBoxTemp.Controls.Add(this.pictureBox1);
            this.groupBoxTemp.Controls.Add(this.label1);
            this.groupBoxTemp.Location = new System.Drawing.Point(319, 51);
            this.groupBoxTemp.Name = "groupBoxTemp";
            this.groupBoxTemp.Size = new System.Drawing.Size(113, 179);
            this.groupBoxTemp.TabIndex = 15;
            this.groupBoxTemp.TabStop = false;
            this.groupBoxTemp.Text = "Temperature";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Temp °C";
            // 
            // groupBoxBrightness
            // 
            this.groupBoxBrightness.Controls.Add(this.pictureBox3);
            this.groupBoxBrightness.Location = new System.Drawing.Point(452, 51);
            this.groupBoxBrightness.Name = "groupBoxBrightness";
            this.groupBoxBrightness.Size = new System.Drawing.Size(113, 179);
            this.groupBoxBrightness.TabIndex = 16;
            this.groupBoxBrightness.TabStop = false;
            this.groupBoxBrightness.Text = "Brightness";
            // 
            // ipAddressControl1
            // 
            this.ipAddressControl1.AllowInternalTab = false;
            this.ipAddressControl1.AutoHeight = true;
            this.ipAddressControl1.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl1.Location = new System.Drawing.Point(98, 52);
            this.ipAddressControl1.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressControl1.Name = "ipAddressControl1";
            this.ipAddressControl1.ReadOnly = false;
            this.ipAddressControl1.Size = new System.Drawing.Size(174, 20);
            this.ipAddressControl1.TabIndex = 21;
            this.ipAddressControl1.Text = "...";
            // 
            // ipAddressControl2
            // 
            this.ipAddressControl2.AllowInternalTab = false;
            this.ipAddressControl2.AutoHeight = true;
            this.ipAddressControl2.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl2.Location = new System.Drawing.Point(98, 78);
            this.ipAddressControl2.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressControl2.Name = "ipAddressControl2";
            this.ipAddressControl2.ReadOnly = false;
            this.ipAddressControl2.Size = new System.Drawing.Size(174, 20);
            this.ipAddressControl2.TabIndex = 22;
            this.ipAddressControl2.Text = "...";
            // 
            // ipAddressControl3
            // 
            this.ipAddressControl3.AllowInternalTab = false;
            this.ipAddressControl3.AutoHeight = true;
            this.ipAddressControl3.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl3.Location = new System.Drawing.Point(98, 104);
            this.ipAddressControl3.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressControl3.Name = "ipAddressControl3";
            this.ipAddressControl3.ReadOnly = false;
            this.ipAddressControl3.Size = new System.Drawing.Size(174, 20);
            this.ipAddressControl3.TabIndex = 23;
            this.ipAddressControl3.Text = "...";
            // 
            // ipAddressControl4
            // 
            this.ipAddressControl4.AllowInternalTab = false;
            this.ipAddressControl4.AutoHeight = true;
            this.ipAddressControl4.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl4.Location = new System.Drawing.Point(98, 130);
            this.ipAddressControl4.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressControl4.Name = "ipAddressControl4";
            this.ipAddressControl4.ReadOnly = false;
            this.ipAddressControl4.Size = new System.Drawing.Size(174, 20);
            this.ipAddressControl4.TabIndex = 24;
            this.ipAddressControl4.Text = "...";
            // 
            // ipAddressControl5
            // 
            this.ipAddressControl5.AllowInternalTab = false;
            this.ipAddressControl5.AutoHeight = true;
            this.ipAddressControl5.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl5.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl5.Location = new System.Drawing.Point(98, 156);
            this.ipAddressControl5.MinimumSize = new System.Drawing.Size(87, 20);
            this.ipAddressControl5.Name = "ipAddressControl5";
            this.ipAddressControl5.ReadOnly = false;
            this.ipAddressControl5.Size = new System.Drawing.Size(174, 20);
            this.ipAddressControl5.TabIndex = 25;
            this.ipAddressControl5.Text = "...";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(6, 42);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 100);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(6, 111);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 50);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // FormFourchipUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(577, 581);
            this.Controls.Add(this.groupBoxBrightness);
            this.Controls.Add(this.groupBoxTemp);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonDisconnected);
            this.MaximizeBox = false;
            this.Name = "FormFourchipUserInterface";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Fourchip project";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFourchipUserInterface_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxTemp.ResumeLayout(false);
            this.groupBoxTemp.PerformLayout();
            this.groupBoxBrightness.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Label labelNICpc;
        private System.Windows.Forms.ComboBox comboBoxNIC;
        private System.Windows.Forms.Label labelMask;
        private System.Windows.Forms.Label labelGW;
        private System.Windows.Forms.Label labelDNS1;
        private System.Windows.Forms.Label labelDNS2;
        private System.Windows.Forms.Button buttonDisconnected;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Button buttonUpdateNIC;
        private System.Windows.Forms.GroupBox groupBoxTemp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxBrightness;
        private IPAddressControlLib.IPAddressControl ipAddressControl5;
        private IPAddressControlLib.IPAddressControl ipAddressControl4;
        private IPAddressControlLib.IPAddressControl ipAddressControl3;
        private IPAddressControlLib.IPAddressControl ipAddressControl2;
        private IPAddressControlLib.IPAddressControl ipAddressControl1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}