namespace QuesterAssistant.Classes.Monitoring
{
    internal static class Monitor
    {
        public static void Start()
        {
            GameCursorMoving.Start();
            Frames.Start();
        }

        public static void Stop()
        {
            GameCursorMoving.Stop();
            Frames.Stop();
        }
    }
}