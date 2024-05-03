using Verse;

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
			Pawn pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(
				kind: kindDef,
				tile: tile,
				colonistRelationChanceFactor: 0f,
				developmentalStages: DevelopmentalStage.Child));
			pawn.ageTracker.AgeChronologicalTicks = pawn.ageTracker.AgeBiologicalTicks;
			return pawn;
		}
	}
}
