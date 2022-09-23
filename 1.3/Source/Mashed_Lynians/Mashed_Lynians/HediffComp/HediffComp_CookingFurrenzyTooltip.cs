using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class HediffComp_CookingFurrenzyTooltip : HediffComp
    {
        public override string CompTipStringExtra => "Mashed_Lynian_CookingFurrenzyTooltip".Translate(parent.pawn.GetStatValue(StatDefOf.CookSpeed).ToStringPercent());
    }
}
