using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fourchip
{
    public partial class FormFourchipUserInterface : Form
    {
        Form owner;

        public FormFourchipUserInterface(Form o,String com, String br)
        {
            InitializeComponent();

            owner = o;

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
    }
}
