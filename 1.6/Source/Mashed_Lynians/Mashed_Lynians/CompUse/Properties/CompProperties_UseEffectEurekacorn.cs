using RimWorld;

namespace Mashed_Lynians
{
    public class CompProperties_UseEffectEurekacorn : CompProperties_UseEffect
    {
        public CompProperties_UseEffectEurekacorn()
        {
            compClass = typeof(CompUseEffect_Eurekacorn);
        }

        public int skillPointCount = 1;
        public bool fillHunger = false;
    }
}
