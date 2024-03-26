using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class Verb_CastAbilityTouch_Pilfer : Verb_CastAbilityTouch
    {
        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            return base.ValidateTarget(target, showMessages) && IsValidPawn(target.Thing);
        }

        public override bool IsUsableOn(Thing target)
        {
            return base.IsUsableOn(target) && IsValidPawn(target);
        }

        public bool IsValidPawn(Thing t)
        {
            return t is Pawn p && !p.inventory.innerContainer.NullOrEmpty();
        }

    }
}
