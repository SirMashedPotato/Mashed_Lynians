using RimWorld;
using Verse;
using System.Collections.Generic;

namespace Mashed_Lynians
{
    public class CompProperties_AbilityPilfer : CompProperties_AbilityEffect
    {
        public CompProperties_AbilityPilfer() => compClass = typeof(CompAbilityEffect_Pilfer);

        public List<ThingDef> guaranteedPilfers;
    }
}
