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
            return skillDef.label.CapitalizeFirst() + " " + pawn.skills.GetSkill(skillDef).Level + " x " + factorPerLevel + ": " + this.StatFactor(pawn).ToStringPercent();
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
            if (pawn.def == raceThingDef)
            {
                return factorPerLevel * (float)pawn.skills.GetSkill(skillDef).Level;
            }
            return 0f;
        }

        public float factorPerLevel = 0.3f;
        public SkillDef skillDef;
        public ThingDef raceThingDef;
    }
}
