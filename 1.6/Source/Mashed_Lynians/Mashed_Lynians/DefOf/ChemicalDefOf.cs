using Verse;
using RimWorld;

namespace Mashed_Lynians
{
	[DefOf]
	public static class ChemicalDefOf
	{

		static ChemicalDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(ChemicalDefOf));
		}

		public static ChemicalDef Mashed_Lynian_FelvineChemical;
	}
}
