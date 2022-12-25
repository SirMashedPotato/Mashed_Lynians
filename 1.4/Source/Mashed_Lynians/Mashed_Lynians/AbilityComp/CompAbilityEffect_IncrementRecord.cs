using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_IncrementRecord : CompAbilityEffect
    {
        public new CompProperties_IncrementRecord Props
        {
            get
            {
                return (CompProperties_IncrementRecord)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (Props.recordDef != null)
            {
                Pawn user = parent.pawn;
                if (target.Pawn != null && target.Pawn == user)
                {
                    user.records.Increment(Props.recordDef);
                }
            }
        }
    }
}