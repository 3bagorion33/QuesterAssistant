using QuesterAssistant.Classes;

namespace QuesterAssistant.InsigniaManager
{
    internal class InsigniaManagerCore : ACore<InsigniaManagerData, InsigniaManagerForm>
    {
        protected override bool IsValid => true;
        protected override bool HookEnableFlag { get; }

        public void InsertInsignias()
        {

        }
    }
}