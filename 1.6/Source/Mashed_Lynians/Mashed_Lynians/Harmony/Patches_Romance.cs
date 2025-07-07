using HarmonyLib;
using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    public class RomanceCheck
    {
        public static bool CanRomance(Pawn initiator, Pawn target, out string opinionExplanation)
        {
            opinionExplanation = null;
            bool initiatorIsLynian = OnStartupUtility.LynianRaces.Contains(initiator.def);
            bool targetIsLynian = OnStartupUtility.LynianRaces.Contains(target.def);
            if (initiatorIsLynian != targetIsLynian)
            {
                opinionExplanation = "Mashed_Lynian_CantRomanceNonLynian".Translate();
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// Makes it so Lynians should only ever romance other Lynians
    /// and can never be romanced by non-Lynians
    /// First patch is for the player forcing romances
    /// Second is for pawn controlled romance chance
    /// Third is just in case the others fail at some point
    /// </summary>
    [HarmonyPatch(typeof(RelationsUtility))]
    [HarmonyPatch("RomanceEligiblePair")]
    public static class RelationsUtility_RomanceEligiblePair_Patch
    {
        public static void Postfix(Pawn initiator, Pawn target, bool forOpinionExplanation, ref AcceptanceReport __result)
        {
            if (__result)
            {
                bool canRomance = RomanceCheck.CanRomance(initiator, target, out string opinionExplanation);
                if (!canRomance && forOpinionExplanation)
                {
                    ///I'm going to be honest here, this doesn't get used. No idea why
                    __result = opinionExplanation;
                    return;
                }
                __result = canRomance;
            }
        }
    }

    [HarmonyPatch(typeof(InteractionWorker_RomanceAttempt))]
    [HarmonyPatch("RandomSelectionWeight")]
    public static class InteractionWorker_RomanceAttempt_RandomSelectionWeight_Patch
    {
        public static void Postfix(Pawn initiator, Pawn recipient, ref float __result)
        {
            if (__result > 0f)
            {
                bool canRomance = RomanceCheck.CanRomance(initiator, recipient, out string _);
                if (!canRomance)
                {
                    __result = 0f;
                }
            }
        }
    }

    [HarmonyPatch(typeof(InteractionWorker_RomanceAttempt))]
    [HarmonyPatch("SuccessChance")]
    public static class InteractionWorker_RomanceAttempt_SuccessChance_Patch
    {
        public static void Postfix(Pawn initiator, Pawn recipient, ref float __result)
        {
            if (__result > 0f)
            {
                bool canRomance = RomanceCheck.CanRomance(initiator, recipient, out string _);
                if (!canRomance)
                {
                    __result = 0f;
                }
            }
        }
    }
}
