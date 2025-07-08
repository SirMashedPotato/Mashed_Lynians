using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Mashed_Lynians
{
    internal class LynianAbilityDef : LynianDef
    {
        [NoTranslate]
        public string backgroundTexPath = "UI/Widgets/DesButBG";
        public AbilityDef abilityDef;

        public override bool AlreadyUnlocked(Pawn pawn)
        {
            return pawn.abilities.GetAbility(abilityDef) != null;
        }

        public override void Purchase(Comp_EurekacornTracker compEurekacornTracker, Pawn pawn)
        {
            base.Purchase(compEurekacornTracker, pawn);
            pawn.abilities.GainAbility(abilityDef);
            Messages.Message("Mashed_Lynian_EurekacornGainedAbility".Translate(pawn, abilityDef), pawn, MessageTypeDefOf.PositiveEvent);
        }

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string item in base.ConfigErrors())
            {
                yield return item;
            }

            if (abilityDef == null)
            {
                yield return "abilityDef is null";
            }
        }
    }
}
