using QuesterAssistant.Classes.Common;
using System;

namespace QuesterAssistant.UpgradeManager
{
    internal class UpgradeManagerData : NotifyHashChanged, IParse<UpgradeManagerData>
    {
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
        }

        public void Parse(UpgradeManagerData source)
        {
        }
    }
}
