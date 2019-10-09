using Launcher.Classes;
using System;
using System.Windows.Forms;

namespace Launcher
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Assemblies.Resolve();
            Run();
        }
        [STAThread]
        private static void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            //System.Reflection.TargetInvocationException
            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatal error : " + Environment.NewLine + ex.ToString());
            }
        }
    }
}
