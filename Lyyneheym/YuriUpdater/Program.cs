using System;
using System.Threading;
using System.Windows.Forms;

namespace YuriUpdater
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mtx = new Mutex(true, "__Yuri?Lyyneheym?Updater__", out bool canCreateNew);
            try
            {
                if (canCreateNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new YuriUpdateForm());
                }
                else
                {
                    MessageBox.Show("Unable to run multi instances of Updater.");
                }
            }
            finally
            {
                if (canCreateNew)
                {
                    mtx.ReleaseMutex();
                }
            }
        }
    }
}
