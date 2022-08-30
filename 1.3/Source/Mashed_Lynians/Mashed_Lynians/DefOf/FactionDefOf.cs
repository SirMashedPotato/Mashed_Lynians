using RimWorld;

namespace Mashed_Lynians
{
	[DefOf]
	public static class FactionDefOf
	{

		static FactionDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(ChemicalDefOf));
		}

		//public static FactionDef Mashed_Lynian_NonPlayerShakalakaWanderers;
	}
}
