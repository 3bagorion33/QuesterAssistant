using Astral;
using QuesterAssistant.Classes;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuesterAssistant.Panels;

namespace QuesterAssistant.UpgradeManager
{
    internal class UpgradeManagerCore : ACore<UpgradeManagerData, UpgradeManagerForm>
    {
        protected override bool IsValid => true;
        protected override bool HookEnableFlag => Data.ToggleHotKey.Enabled;

        public event Action TasksStarted;
        public event Action TasksStopped;
        public bool TasksIsRunning => !thread?.IsCompleted ?? false;

        private Task thread;
        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        private int runTaskStartIdx;
        private int runTaskStopIdx;
        private int runCount;

        public void ToggleTasks()
        {
            switch (TasksIsRunning)
            {
                case false:
                    StartTasks(taskStartIdx: 0);
                    break;
                case true:
                    StopTasks();
                    break;
            }
        }

        public Task StartTasks(int taskStartIdx = -1, int taskStopIdx = -1, int count = 0)
        {
            if ((thread == null || thread.IsCompleted) && ((taskStartIdx > -1) || (taskStopIdx > -1)))
            {
                runTaskStartIdx = taskStartIdx;
                runTaskStopIdx = taskStopIdx;
                runCount = count;
                cancelTokenSource = new CancellationTokenSource();
                thread = Task.Factory.StartNew(Upgrade, cancelTokenSource.Token);
            }
            return thread;
        }

        public void StopTasks()
        {
            cancelTokenSource.Cancel(true);
        }

        private void Upgrade()
        {
            UpgradeManagerData.Task.Result result = UpgradeManagerData.Task.Result.Null;
            try
            {
                TasksStarted?.Invoke();
                using (cancelTokenSource.Token.Register(Thread.CurrentThread.Abort))
                {
                    if (Panel.CurrentProfile != null && Panel.CurrentProfile.Tasks.Any())
                    {
                        result = Panel.CurrentProfile.Run(runTaskStartIdx, runTaskStopIdx, runCount);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Logger.WriteLine("Upgrade has been aborted!");
            }
#if DEBUG
            catch (Exception e)
            {
                QMessageBox.ShowError(e.ToString());
            }
#endif
            finally
            {
                TasksStopped?.Invoke();
                if (result < 0) Logger.WriteLine($"Upgrade failed with result: {result}");
            }
        }

        protected override void KeyboardHookDown(KeyEventArgs e)
        {
            if ((Core.IsAstralForeground || Core.IsGameForeground) && (Data.ToggleHotKey.Keys == e.KeyData))
            {
                ToggleTasks();
            }
        }
    }
}
