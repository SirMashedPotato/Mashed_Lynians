using RimWorld;
using System;
using System.Collections.Generic;
using System.Xml;
using Verse;

namespace Mashed_Lynians
{
    public class BookOutcomeProperties_GainLynianKnowledge : BookOutcomeProperties
    {
        public LynianKnowledgeDef fixedKnowledgeDef;
        public List<KnowledgeChoice> knowledgeDefs = new List<KnowledgeChoice>();

        public override Type DoerClass => typeof(ReadingOutcomeDoerGainLynianKnowledge);
    }

    public class KnowledgeChoice
    {
        public LynianKnowledgeDef knowledgeDef;
        public int selectionWeight;

        public void LoadDataFromXmlCustom(XmlNode xmlRoot)
        {
            DirectXmlCrossRefLoader.RegisterObjectWantsCrossRef(this, "knowledgeDef", xmlRoot);
            selectionWeight = ParseHelper.FromString<int>(xmlRoot.FirstChild.Value);
        }
    }
}
