using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.Grammar;

namespace Mashed_Lynians
{
    public class LynianKnowledgeDef : Def
    {
        [NoTranslate]
        public string iconTexPath = "UI/Widgets/Mashed_Lynian_KnowledgeDefault";
        [NoTranslate]
        public string backgroundTexPath = "UI/Icons/DesButBG";
        public float knowledgeCost = 500;
        public RulePack generalRules;

        public override void ResolveReferences()
        {
            base.ResolveReferences();

            List<LynianAbilityDef> abilityDefs = DefDatabase<LynianAbilityDef>.AllDefsListForReading.Where(x => x?.requiredKnowledgeDef == this).ToList();

            if (!abilityDefs.NullOrEmpty())
            {
                if (descriptionHyperlinks.NullOrEmpty())
                {
                    descriptionHyperlinks = new List<DefHyperlink>();
                }

                foreach (LynianAbilityDef abilityDef in abilityDefs)
                {
                    descriptionHyperlinks.Add(abilityDef.abilityDef);
                }
            }
        }

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
                if (Completed(compEurekacornTracker) && message)
                {
                    Messages.Message("Mashed_Lynians_Eurekacorn_GainedKnowledge".Translate(compEurekacornTracker.parent as Pawn, LabelCap), compEurekacornTracker.parent, MessageTypeDefOf.PositiveEvent);
                }
            }
        }
    }
}
