using RimWorld;

namespace Mashed_Lynians
{
    public class CompProperties_IncrementRecord : CompProperties_AbilityEffect
    {
        public RecordDef recordDef;
        public CompProperties_IncrementRecord()
        {
            this.compClass = typeof(CompAbilityEffect_IncrementRecord);
        }
    }
}
