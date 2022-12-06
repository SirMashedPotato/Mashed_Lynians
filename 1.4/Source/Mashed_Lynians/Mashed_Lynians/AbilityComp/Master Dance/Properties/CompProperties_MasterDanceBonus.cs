using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompProperties_MasterDanceBonus : CompProperties_AbilityEffectWithDuration
    {
        public HediffDef requiredHediffDef;
        public HediffDef affectedHediffDef;
        public HediffDef addedHediffDef;
        public NeedDef needDef;
        public bool replaceExisting = true;
        public bool onlyBrain = true;
        public float severityReduction = 1f;
        public float severity = 0.5f;
        public float needIncreaseFactor = 0.15f;
        public bool onlyHostile = false;
    }
}
