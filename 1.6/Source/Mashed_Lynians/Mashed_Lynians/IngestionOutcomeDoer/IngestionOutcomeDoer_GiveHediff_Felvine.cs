using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    /// <summary>
    /// Basically an exact copy because IngestionOutcomeDoer_GiveHediff.DoIngestionOutcomeSpecial is protected
    /// I guess I could've done this through Harmony patching, but eh
	/// TODO see if this can be redone through HAR stuff
	/// TODO int ingestedCount is new, probably need to update this
    /// </summary>
    public class IngestionOutcomeDoer_GiveHediff_Felvine : IngestionOutcomeDoer_GiveHediff
    {
		protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
		{
            //TODO find an alternative to doing this
			if (!Utility.PawnCanUseFelvine(pawn))
			{
                if (altHediffDef != null)
                {
					pawn.health.AddHediff(altHediffDef);
				}
				return;
			}
            Hediff hediff = HediffMaker.MakeHediff(hediffDef, pawn);
            float effect = ((!(severity > 0f)) ? hediffDef.initialSeverity : severity);
            AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize(pawn, toleranceChemical, ref effect, multiplyByGeneToleranceFactors, divideByBodySize);
            hediff.Severity = effect;
            pawn.health.AddHediff(hediff);
        }

		public HediffDef altHediffDef;
		private bool divideByBodySize;
	}
}
