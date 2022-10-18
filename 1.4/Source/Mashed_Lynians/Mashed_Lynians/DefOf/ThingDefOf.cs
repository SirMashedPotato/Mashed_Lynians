using Verse;
using RimWorld;

namespace Mashed_Lynians
{
	[DefOf]
	public static class ThingDefOf
	{

		static ThingDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
		}

		public static ThingDef Mashed_Lynian_LynianFur;
		public static ThingDef Mashed_Lynian_LynianThickFur;
		public static ThingDef Mashed_Lynian_LynianLeather;
		public static ThingDef Mashed_Lynian_LynianFineFur;
		public static ThingDef Mashed_Lynian_PawnDigging;
	}
}
