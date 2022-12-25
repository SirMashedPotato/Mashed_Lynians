using Verse;

namespace Mashed_Lynians
{
    public class HediffComp_DisappearsOnDowned : HediffComp
    {
        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            if (base.Pawn.Spawned && !base.Pawn.Dead && base.Pawn.Downed)
            {
                base.Pawn.health.RemoveHediff(this.parent);
            }
        }
    }
}
