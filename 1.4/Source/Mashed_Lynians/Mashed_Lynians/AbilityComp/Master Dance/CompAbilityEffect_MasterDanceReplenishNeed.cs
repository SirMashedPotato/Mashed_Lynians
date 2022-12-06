using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_MasterDanceReplenishNeed : CompAbilityEffect
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
                    if (Props.needDef != null)
                    {
                        ReplenishNeed(target.Pawn);
                    }
                }
            }
        }

        private void ReplenishNeed(Pawn pawn)
        {
            Need need= pawn.needs.TryGetNeed(Props.needDef);
            if (need != null)
            {
                need.CurLevel += need.MaxLevel * Props.severity;
            }
        }
    }
}
