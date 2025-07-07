using Verse;

namespace Mashed_Lynians
{
    public class HediffComp_DisappearsOnDowned : HediffComp
    {
        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (Pawn.Spawned && !Pawn.Dead && Pawn.Downed)
            {
                Pawn.health.RemoveHediff(parent);
            }
        }
    }
}
