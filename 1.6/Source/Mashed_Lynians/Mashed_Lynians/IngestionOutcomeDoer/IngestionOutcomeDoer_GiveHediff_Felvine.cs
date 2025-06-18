using System.Collections.Generic;
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
    public class IngestionOutcomeDoer_GiveHediff_Felvine : IngestionOutcomeDoer
    {
		protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
		{
			if (!Utility.PawnCanUseFelvine(pawn))
			{
                if (altHediffDef != null)
                {
					pawn.health.AddHediff(altHediffDef);
				}
				return;
			}
			Hediff hediff = HediffMaker.MakeHediff(this.hediffDef, pawn, null);
			float num;
			if (this.severity > 0f)
			{
				num = this.severity;
			}
			else
			{
				num = this.hediffDef.initialSeverity;
			}
			if (this.divideByBodySize)
			{
				num /= pawn.BodySize;
			}
			AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize_NewTemp(pawn, this.toleranceChemical, ref num, multiplyByGeneToleranceFactors);
			hediff.Severity = num;
			pawn.health.AddHediff(hediff, null, null, null);
		}

		public override IEnumerable<StatDrawEntry> SpecialDisplayStats(ThingDef parentDef)
		{
			if (parentDef.IsDrug && this.chance >= 1f)
			{
				foreach (StatDrawEntry statDrawEntry in this.hediffDef.SpecialDisplayStats(StatRequest.ForEmpty()))
				{
					yield return statDrawEntry;
				}
            }
		}

		public HediffDef hediffDef;
		public HediffDef altHediffDef;
		public float severity = -1f;
		public ChemicalDef toleranceChemical;
		private bool divideByBodySize;
		public bool multiplyByGeneToleranceFactors;
	}
}
