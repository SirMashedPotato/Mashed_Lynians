using RimWorld;

namespace Mashed_Lynians
{
	[DefOf]
	public static class ThoughtDefOf
	{

		static ThoughtDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(ThoughtDefOf));
		}

		public static ThoughtDef Mashed_Lynian_AttemptedPilfer;
	}
}
