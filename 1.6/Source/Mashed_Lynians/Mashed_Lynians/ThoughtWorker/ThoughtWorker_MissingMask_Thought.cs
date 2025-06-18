using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    public class ThoughtWorker_MissingMask_Thought : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p) => MaskThoughtUtility.Valid(p) && !MaskThoughtUtility.Satisfied(p);
    }

    public class ThoughtWorker_WearingMask_Thought : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p) => MaskThoughtUtility.Valid(p) && MaskThoughtUtility.Satisfied(p);
    }

    public static class MaskThoughtUtility
    {
        public static bool Valid(Pawn p) => ValidNow(p) && Utility.PawnIsLynian(p);

        public static bool ValidNow(Pawn p) => ExpectationsUtility.CurrentExpectationFor(p).order > 0;

        public static bool Satisfied(Pawn p) => p.apparel.BodyPartGroupIsCovered(BodyPartGroupDefOf.FullHead);
    }
}
