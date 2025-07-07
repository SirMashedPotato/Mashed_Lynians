using HarmonyLib;
using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    /// <summary>
    /// Replaces potential Felvine addiction with Alcohol addiction during pawn generation, for non-cat-like lynians.
    /// </summary>
    [HarmonyPatch(typeof(PawnAddictionHediffsGenerator))]
    [HarmonyPatch("ApplyAddiction")]
    public static class PawnAddictionHediffsGenerator_ApplyAddiction_Patch
    {
        public static void Prefix(Pawn pawn, ref ChemicalDef chemicalDef)
        {
            if (chemicalDef == ChemicalDefOf.Mashed_Lynian_FelvineChemical && !Utility.PawnCanUseFelvine(pawn))
            {
                chemicalDef = RimWorld.ChemicalDefOf.Alcohol;
            }
        }
    }
}
