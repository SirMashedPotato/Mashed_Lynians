using Verse;

namespace Mashed_Lynians
{
    public class HediffComp_HuntersMark : HediffComp
    {
        public override void Notify_PawnPostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            base.Notify_PawnPostApplyDamage(dinfo, totalDamageDealt);
            parent.pawn.health.RemoveHediff(parent);
        }
    }
}
