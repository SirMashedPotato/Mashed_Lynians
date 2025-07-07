using RimWorld;

namespace Mashed_Lynians
{
	[DefOf]
	public static class HistoryEventDefOf
	{
		static HistoryEventDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(HistoryEventDefOf));

		public static HistoryEventDef Mashed_Lynian_AttemptedPilfer;
	}
}
