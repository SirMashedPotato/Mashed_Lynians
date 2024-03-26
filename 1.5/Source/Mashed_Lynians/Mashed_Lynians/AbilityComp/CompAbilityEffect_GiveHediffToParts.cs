using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_GiveHediffToParts : CompAbilityEffect_WithDuration
    {
        public new CompProperties_AbilityGiveHediffToParts Props
        {
            get
            {
                return (CompProperties_AbilityGiveHediffToParts)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target.Thing is Pawn p)
            {
                if (Props.partsToAffect != null && Props.hediffDef != null)
                {
                    foreach (BodyPartRecord bodyPartRecord in p.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null))
                    {
                        if (bodyPartRecord.def == Props.partsToAffect)
                        {
                            Hediff hediff = HediffMaker.MakeHediff(Props.hediffDef, p, bodyPartRecord);
                            HediffComp_Disappears hediffComp_Disappears = hediff.TryGetComp<HediffComp_Disappears>();
                            if (hediffComp_Disappears != null)
                            {
                                hediffComp_Disappears.ticksToDisappear = GetDurationSeconds(p).SecondsToTicks();
                            }
                            p.health.AddHediff(hediff, null, null, null);
                        }
                    }
                }
            }
           
        }
    }
}
