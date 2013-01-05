using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using System.Management;
using Microsoft.VisualBasic;
using System.Windows.Forms.DataVisualization.Charting;

namespace Fourchip
{
    public partial class FormFourchipUserInterface : Form
    {
        Form owner;

        double[] tempChart = new double[15];
        double[] brightChart = new double[15];

        int tabTempIndex = 0;
        int tabBrightIndex = 0;

        int j = 0;

        //
        //Form initialization
        //
        public FormFourchipUserInterface(Form o,String com, String br)
        {
            InitializeComponent();

            //saving the toplevel form
            owner = o;

            //filling the combobox with NIC names
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                comboBoxNIC.Items.Add(nic.Name);
            }

            //Setting the correct value for COM port.
            //If no COM Port is created on the PC, the application will exit.
            try
            {
                serialPort.PortName = com;
                serialPort.BaudRate = int.Parse(br);
                serialPort.Open();
            }
            catch ( Exception ex )
            {
               System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            //setting default values on Form creation
            labelTempValue.Text = "0";
            pictureBoxBrightness.Image = Fourchip.Properties.Resources.moon;
            //By default, no NIC is selected, there is no need to modify the IP on an "non-existant" NIC
            textBoxIPbyte1.Enabled = false;
            textBoxIPbyte2.Enabled = false;
            textBoxIPbyte3.Enabled = false;
            textBoxIPbyte4.Enabled = false;

            textBoxMASKbyte1.Enabled = false;
            textBoxMASKbyte2.Enabled = false;
            textBoxMASKbyte3.Enabled = false;
            textBoxMASKbyte4.Enabled = false;

            textBoxGWbyte1.Enabled = false;
            textBoxGWbyte2.Enabled = false;
            textBoxGWbyte3.Enabled = false;
            textBoxGWbyte4.Enabled = false;

            textBoxDNS1byte1.Enabled = false;
            textBoxDNS1byte2.Enabled = false;
            textBoxDNS1byte3.Enabled = false;
            textBoxDNS1byte4.Enabled = false;

            textBoxDNS2byte1.Enabled = false;
            textBoxDNS2byte2.Enabled = false;
            textBoxDNS2byte3.Enabled = false;
            textBoxDNS2byte4.Enabled = false;
        }

        //
        //Performed action when the exit button is clicked.
        //
        private void FormFourchipUserInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
                if (serialPort.IsOpen == true)
                {
                serialPort.Write("#99@");
                }
                //destroying the form
                this.Dispose();
                //exiting the application
                Application.Exit();
        }

        //
        //Performed action when disconnected button is clicked.
        //
        private void buttonDisconnected_Click(object sender, EventArgs e)
        {
                //sending a "GOODBYE" signal to the card
                serialPort.Write("#99@");
                //destroying the form
                this.Dispose();
                //restarting the application to go back to the scan screen
                Application.Restart();
        }

        //
        //Performed action when the LED checkBox State is changed.
        //
        private void checkBoxLEDState_CheckedChanged(object sender, EventArgs e)
        {
            //The checkbox is checked
            if (checkBoxLEDstate.Checked == true)
            {
                //Enabling the numericUpDown
                frequencySpinner.Enabled = true;
                //Sending a "LED ON" signal
                serialPort.WriteLine(Rs232_string.LED + "ON\n");
            }
            //The checkbox is not checked
            else
            {
                //Disabling the numericUpDown
                frequencySpinner.Enabled = false;
                frequencySpinner.Value = 1;
                //Sending the default LED blinking value and a "LED OFF" signal
                serialPort.WriteLine(Rs232_string.LED + "OFF\n");
            }
        }

        //
        //Performing operations to modify the address textboxes in function of the selected NIC
        //
        private void comboBoxNIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            NetworkInterface nicSelected = null;
            String[] byteSplit;
            IPAddress[] dnsList = new IPAddress[2];
            int i = 0;

            textBoxIPbyte1.Enabled = true;
            textBoxIPbyte2.Enabled = true;
            textBoxIPbyte3.Enabled = true;
            textBoxIPbyte4.Enabled = true;

            textBoxMASKbyte1.Enabled = true;
            textBoxMASKbyte2.Enabled = true;
            textBoxMASKbyte3.Enabled = true;
            textBoxMASKbyte4.Enabled = true;

            //Getting the NIC list
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                //Searching the selected NIC in NIC list
                if (String.Compare(nic.Name, comboBoxNIC.SelectedItem.ToString()) == 0)
                {
                    nicSelected = nic;
                    //Getting the IP Address linked to the NIC
                    foreach (UnicastIPAddressInformation ip in nicSelected.GetIPProperties().UnicastAddresses)
                    {
                        //Unlocking all NIC options
                        if (ip.Address.ToString() != "::1" && ip.Address.ToString() != "127.0.0.1")
                        {
                            //Enabling textboxes
                            textBoxGWbyte1.Enabled = true;
                            textBoxGWbyte2.Enabled = true;
                            textBoxGWbyte3.Enabled = true;
                            textBoxGWbyte4.Enabled = true;

                            textBoxDNS1byte1.Enabled = true;
                            textBoxDNS1byte2.Enabled = true;
                            textBoxDNS1byte3.Enabled = true;
                            textBoxDNS1byte4.Enabled = true;

                            textBoxDNS2byte1.Enabled = true;
                            textBoxDNS2byte2.Enabled = true;
                            textBoxDNS2byte3.Enabled = true;
                            textBoxDNS2byte4.Enabled = true;

                            //Setting the IP Address
                            byteSplit = ip.Address.ToString().Split('.');

                            textBoxIPbyte1.Text = byteSplit[0];
                            textBoxIPbyte2.Text = byteSplit[1];
                            textBoxIPbyte3.Text = byteSplit[2];
                            textBoxIPbyte4.Text = byteSplit[3];

                            //Setting the Mask
                            byteSplit = ip.IPv4Mask.ToString().Split('.');

                            textBoxMASKbyte1.Text = byteSplit[0];
                            textBoxMASKbyte2.Text = byteSplit[1];
                            textBoxMASKbyte3.Text = byteSplit[2];
                            textBoxMASKbyte4.Text = byteSplit[3];

                            //Getting the gateway address linked to the NIC
                            foreach (GatewayIPAddressInformation gw in nicSelected.GetIPProperties().GatewayAddresses)
                            {
                                byteSplit = gw.Address.ToString().Split('.');

                                textBoxGWbyte1.Text = byteSplit[0];
                                textBoxGWbyte2.Text = byteSplit[1];
                                textBoxGWbyte3.Text = byteSplit[2];
                                textBoxGWbyte4.Text = byteSplit[3];
                            }

                            //Getting DNS addresses linked to the NIC
                            i = 0;
                            foreach (IPAddress dns in nicSelected.GetIPProperties().DnsAddresses)
                            {
                                dnsList[i] = dns;
                                i++;
                            }

                            //Verifying that there is no DNS registered
                            if (dnsList[0] == null)
                            {
                                //Setting the DNS addresses to 0.0.0.0
                                textBoxDNS1byte1.Text = "0";
                                textBoxDNS1byte2.Text = "0";
                                textBoxDNS1byte3.Text = "0";
                                textBoxDNS1byte4.Text = "0";

                                textBoxDNS2byte1.Text = "0";
                                textBoxDNS2byte2.Text = "0";
                                textBoxDNS2byte3.Text = "0";
                                textBoxDNS2byte4.Text = "0";
                            }
                            //The primary DNS is registered
                            else
                            {
                                //Setting the primary DNS address
                                byteSplit = dnsList[0].ToString().Split('.');

                                textBoxDNS1byte1.Text = byteSplit[0];
                                textBoxDNS1byte2.Text = byteSplit[1];
                                textBoxDNS1byte3.Text = byteSplit[2];
                                textBoxDNS1byte4.Text = byteSplit[3];

                                //The secondary DNS is registered
                                if (dnsList[1] != null)
                                {
                                    //Setting the secondary DNS address
                                    byteSplit = dnsList[1].ToString().Split('.');

                                    textBoxDNS2byte1.Text = byteSplit[0];
                                    textBoxDNS2byte2.Text = byteSplit[1];
                                    textBoxDNS2byte3.Text = byteSplit[2];
                                    textBoxDNS2byte4.Text = byteSplit[3];
                                }
                                //The secondary DNS is NOT registered
                                else
                                {
                                    //Setting the secondary DNS address to 0.0.0.0
                                    textBoxDNS2byte1.Text = "0";
                                    textBoxDNS2byte2.Text = "0";
                                    textBoxDNS2byte3.Text = "0";
                                    textBoxDNS2byte4.Text = "0";
                                }
                            }
                        }
                        //A local loopback doesn't need any DNS or gateway information, then, we lock it.
                        else
                        {
                            if (ip.Address.ToString() != "::1")
                            {
                                //Setting the IP Address
                                byteSplit = ip.Address.ToString().Split('.');

                                textBoxIPbyte1.Text = byteSplit[0];
                                textBoxIPbyte2.Text = byteSplit[1];
                                textBoxIPbyte3.Text = byteSplit[2];
                                textBoxIPbyte4.Text = byteSplit[3];

                                //Setting the Mask
                                byteSplit = ip.IPv4Mask.ToString().Split('.');

                                textBoxMASKbyte1.Text = byteSplit[0];
                                textBoxMASKbyte2.Text = byteSplit[1];
                                textBoxMASKbyte3.Text = byteSplit[2];
                                textBoxMASKbyte4.Text = byteSplit[3];

                                //Locking gateway information
                                textBoxGWbyte1.Enabled = false;
                                textBoxGWbyte2.Enabled = false;
                                textBoxGWbyte3.Enabled = false;
                                textBoxGWbyte4.Enabled = false;

                                textBoxGWbyte1.Text = "0";
                                textBoxGWbyte2.Text = "0";
                                textBoxGWbyte3.Text = "0";
                                textBoxGWbyte4.Text = "0";

                                //Locking primary dns information
                                textBoxDNS1byte1.Enabled = false;
                                textBoxDNS1byte2.Enabled = false;
                                textBoxDNS1byte3.Enabled = false;
                                textBoxDNS1byte4.Enabled = false;

                                textBoxDNS1byte1.Text = "0";
                                textBoxDNS1byte2.Text = "0";
                                textBoxDNS1byte3.Text = "0";
                                textBoxDNS1byte4.Text = "0";

                                //Locking secondary dns information
                                textBoxDNS2byte1.Enabled = false;
                                textBoxDNS2byte2.Enabled = false;
                                textBoxDNS2byte3.Enabled = false;
                                textBoxDNS2byte4.Enabled = false;

                                textBoxDNS2byte1.Text = "0";
                                textBoxDNS2byte2.Text = "0";
                                textBoxDNS2byte3.Text = "0";
                                textBoxDNS2byte4.Text = "0";
                            }
                        }
                    }
                }
            }
        }

        //
        //Method used to set IP informations to the NIC via netsh command
        //
        private void setIPConfig( String selectedInterface, String ip, String mask, String gw, String dns1, String dns2)
        {
            //Performing NETSH operation to add IP, MASK and GW
            Process p = new Process();
            ProcessStartInfo IPProcess = new ProcessStartInfo("netsh", "interface ip set address \"" + selectedInterface + "\" static " + ip + " " + mask + " " + gw + " 1");
            //Hiding the CMD prompt window
            IPProcess.WindowStyle = ProcessWindowStyle.Hidden;
            
            p.StartInfo = IPProcess;
            //Running a NETSH command needs administrator rights
            p.StartInfo.Verb = "runas";
            p.Start();

            //Performing NETSH operation to delete dnsservers
            p = new Process();
            ProcessStartInfo DNSProcess = new ProcessStartInfo("netsh", "interface ip delete dnsservers \"" + selectedInterface + "\" all");
            DNSProcess.WindowStyle = ProcessWindowStyle.Hidden;
           
            p.StartInfo = DNSProcess;
            p.StartInfo.Verb = "runas";
            p.Start();
               
            //There is no secondary DNS registered
            if (String.Compare(dns2,"0.0.0.0")!=0)
            {
                //Performing NETSH operation to add primary dns server
                p = new Process();
                DNSProcess = new ProcessStartInfo("netsh", "interface ip add dns \"" + selectedInterface + "\" "+ dns1 +"");
                DNSProcess.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo = DNSProcess;
                p.StartInfo.Verb = "runas";
                p.Start();
                
                //Performing NETSH operation to add secondary dns server
                p = new Process();
                DNSProcess = new ProcessStartInfo("netsh", "interface ip add dns \"" + selectedInterface + "\" " + dns2 + " index=2");
                DNSProcess.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo = DNSProcess;
                p.StartInfo.Verb = "runas";
                p.Start();
            }
            //There is no secondary DNS registered
            else
            {
                //There is a primary DNS registered
                if(String.Compare(dns1,"0.0.0.0")!=0)
                {
                    //Performing NETSH operation to add primary dns server
                    p = new Process();
                    DNSProcess = new ProcessStartInfo("netsh", "interface ip add dns \"" + selectedInterface + "\" " + dns1 + "");
                    DNSProcess.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo = DNSProcess;
                    p.StartInfo.Verb = "runas";
                    p.Start();
                }
                
            }
            
        }
        
        //
        //Performing operations to update computer NIC informations
        //
        private void buttonUpdateNICpc_Click(object sender, EventArgs e)
        {
            //Setting the informations into String for legibility purposes
            String ip = textBoxIPbyte1.Text + "." + textBoxIPbyte2.Text + "." + textBoxIPbyte3.Text + "." + textBoxIPbyte4.Text;
            String mask = textBoxMASKbyte1.Text + "." + textBoxMASKbyte2.Text + "." + textBoxMASKbyte3.Text + "." + textBoxMASKbyte4.Text;
            String gw = textBoxGWbyte1.Text + "." + textBoxGWbyte2.Text + "." + textBoxGWbyte3.Text + "." + textBoxGWbyte4.Text;
            String dns1 = textBoxDNS1byte1.Text + "." + textBoxDNS1byte2.Text + "." + textBoxDNS1byte3.Text + "." + textBoxDNS1byte4.Text;
            String dns2 = textBoxDNS2byte1.Text + "." + textBoxDNS2byte2.Text + "." + textBoxDNS2byte3.Text + "." + textBoxDNS2byte4.Text;
            
            //Handling IPAddress parser exceptions
            try
            {
                //Performing parsing tests to validate IP informations
                Console.Write(IPAddress.Parse(ip));
                Console.Write(IPAddress.Parse(mask));
                Console.Write(IPAddress.Parse(gw));
                
                Console.Write(IPAddress.Parse(dns1));
                Console.Write(IPAddress.Parse(dns2));

                //Handling netsh process exception
                try
                {
                    setIPConfig(comboBoxNIC.SelectedItem.ToString(), ip, mask, gw, dns1, dns2);
                    System.Windows.Forms.MessageBox.Show("Your IP parameters has been successfully updated");
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("The IP parameters update has encoutered an error");
                }
                
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Your IP parameters are not correct.\n\nPlease check that each part is between 0 and 255\n\nN.B. : Remember that if you don't want to add a DNS, you have to enter \"0.0.0.0\"");
            }
       }

        //
        //Performing ping between the computer and the card
        //
        private void buttonConnecTest_Click(object sender, EventArgs e)
       {
           //Handling IPAddress parser exceptions
           try
           {
               //Performing parsing tests to validate IP informations
               Console.Write(IPAddress.Parse(textBoxIPbyte1.Text + "." + textBoxIPbyte2.Text + "." + textBoxIPbyte3.Text + "." + textBoxIPbyte4.Text));
               Console.Write(IPAddress.Parse(textBoxMASKbyte1.Text + "." + textBoxMASKbyte2.Text + "." + textBoxMASKbyte3.Text + "." + textBoxMASKbyte4.Text));
               Console.Write(IPAddress.Parse(textBoxGWbyte1.Text + "." + textBoxGWbyte2.Text + "." + textBoxGWbyte3.Text + "." + textBoxGWbyte4.Text));

               Console.Write(IPAddress.Parse(textBoxIPcardbyte1.Text + "." + textBoxIPcardbyte2.Text + "." + textBoxIPcardbyte3.Text + "." + textBoxIPcardbyte4.Text));
               Console.Write(IPAddress.Parse(textBoxMASKcardbyte1.Text + "." + textBoxMASKcardbyte2.Text + "." + textBoxMASKcardbyte3.Text + "." + textBoxMASKcardbyte4.Text));
               Console.Write(IPAddress.Parse(textBoxGWcardbyte1.Text + "." + textBoxGWcardbyte2.Text + "." + textBoxGWcardbyte3.Text + "." + textBoxGWcardbyte4.Text));

               //Running a thread in order to prevent the program being blocked by this task
               Thread icmpThread = new Thread(ICMPtest);
               icmpThread.Start();

               //Modifying the button to prevent launching 2 icmp tests simultaneously
               buttonConnecTest.Text = "In progress";
               buttonConnecTest.Enabled = false;
               labelICMPinfos.Text = "";
           }
           catch
           {
               System.Windows.Forms.MessageBox.Show("Your IP parameters are not correct.\n\nPlease check that each part is between 0 and 255");
           }
       }

        //
        //Method used to perform ICMP test
        //
        private void ICMPtest()
       {
           //IP to ping is stored into a string for legibility purposes
           String ipToPing = textBoxIPcardbyte1.Text + "." + textBoxIPcardbyte2.Text + "." + textBoxIPcardbyte3.Text + "." + textBoxIPcardbyte4.Text;
            
           //Handling CMD command exceptions
           try
           {
               //Sending the command to CMD
               ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c ping " + ipToPing);
               
               //Redirecting Standard Output stream
               procStartInfo.StandardOutputEncoding = Encoding.ASCII;
               procStartInfo.RedirectStandardOutput = true;
               procStartInfo.UseShellExecute = false;

               //Prevent CMD from opening a window
               procStartInfo.CreateNoWindow = true;

               //Starting the process
               Process proc = new Process();
               proc.StartInfo = procStartInfo;
               proc.Start();

               //Setting the Standard Output Stream to the label
               labelICMPinfos.Text = proc.StandardOutput.ReadToEnd();
               Console.WriteLine(proc.StandardOutput.ReadToEnd());
               buttonConnecTest.Text = "Launch";
               buttonConnecTest.Enabled = true;
           }
           catch
           {
               System.Windows.Forms.MessageBox.Show("The connectivity test has encoutered an error");
           }
       }

        //
        //Performing operations to update card NIC informations
        //
        private void buttonUpdateNICcard_Click(object sender, EventArgs e)
       {
           //Handling IPAddress parser exception
           try
           {
               //Validating ip addresses
               Console.Write(IPAddress.Parse(textBoxIPcardbyte1.Text + "." + textBoxIPcardbyte2.Text + "." + textBoxIPcardbyte3.Text + "." + textBoxIPcardbyte4.Text));
               Console.Write(IPAddress.Parse(textBoxMASKcardbyte1.Text + "." + textBoxMASKcardbyte2.Text + "." + textBoxMASKcardbyte3.Text + "." + textBoxMASKcardbyte4.Text));
               Console.Write(IPAddress.Parse(textBoxGWcardbyte1.Text + "." + textBoxGWcardbyte2.Text + "." + textBoxGWcardbyte3.Text + "." + textBoxGWcardbyte4.Text));

               Console.Write(IPAddress.Parse(textBoxDNS1cardbyte1.Text + "." + textBoxDNS1cardbyte2.Text + "." + textBoxDNS1cardbyte3.Text + "." + textBoxDNS1cardbyte4.Text));
               Console.Write(IPAddress.Parse(textBoxDNS2cardbyte1.Text + "." + textBoxDNS2cardbyte2.Text + "." + textBoxDNS2cardbyte3.Text + "." + textBoxDNS2cardbyte4.Text));

               //Sending a 31 ( IP card update ) command to the card with its IP address
               serialPort.WriteLine("#31@" + textBoxIPcardbyte1.Text + "." + textBoxIPcardbyte2.Text + "." + textBoxIPcardbyte3.Text + "." + textBoxIPcardbyte4.Text);
               System.Threading.Thread.Sleep(10);

               //Sending a 32 ( Mask card update ) command to the card with its Mask
               serialPort.WriteLine("#32@" + textBoxMASKcardbyte1.Text + "." + textBoxMASKcardbyte2.Text + "." + textBoxMASKcardbyte3.Text + "." + textBoxMASKcardbyte4.Text);
               System.Threading.Thread.Sleep(10);

               //Sending a 33 ( Gateway card update ) command to the card with its gateway address
               serialPort.WriteLine("#33@" + textBoxGWcardbyte1.Text + "." + textBoxGWcardbyte2.Text + "." + textBoxGWcardbyte3.Text + "." + textBoxGWcardbyte4.Text);
               System.Threading.Thread.Sleep(10);

               //Sending a 34 ( Primary DNS card update ) command to the card with its dns address
               serialPort.WriteLine("#34@" + textBoxDNS1cardbyte1.Text + "." + textBoxDNS1cardbyte2.Text + "." + textBoxDNS1cardbyte3.Text + "." + textBoxDNS1cardbyte4.Text);
               System.Threading.Thread.Sleep(10);

               //Sending a 35 ( Secondary DNS card update ) command to the card with its dns address
               serialPort.WriteLine("#35@" + textBoxDNS2cardbyte1.Text + "." + textBoxDNS2cardbyte2.Text + "." + textBoxDNS2cardbyte3.Text + "." + textBoxDNS2cardbyte4.Text);
               System.Threading.Thread.Sleep(10);

               //Prompting the user to tell him that the configuration has been correctly changed
               System.Windows.Forms.MessageBox.Show("The card's IP configuration has been changed.");
               this.Refresh();
           }
           catch
           {
               System.Windows.Forms.MessageBox.Show("Your IP parameters are not correct.\n\nPlease check that each part is between 0 and 255\n\nN.B. : Remember that if you don't want to add a DNS, you have to enter \"0.0.0.0\"");
           }
       }

        //
        //Performing operations when data are received on the serial port
        //
        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
       {
           //Handling serial port exceptions
           try
           {
               //Reading the buffer
               String data = serialPort.ReadLine();
               Console.Write(data+"\n");

               // code received is 04 ( Temperature )
               if (String.Compare(data.Substring(0, 4), Rs232_string.TEMP) == 0)
               {
                   //removing the code part
                   String infoString = data.Substring(4);
                   
                   //Comparing the received value to the actual one
                   if (double.Parse(infoString) < double.Parse(labelTempValue.Text))
                   {
                       //lighting the right arrow
                       pictureBoxArrowUp.Image = Fourchip.Properties.Resources.arrow_darkgreen;
                       pictureBoxArrowDown.Image = Fourchip.Properties.Resources.arrow_red;
                       labelTempValue.Text = infoString;
                   }
                   else
                   {
                       if (double.Parse(infoString) > double.Parse(labelTempValue.Text))
                       {
                           pictureBoxArrowDown.Image = Fourchip.Properties.Resources.arrow_darkred;
                           pictureBoxArrowUp.Image = Fourchip.Properties.Resources.arrow_green;
                           labelTempValue.Text = infoString;
                       }
                       else
                       {
                           //if the received value is the same as the actual one, arrows are deactivated
                           pictureBoxArrowUp.Image = Fourchip.Properties.Resources.arrow_darkgreen;
                           pictureBoxArrowDown.Image = Fourchip.Properties.Resources.arrow_darkred;
                       }
                   }

                   //Adding values to display in the chart
                   if (tabTempIndex < 15)
                   {
                       tempChart[tabTempIndex] = double.Parse(infoString);
                       tabTempIndex++;
                   }
                   else
                   {
                       tabTempIndex = 0;
                       int j = 0;
                       while (j < 15)
                       {
                           tempChart[j] = 0;
                           j++;
                       }
                        tempChart[tabTempIndex] = double.Parse(infoString);
                   }
               }
               else
               {
                   // code received is 05 ( Brightness )
                   if (String.Compare(data.Substring(0, 4), Rs232_string.LIGHT) == 0)
                   {
                       //Removing the code part
                       String infoString = data.Substring(4);
                       
                       //In function of the value, the image is changed to moon or sun
                       if (double.Parse(infoString) < 50.0)
                       {
                           pictureBoxBrightness.Image = Fourchip.Properties.Resources.moon;
                           labelBrightnessValue.Text = infoString;
                       }
                       else
                       {
                           pictureBoxBrightness.Image = Fourchip.Properties.Resources.sun;
                           labelBrightnessValue.Text = infoString;
                       }

                       //Adding values to display in the chart
                       if (tabBrightIndex < 15)
                       {
                           brightChart[tabBrightIndex] = double.Parse(infoString);
                           tabBrightIndex++;
                       }
                       else
                       {
                           tabBrightIndex = 0;
                           int j = 0;
                           while (j < 15)
                           {
                               brightChart[j] = 0;
                               j++;
                           }
                           brightChart[tabBrightIndex] = double.Parse(infoString);
                       }
                   }
                   else
                   {
                       // code received is 07 ( Chrono )
                       if (String.Compare(data.Substring(0, 4), Rs232_string.CHRONO) == 0)
                       {
                           //Splitting the informations to get minutes and hours separately
                           String[] infoString = data.Substring(4).Split(';');
                           //Displaying the time
                           toolStripStatusLabelChrono.Text = "Connection time : " + infoString[0] + ":" + infoString[1];
                       }
                       else
                       {
                           //code received is 82 ( PW_ACK )
                           if (String.Compare(data.Substring(0, 4), Rs232_string.PW_ACK) == 0)
                           {
                               //Removing the code part
                               String infoString = data.Substring(4);

                               //Handling the PW_ACK value
                               if (String.Compare(infoString, "0") == 0)
                               {
                                   System.Windows.Forms.MessageBox.Show("Password change has occured an error.\n\nRestart password change procedure by clicking \"Change password\" again.\n\nPlease remember that you have 30s to enter your password and finish by pushing CENTER");
                               }
                               else
                               {
                                   System.Windows.Forms.MessageBox.Show("Password successfully changed");
                               }
                               //Enabling the "change password" button
                               buttonPasswordChange.Enabled = true;
                           }
                           else
                           {
                               //code received is 81 ( USER_CHANGE ack )
                               if (String.Compare(data.Substring(0, 4), Rs232_string.USER_CHANGE) == 0)
                               {
                                   //Removing the code part
                                   String infoString = data.Substring(4);

                                   //Handling the USER_CHANGE ack
                                   if (String.Compare(infoString, "0") == 0)
                                   {
                                       System.Windows.Forms.MessageBox.Show("Username change has occured an error.\n\nRestart username change procedure by clicking \"Change username\" again.");
                                   }
                                   else
                                   {
                                       System.Windows.Forms.MessageBox.Show("Username successfully changed");
                                   }
                                   //Enabling the "change username" button
                                   buttonUsernameChange.Enabled = true;
                               }
                           }
                       }
                   }
               }
           }
           catch
           {
           }
       }

        //
        //Performing operations when the numericUpDown value is changed
        //
        private void frequencySpinner_ValueChanged(object sender, EventArgs e)
       {
            //Sending the frequency value with a LED code
            serialPort.Write(Rs232_string.LED +frequencySpinner.Value);
       }

        //
        //Performing operations to allow the user to change his username on the rfid card
        //
        private void buttonUsernameChange_Click(object sender, EventArgs e)
       {
           //Getting user informations
           String name = Interaction.InputBox("Please enter your firstname : ");
           String firstname = Interaction.InputBox("Please enter your name : ");
           
           //Removing possible spaces at the start/end of the informations
           name.Trim();
           firstname.Trim();

           //Linking the two informations to make only one
           String username = firstname+" "+name;
           
           //Verifying that the String is really "firstname name" without special characters
           if (Regex.IsMatch(username, @"^([A-Z][a-z]+)(-[A-Z][a-z]+)?( ([a-zA-Z]')?[A-Z][a-z]+)+$") == false || username.Length > 19)
           {
               System.Windows.Forms.MessageBox.Show("Name and firstname check error. Please verify them");
           }
           else
           {
               System.Windows.Forms.MessageBox.Show("Please keep the card on the top of the RFID reader during the operation");
               serialPort.WriteLine(Rs232_string.USER_CHANGE + firstname + " " + name);
               buttonUsernameChange.Enabled = false;
           }
       }

        //
        //Performing operations to allow the user to change his password on the rfid card
        //
        private void buttonPasswordChange_Click(object sender, EventArgs e)
       {
           System.Windows.Forms.MessageBox.Show("Please enter your password on the card's keyboard and finish by pressing CENTER");
           //Sending a PW_CHANGE request to the card
           serialPort.WriteLine(Rs232_string.PW_CHANGE);
           buttonPasswordChange.Enabled = false;
       }

        //
        //Display a temperature chart when clicking on the temperature
        //
        private void labelTempValue_Click(object sender, EventArgs e)
        {
            FormFourchipChart formFourchipChart = new FormFourchipChart(tempChart,tabTempIndex,1);
            formFourchipChart.ShowDialog();
        }

        //
        //Display a brightness chart when clicking on the sun/moon image
        //
        private void pictureBoxBrightness_Click(object sender, EventArgs e)
        {
            FormFourchipChart formFourchipChart = new FormFourchipChart(brightChart, tabBrightIndex, 2);
            formFourchipChart.ShowDialog();
        }

        //
        //Display a temperature chart when clicking on the temperature symbol
        //
        private void labelTemp_Click(object sender, EventArgs e)
        {
            FormFourchipChart formFourchipChart = new FormFourchipChart(tempChart, tabTempIndex, 1);
            formFourchipChart.ShowDialog();
        }

        //
        //Display a brightness chart when clicking on the brightness value
        //
        private void labelBrightnessValue_Click(object sender, EventArgs e)
        {
            FormFourchipChart formFourchipChart = new FormFourchipChart(brightChart, tabBrightIndex, 2);
            formFourchipChart.ShowDialog();
        }
    }
}
