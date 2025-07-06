using RimWorld;

namespace Mashed_Lynians
{
    public class CompProperties_AbilityIncrementRecord : CompProperties_AbilityEffect
    {
        public CompProperties_AbilityIncrementRecord() => compClass = typeof(CompAbilityEffect_IncrementRecord);

        public RecordDef recordDef;
    }
}
