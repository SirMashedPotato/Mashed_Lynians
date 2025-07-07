﻿using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompProperties_AbilityDoDamage : CompProperties_AbilityEffect
    {
        public CompProperties_AbilityDoDamage() => compClass = typeof(CompAbilityEffect_DoDamage);

        public DamageDef damageDef;
        public float damageAmount = 10f;
        public bool onlyHostile = true;
    }
}
