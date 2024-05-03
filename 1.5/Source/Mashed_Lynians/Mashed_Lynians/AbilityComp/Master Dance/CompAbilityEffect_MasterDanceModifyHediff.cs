using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    class CompAbilityEffect_MasterDanceModifyHediff : CompAbilityEffect
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
                    if (Props.affectedHediffDef != null)
                    {
                        ModifyHediffSeverity(target.Pawn);
                    }
                }
            }
        }

        private void ModifyHediffSeverity(Pawn pawn)
        {
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(Props.affectedHediffDef);
            if (hediff != null)
            {
                hediff.Severity = Props.severity;
            }
        }
    }
}
