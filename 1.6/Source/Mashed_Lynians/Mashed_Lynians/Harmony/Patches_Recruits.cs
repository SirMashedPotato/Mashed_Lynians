using HarmonyLib;
using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    //TODO, possibly replace this entire system

    /// <summary>
    /// Makes it so that custom pawnKinds can be sold as 'slaves'
    /// </summary>
    [HarmonyPatch(typeof(TraderCaravanUtility))]
    [HarmonyPatch("GetTraderCaravanRole")]
    public static class TraderCaravanUtility_GetTraderCaravanRole_Patch
    {
        [HarmonyPostfix]
        public static void Lynians_GetTraderCaravanRole_Patch(Pawn p, ref TraderCaravanRole __result)
        {
            PawnKindProperties props = PawnKindProperties.Get(p.kindDef);
            if (props != null && props.purchasableFromTrader)
            {
                __result = TraderCaravanRole.Chattel;
            }
        }
    }

    /// <summary>
    /// Ensures recruit pawn kinds are not sold into slavery
    /// </summary>
    [HarmonyPatch(typeof(Pawn_GuestTracker))]
    [HarmonyPatch("RandomizeJoinStatus")]
    public static class Pawn_GuestTracker_RandomizeJoinStatus_Patch
    {
        [HarmonyPostfix]
        public static void Lynians_RandomizeJoinStatus_Patch(ref Pawn ___pawn, ref JoinStatus ___joinStatus)
        {
            if (___joinStatus != JoinStatus.JoinAsColonist)
            {
                if (Utility.IsRecruitCheck(___pawn))
                {
                    ___joinStatus = JoinStatus.JoinAsColonist;
                }
            }
        }
    }

    /// <summary>
    /// Remove the thought as Lynian recruits are not slaves
    /// </summary>
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("PreTraded")]
    public static class Pawn_PreTraded_Patch
    {
        [HarmonyPostfix]
        public static void Lynians_PreTraded_Patch(ref Pawn __instance)
        {
            if (Utility.IsRecruitCheck(__instance))
            {
                Need_Mood mood = __instance.needs.mood;
                if (mood != null)
                {
                    mood.thoughts.memories.RemoveMemoriesOfDef(RimWorld.ThoughtDefOf.FreedFromSlavery);
                }
            }
        }
    }
}
