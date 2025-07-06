using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    public class CompProperties_AbilityGiveHediffToParts : CompProperties_AbilityEffectWithDuration
    {
        public CompProperties_AbilityGiveHediffToParts() => compClass = typeof(CompAbilityEffect_GiveHediffToParts);

        public BodyPartDef partsToAffect;
        public HediffDef hediffDef;
    }
}
