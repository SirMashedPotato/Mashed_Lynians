using RimWorld;
using System;

namespace Mashed_Lynians
{
    public class BookOutcomeProperties_GainLynianKnowledge : BookOutcomeProperties
    {
        public LynianKnowledgeDef knowledgeDef;

        public override Type DoerClass => typeof(ReadingOutcomeDoerGainLynianKnowledge);
    }
}
