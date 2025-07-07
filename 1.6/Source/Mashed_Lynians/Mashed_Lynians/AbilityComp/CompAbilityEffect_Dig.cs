using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_Dig : CompAbilityEffect
    {
        public new CompProperties_AbilityDig Props => (CompProperties_AbilityDig)props;

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
            SkillRecord skill = parent.pawn.skills.GetSkill(Props.skillDef);
            return skill.Level >= Props.requiredLevel;
        }

        public override bool GizmoDisabled(out string reason)
        {
            if (Mashed_Lynians_ModSettings.EnableDigSkillRequirement && !HighEnoughSkill())
            {
                reason = "Mashed_Lynian_AbilitySkillTooLow".Translate(Props.skillDef.label, Props.requiredLevel);
                return true;
            }
            return base.GizmoDisabled(out reason);
        }

    }
}
