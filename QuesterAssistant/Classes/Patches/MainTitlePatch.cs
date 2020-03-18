using System.Threading;
using System.Threading.Tasks;
using Astral.Forms;
using MyNW.Internals;
using QuesterAssistant.Classes.Extensions;
using QuesterAssistant.Classes.Monitoring;
using QuesterAssistant.Classes.Reflection;

namespace QuesterAssistant.Classes.Patches
{
    internal class MainTitlePatch
    {
        private static readonly Main MainForm = typeof(Astral.Controllers.Forms).GetStaticPropertyValue("Main") as Main;

        public static void RunOnce()
        {
            if (MainForm.GetFieldValue("\u0003") is Thread thread && thread.IsAlive)
                thread.Abort();
            Task.Factory.StartNew(MainTitleRewrite, TaskCreationOptions.LongRunning);
        }

        private static void MainTitleRewrite()
        {
            for (;;)
            {
                try
                {
                    var str = string.Empty;
                    if (EntityManager.LocalPlayer != null && MainForm.Text != EntityManager.LocalPlayer.Name)
                        str = EntityManager.LocalPlayer.IsValid
                            ? EntityManager.LocalPlayer.Name.Hide()
                            : @"Offline";

                    if (GameClient.Process != null)
                        str += $" - PID:{GameClient.Process.Id}";
                    MainForm.Text = str;
                }
                catch { }
                Pause.Sleep(250);
            }
        }
    }
}