using RimWorld;

namespace Mashed_Lynians
{
	[DefOf]
	public static class StatDefOf
	{
		static StatDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(StatDefOf));

		public static StatDef Mashed_Lynian_PilferChance;
		public static StatDef CookSpeed;
	}
}
