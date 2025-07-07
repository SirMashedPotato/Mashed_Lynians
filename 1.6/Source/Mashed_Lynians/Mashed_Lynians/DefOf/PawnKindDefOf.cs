using Verse;
using RimWorld;

namespace Mashed_Lynians
{
	[DefOf]
	public static class PawnKindDefOf
	{
		static PawnKindDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));

		public static PawnKindDef Mashed_Lynian_ShakalakaWanderer;

		public static PawnKindDef Mashed_Lynian_BoaboaColonist;
		public static PawnKindDef Mashed_Lynian_FelyneColonist;
		public static PawnKindDef Mashed_Lynian_GajalakaColonist;
		public static PawnKindDef Mashed_Lynian_GrimalkyneColonist;
		public static PawnKindDef Mashed_Lynian_MelynxColonist;
		public static PawnKindDef Mashed_Lynian_ShakalakaColonist;
		public static PawnKindDef Mashed_Lynian_UrukiColonist;
	}
}
