using System;
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
    public partial class FormFourchipLogin : Form
    {
        Form owner;
        int tryCount = 0;

        //
        //Form initialization
        //
        public FormFourchipLogin(String p, String n, Form o, String com, String br)
        {
            InitializeComponent();

            owner = o;

            //configuring the serial communication with previously selected parameters
            serialPort.PortName = com;
            serialPort.BaudRate = int.Parse(br);
            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            
            //welcome message
            labelWelcome.Text = "Bonjour " + p + " " + n + "\nVeuillez insérer votre mot de passe via les boutons poussoirs et terminer par la touche CENTER";
        }

        //
        //Restarting the application if the form is closed
        //
        private void FormFourchipLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Application.Restart(); 
        }

        //
        //Handling type of datas received on serial port
        //
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try{
                String data = serialPort.ReadLine();

                if( String.Compare(data.Substring(0, 4), "#02@") ==0){
                    if (String.Compare(data.Substring(4), "TRUE") == 0)
                    {
                        //generating a new window
                        serialPort.Close();
                        FormFourchipUserInterface formFourchipUserInterface = new FormFourchipUserInterface(owner);
                        formFourchipUserInterface.ShowDialog();
                    }
                    else
                    {
                        tryCount++;

                        if (tryCount == 3)
                        {
                            System.Windows.Forms.MessageBox.Show("Mot de passe incorrect.\n\nFin de la connexion");
                            this.Dispose();
                            Application.Exit(); 
                        }
                        else
                        {
                            if (tryCount < 3)
                            {
                                System.Windows.Forms.MessageBox.Show("Mot de passe incorrect.\n\nTentative de connexion : "+tryCount);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }


    }
}
