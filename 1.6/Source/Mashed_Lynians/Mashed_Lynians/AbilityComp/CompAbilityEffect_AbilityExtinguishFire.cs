using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_AbilityExtinguishFire : CompAbilityEffect
    {
        public new CompProperties_AbilityCleanFilth Props
        {
            get
            {
                return (CompProperties_AbilityCleanFilth)this.props;
            }
        }


        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target.Thing != null && target.Thing is Fire fire)
            {
                FleckMaker.ThrowDustPuff(target.Cell, target.Thing.Map, 1f);
                fire.Destroy();
                parent.pawn.records.Increment(RimWorld.RecordDefOf.FiresExtinguished);
            }
        }
    }
}
