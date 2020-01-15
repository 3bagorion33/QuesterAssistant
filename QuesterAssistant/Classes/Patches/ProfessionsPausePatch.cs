namespace QuesterAssistant.Classes.Patches
{
    internal class ProfessionsPausePatch
    {
        internal static void Astral_Professions_FSM_States_Main_RandomPause(int min, int max)
        {
            if (min != 5 && max == min + 5)
            {
                Pause.Sleep(250);
                return;
            }
            Pause.RandomSleep(min * 1000, max * 1000);
        }
    }
}
