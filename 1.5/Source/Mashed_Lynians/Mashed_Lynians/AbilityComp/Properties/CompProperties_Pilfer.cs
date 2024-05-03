using RimWorld;
using Verse;
using System.Collections.Generic;

namespace Mashed_Lynians
{
    public class CompProperties_Pilfer : CompProperties_AbilityEffect
    {
        public CompProperties_Pilfer()
        {
            this.compClass = typeof(CompAbilityEffect_Pilfer);
        }

        public List<ThingDef> guaranteedPilfers;
    }
}
