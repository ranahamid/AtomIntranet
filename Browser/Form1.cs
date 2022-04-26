using System;
using System.Windows.Forms;
using Atom_Intranet;
using Microsoft.Win32;

namespace Atom_Intranet
{
    public partial class Form1 : Form
    {
        //Set the default address
        private readonly string WebPage = "http://192.168.0.109/atomintranet/";



        //create Registry key to launch this app in the startup 
        
        //RegistryKey rkApp = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private readonly RegistryKey rkApp =
            Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);


        public Form1()
        {
            InitializeComponent();
            webBrowser1.AllowWebBrowserDrop = false;
            // bool y = true;
            //   RegisterInStartup(y);

            //Set back and forward button disabled.
            button3.Enabled = false;
            button2.Enabled = false;


           

            //Check the startup 
            if (rkApp.GetValue("Atom Intranet") == null)
            {
                disableAtStartupToolStripMenuItem.Enabled = false;
                checkBox1.Checked = false;
            }
            else
            {
                runAtStartupToolStripMenuItem.Enabled = false;
                checkBox1.Checked =  true;
            }

            //Navigate to the default address
            Navigation_func(WebPage);
        }


        //Home button;
        private void button1_Click(object sender, EventArgs e)
        {
      
            Navigation_func(WebPage);
        }


        //Back Button
        private void button2_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
            {
                webBrowser1.GoBack();
            }
        }

   
        //Forward Button
        private void button3_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
            {
                webBrowser1.GoForward();
            }
        }


        //Refresh
        private void button4_Click(object sender, EventArgs e)
        {
            if (!webBrowser1.Url.Equals("about:blank"))
            {
                webBrowser1.Refresh();
            }
        }


        //After a request completed
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        


            if (webBrowser1.CanGoForward)
            {
                button3.Enabled = true;
            }

            if (webBrowser1.CanGoBack)
            {
                button2.Enabled = true;
            }
        }

     

        //Navigation function
        private void Navigation_func(string address)
        {
            //check if the address is blank or null
            if (string.IsNullOrEmpty(address))
                return;


            //check if the page is blank
            if (address.Equals("about:blank"))
                return;

            //check if the page is start with http or https or file
            if (!address.StartsWith("http://") && !address.StartsWith("https://") && !address.StartsWith("file:/"))
            {
                address = "http://" + address;
            }

            try
            {
                //Actual Navigation
                //webBrowser1.Navigate(new Uri(address));
                webBrowser1.Navigate(address);
            }
            catch (UriFormatException)
            {
            }
        }

     


        //Exit MenuItem
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Start Up the program
        private void runAtStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //run at startup
            if (rkApp.GetValue("Atom Intranet") == null)
            {
                rkApp.SetValue("Atom Intranet", Application.ExecutablePath);


                checkBox1.Checked = true;

                runAtStartupToolStripMenuItem.Enabled = false;
                disableAtStartupToolStripMenuItem.Enabled = true;
                MessageBox.Show("Enable for run at Startup of windows", "Enable");

            }
        
        }

        //Disable to run at the start up
        private void disableAtStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //disable
            if (!(rkApp.GetValue("Atom Intranet") == null))
            {
                rkApp.DeleteValue("Atom Intranet", false);

                checkBox1.Checked = false;


                runAtStartupToolStripMenuItem.Enabled = true;
                disableAtStartupToolStripMenuItem.Enabled = false;
                MessageBox.Show("Disable for run at Startup of windows", "Disable");
            }
         
        }

        //about MenuItem
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutBox1();
            //Show the about window
            about.Show();
        }


        //Print MenuItem
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintDialog();
      
        }


        //Save As MenuItem
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowSaveAsDialog();
        }


        //PageSetUp MenuItem
        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPageSetupDialog();
        }


        //Print Preview MenuItem
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintPreviewDialog();
        }


        //Propertiess MenuItem
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPropertiesDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
         
                //run at startup
                if (rkApp.GetValue("Atom Intranet") == null)
                {
                    rkApp.SetValue("Atom Intranet", Application.ExecutablePath);



                    runAtStartupToolStripMenuItem.Enabled = false;
                    disableAtStartupToolStripMenuItem.Enabled = true;
                    MessageBox.Show("Enable for run at Startup of windows", "Enable");

                }
            }
            else
            {
                if (!(rkApp.GetValue("Atom Intranet") == null))
                {
                    rkApp.DeleteValue("Atom Intranet", false);

     

                    runAtStartupToolStripMenuItem.Enabled = true;
                    disableAtStartupToolStripMenuItem.Enabled = false;
                    MessageBox.Show("Disable for run at Startup of windows", "Disable");
                }
            }
        }


        //cookie
    }
}
