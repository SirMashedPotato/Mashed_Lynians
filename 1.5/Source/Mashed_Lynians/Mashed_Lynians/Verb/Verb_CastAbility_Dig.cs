using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class Verb_CastAbility_Dig : Verb_CastAbility
    {
        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            return base.ValidateTarget(target, showMessages) && IsValidTarget(target, ability.pawn);
        }

        public bool IsValidTarget(LocalTargetInfo t, Pawn p)
        {
            if (t.Cell != null && !t.Cell.Fogged(p.Map))
            {
                if (!t.Cell.Impassable(p.Map) && t.Cell.InBounds(p.Map))
                {
                    TerrainDef td = t.Cell.GetTerrain(p.Map);
                    if (td != null && td.affordances.Contains(TerrainAffordanceDefOf.Diggable))
                    {
                        TerrainDef td2 = p.Position.GetTerrain(p.Map);
                        return td2 != null && td2.affordances.Contains(TerrainAffordanceDefOf.Diggable);
                    }
                }
            }
            return false;
        }

        protected override bool TryCastShot()
        {
            return base.TryCastShot() && DoDig(this.CasterPawn, this.currentTarget, this.verbProps);
        }

        /// <summary>
        /// Prevents the pawn queing the cast ability job straight after 'landing'
        /// </summary>
        public override void OrderForceTarget(LocalTargetInfo target)
        {
            JumpUtility.OrderJump(this.CasterPawn, target, this, this.EffectiveRange);
        }

        public static bool DoDig(Pawn pawn, LocalTargetInfo currentTarget, VerbProperties verbProps)
        {
            IntVec3 position = pawn.Position;
            IntVec3 targetCell = currentTarget.Cell;
            Map map = pawn.Map;
            bool flag = Find.Selector.IsSelected(pawn);
            PawnFlyer pawnFlyer = PawnFlyer.MakeFlyer(ThingDefOf.Mashed_Lynian_PawnDigging, pawn, targetCell, null, verbProps.soundLanding, false);
            if (pawnFlyer != null)
            {
                GenSpawn.Spawn(pawnFlyer, targetCell, map, WipeMode.Vanish);
                pawn.records.AddTo(RecordDefOf.Mashed_Lynian_DiggingDistance, position.DistanceTo(targetCell));
                if (flag)
                {
                    Find.Selector.Select(pawn, false, false);
                }
                return true;
            }
            return false;
        }
    }
}
