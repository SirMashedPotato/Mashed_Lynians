using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    public class ThoughtWorker_MissingMask_GoldenGajalakaThought : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p) => MaskThoughtUtility.Valid(p) && !MaskThoughtUtility.WearingValidMask_GoldenGajalaka(p);
    }

    public class ThoughtWorker_WearingMask_GoldenGajalakaThought : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p) => MaskThoughtUtility.Valid(p) && MaskThoughtUtility.WearingValidMask_GoldenGajalaka(p);
    }
}
