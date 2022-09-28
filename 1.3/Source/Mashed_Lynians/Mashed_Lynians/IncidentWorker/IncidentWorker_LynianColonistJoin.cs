using System;
using System.Linq;
using System.Collections.Generic;
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
			return this.CanSpawnJoiner(map);
		}

		public virtual Pawn GeneratePawn()
		{
			Gender? fixedGender = null;
			if (this.def.pawnFixedGender != Gender.None)
			{
				fixedGender = new Gender?(this.def.pawnFixedGender);
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
			return PawnGenerator.GeneratePawn(new PawnGenerationRequest(pawnKindList.RandomElement(), Faction.OfPlayer, PawnGenerationContext.NonPlayer, -1, true, false, false, false, true, this.def.pawnMustBeCapableOfViolence, RelationWithColonistWeight, false, true, true, true, false, false, false, false, 0f, 0f, null, 1f, null, null, null, null, null, null, null, fixedGender, null, null, null, null, ideo, false, false, false));
		}

		public virtual bool CanSpawnJoiner(Map map)
		{
			IntVec3 intVec;
			return this.TryFindEntryCell(map, out intVec);
		}

		public virtual void SpawnJoiner(Map map, Pawn pawn)
		{
			IntVec3 loc;
			this.TryFindEntryCell(map, out loc);
			GenSpawn.Spawn(pawn, loc, map, WipeMode.Vanish);
		}

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			if (!this.CanSpawnJoiner(map))
			{
				return false;
			}
			Pawn pawn = this.GeneratePawn();
			this.SpawnJoiner(map, pawn);
			if (this.def.pawnHediff != null)
			{
				pawn.health.AddHediff(this.def.pawnHediff, null, null, null);
			}
			TaggedString baseLetterText = (this.def.pawnHediff != null) ? this.def.letterText.Formatted(pawn.Named("PAWN"), this.def.pawnHediff.Named("HEDIFF")).AdjustedFor(pawn, "PAWN", true) : this.def.letterText.Formatted(pawn.Named("PAWN")).AdjustedFor(pawn, "PAWN", true);
			TaggedString baseLetterLabel = this.def.letterLabel.Formatted(pawn.Named("PAWN")).AdjustedFor(pawn, "PAWN", true);
			PawnRelationUtility.TryAppendRelationsWithColonistsInfo(ref baseLetterText, ref baseLetterLabel, pawn);
			base.SendStandardLetter(baseLetterLabel, baseLetterText, LetterDefOf.PositiveEvent, parms, pawn, Array.Empty<NamedArgument>());
			return true;
		}

		private bool TryFindEntryCell(Map map, out IntVec3 cell)
		{
			return CellFinder.TryFindRandomEdgeCellWith((IntVec3 c) => map.reachability.CanReachColony(c) && !c.Fogged(map), map, CellFinder.EdgeRoadChance_Neutral, out cell);
		}

		private const float RelationWithColonistWeight = 20f;

		public static readonly List<PawnKindDef> pawnKindList = new List<PawnKindDef> 
		{ 
			PawnKindDefOf.Mashed_Lynian_BoaboaColonist, PawnKindDefOf.Mashed_Lynian_FelyneColonist, PawnKindDefOf.Mashed_Lynian_GajalakaColonist, 
			PawnKindDefOf.Mashed_Lynian_GrimalkyneColonist, PawnKindDefOf.Mashed_Lynian_MelynxColonist, PawnKindDefOf.Mashed_Lynian_ShakalakaColonist, 
			PawnKindDefOf.Mashed_Lynian_UrukiColonist
		};
	}
}
