using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    public class CompProperties_UseEffectEurekacorn : CompProperties_UseEffect
    {
        public CompProperties_UseEffectEurekacorn()
        {
            this.compClass = typeof(CompUseEffect_Eurekacorn);
        }

        public AbilityDef ability;
        public HediffDef hediff;
        public bool fillHunger = false;
    }
}
