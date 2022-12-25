using HarmonyLib;
using RimWorld;
using Verse;

namespace Mashed_Lynians
{
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
        [HarmonyPostfix]
        public static void Lynians_RomanceEligiblePair_Patch(Pawn initiator, Pawn target, bool forOpinionExplanation, ref AcceptanceReport __result)
        {
            if (__result)
            {
                RaceProperties propsInitiator = RaceProperties.Get(initiator.def);
                RaceProperties propsTarget = RaceProperties.Get(target.def);
                ///Both are non-Lynians, so just return
                if (propsInitiator == null && propsTarget == null)
                {
                    return;
                }
                ///Both are non-Lynians, so just return
                if (propsInitiator != null && !propsInitiator.isLynian && propsTarget != null && !propsTarget.isLynian)
                {
                    return;
                }
                ///One is not a Lynian
                if (propsInitiator == null || !propsInitiator.isLynian || propsTarget == null || !propsTarget.isLynian)
                {
                    if (!forOpinionExplanation)
                    {
                        __result = "Mashed_Lynian_CantRomanceNonLynian".Translate();
                        return;
                    }
                    __result = false;
                }
            }
        }
    }

    [HarmonyPatch(typeof(InteractionWorker_RomanceAttempt))]
    [HarmonyPatch("RandomSelectionWeight")]
    public static class InteractionWorker_RomanceAttempt_RandomSelectionWeight_Patch
    {
        [HarmonyPostfix]
        public static void Lynians_RomanceRandomSelectionWeight_Patch(Pawn initiator, Pawn recipient, ref float __result)
        {
            if (__result > 0f)
            {
                RaceProperties propsInitiator = RaceProperties.Get(initiator.def);
                RaceProperties propsRecipient = RaceProperties.Get(recipient.def);
                ///Both are non-Lynians, so just return
                if (propsInitiator == null && propsRecipient == null)
                {
                    return;
                }
                ///Both are non-Lynians, so just return
                if (propsInitiator != null && !propsInitiator.isLynian && propsRecipient != null && !propsRecipient.isLynian)
                {
                    return;
                }
                ///One is not a Lynian
                if (propsInitiator == null || !propsInitiator.isLynian || propsRecipient == null || !propsRecipient.isLynian)
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
        [HarmonyPostfix]
        public static void Lynians_RomanceSuccessChance_Patch(Pawn initiator, Pawn recipient, ref float __result)
        {
            if (__result > 0f)
            {
                RaceProperties propsInitiator = RaceProperties.Get(initiator.def);
                RaceProperties propsRecipient = RaceProperties.Get(recipient.def);
                ///Both are non-Lynians, so just return
                if (propsInitiator == null && propsRecipient == null)
                {
                    return;
                }
                ///Both are non-Lynians, so just return
                if (propsInitiator != null && !propsInitiator.isLynian && propsRecipient != null && !propsRecipient.isLynian)
                {
                    return;
                }
                ///One is not a Lynian
                if (propsInitiator == null || !propsInitiator.isLynian || propsRecipient == null || !propsRecipient.isLynian)
                {
                    __result = 0f;
                }
            }
        }
    }
}
