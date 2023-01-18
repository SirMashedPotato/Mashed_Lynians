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
                return (CompProperties_Dig)props;
            }
        }

        public override bool GizmoDisabled(out string reason)
        {
            if (!HighEnoughSkill())
            {
                reason = "Mashed_Lynian_AbilitySkillTooLow".Translate(SkillDefOf.Mining.label, Props.requiredLevel);
                return true;
            }
            return base.GizmoDisabled(out reason);
        }

        public override bool CanCast 
        {
            get
            {
                if (parent.pawn.Faction != Faction.OfPlayerSilentFail)
                {
                    return base.CanCast && HighEnoughSkill();
                }
                return base.CanCast;
            }
        }

        private bool HighEnoughSkill()
        {
            SkillRecord skill = parent.pawn.skills.GetSkill(SkillDefOf.Mining);
            return skill.Level >= Props.requiredLevel;
        }

    }
}
