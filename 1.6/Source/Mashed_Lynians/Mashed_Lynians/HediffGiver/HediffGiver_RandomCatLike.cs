using Verse;

namespace Mashed_Lynians
{
    public class HediffGiver_RandomCatLike : HediffGiver
    {
		public override void OnIntervalPassed(Pawn pawn, Hediff cause)
		{
			if (Rand.MTBEventOccurs(mtbDays, 60000f, 60f) && Mashed_Lynians_ModSettings.EnableHairballs && CanApply(pawn) && TryApply(pawn, null))
			{
				SendLetter(pawn, cause);
			}
		}

		public bool CanApply(Pawn pawn)
        {
			return Utility.PawnIsCatLike(pawn);
        }

		public float mtbDays;
	}

}
