using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Mashed_Lynians
{
    public class LynianKnowledgeDef : Def
    {
        [NoTranslate]
        public string backgroundTexPath = "UI/Widgets/DesButBG";
        public ThingDef bookDef;
        public float knowledgeCost = 500;

        public bool Completed(Pawn pawn)
        {
            Comp_EurekacornTracker compEurekacornTracker = pawn.GetComp<Comp_EurekacornTracker>();
            if (compEurekacornTracker == null)
            {
                return false;
            }
            return Completed(compEurekacornTracker);
        }

        public bool Completed(Comp_EurekacornTracker compEurekacornTracker)
        {
            return Completed(compEurekacornTracker.knowledgeTracker.TryGetValue(this, 0));
        }

        public bool Completed(float currentCount)
        {
            return currentCount >= knowledgeCost;
        }

        public float Progress(Comp_EurekacornTracker compEurekacornTracker)
        {
            return compEurekacornTracker.knowledgeTracker.TryGetValue(this, 0);
        }

        public float CompletionProgress(Comp_EurekacornTracker compEurekacornTracker)
        {
            return compEurekacornTracker.knowledgeTracker.TryGetValue(this, 0) / knowledgeCost;
        }

        public void GainKnowledge(Comp_EurekacornTracker compEurekacornTracker, float knowledgeGain, bool message = true)
        {

            if (!compEurekacornTracker.knowledgeTracker.ContainsKey(this))
            {
                compEurekacornTracker.knowledgeTracker.Add(this, 0);
            }
            float finalCount = Mathf.Clamp(knowledgeGain, 0, knowledgeCost - compEurekacornTracker.knowledgeTracker[this]);
            if (finalCount > 0)
            {
                compEurekacornTracker.knowledgeTracker[this] += finalCount;
                if (message)
                {
                    Messages.Message("Mashed_Lynians_Eurekacorn_GainedKnowledge".Translate(compEurekacornTracker.parent as Pawn, LabelCap), compEurekacornTracker.parent, MessageTypeDefOf.PositiveEvent);
                }
            }
        }

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string item in base.ConfigErrors())
            {
                yield return item;
            }

            if (bookDef == null)
            {
                yield return "bookDef is null";
            }
        }
    }
}
