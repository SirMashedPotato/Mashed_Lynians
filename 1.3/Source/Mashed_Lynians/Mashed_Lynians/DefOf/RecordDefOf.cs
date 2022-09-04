using RimWorld;

namespace Mashed_Lynians
{
	[DefOf]
	public static class RecordDefOf
	{

		static RecordDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(RecordDefOf));
		}

		public static RecordDef Mashed_Lynian_PilferedNumber;
		public static RecordDef Mashed_Lynian_PilferedValue;
		public static RecordDef Mashed_Lynian_DiggingDistance;
		public static RecordDef Mashed_Lynian_GrowthPromoted;
	}
}
