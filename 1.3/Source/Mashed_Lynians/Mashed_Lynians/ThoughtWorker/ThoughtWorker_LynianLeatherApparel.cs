using RimWorld;
using Verse;
using System.Collections.Generic;

namespace Mashed_Lynians
{
	[StaticConstructorOnStartup]
	public static class LeatherList
	{
		public static HashSet<ThingDef> list = new HashSet<ThingDef>
		{
			ThingDefOf.Mashed_Lynian_LynianFur,
			ThingDefOf.Mashed_Lynian_LynianLeather,
			ThingDefOf.Mashed_Lynian_LynianThickFur,
			ThingDefOf.Mashed_Lynian_LynianFineFur
		};

		public static bool ContainsLeather(ThingDef def) => list.Contains(def);
    }

    public class ThoughtWorker_LynianLeatherApparel : ThoughtWorker
	{
		public static ThoughtState CurrentThoughtState(Pawn p)
		{
			/*
			if (!ESCP_Sload_ModSettings.LeatherThoughtSload)
			{
				return ThoughtState.Inactive;
			}
			*/
			string text = null;
			int num = 0;
			List<Apparel> wornApparel = p.apparel.WornApparel;
			for (int i = 0; i < wornApparel.Count; i++)
			{
				if (wornApparel[i].Stuff != null && LeatherList.ContainsLeather(wornApparel[i].Stuff))
				{
					if (text == null)
					{
						text = wornApparel[i].def.label;
					}
					num++;
				}
			}
			if (num == 0)
			{
				return ThoughtState.Inactive;
			}
			if (num >= 5)
			{
				return ThoughtState.ActiveAtStage(4, text);
			}
			return ThoughtState.ActiveAtStage(num - 1, text);
		}

		protected override ThoughtState CurrentStateInternal(Pawn p)
		{
			return ThoughtWorker_LynianLeatherApparel.CurrentThoughtState(p);
		}
	}
}
