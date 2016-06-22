using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoxtelPLA
{
    public partial class Form1 : Form
    {
        private string password;
        private string SSID;
        private PLAtools platools;
        private NF17Tools nf17tools;

        public Form1()
        {
            string SSID = "initial";
            string password = "initial";

            InitializeComponent();
               platools = new PLAtools();
                platools.loginNP508();
                platools.NP508GetWirelessInfo(ref SSID, ref password);
                platools.logoutNP508();
                PLAssid.Text = SSID;
                PLApassword.Text = password; 

            nf17tools = new NF17Tools();
            nf17tools.nf17ACVsetup();
            modemSSID.Text = nf17tools.SSID;
            modemPassword.Text = nf17tools.wPassword;
            

          
            
            
        }

       

        private void PLApassword_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void PLAssid_TextChanged(object sender, EventArgs e)
        {
           //SSID = PLApassword.Text;
           // platools.loginNP508();
          //  platools.NP508SetWirelessInfo(SSID, password);
           // platools.logoutNP508();
           // platools.NP508SetWirelessInfo(SSID, password);
          
              
        }

        private void modemSSID_TextChanged(object sender, EventArgs e)
        {
            SSID = modemSSID.Text;
        }

        private void modemPassword_TextChanged(object sender, EventArgs e)
        {
            password = modemPassword.Text;
        }

        private void syncButton_Click(object sender, EventArgs e)
        {   platools.loginNP508();
            platools.NP508SetWirelessInfo(SSID, password);
            platools.logoutNP508();
        }
    }
}
