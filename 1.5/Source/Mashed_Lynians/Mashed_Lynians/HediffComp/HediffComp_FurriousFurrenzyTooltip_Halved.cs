using Verse;
using RimWorld;

namespace Mashed_Lynians
{
	public class HediffComp_FurriousFurrenzyTooltip_Halved : HediffComp
	{
		public HediffCompProperties_FurriousFurrenzyTooltip Props
		{
			get
			{
				return (HediffCompProperties_FurriousFurrenzyTooltip)this.props;
			}
		}

		public override string CompTipStringExtra
        {
			get
			{
				string tooltip = null;

                if (!Props.statDefs.NullOrEmpty())
                {
					tooltip = "";
					foreach (StatDef statDef in Props.statDefs)
                    {
						tooltip += "Mashed_Lynian_WorkingFurrenzyTooltip_Halved".Translate(statDef.label, parent.pawn.GetStatValue(statDef).ToStringPercent()) + "\n";

					}
                }

				return tooltip;
			}
        }
	}
}
