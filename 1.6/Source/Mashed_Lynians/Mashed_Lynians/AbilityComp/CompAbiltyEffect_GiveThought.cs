using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompAbiltyEffect_GiveThought : CompAbilityEffect
    {
        public new CompProperties_AbilityGiveThought Props
        {
            get
            {
                return (CompProperties_AbilityGiveThought)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target != null && target.Pawn != null && target.Pawn.needs.mood != null)
            {
                if (Props.thoughtDef != null)
                {
                    GiveThought(target.Pawn);
                }
            }
        }

        private void GiveThought(Pawn pawn)
        {
            if (Props.replaceExisting)
            {
                pawn.needs.mood.thoughts.memories.RemoveMemoriesOfDef(Props.thoughtDef);
            }
            pawn.needs.mood.thoughts.memories.TryGainMemory(Props.thoughtDef, null, null);
            if (Props.masterHediff != null && parent.pawn.health.hediffSet.GetFirstHediffOfDef(Props.masterHediff) != null)
            {
                pawn.needs.mood.thoughts.memories.TryGainMemory(Props.thoughtDef, null, null);
            }
        }
    }
}
