using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    class CompAbilityEffect_MasterDanceAddHediff : CompAbilityEffect_WithDuration
    {
        public new CompProperties_MasterDanceBonus Props
        {
            get
            {
                return (CompProperties_MasterDanceBonus)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            Pawn user = parent.pawn;
            if (Props.requiredHediffDef != null && user.health.hediffSet.GetFirstHediffOfDef(Props.requiredHediffDef) != null)
            {
                if (target != null && target.Pawn != null)
                {
                    if (Props.addedHediffDef != null)
                    {
                        AddHediff(target.Pawn);
                    }
                }
            }
        }

        private void AddHediff(Pawn pawn)
        {
            if (Props.replaceExisting)
            {
                Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(Props.addedHediffDef, false);
                if (firstHediffOfDef != null)
                {
                    pawn.health.RemoveHediff(firstHediffOfDef);
                }
            }
            Hediff hediff = HediffMaker.MakeHediff(Props.addedHediffDef, pawn, Props.onlyBrain ? pawn.health.hediffSet.GetBrain() : null);
            HediffComp_Disappears hediffComp_Disappears = hediff.TryGetComp<HediffComp_Disappears>();
            if (hediffComp_Disappears != null)
            {
                hediffComp_Disappears.ticksToDisappear = GetDurationSeconds(pawn).SecondsToTicks();
            }
            if (Props.severity >= 0f)
            {
                hediff.Severity = Props.severity;
            }
            pawn.health.AddHediff(hediff, null, null, null);
        }
    }
}
