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

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target.Cell != null)
            {
                Pawn p = this.parent.pawn;
                Map map = p.Map;
                IntVec3 oldCell = p.Position;
                //p.Position = target.Cell;
                PawnFlyer pawnFlyer = PawnFlyer.MakeFlyer(ThingDefOf.Mashed_Lynian_PawnDigging, p, target.Cell, null, SoundDefOf.Roof_Collapse);
                if (pawnFlyer != null)
                {
                    GenSpawn.Spawn(pawnFlyer, target.Cell, map, WipeMode.Vanish);
                    p.records.AddTo(RecordDefOf.Mashed_Lynian_DiggingDistance, oldCell.DistanceTo(target.Cell));
                }
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
