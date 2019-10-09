namespace QuesterAssistant.Classes.Common
{
    interface IParse<T> where T : class
    {
        void Parse(T source);
        void Init();
    }
}
