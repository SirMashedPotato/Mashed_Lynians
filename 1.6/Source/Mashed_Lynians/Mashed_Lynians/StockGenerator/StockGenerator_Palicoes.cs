using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using RimWorld.Planet;

namespace Mashed_Lynians
{
    public class StockGenerator_Palicoes : StockGenerator
    {
		/// <summary>
		/// Make sure to never add the same pawnKind as a possible caravan member
		/// </summary>
		public override IEnumerable<Thing> GenerateThings(PlanetTile forTile, Faction faction = null)
		{
			///Likely the cause if none are appearing in trade caravans
			///It's a vanilla mechanic, so keeping as is
			///Can just disable through xml if they want
			if (respectPopulationIntent && Rand.Value > StorytellerUtilityPopulation.PopulationIntent)
			{
				yield break;
			}
			int count = countRange.RandomInRange;
			for (int i = 0; i < count; i++)
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
				PawnKindDef kindDef;

				if (!pawnKindDefList.NullOrEmpty())
				{
					kindDef = pawnKindDefList.RandomElement();
				}
				else
				{
					kindDef = pawnKindDef;
				}
				DevelopmentalStage developmentalStages = Find.Storyteller.difficulty.ChildrenAllowed ? (DevelopmentalStage.Child | DevelopmentalStage.Adult) : DevelopmentalStage.Adult;

				yield return PawnGenerator.GeneratePawn(new PawnGenerationRequest(
					kind: kindDef,
					faction: faction,
					tile: forTile,
					colonistRelationChanceFactor: 0f,
					developmentalStages: developmentalStages));
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
		public List<PawnKindDef> pawnKindDefList;
	}
}
