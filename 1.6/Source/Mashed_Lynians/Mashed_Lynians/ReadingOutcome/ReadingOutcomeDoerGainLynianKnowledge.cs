using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.Grammar;

namespace Mashed_Lynians
{
    public class ReadingOutcomeDoerGainLynianKnowledge : BookOutcomeDoer
    {
        public new BookOutcomeProperties_GainLynianKnowledge Props => (BookOutcomeProperties_GainLynianKnowledge)props;

        protected LynianKnowledgeDef knowledgeDef;

        public override void OnBookGenerated(Pawn author = null)
        {
            base.OnBookGenerated(author);
            if (knowledgeDef == null)
            {
                if (Props.fixedKnowledgeDef != null)
                {
                    knowledgeDef = Props.fixedKnowledgeDef;
                }
                else
                {
                    knowledgeDef = Props.knowledgeDefs.RandomElementByWeight(x=>x.selectionWeight).knowledgeDef;
                }
            }
        }

        public override bool DoesProvidesOutcome(Pawn reader)
        {
            Comp_EurekacornTracker compEurekacornTracker = reader.TryGetComp<Comp_EurekacornTracker>();
            if (compEurekacornTracker == null)
            {
                return false;
            }

            return compEurekacornTracker.knowledgeTracker.TryGetValue(knowledgeDef, 0) < knowledgeDef.knowledgeCost;
        }

        protected virtual float GetBaseValue()
        {
            return BookUtility.GetResearchExpForQuality(Quality);
        }

        public override void OnReadingTick(Pawn reader, float factor)
        {
            Comp_EurekacornTracker compEurekacornTracker = reader.TryGetComp<Comp_EurekacornTracker>();
            knowledgeDef.GainKnowledge(compEurekacornTracker, GetBaseValue() * factor);
        }

        public override IEnumerable<Dialog_InfoCard.Hyperlink> GetHyperlinks()
        {
            yield return new Dialog_InfoCard.Hyperlink(knowledgeDef);
        }

        public override string GetBenefitsString(Pawn reader = null)
        {
            return string.Format(" - {0}: {1}", knowledgeDef.LabelCap, "PerHour".Translate((BookUtility.GetResearchExpForQuality(Quality) * GenDate.TicksPerHour).ToStringDecimalIfSmall()));
        }

        public override IEnumerable<RulePack> GetTopicRulePacks()
        {
            yield return knowledgeDef.generalRules;
        }

        public override void PostExposeData()
        {
            Scribe_Defs.Look(ref knowledgeDef, "lynianKnowledgeDef");
        }
    }
}
