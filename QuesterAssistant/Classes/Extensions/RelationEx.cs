using Condition = Astral.Quester.Classes.Condition;

namespace QuesterAssistant.Classes.Extensions
{
    internal static class RelationEx
    {
        public static bool Compare(this Condition.Relation relation, dynamic a, dynamic b)
        {
            switch (relation)
            {
                case Condition.Relation.Equal:
                    return a == b;
                case Condition.Relation.NotEqual:
                    return a != b;
                case Condition.Relation.Inferior:
                    return a < b;
                case Condition.Relation.Superior:
                    return a > b;
                default:
                    return false;
            }
        }
    }
}