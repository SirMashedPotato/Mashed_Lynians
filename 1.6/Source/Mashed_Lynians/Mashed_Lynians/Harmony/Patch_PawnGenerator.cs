using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace Mashed_Lynians
{
    /// <summary>
    /// Adds additional abilities.
    /// Still handling guaranteed abilities here so they can be applied on a race level.
    /// </summary>
    [HarmonyPatch(typeof(PawnGenerator))]
    [HarmonyPatch("GenerateInitialHediffs")]
    public static class PawnGenerator_GenerateInitialHediffs_Patch
    {
        [HarmonyPostfix]
        public static void LyniansPatch(Pawn pawn)
        {
            RaceProperties props = RaceProperties.Get(pawn.def);
            if (props == null)
            {
                return;
            }

            if (!props.startingAbilities.NullOrEmpty())
            {
                foreach (AbilityDef ability in props.startingAbilities) 
                {
                    pawn.abilities.GainAbility(ability);
                }
            }

            if (!props.oneOfRandomAbility.NullOrEmpty() && Rand.Chance(props.oneOfRandomChance))
            {
                pawn.abilities.GainAbility(props.oneOfRandomAbility.RandomElement());
            }
        }
    }

    /// <summary>
    /// Replaces the man in black pawnkind
    /// </summary>
    [HarmonyPatch(typeof(PawnGenerator))]
    [HarmonyPatch("GeneratePawn")]
    [HarmonyPatch(new Type[] { typeof(PawnGenerationRequest) })]
    public static class PawnGenerator_GeneratePawn_Patch
    {
        public static void Prefix(ref PawnGenerationRequest request)
        {
            if (request.Faction != null && Mashed_Lynians_ModSettings.EnableAcornKnight)
            {
                FactionProperties props = FactionProperties.Get(request.Faction.def);
                if (props != null && props.manInBlackReplacer != null && request.KindDef == PawnKindDefOf.StrangerInBlack)
                {
                    request.KindDef = props.manInBlackReplacer;
                }
            }

        }
    }
}
