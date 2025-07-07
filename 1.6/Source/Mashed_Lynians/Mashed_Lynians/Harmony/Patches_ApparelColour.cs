using HarmonyLib;
using Verse;

namespace Mashed_Lynians
{
    /// <summary>
    /// Recolours specific apparel using colorGenerator when crafted.
    /// </summary>
    [HarmonyPatch(typeof(GenRecipe))]
    [HarmonyPatch("PostProcessProduct")]
    public static class GenRecipe_PostProcessProduct_Patch
    {
        public static void Postfix(Thing __result)
        {
            ApparelProperties props = ApparelProperties.Get(__result.def);
            if (props != null && props.overrideColour)
            {
                if (__result.def.colorGenerator != null)
                {
                    CompColorableUtility.SetColor(__result, __result.def.colorGenerator.NewRandomizedColor(), true);
                }
            }
        }
    }

    /// <summary>
    /// Recolours specific apparel using colorGenerator.
    /// Covers most cases, apart from crafting
    /// </summary>
    [HarmonyPatch(typeof(ThingMaker))]
    [HarmonyPatch("MakeThing")]
    public static class ThingMaker_MakeThing_Patch
    {
        public static void Postfix(Thing __result)
        {
            ApparelProperties props = ApparelProperties.Get(__result.def);
            if (props != null && props.overrideColour)
            {
                if (__result.def.colorGenerator != null)
                {
                    CompColorableUtility.SetColor(__result, __result.def.colorGenerator.NewRandomizedColor(), true);
                }
            }
        }
    }
}
