using System;
using IEC61850.Client;
using IEC61850.Common;
using System.Windows.Forms;

namespace IED
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {

            IedConnection con = new IedConnection();

            string hostname;
            hostname = "192.168.1.101";
            try
            {
                con.Connect(hostname, 102);
                radioButton1.Checked = true;
                con.Abort();
            }
            catch (IedConnectionException)
            {
                radioButton1.Checked = false;
            }
            con.Dispose();
        }
    }
}
