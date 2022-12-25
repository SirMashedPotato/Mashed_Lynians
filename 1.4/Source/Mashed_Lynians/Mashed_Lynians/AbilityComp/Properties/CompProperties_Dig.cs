using RimWorld;

namespace Mashed_Lynians
{
    public class CompProperties_Dig : CompProperties_AbilityEffect
    {
        public int requiredLevel = 10;
        public CompProperties_Dig()
        {
            this.compClass = typeof(CompAbilityEffect_Dig);
        }
    }
}
