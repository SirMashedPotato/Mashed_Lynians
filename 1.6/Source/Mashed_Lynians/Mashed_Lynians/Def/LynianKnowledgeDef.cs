using System.Collections.Generic;
using Verse;

namespace Mashed_Lynians
{
    public class LynianKnowledgeDef : Def
    {
        [NoTranslate]
        public string backgroundTexPath = "UI/Widgets/DesButBG";
        public ThingDef bookDef;
        public int knowledgeCost = 500;

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

        public bool Completed(int currentCount)
        {
            return currentCount >= knowledgeCost;
        }

        public int Progress(Comp_EurekacornTracker compEurekacornTracker)
        {
            return compEurekacornTracker.knowledgeTracker.TryGetValue(this, 0);
        }

        public float CompletionProgress(Comp_EurekacornTracker compEurekacornTracker)
        {
            return compEurekacornTracker.knowledgeTracker.TryGetValue(this, 0) / (float)knowledgeCost;
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
