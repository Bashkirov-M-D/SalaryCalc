using SalaryCalc.Other;
using System;
using System.Windows.Forms;

namespace SalaryCalc
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            User user = new User();
            LoginForm login = new LoginForm(user);
            Application.Run(login);
            login.Dispose();
            Application.Run(new MainForm(user));
        }
    }
}
