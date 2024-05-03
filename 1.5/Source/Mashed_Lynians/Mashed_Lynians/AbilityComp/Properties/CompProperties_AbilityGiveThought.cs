using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompProperties_AbilityGiveThought : CompProperties_AbilityEffectWithDuration
    {
        public CompProperties_AbilityGiveThought()
        {
            this.compClass = typeof(CompAbiltyEffect_GiveThought);
        }

        public ThoughtDef thoughtDef;
        public HediffDef masterHediff;
        public bool replaceExisting = true;
    }
}
