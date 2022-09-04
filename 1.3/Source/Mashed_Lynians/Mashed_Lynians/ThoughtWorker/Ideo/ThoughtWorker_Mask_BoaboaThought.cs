using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    public class ThoughtWorker_MissingMask_BoaboaThought : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p) => MaskThoughtUtility.Valid(p.def) && !MaskThoughtUtility.WearingValidMask_Boaboa(p);
    }

    public class ThoughtWorker_WearingMask_BoaboaThought : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p) => MaskThoughtUtility.Valid(p.def) && MaskThoughtUtility.WearingValidMask_Boaboa(p);
    }
}
