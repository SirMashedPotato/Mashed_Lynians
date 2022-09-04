using Verse;
using RimWorld;
using UnityEngine;

namespace Mashed_Lynians
{
    class CompAbilityEffect_PromoteGrowth : CompAbilityEffect
    {
        public new CompProperties_PromoteGrowth Props
        {
            get
            {
                return (CompProperties_PromoteGrowth)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target.Thing != null && target.Thing is Plant)
            {
                Plant plant = target.Thing as Plant;
                Pawn user = parent.pawn;

                float growthBoost = BoostAmount();
                plant.Growth += Mathf.Clamp(1f, plant.Growth, growthBoost);
                user.records.AddTo(RecordDefOf.Mashed_Lynian_GrowthPromoted, growthBoost*100);
                FleckMaker.ThrowDustPuff(plant.Position, plant.Map, 1f);
            }
        }

        public float BoostAmount()
        {
            return Mathf.Clamp(Props.growthCap, 0f, parent.pawn.skills.GetSkill(SkillDefOf.Plants).Level * Props.growthPerLevel);
        }

        public override bool GizmoDisabled(out string reason)
        {
            if (parent.pawn.skills.GetSkill(SkillDefOf.Plants).TotallyDisabled)
            {
                reason = "Mashed_Lynian_AbilityDisabled_Skill".Translate(parent.pawn.NameFullColored, SkillDefOf.Plants.label);
                return true;
            }
            if(parent.pawn.skills.GetSkill(SkillDefOf.Plants).Level <= 0)
            {
                reason = "Mashed_Lynian_AbilityDisabled_SkillLow".Translate(parent.pawn.NameFullColored, SkillDefOf.Plants.label);
                return true;
            }
            return base.GizmoDisabled(out reason);
        }

        public override string ExtraTooltipPart()
        {
            return "Mashed_Lynian_PromoteGrowthDetails".Translate(BoostAmount().ToStringPercent());
        }

        public override string ExtraLabelMouseAttachment(LocalTargetInfo target)
        {
            return ExtraTooltipPart();
        }
    }
}
