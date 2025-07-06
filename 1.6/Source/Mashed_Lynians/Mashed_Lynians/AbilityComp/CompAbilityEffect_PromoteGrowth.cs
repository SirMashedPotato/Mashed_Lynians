using Verse;
using RimWorld;
using UnityEngine;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_PromoteGrowth : CompAbilityEffect
    {
        public new CompProperties_AbilityPromoteGrowth Props => (CompProperties_AbilityPromoteGrowth)this.props;

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
            Pawn p = parent.pawn;
            float skillLevel = (float)p.skills.GetSkill(Props.skillDef).Level;
            float growthBoost;
            if (skillLevel >= 20f)
            {
                growthBoost = Props.growthPerLevel * 20f;
                skillLevel -= 20f;
                growthBoost += (Props.growthPerOverLevel * skillLevel);
            }
            else
            {
                growthBoost = Props.growthPerLevel * skillLevel;
            }
            return Mathf.Clamp(growthBoost, 0f, 1f);
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

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            if (target.Thing == null)
            {
                return false;
            }

            if (target.Thing is Plant p && p.Growth < 1f)
            {
                return base.Valid(target, throwMessages);
            }
            else
            {
                return false;
            }
        }

        public override string ExtraTooltipPart()
        {
            return "Mashed_Lynian_PromoteGrowthDetails".Translate(BoostAmount().ToStringPercent());
        }

        public override string ExtraLabelMouseAttachment(LocalTargetInfo target)
        {
            if (target.Thing is Plant p)
            {
                return ExtraTooltipPart() + "Mashed_Lynian_PromoteGrowthDetails_ExtraLabel".Translate(p.Growth.ToStringPercent());
            }
            return ExtraTooltipPart();
        }
    }
}
