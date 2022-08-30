using Verse;
using RimWorld;

namespace Mashed_Lynians
{
	[DefOf]
	public static class PawnKindDefOf
	{

		static PawnKindDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
		}

		public static PawnKindDef Mashed_Lynian_ShakalakaWanderer;
	}
}
