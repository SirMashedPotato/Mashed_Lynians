using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class JobGiver_AICastFurrenzy : JobGiver_AICastAbility
    {
        protected override LocalTargetInfo GetTarget(Pawn caster, Ability ability)
        {
            return caster;
        }
    }
}
