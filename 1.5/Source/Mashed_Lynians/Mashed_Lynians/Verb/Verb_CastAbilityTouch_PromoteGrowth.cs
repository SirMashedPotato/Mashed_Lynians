using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class Verb_CastAbilityTouch_PromoteGrowth : Verb_CastAbilityTouch
    {
        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            return base.ValidateTarget(target, showMessages) && IsValidPlant(target.Thing);
        }

        public override bool IsUsableOn(Thing target)
        {
            return base.IsUsableOn(target) && IsValidPlant(target);
        }

        public bool IsValidPlant(Thing t)
        {
            return t is Plant p && p.Growth < 1f;
        }

    }
}
