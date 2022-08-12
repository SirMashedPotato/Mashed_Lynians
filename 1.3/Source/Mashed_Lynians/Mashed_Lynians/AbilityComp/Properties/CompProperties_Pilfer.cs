﻿using RimWorld;
using Verse;
using System.Collections.Generic;

namespace Mashed_Lynians
{
    class CompProperties_Pilfer : CompProperties_AbilityEffect
    {
        public CompProperties_Pilfer()
        {
            this.compClass = typeof(CompAbilityEffect_Pilfer);
        }

        public List<ThingDef> guaranteedPilfers;
        public HediffDef guaranteedPilferHediff;
        public StatDef stat;
    }
}
