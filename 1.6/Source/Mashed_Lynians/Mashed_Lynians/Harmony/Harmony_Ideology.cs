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
        [HarmonyPostfix]
        public static void Lynians_CompGiveHediffSeverity_Patch(Pawn pawn, ref bool __result, ref CompGiveHediffSeverity __instance)
        {
            if (ModsConfig.IdeologyActive)
            {
                if (__instance.parent.def == ThingDefOf.Mashed_Lynian_FelvineStone)
                {
                    if (__result)
                    {
                        __result = Utility.PawnCanUseFelvine(pawn);
                    }
                }
            }
           
        }
    }
}
