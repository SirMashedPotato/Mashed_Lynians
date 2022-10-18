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
    }
}
