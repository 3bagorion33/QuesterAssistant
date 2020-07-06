using Astral.Logic.NW;
using Astral.Quester.Classes;
using QuesterAssistant.Classes;

namespace QuesterAssistant.Conditions
{
    public class FreeOrderSlots : Condition
    {
        private int Count => 3 - Professions2.CurrentOrders.Count;
        public override void Reset() { }

        public override bool IsValid
        {
            get
            {
                ProfessionsHelper.RefreshAssignments();
                switch (Sign)
                {
                    case Relation.Equal:
                        return Count == Value;
                    case Relation.NotEqual:
                        return Count != Value;
                    case Relation.Inferior:
                        return Count < Value;
                    case Relation.Superior:
                        return Count > Value;
                    default:
                        return false;
                }
            }
        }

        public override string TestInfos => $"Free order slots : {Count}";
        public override string ToString() => $"Free order slots {Sign} to {Value}";

        public Relation Sign { get; set; }
        public int Value { get; set; }
    }
}