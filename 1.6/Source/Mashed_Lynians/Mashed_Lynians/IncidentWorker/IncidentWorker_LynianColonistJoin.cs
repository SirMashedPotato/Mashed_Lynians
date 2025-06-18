using System;
using System.Linq;
using Verse;
using RimWorld;

namespace Mashed_Lynians
{
	/// <summary>
	/// Got a little lazy with this, could probably do with a redo
	/// </summary>

    public class IncidentWorker_LynianColonistJoin : IncidentWorker
	{
		protected override bool CanFireNowSub(IncidentParms parms)
		{
			if (!base.CanFireNowSub(parms))
			{
				return false;
			}
			Map map = (Map)parms.target;
			return CanSpawnJoiner(map);
		}

		public virtual Pawn GeneratePawn()
		{
			Gender? fixedGender = null;
			if (def.pawnFixedGender != Gender.None)
			{
				fixedGender = new Gender?(def.pawnFixedGender);
			}
			Ideo ideo = null;
			if (ModsConfig.IdeologyActive)
			{
				ideo = (from i in Find.IdeoManager.IdeosListForReading
						where !Faction.OfPlayer.ideos.Has(i)
						select i).RandomElementWithFallback(null);
				if (ideo == null)
				{
					ideo = (from i in Find.IdeoManager.IdeosListForReading
							where !Faction.OfPlayer.ideos.IsPrimary(i)
							select i).RandomElementWithFallback(null);
				}
			}
			return PawnGenerator.GeneratePawn(new PawnGenerationRequest(
				kind: Utility.lynianColonistKindList.RandomElement(),
				faction: Faction.OfPlayer,
				mustBeCapableOfViolence: def.pawnMustBeCapableOfViolence, 
				relationWithExtraPawnChanceFactor: RelationWithColonistWeight,
				fixedGender: fixedGender, 
				fixedIdeo: ideo, 
				developmentalStages: DevelopmentalStage.Adult));
		}

		public virtual bool CanSpawnJoiner(Map map)
		{
            return TryFindEntryCell(map, out _);
        }

		public virtual void SpawnJoiner(Map map, Pawn pawn)
		{
            TryFindEntryCell(map, out IntVec3 loc);
            GenSpawn.Spawn(pawn, loc, map, WipeMode.Vanish);
		}

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			if (!CanSpawnJoiner(map))
			{
				return false;
			}
			Pawn pawn = GeneratePawn();
			SpawnJoiner(map, pawn);
			if (def.pawnHediff != null)
			{
				pawn.health.AddHediff(def.pawnHediff, null, null, null);
			}
			TaggedString baseLetterText = (def.pawnHediff != null) ? def.letterText.Formatted(pawn.Named("PAWN"), def.pawnHediff.Named("HEDIFF")).AdjustedFor(pawn, "PAWN", true) : def.letterText.Formatted(pawn.Named("PAWN")).AdjustedFor(pawn, "PAWN", true);
			TaggedString baseLetterLabel = def.letterLabel.Formatted(pawn.Named("PAWN")).AdjustedFor(pawn, "PAWN", true);
			PawnRelationUtility.TryAppendRelationsWithColonistsInfo(ref baseLetterText, ref baseLetterLabel, pawn);
			base.SendStandardLetter(baseLetterLabel, baseLetterText, LetterDefOf.PositiveEvent, parms, pawn, Array.Empty<NamedArgument>());
			return true;
		}

		private bool TryFindEntryCell(Map map, out IntVec3 cell)
		{
			return CellFinder.TryFindRandomEdgeCellWith((IntVec3 c) => map.reachability.CanReachColony(c) && !c.Fogged(map), map, CellFinder.EdgeRoadChance_Neutral, out cell);
		}

		private const float RelationWithColonistWeight = 20f;
	}
}
