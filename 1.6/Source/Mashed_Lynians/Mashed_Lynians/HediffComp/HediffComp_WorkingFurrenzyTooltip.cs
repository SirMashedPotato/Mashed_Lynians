using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class HediffComp_WorkingFurrenzyTooltip : HediffComp
    {
		public HediffCompProperties_WorkingFurrenzyTooltip Props
		{
			get
			{
				return (HediffCompProperties_WorkingFurrenzyTooltip)this.props;
			}
		}

		public override string CompTipStringExtra => "Mashed_Lynian_WorkingFurrenzyTooltip".Translate(Props.statDef.label, parent.pawn.GetStatValue(Props.statDef).ToStringPercent());
    }
}
