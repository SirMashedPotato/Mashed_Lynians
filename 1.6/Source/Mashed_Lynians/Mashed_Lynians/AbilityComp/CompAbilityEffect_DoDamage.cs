﻿using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    class CompAbilityEffect_DoDamage : CompAbilityEffect
    {
        public new CompProperties_AbilityDoDamage Props => (CompProperties_AbilityDoDamage)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (Props.damageDef != null)
            {
                if (target.Pawn != null)
                {
                    if (Props.onlyHostile && !target.Pawn.HostileTo(parent.pawn))
                    {
                        return;
                    }
                    DamageInfo damageInfo = new DamageInfo
                    {
                        Def = Props.damageDef
                    };
                    damageInfo.SetAmount(Props.damageAmount);
                    target.Pawn.TakeDamage(damageInfo);
                }
            }
        }
    }
}