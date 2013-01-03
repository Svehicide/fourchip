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
                comboBoxNIC.Items.Add(nic.Description);
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
            }
            else
            {
                frequencySpinner.Enabled = false;
            }
        }

        private void comboBoxNIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void setIPConfig( String selectedInterface, String ip, String mask, String gw, String dns1, String dns2)
        {

            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapter");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                Console.Write("{0} - {1}\n\n", objMO["Description"],objMO["Name"]);
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
    }
}
