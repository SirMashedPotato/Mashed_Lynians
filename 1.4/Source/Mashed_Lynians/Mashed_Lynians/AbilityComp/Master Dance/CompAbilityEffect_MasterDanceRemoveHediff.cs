using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_MasterDanceRemoveHediff : CompAbilityEffect
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
                        ReduceHediffSeverity(target.Pawn);
                    }
                }
            }
        }

        private void ReduceHediffSeverity(Pawn pawn)
        {
            Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(Props.affectedHediffDef);
            if (hediff != null)
            {
                hediff.Severity -= Props.needIncreaseFactor;
            }
        }
    }
}
