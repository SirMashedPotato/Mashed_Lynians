using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Mashed_Lynians
{
    public class Comp_EurekacornTracker : ThingComp
    {
        private const int maxSkillPoints = 30;
        private int skillPointCount = 0;
        //dictionary of knowledge, int is progress
        //public Dictionary<LynianKnowledgeDef, int> knowledgeTracker = new Dictionary<LynianKnowledgeDef, int>();

        public int SkillPointCount => skillPointCount;
        public int MaxSkillPoints => maxSkillPoints;

        public bool CanGainSkillPoints(int pointsGained = 1)
        {
            return skillPointCount + pointsGained <= maxSkillPoints;
        }

        public void GainSkillPoints(int pointsGained)
        {
            skillPointCount += pointsGained;
            Messages.Message("Mashed_Lynians_Eurekacorn_GainedPoints".Translate(parent, pointsGained), parent, MessageTypeDefOf.PositiveEvent, false);
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo gizmo in base.CompGetGizmosExtra())
            {
                yield return gizmo;
            }


            yield return new Command_Action
            {
                defaultLabel = "Mashed_Lynians_EurekacornMenu_Label".Translate(),
                defaultDesc = "Mashed_Lynians_EurekacornMenu_Desc".Translate(parent),
                icon = ContentFinder<Texture2D>.Get("UI/Gizmos/Mashed_Lynian_EurekacornMenu", true),
                action = delegate ()
                {
                    Page_EurekacornMenu page = new Page_EurekacornMenu(parent as Pawn, this);
                    Find.WindowStack.Add(page);
                },
            };
        }

        public override void PostExposeData()
        {
            Scribe_Values.Look(ref skillPointCount, "skillPointCount", 0);
            base.PostExposeData();
        }
    }
}
