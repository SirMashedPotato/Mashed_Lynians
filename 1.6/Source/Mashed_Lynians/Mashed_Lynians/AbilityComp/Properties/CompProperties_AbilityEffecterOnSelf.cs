using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompProperties_AbilityEffecterOnSelf : CompProperties_AbilityEffect
	{
		public CompProperties_AbilityEffecterOnSelf()
		{
			compClass = typeof(CompAbilityEffect_EffecterOnSelf);
		}

		public EffecterDef effecterDef;
		public int maintainForTicks = -1;
		public float scale = 1f;
	}
}
