using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    public class ThoughtWorker_MissingMask_GajalakaThought : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p) => MaskThoughtUtility.Valid(p) && !MaskThoughtUtility.WearingValidMask_Gajalaka(p);
    }

    public class ThoughtWorker_WearingMask_GajalakaThought : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p) => MaskThoughtUtility.Valid(p) && MaskThoughtUtility.WearingValidMask_Gajalaka(p);
    }
}
