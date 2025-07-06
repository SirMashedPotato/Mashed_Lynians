using System.Collections.Generic;
using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    /// <summary>
    /// Basically an exact copy because IngestionOutcomeDoer_OffsetNeed.DoIngestionOutcomeSpecial is protected
    /// I guess I could've done this through Harmony patching, but eh
    /// TODO see if this can be redone through HAR stuff
	/// TODO int ingestedCount is new, probably need to update this
    /// </summary>
    public class IngestionOutcomeDoer_OffsetNeed_Felvine : IngestionOutcomeDoer_OffsetNeed
    {
        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
        {
            if (!Utility.PawnCanUseFelvine(pawn))
            {
                return;
            }
            if (pawn.needs != null && pawn.needs.TryGetNeed(this.need, out var need))
            {
                float effect = offset * ingestedCount;
                AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize(pawn, toleranceChemical, ref effect, applyGeneToleranceFactor: false);
                if (perIngested)
                {
                    effect *= ingested.stackCount;
                }
                need.CurLevel += effect;
            }
        }
    }
}
