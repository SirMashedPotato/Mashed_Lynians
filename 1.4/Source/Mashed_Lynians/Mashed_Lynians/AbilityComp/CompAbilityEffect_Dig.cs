using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_Dig : CompAbilityEffect
    {
        public new CompProperties_Dig Props
        {
            get
            {
                return (CompProperties_Dig)this.props;
            }
        }

        public override bool GizmoDisabled(out string reason)
        {
            Pawn p = this.parent.pawn;
            SkillRecord skill = p.skills.GetSkill(SkillDefOf.Mining);
            if (skill.Level < Props.requiredLevel)
            {
                reason = "Mashed_Lynian_AbilitySkillTooLow".Translate(skill.def.label, Props.requiredLevel);
                return true;
            }
            return base.GizmoDisabled(out reason);
        }

    }
}
