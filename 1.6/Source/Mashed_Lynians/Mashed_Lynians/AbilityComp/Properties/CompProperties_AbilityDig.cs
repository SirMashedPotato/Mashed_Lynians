using RimWorld;

namespace Mashed_Lynians
{
    public class CompProperties_AbilityDig : CompProperties_AbilityEffect
    {
        public CompProperties_AbilityDig() => compClass = typeof(CompAbilityEffect_Dig);

        public int requiredLevel = 10;
        public SkillDef skillDef;
    }
}
