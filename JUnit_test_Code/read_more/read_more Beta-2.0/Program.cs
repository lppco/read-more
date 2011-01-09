using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace read_more
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int isProcessRunning =
                System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName)
                    .Length;
            if (isProcessRunning != 1)
            {
                MessageBox.Show(Constants.APPREPEATED, Constants.ERRORTIP,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);   
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
