using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    class StatPart_SkillOffset : StatPart
    {
        public override string ExplanationPart(StatRequest req)
        {
            Pawn pawn;
            if ((pawn = (req.Thing as Pawn)) == null)
            {
                return null;
            }
            return skillDef.label.CapitalizeFirst() + " " + pawn.skills.GetSkill(skillDef).Level + " x " + factorPerLevel + "(>20: " + factorPerOverlevel + "): " + this.StatFactor(pawn).ToStringPercent();
        }

        public override void TransformValue(StatRequest req, ref float val)
        {
            Pawn pawn;
            if ((pawn = (req.Thing as Pawn)) == null)
            {
                return;
            }
            val += this.StatFactor(pawn);
        }

        public float StatFactor(Pawn pawn)
        {
            if (pawn.abilities.GetAbility(abilityDef) != null)
            {
                float skillLevel = (float)pawn.skills.GetSkill(skillDef).Level;
                if (skillLevel >= 20f)
                {
                    float chance = factorPerLevel * 20f;
                    skillLevel -= 20f;
                    return chance + (factorPerOverlevel * skillLevel);
                } 
                else
                {
                    return factorPerLevel * skillLevel;
                }
            }
            return 0f;
        }

        public float factorPerLevel = 0.3f;
        public float factorPerOverlevel = 0.001f;
        public SkillDef skillDef;
        public AbilityDef abilityDef;
    }
}
