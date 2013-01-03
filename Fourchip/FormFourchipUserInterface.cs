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

namespace Fourchip
{
    public partial class FormFourchipUserInterface : Form
    {
        Form owner;

        public FormFourchipUserInterface(Form o,String com, String br)
        {
            InitializeComponent();

            owner = o;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                comboBoxNIC.Items.Add(nic.Name);
            }
          
            serialPort.PortName = com;
            serialPort.BaudRate = int.Parse(br);
            
            try
            {
                serialPort.Open();
            }
            catch ( Exception ex )
            {
               System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            comboBoxNIC.SelectedIndex = 0;
            labelTempValue.Text = "0";
        }

        private void FormFourchipUserInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
                this.Dispose();
                Application.Exit();
        }

        private void buttonDisconnected_Click(object sender, EventArgs e)
        {
                serialPort.Write("#99@");
                this.Dispose();
                Application.Restart();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLEDstate.Checked == true)
            {
                frequencySpinner.Enabled = true;
                Console.Write(Rs232_string.LED + "ON\n");
                serialPort.WriteLine(Rs232_string.LED + "ON\n");
            }
            else
            {
                frequencySpinner.Enabled = false;
                frequencySpinner.Value = 1;
                Console.Write(Rs232_string.LED + "OFF\n");
                serialPort.WriteLine(Rs232_string.LED + "OFF\n");

            }
        }

        private void comboBoxNIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            NetworkInterface nicSelected = null;
            String[] byteSplit;
            IPAddress[] dnsList = new IPAddress[2];
            int i = 0;



            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (String.Compare(nic.Name, comboBoxNIC.SelectedItem.ToString()) == 0)
                {
                    nicSelected = nic;

                    foreach (UnicastIPAddressInformation ip in nicSelected.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.ToString() != "::1" && ip.Address.ToString() != "127.0.0.1")
                        {
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

                            byteSplit = ip.Address.ToString().Split('.');

                            textBoxIPbyte1.Text = byteSplit[0];
                            textBoxIPbyte2.Text = byteSplit[1];
                            textBoxIPbyte3.Text = byteSplit[2];
                            textBoxIPbyte4.Text = byteSplit[3];

                            byteSplit = ip.IPv4Mask.ToString().Split('.');

                            textBoxMASKbyte1.Text = byteSplit[0];
                            textBoxMASKbyte2.Text = byteSplit[1];
                            textBoxMASKbyte3.Text = byteSplit[2];
                            textBoxMASKbyte4.Text = byteSplit[3];

                            foreach (GatewayIPAddressInformation gw in nicSelected.GetIPProperties().GatewayAddresses)
                            {
                                byteSplit = gw.Address.ToString().Split('.');

                                textBoxGWbyte1.Text = byteSplit[0];
                                textBoxGWbyte2.Text = byteSplit[1];
                                textBoxGWbyte3.Text = byteSplit[2];
                                textBoxGWbyte4.Text = byteSplit[3];
                            }

                            i = 0;
                            foreach (IPAddress dns in nicSelected.GetIPProperties().DnsAddresses)
                            {
                                dnsList[i] = dns;
                                i++;
                            }

                            if (dnsList[0] == null)
                            {

                                textBoxDNS1byte1.Text = "0";
                                textBoxDNS1byte2.Text = "0";
                                textBoxDNS1byte3.Text = "0";
                                textBoxDNS1byte4.Text = "0";

                                textBoxDNS2byte1.Text = "0";
                                textBoxDNS2byte2.Text = "0";
                                textBoxDNS2byte3.Text = "0";
                                textBoxDNS2byte4.Text = "0";
                            }
                            else
                            {
                                byteSplit = dnsList[0].ToString().Split('.');

                                textBoxDNS1byte1.Text = byteSplit[0];
                                textBoxDNS1byte2.Text = byteSplit[1];
                                textBoxDNS1byte3.Text = byteSplit[2];
                                textBoxDNS1byte4.Text = byteSplit[3];

                                if (dnsList[1] != null)
                                {
                                    byteSplit = dnsList[1].ToString().Split('.');

                                    textBoxDNS2byte1.Text = byteSplit[0];
                                    textBoxDNS2byte2.Text = byteSplit[1];
                                    textBoxDNS2byte3.Text = byteSplit[2];
                                    textBoxDNS2byte4.Text = byteSplit[3];
                                }
                                else
                                {
                                    textBoxDNS2byte1.Text = "0";
                                    textBoxDNS2byte2.Text = "0";
                                    textBoxDNS2byte3.Text = "0";
                                    textBoxDNS2byte4.Text = "0";
                                }
                            }
                        }
                        else
                        {
                            if (ip.Address.ToString() != "::1")
                            {
                                byteSplit = ip.Address.ToString().Split('.');

                                textBoxIPbyte1.Text = byteSplit[0];
                                textBoxIPbyte2.Text = byteSplit[1];
                                textBoxIPbyte3.Text = byteSplit[2];
                                textBoxIPbyte4.Text = byteSplit[3];

                                byteSplit = ip.IPv4Mask.ToString().Split('.');

                                textBoxMASKbyte1.Text = byteSplit[0];
                                textBoxMASKbyte2.Text = byteSplit[1];
                                textBoxMASKbyte3.Text = byteSplit[2];
                                textBoxMASKbyte4.Text = byteSplit[3];

                                textBoxGWbyte1.Enabled = false;
                                textBoxGWbyte2.Enabled = false;
                                textBoxGWbyte3.Enabled = false;
                                textBoxGWbyte4.Enabled = false;

                                textBoxGWbyte1.Text = "0";
                                textBoxGWbyte2.Text = "0";
                                textBoxGWbyte3.Text = "0";
                                textBoxGWbyte4.Text = "0";

                                textBoxDNS1byte1.Enabled = false;
                                textBoxDNS1byte2.Enabled = false;
                                textBoxDNS1byte3.Enabled = false;
                                textBoxDNS1byte4.Enabled = false;

                                textBoxDNS1byte1.Text = "0";
                                textBoxDNS1byte2.Text = "0";
                                textBoxDNS1byte3.Text = "0";
                                textBoxDNS1byte4.Text = "0";

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

        private void setIPConfig( String selectedInterface, String ip, String mask, String gw, String dns1, String dns2)
        {
            Process p = new Process();
            ProcessStartInfo IPProcess = new ProcessStartInfo("netsh", "interface ip set address \"" + selectedInterface + "\" static " + ip + " " + mask + " " + gw + " 1");
            IPProcess.WindowStyle = ProcessWindowStyle.Hidden;
            
            p.StartInfo = IPProcess;
            p.StartInfo.Verb = "runas";
            p.Start();

            p = new Process();
            ProcessStartInfo DNSProcess = new ProcessStartInfo("netsh", "interface ip delete dnsservers \"" + selectedInterface + "\" all");
            DNSProcess.WindowStyle = ProcessWindowStyle.Hidden;
           
            p.StartInfo = DNSProcess;
            p.StartInfo.Verb = "runas";
            p.Start();
                
            if (String.Compare(dns2,"0.0.0.0")!=0)
            {
                p = new Process();
                DNSProcess = new ProcessStartInfo("netsh", "interface ip add dns \"" + selectedInterface + "\" "+ dns1 +"");
                DNSProcess.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo = DNSProcess;
                p.StartInfo.Verb = "runas";
                p.Start();
                
                p = new Process();
                DNSProcess = new ProcessStartInfo("netsh", "interface ip add dns \"" + selectedInterface + "\" " + dns2 + " index=2");
                DNSProcess.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo = DNSProcess;
                p.StartInfo.Verb = "runas";
                p.Start();
                
            }
            else
            {
                if(String.Compare(dns1,"0.0.0.0")!=0)
                {
                    p = new Process();
                    DNSProcess = new ProcessStartInfo("netsh", "interface ip add dns \"" + selectedInterface + "\" " + dns1 + "");
                    DNSProcess.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo = DNSProcess;
                    p.StartInfo.Verb = "runas";
                    p.Start();
                }
                
            }
            
        }
        
        private void buttonUpdateNICpc_Click(object sender, EventArgs e)
        {
            String ip = textBoxIPbyte1.Text + "." + textBoxIPbyte2.Text + "." + textBoxIPbyte3.Text + "." + textBoxIPbyte4.Text;
            String mask = textBoxMASKbyte1.Text + "." + textBoxMASKbyte2.Text + "." + textBoxMASKbyte3.Text + "." + textBoxMASKbyte4.Text;
            String gw = textBoxGWbyte1.Text + "." + textBoxGWbyte2.Text + "." + textBoxGWbyte3.Text + "." + textBoxGWbyte4.Text;
            String dns1 = textBoxDNS1byte1.Text + "." + textBoxDNS1byte2.Text + "." + textBoxDNS1byte3.Text + "." + textBoxDNS1byte4.Text;
            String dns2 = textBoxDNS2byte1.Text + "." + textBoxDNS2byte2.Text + "." + textBoxDNS2byte3.Text + "." + textBoxDNS2byte4.Text;

            Console.Write(ip + " " + mask + " " + gw + " " + dns1 + " " + dns2);
            setIPConfig(comboBoxNIC.SelectedItem.ToString(), ip, mask, gw, dns1, dns2);
        }

       private void buttonConnecTest_Click(object sender, EventArgs e)
       {
           Thread icmpThread = new Thread(ICMPtest);

           icmpThread.Start();

           buttonConnecTest.Text = "In progress";
           buttonConnecTest.Enabled = false;
           labelICMPinfos.Text = "";
       }

       private void ICMPtest()
       {
           String ipToPing = textBoxIPcardbyte1.Text + "." + textBoxIPcardbyte2.Text + "." + textBoxIPcardbyte3.Text + "." + textBoxIPcardbyte4.Text;

           try
           {
               ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c ping " + ipToPing);

               procStartInfo.StandardOutputEncoding = Encoding.ASCII;
               procStartInfo.RedirectStandardOutput = true;
               procStartInfo.UseShellExecute = false;

               procStartInfo.CreateNoWindow = true;

               Process proc = new Process();
               proc.StartInfo = procStartInfo;
               proc.Start();

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

       private void buttonUpdateNICcard_Click(object sender, EventArgs e)
       {
           serialPort.WriteLine("#31@" + textBoxIPcardbyte1.Text + "." + textBoxIPcardbyte2.Text + "." + textBoxIPcardbyte3.Text + "." + textBoxIPcardbyte4.Text);
           System.Threading.Thread.Sleep(10);
           serialPort.WriteLine("#32@" + textBoxMASKcardbyte1.Text + "." + textBoxMASKcardbyte2.Text + "." + textBoxMASKcardbyte3.Text + "." + textBoxMASKcardbyte4.Text);
           System.Threading.Thread.Sleep(10);
           serialPort.WriteLine("#33@" + textBoxGWcardbyte1.Text + "." + textBoxGWcardbyte2.Text + "." + textBoxGWcardbyte3.Text + "." + textBoxGWcardbyte4.Text);
           System.Threading.Thread.Sleep(10);
           serialPort.WriteLine("#34@" + textBoxDNS1cardbyte1.Text + "." + textBoxDNS1cardbyte2.Text + "." + textBoxDNS1cardbyte3.Text + "." + textBoxDNS1cardbyte4.Text);
           System.Threading.Thread.Sleep(10);
           serialPort.WriteLine("#35@" + textBoxDNS2cardbyte1.Text + "." + textBoxDNS2cardbyte2.Text + "." + textBoxDNS2cardbyte3.Text + "." + textBoxDNS2cardbyte4.Text);
           System.Threading.Thread.Sleep(10);

           System.Windows.Forms.MessageBox.Show("The card's IP configuration has been changed.");

           this.Refresh();
       }

       private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
       {
           try
           {
               String data = serialPort.ReadLine();
               // code received is 04 ( Temperature )
               if (String.Compare(data.Substring(0, 4), Rs232_string.TEMP) == 0)
               {
                   //removing the code part
                   String infoString = data.Substring(4);

                   if (int.Parse(infoString) < int.Parse(labelTempValue.Text))
                   {
                       pictureBoxArrowUp.Image = Fourchip.Properties.Resources.arrow_darkgreen;
                       pictureBoxArrowDown.Image = Fourchip.Properties.Resources.arrow_red;
                       labelTempValue.Text = infoString;
                   }
                   else
                   {
                       if (int.Parse(infoString) > int.Parse(labelTempValue.Text))
                       {
                           pictureBoxArrowDown.Image = Fourchip.Properties.Resources.arrow_darkred;
                           pictureBoxArrowUp.Image = Fourchip.Properties.Resources.arrow_green;
                           labelTempValue.Text = infoString;
                       }
                       else
                       {
                           pictureBoxArrowUp.Image = Fourchip.Properties.Resources.arrow_darkgreen;
                           pictureBoxArrowDown.Image = Fourchip.Properties.Resources.arrow_darkred;
                       }
                   }
               }
               else
               {
                   // code received is 05 ( Brightness )
                   if (String.Compare(data.Substring(0, 4), Rs232_string.LIGHT) == 0)
                   {
                       String infoString = data.Substring(4);
                       // a faire.
                   }
                   else
                   {
                       // code received is 07 ( Chrono )
                       if (String.Compare(data.Substring(0, 4), Rs232_string.CHRONO) == 0)
                       {
                           String[] infoString = data.Substring(4).Split(';');

                           toolStripStatusLabelChrono.Text = "Connection time : " + infoString[0] + ":" + infoString[1];
                       }
                   }
               }
           }
           catch
           {
           }
       }

       private void frequencySpinner_ValueChanged(object sender, EventArgs e)
       {
           Console.Write(Rs232_string.LED + frequencySpinner.Value+"\n");
           //serialPort.Write(Rs232_string.LED +frequencySpinner.Value);
       }
    }
}
