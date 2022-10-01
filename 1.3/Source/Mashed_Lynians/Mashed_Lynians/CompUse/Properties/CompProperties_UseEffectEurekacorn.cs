using RimWorld;

namespace Mashed_Lynians
{
    public class CompProperties_UseEffectEurekacorn : CompProperties_UseEffectArtifact
    {
        public CompProperties_UseEffectEurekacorn()
        {
            this.compClass = typeof(CompUseEffect_Eurekacorn);
        }

        public AbilityDef ability;
    }
}
