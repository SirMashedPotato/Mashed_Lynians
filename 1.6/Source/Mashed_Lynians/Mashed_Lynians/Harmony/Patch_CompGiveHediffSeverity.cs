using HarmonyLib;
using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    /// <summary>
    /// Ensures only pawns that can ingest felvine are affected by the felvine stone
    /// </summary>
    [HarmonyPatch(typeof(CompGiveHediffSeverity))]
    [HarmonyPatch("AppliesTo")]
    public static class CompGiveHediffSeverity_AppliesTo_Patch
    {
        public static void Postfix(Pawn pawn, ref bool __result, ref CompGiveHediffSeverity __instance)
        {
            if (!__result)
            {
                return;
            }

            if (!ModsConfig.IdeologyActive)
            {
                return;
            }

            if (__instance.parent.def == ThingDefOf.Mashed_Lynian_FelvineStone)
            {
                __result = Utility.PawnCanUseFelvine(pawn);
            }
        }
    }
}
