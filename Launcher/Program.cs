using Launcher.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Launcher
{
    static class Program
    {
        private static MainForm form;
        public static Rectangle FormRectangle =>
            new Rectangle(form.Location, form.Size);
        
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
                form = new MainForm();
                Application.Run(form);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Fatal error : {Environment.NewLine}{ex}");
            }
        }
    }
}
