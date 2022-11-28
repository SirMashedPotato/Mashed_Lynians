using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public static class SiteUtility
    {
		/// <summary>
		/// Only used if Biotech is active
		/// Ensures pawn is always generated as a child
		/// </summary>
		public static Pawn GenerateChildPawn(int tile, PawnKindDef kindDef)
		{
			Pawn pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(kindDef, null, PawnGenerationContext.NonPlayer, tile,
				true, false, true, true, false, 1f, false, true, false, true, true, false, false, false, false, 0f, 0f, null, 1f, null, null, null, null, null, null,
				null, null, null, null, null, null, false, false, false, false, null, null, null, null, null, 0f, DevelopmentalStage.Adult, null, null, null, true)
			{
				AllowedDevelopmentalStages = DevelopmentalStage.Child
			});
			pawn.ageTracker.AgeChronologicalTicks = pawn.ageTracker.AgeBiologicalTicks;
			return pawn;
		}
	}
}
