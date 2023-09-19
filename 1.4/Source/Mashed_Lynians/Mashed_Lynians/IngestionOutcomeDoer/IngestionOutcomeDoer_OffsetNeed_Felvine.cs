using System.Collections.Generic;
using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    /// <summary>
    /// Basically an exact copy because IngestionOutcomeDoer_OffsetNeed.DoIngestionOutcomeSpecial is protected
    /// I guess I could've done this through Harmony patching, but eh
    /// </summary>
    public class IngestionOutcomeDoer_OffsetNeed_Felvine : IngestionOutcomeDoer
    {
        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
        {
            if (pawn.needs == null)
            {
                return;
            }
            if (!Utility.PawnCanUseFelvine(pawn))
            {
                return;
            }
            Need need = pawn.needs.TryGetNeed(this.need);
            if (need == null)
            {
                return;
            }
            float num = this.offset;
            AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize_NewTemp(pawn, this.toleranceChemical, ref num, false);
            if (this.perIngested)
            {
                num *= (float)ingested.stackCount;
            }
            need.CurLevel += num;
        }

        public override IEnumerable<StatDrawEntry> SpecialDisplayStats(ThingDef parentDef)
        {
            string str = (this.offset >= 0f) ? "+" : string.Empty;
            yield return new StatDrawEntry(StatCategoryDefOf.Drug, this.need.LabelCap, str + this.offset.ToStringPercent(), this.need.description, this.need.listPriority, null, null, false);
        }

        public NeedDef need;
        public float offset;
        public ChemicalDef toleranceChemical;
        public bool perIngested;
    }
}
