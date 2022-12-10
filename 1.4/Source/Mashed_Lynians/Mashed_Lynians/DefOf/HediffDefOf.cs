using Verse;
using RimWorld;

namespace Mashed_Lynians
{
	[DefOf]
	public static class HediffDefOf
	{

		static HediffDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(HediffDefOf));
		}

		public static HediffDef Mashed_Lynian_PilferedFelvine;
		public static HediffDef Mashed_Lynian_FelvineHighFrenzy;
		public static HediffDef Mashed_Lynian_FelvineTolerance;

		public static HediffDef Mashed_Lynian_LynianCookingFurrenzy;
		public static HediffDef Mashed_Lynian_LynianFarmingFurrenzy;
		public static HediffDef Mashed_Lynian_LynianCleaningFurrenzy;
		//public static HediffDef Mashed_Lynian_PurrserkerRage;
	}
}
