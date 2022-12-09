using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class StockGenerator_Palicoes : StockGenerator
    {
		/// <summary>
		/// Make sure to never add the same pawnKind as a possible caravan member
		/// </summary>
		public override IEnumerable<Thing> GenerateThings(int forTile, Faction faction = null)
		{
			if (respectPopulationIntent && Rand.Value > StorytellerUtilityPopulation.PopulationIntent)
			{
				yield break;
			}
			int count = countRange.RandomInRange;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
                if (faction == null)
                {
					if (!(from fac in Find.FactionManager.AllFactionsVisible
						  where fac != Faction.OfPlayer && fac.def.humanlikeFaction && !fac.temporary
						  select fac).TryRandomElement(out faction))
					{
						yield break;
					}
				}
				DevelopmentalStage developmentalStages = Find.Storyteller.difficulty.ChildrenAllowed ? (DevelopmentalStage.Child | DevelopmentalStage.Adult) : DevelopmentalStage.Adult;
				PawnGenerationRequest request = new PawnGenerationRequest(pawnKindDef, faction, PawnGenerationContext.NonPlayer, 
					forTile, false, false, false, true, false, 1f, !trader.orbital, true, false, true, true, false, false, false, false, 0f, 0f, null, 1f, null, null, null, null, null, null, 
					null, null, null, null, null, null, false, false, false, false, null, null, null, null, null, 0f, developmentalStages, null, null, null, false);
				yield return PawnGenerator.GeneratePawn(request);
				num = i;
			}
			yield break;
		}

		public override bool HandlesThingDef(ThingDef thingDef)
		{
			if (thingDef.category == ThingCategory.Pawn && thingDef.tradeability > Tradeability.None)
            {
				RaceProperties raceProps = RaceProperties.Get(thingDef);
				return raceProps != null && raceProps.isLynian;
            }
			return false;
		}

		private bool respectPopulationIntent;
        public PawnKindDef pawnKindDef;
    }
}
