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
	}
}
