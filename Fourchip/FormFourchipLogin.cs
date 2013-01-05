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
        FormFourchipUserInterface formFourchipUserInterface;

        //
        //Form initialization
        //
        public FormFourchipLogin(String identity, Form o, String com, String br)
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
            labelWelcome.Text = "Hello " +identity+ "\nPlease enter your password with the card's keyboard and finish by pressing CENTER";
        }

        //
        //Restarting the application if the form is closed
        //
        private void FormFourchipLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formFourchipUserInterface != null)
            {
                //The UserInterface is visible and the Login window has to be closed
                this.Dispose();
            }
            else
            {
                if (serialPort.IsOpen == true)
                {
                    serialPort.Write("#99@");
                }
                //The UserInterface is not visible
                this.Dispose();
                Application.Restart();
            }
             
        }

        //
        //Handling type of datas received on serial port
        //
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try{
                String data = serialPort.ReadLine();

                Console.WriteLine(data);
                // code received is 02 ( Enter the password )
                if( String.Compare(data.Substring(0, 4), "#02@") ==0)
                {
                    if (String.Compare(data.Substring(4), "1") == 0)
                    {
                        //generating a new window
                        serialPort.Close();
                        formFourchipUserInterface = new FormFourchipUserInterface(owner,serialPort.PortName,serialPort.BaudRate.ToString());
                        this.Hide();
                        formFourchipUserInterface.ShowDialog();
                    }
                    else
                    {
                        if (String.Compare(data.Substring(4), "0") == 0)
                        {
                            //The user has 3 try to enter the right password
                            tryCount++;

                            if (tryCount == 3)
                            {
                                //After 3 retries, the application is restarted
                                System.Windows.Forms.MessageBox.Show("Incorrect Password.\n\nEnd of connection");
                                this.Dispose();
                                Application.Exit();
                            }
                            else
                            {
                                if (tryCount < 3)
                                {
                                    //If the password is not correct, the user will be prompted
                                    System.Windows.Forms.MessageBox.Show("Incorrect Password.\n\nConnection attemp : " + tryCount);
                                }
                            }
                        }
                        else
                        {
                            if (String.Compare(data.Substring(4), "2") == 0)
                            {
                                //The user has take too much time to enter his password.
                                System.Windows.Forms.MessageBox.Show("Time out.\nPlease enter your password faster next time");
                                this.Dispose();
                                Application.Exit();
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
