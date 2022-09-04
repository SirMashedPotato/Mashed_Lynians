using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    public class ThoughtWorker_MissingMask_ShakalakaThought : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p) => MaskThoughtUtility.Valid(p.def) && !MaskThoughtUtility.WearingValidMask_Shakalaka(p);
    }

    public class ThoughtWorker_WearingMask_ShakalakaThought : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p) => MaskThoughtUtility.Valid(p.def) && MaskThoughtUtility.WearingValidMask_Shakalaka(p);
    }
}
