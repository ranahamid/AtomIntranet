using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;

namespace Atom_Intranet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
/*

            //create desktop shortcuts
            String linkName = "Atom Intranet";

            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            using (StreamWriter writer = new StreamWriter(deskDir + "\\" + linkName + ".url"))
            {
                string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
                writer.Flush();
            }


*/


            //create Trigger

            // Get the service on the local machine
            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Request to run at the specified time";

                // Create a trigger that will fire the task at this time every other day   
                DateTime now = DateTime.Now;

                DateTime now2 = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                
                //Set The Specified time to start up the program
                DateTime last = new DateTime(now.Year, now.Month, now.Day, 19, 00, 00);
              

                //testing
                //  last = new DateTime(now.Year, now.Month, now.Day, 11, 06, 40);

                //calculate the left time 
                TimeSpan d = last.Subtract(now2);


                //trigger the event to start up the program
                td.Triggers.Add(new RegistrationTrigger { Delay = d });



                // Create an action that will launch this program whenever the trigger fires
                td.Actions.Add(new ExecAction(Assembly.GetExecutingAssembly().Location, null, null));
               
                
                //to launch another program. for example notepad
                //td.Actions.Add(new ExecAction("notepad.exe", null, null));


                // Register the task in the root folder
                ts.RootFolder.RegisterTaskDefinition(@"Test", td);

                // Remove the task we just created
                //   ts.RootFolder.DeleteTask("Test");
            }




        }
    }
}
