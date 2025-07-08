using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    public abstract class LynianDef : Def
    {
        [MustTranslate]
        public new string description;
        [MustTranslate]
        public string extraTooltip;
        public int skillPointCost = 0;
        public LynianKnowledgeDef requiredKnowledgeDef;

        public virtual bool AlreadyUnlocked(Pawn pawn)
        {
            return false;
        }

        public virtual bool CanPurchase(Comp_EurekacornTracker compEurekacornTracker)
        {
            return CanPurchase(compEurekacornTracker.SkillPointCount);
        }

        public virtual bool CanPurchase(int skillPointCount)
        {
            return skillPointCount >= skillPointCost;
        }

        public virtual void Purchase(Comp_EurekacornTracker compEurekacornTracker, Pawn pawn)
        {
            compEurekacornTracker.SpendSkillPoints(skillPointCost);
        }

        public virtual AcceptanceReport PawnRequirementsMet(Comp_EurekacornTracker compEurekacornTracker, Pawn pawn)
        {
            if (requiredKnowledgeDef != null)
            {
                if (requiredKnowledgeDef.Completed(compEurekacornTracker))
                {
                    return "Mashed_Lynians_Eurekacorn_RequiresKnowledge".Translate(pawn);
                }
            }
            return true;
        }
    }
}
