namespace QuesterAssistant.Classes.Monitoring
{
    internal static class Monitor
    {
        public static readonly Foreground Foreground = new Foreground();

        public static void Start()
        {
            GameCursorMoving.Start();
        }

        public static void Stop()
        {
            GameCursorMoving.Stop();
        }
    }
}