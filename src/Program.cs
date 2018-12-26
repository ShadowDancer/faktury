using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Faktury.Windows;

namespace Faktury
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("pl-PL");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;


            var applicationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "Faktury");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(applicationDirectory));
        }
    }
}