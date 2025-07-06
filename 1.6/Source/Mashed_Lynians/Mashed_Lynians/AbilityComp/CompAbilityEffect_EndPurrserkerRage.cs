using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_EndPurrserkerRage : CompAbilityEffect
    {
        public new CompProperties_EndPurrserkerRage Props
        {
            get
            {
                return (CompProperties_EndPurrserkerRage)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            Pawn p = parent.pawn;
            if (target.Pawn != null && target.Pawn == p)
            {
                RemoveHediff(p, HediffDefOf.Mashed_Lynian_PurrserkerRage);
                RemoveHediff(p, HediffDefOf.Mashed_Lynian_PurrserkerClaws);
                RemoveHediff(p, HediffDefOf.Mashed_Lynian_PurrserkerClaws);
            }
        }

        public void RemoveHediff(Pawn p, HediffDef def)
        {
            Hediff h = p.health.hediffSet.GetFirstHediffOfDef(def);
            if (h != null)
            {
                p.health.RemoveHediff(h);
            }
        }
    }
}
