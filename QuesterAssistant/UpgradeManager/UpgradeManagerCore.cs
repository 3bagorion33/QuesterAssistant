using Astral;
using QuesterAssistant.Classes;
using QuesterAssistant.Panels;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuesterAssistant.UpgradeManager
{
    internal class UpgradeManagerCore : ACore<UpgradeManagerData, UpgradeManagerForm>
    {
        protected override bool IsValid => true; // Data.Profiles?.Any() ?? false;
        protected override bool HookEnableFlag => false;

        public event Action TasksStarted;
        public event Action TasksStopped;
        public bool TasksIsRunning => !thread?.IsCompleted ?? false;

        private Task thread;
        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        private UpgradeManagerData.Profile runProfile;
        private int runTaskStartIdx;
        private int runTaskStopIdx;
        private int runCount;

        public void StartTasks(UpgradeManagerData.Profile profile, int taskStartIdx = -1, int taskStopIdx = -1, int count = 0)
        {
            if ((thread == null || thread.IsCompleted) && ((taskStartIdx > -1) || (taskStopIdx > -1)))
            {
                runProfile = profile;
                runTaskStartIdx = taskStartIdx;
                runTaskStopIdx = taskStopIdx;
                runCount = count;
                cancelTokenSource = new CancellationTokenSource();
                thread = Task.Factory.StartNew(Upgrade, cancelTokenSource.Token);
            }
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
                    int count = (runCount == 0) ? runProfile.IterationsCount : runCount;

                    if (runTaskStopIdx < 0)
                        runTaskStopIdx = runProfile.Tasks.Count - 1;

                    if (runTaskStartIdx < 0)
                        runTaskStartIdx = 0;

                    void UpToDown(int start, int stop)
                    {
                        for (int j = start; j <= stop; j++)
                        {
                            int i = 0;
                            while ((i < count) && ((result = runProfile.Tasks[j].Run()) > 0))
                            {
                                if (result == UpgradeManagerData.Task.Result.Evolved) i++;
                            }
                        }
                    }

                    if (runProfile.Algorithm == UpgradeManagerData.Profile.AlgorithmDirection.UpToDown || runTaskStartIdx == runTaskStopIdx)
                    {
                        UpToDown(runTaskStartIdx, runTaskStopIdx);
                        return;
                    }
                    if (runProfile.Algorithm == UpgradeManagerData.Profile.AlgorithmDirection.DownToUp)
                    {
                        int j = 0;
                        do
                        {
                            for (int i = runTaskStopIdx; i >= runTaskStartIdx; i--)
                            {
                                result = runProfile.Tasks[i].Run();
                                if (result == UpgradeManagerData.Task.Result.Evolved)
                                {
                                    UpToDown(i + 1, runTaskStopIdx);
                                    j++;
                                    break;
                                }
                            }
                        }
                        while (j < count && result > 0);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Logger.WriteLine("Upgrade has been aborted!");
            }
            catch (Exception e)
            {
                ErrorBox.Show(e.ToString());
            }
            finally
            {
                TasksStopped?.Invoke();
                if (result < 0) Logger.WriteLine($"Upgrade failed with result: {result}");
            }
        }

        protected override void KeyboardHook(object sender, KeyEventArgs e)
        {
        }
    }
}
