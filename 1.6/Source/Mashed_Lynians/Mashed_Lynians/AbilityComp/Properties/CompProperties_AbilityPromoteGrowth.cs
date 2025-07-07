using RimWorld;

namespace Mashed_Lynians
{
    public class CompProperties_AbilityPromoteGrowth : CompProperties_AbilityEffect
    {
        public CompProperties_AbilityPromoteGrowth() => compClass = typeof(CompAbilityEffect_PromoteGrowth);

        public float growthPerLevel = 0.01f;
        public float growthPerOverLevel = 0.001f;
        public SkillDef skillDef;
    }
}
