using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_RemoveHediffs : CompAbilityEffect
    {
        public new CompProperties_AbilityRemoveHediffs Props => (CompProperties_AbilityRemoveHediffs)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            Pawn p = parent.pawn;
            if (target.Pawn != null && target.Pawn == p)
            {
                foreach (HediffDef hediffDef in Props.hediffDefs)
                {
                    RemoveHediff(p, hediffDef);
                }
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
