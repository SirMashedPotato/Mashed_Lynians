using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Mashed_Lynians
{
    public class CompProperties_AbilityRemoveHediffs : CompProperties_AbilityEffect
    {
        public CompProperties_AbilityRemoveHediffs() => compClass = typeof(CompAbilityEffect_RemoveHediffs);

        public List<HediffDef> hediffDefs = new List<HediffDef>();
    }
}
