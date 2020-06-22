using System.Collections.Generic;
using QuesterAssistant.Classes.Common;

namespace QuesterAssistant.Classes
{
    internal class DebugAction
    {
        private readonly List<string> infos = new List<string>();
        private char nestedStr = '\t';
        private int nestingLevel = 0;

        public void AddInfo(string info, int changeNesting = 0)
        {
            infos.Add(new string(nestedStr, nestingLevel) + info);
            nestingLevel = MathTools.Max(nestingLevel + changeNesting, 0);
        }

        public string Infos => string.Join("\n", infos);

        public void ClearInfos()
        {
            infos.Clear();
        }
    }
}