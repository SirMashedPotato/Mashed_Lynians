using System.Collections.Generic;
using RimWorld.QuestGen;
using Verse;
using Verse.Grammar;
using RimWorld;
using RimWorld.Planet;

namespace Mashed_Lynians
{
    public class SitePartWorker_ShakalakaWanderer : SitePartWorker_DownedRefugee
    {
		public override void Notify_GeneratedByQuestGen(SitePart part, Slate slate, List<Rule> outExtraDescriptionRules, Dictionary<string, string> outExtraDescriptionConstants)
		{
			base.Notify_GeneratedByQuestGen(part, slate, outExtraDescriptionRules, outExtraDescriptionConstants);
			PawnKindDef pawnKind = slate.Get<PawnKindDef>("refugeeKind", PawnKindDefOf.Mashed_Lynian_ShakalakaWanderer, false);
			float chanceForFaction = slate.Exists("refugeeFactionChance", false) ? 0f : slate.Get<float>("refugeeFactionChance", 0f, false);
			Pawn pawn = DownedRefugeeQuestUtility.GenerateRefugee(part.site.Tile, pawnKind, chanceForFaction);
			part.things = new ThingOwner<Pawn>(part, true, LookMode.Deep);
			part.things.TryAdd(pawn, true);
			if (pawn.relations != null)
			{
				pawn.relations.everSeenByPlayer = true;
			}
			Pawn mostImportantColonyRelative = PawnRelationUtility.GetMostImportantColonyRelative(pawn);
			if (mostImportantColonyRelative != null)
			{
				PawnRelationDef mostImportantRelation = mostImportantColonyRelative.GetMostImportantRelation(pawn);
				TaggedString taggedString = "";
				if (mostImportantRelation != null && mostImportantRelation.opinionOffset > 0)
				{
					pawn.relations.relativeInvolvedInRescueQuest = mostImportantColonyRelative;
					taggedString = "\n\n" + "RelatedPawnInvolvedInQuest".Translate(mostImportantColonyRelative.LabelShort, mostImportantRelation.GetGenderSpecificLabel(pawn), mostImportantColonyRelative.Named("RELATIVE"), pawn.Named("PAWN")).AdjustedFor(pawn, "PAWN", true);
				}
				else
				{
					PawnRelationUtility.TryAppendRelationsWithColonistsInfo(ref taggedString, pawn);
				}
				outExtraDescriptionRules.Add(new Rule_String("pawnInvolvedInQuestInfo", taggedString));
			}
			slate.Set<Pawn>("refugee", pawn, false);
		}
	}
}
