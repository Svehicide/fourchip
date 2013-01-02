﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Fourchip
{ 
    public partial class FormFourchipScan : Form
    {
        //
        //Form initialization
        //
        public FormFourchipScan()
        {
            InitializeComponent();

            //Filling ports tab with COM ports available on PC
            String[] ports = System.IO.Ports.SerialPort.GetPortNames();

            //Setting the first correct value for COM port.
            //If no COM Port is created on the PC, the application will exit.
            try
            {
                serialPort.PortName = String.Copy(ports[0]);
            }
            catch
            {
               System.Windows.Forms.MessageBox.Show("Aucun port COM disponible.\n\nVeuillez vérifier que votre carte est bien reconnue par votre ordinateur");
               this.Close();
            }
            
            //Adding tab informations in a ComboBox
            foreach (string port in ports)
            {
                comboBoxCOMPorts.Items.Add(port);
            }

            //BaudRate combobox is disabled because no COM port is selected
            comboBoxBaudRate.Enabled = false;

            //allowing cross-threads operations
            CheckForIllegalCrossThreadCalls = false;
        }

        //
        //Setting the port name to the value selected in the ComboBox
        //
        private void comboBoxCOMPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            //closing the serialport connection to allow modifications
            if (serialPort.IsOpen == true)
            {
                serialPort.Close();
            }

            //setting the port to the selected item in the combobox
            serialPort.PortName = (String)comboBoxCOMPorts.SelectedItem;

            //Handling exception if the port is used by something else
            try
            {
                comboBoxBaudRate.Enabled = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        //
        //Setting the baud rate to the value select in the ComboBox
        //
        private void comboBoxBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == true)
            {
                serialPort.Close();
            }

            serialPort.BaudRate = int.Parse((String)comboBoxBaudRate.SelectedItem);
            
            try
            {
                serialPort.Open();
                //Sending a connection request to the card
                serialPort.Write(Rs232_string.HELLO);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Votre port COM ("+serialPort.PortName+") semble être utilisé. Veuillez vérifier qu'il est libre.");
            }
            
            
        }

        //
        //Handling type of datas received on serial port
        //
        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                String data = serialPort.ReadLine();

                // code received is 01 ( Scan the card )
                if (String.Compare(data.Substring(0, 4), Rs232_string.SCAN) == 0)
                {
                    //removing the code part
                    String infoString = data.Substring(4);
                    //splitting the informations into individual strings
                    String[] infos = infoString.Split(';');

                    //generating a new window
                    serialPort.Close();
                    FormFourchipLogin formFourchipLogin = new FormFourchipLogin(infos[0], infos[1], this, serialPort.PortName, serialPort.BaudRate.ToString());
                    this.Hide();
                    formFourchipLogin.ShowDialog();
                }
            }
            catch
            {
            }
        }

        public SerialPort getserialPort { get; set; }
    
    }   
}

