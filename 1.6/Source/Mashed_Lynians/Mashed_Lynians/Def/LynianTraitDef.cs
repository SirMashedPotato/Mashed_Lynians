using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Mashed_Lynians
{
    public class LynianTraitDef : LynianDef
    {
        public TraitDef traitDef;
        public int traitDegree = 0;

        public override bool AlreadyUnlocked(Pawn pawn)
        {
            return pawn.story.traits.HasTrait(traitDef, traitDegree);
        }

        public override AcceptanceReport PawnRequirementsMet(Comp_EurekacornTracker compEurekacornTracker, Pawn pawn)
        {
            if (!traitDef.conflictingTraits.NullOrEmpty())
            {
                foreach (Trait trait in pawn.story.traits.allTraits)
                {
                    if (traitDef.ConflictsWith(trait))
                    {
                        return "Mashed_Lynians_Eurekacorn_ConflictingTrait".Translate(trait.LabelCap);
                    }
                }
            }

            return base.PawnRequirementsMet(compEurekacornTracker, pawn);
        }

        public override void Purchase(Comp_EurekacornTracker compEurekacornTracker, Pawn pawn)
        {
            base.Purchase(compEurekacornTracker, pawn);
            Trait trait = new Trait(traitDef, traitDegree, true);
            pawn.story.traits.GainTrait(trait);
            Messages.Message("Mashed_Lynians_Eurekacorn_GainedTrait".Translate(pawn, trait.LabelCap), pawn, MessageTypeDefOf.PositiveEvent);
        }

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string item in base.ConfigErrors())
            {
                yield return item;
            }

            if (traitDef == null)
            {
                yield return "traitDef is null";
            }
        }
    }
}
