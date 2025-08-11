using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Mashed_Lynians
{
    public class ReadingOutcomeDoerGainLynianKnowledge : BookOutcomeDoer
    {
        public new BookOutcomeProperties_GainLynianKnowledge Props => (BookOutcomeProperties_GainLynianKnowledge)props;

        public override bool DoesProvidesOutcome(Pawn reader)
        {
            Comp_EurekacornTracker compEurekacornTracker = reader.TryGetComp<Comp_EurekacornTracker>();
            if (compEurekacornTracker == null)
            {
                return false;
            }

            return compEurekacornTracker.knowledgeTracker.TryGetValue(Props.knowledgeDef, 0) < Props.knowledgeDef.knowledgeCost;
        }

        protected virtual float GetBaseValue()
        {
            return BookUtility.GetResearchExpForQuality(Quality);
        }

        public override void OnReadingTick(Pawn reader, float factor)
        {
            Comp_EurekacornTracker compEurekacornTracker = reader.TryGetComp<Comp_EurekacornTracker>();
            Props.knowledgeDef.GainKnowledge(compEurekacornTracker, GetBaseValue() * factor);
        }

        public override IEnumerable<Dialog_InfoCard.Hyperlink> GetHyperlinks()
        {
            yield return new Dialog_InfoCard.Hyperlink(Props.knowledgeDef);
        }

        public override string GetBenefitsString(Pawn reader = null)
        {

            return string.Format("{0}: {1}", Props.knowledgeDef.LabelCap, "PerHour".Translate((BookUtility.GetResearchExpForQuality(Quality) * GenDate.TicksPerHour).ToStringDecimalIfSmall()));
        }
    }
}
