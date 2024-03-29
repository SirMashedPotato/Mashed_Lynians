﻿using HarmonyLib;
using System.Text;
using Verse;
using System.Reflection;
using RimWorld;
using System;

namespace Mashed_Lynians
{
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.Mashed_Lynians");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    /// <summary>
    /// Just adds hediffs and abilities to pawns when they are generated.
    /// </summary>
    [HarmonyPatch(typeof(PawnGenerator))]
    [HarmonyPatch("GenerateInitialHediffs")]
    public static class PawnGenerator_GenerateInitialHediffs_Patch
    {
        [HarmonyPostfix]
        public static void LyniansPatch(Pawn pawn)
        {
            RaceProperties props = RaceProperties.Get(pawn.def);
            if (props != null)
            {
                if (!props.hediffsToAdd.NullOrEmpty())
                {
                    foreach (HediffDef hd in props.hediffsToAdd)
                    {
                        pawn.health.AddHediff(hd);
                    }
                }
                if (!props.abilitiesToAdd.NullOrEmpty())
                {
                    foreach (AbilityDef ad in props.abilitiesToAdd)
                    {
                        pawn.abilities.GainAbility(ad);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Replaces potential Felvine addiction with Alcohol addiction during pawn generation, for non-cat-like lynians.
    /// </summary>
    [HarmonyPatch(typeof(PawnAddictionHediffsGenerator))]
    [HarmonyPatch("ApplyAddiction")]
    public static class PawnAddictionHediffsGenerator_ApplyAddiction_Patch
    {
        [HarmonyPrefix]
        public static void LyniansPatch(Pawn pawn, ref ChemicalDef chemicalDef)
        {
            RaceProperties props = RaceProperties.Get(pawn.def);
            if (chemicalDef == ChemicalDefOf.Mashed_Lynian_FelvineChemical && (props == null || !props.canUseFelvine))
            {
                chemicalDef = RimWorld.ChemicalDefOf.Alcohol;
            }
        }
    }

    /// <summary>
    /// Allows for backpacks that increase carry weight in caravans.
    /// </summary>
    [HarmonyPatch(typeof(MassUtility))]
    [HarmonyPatch("Capacity")]
    public static class MassUtility_Capacity_Patch
    {
        [HarmonyPostfix]
        public static void LyniansPatch(ref float __result, Pawn p, StringBuilder explanation = null)
        {
            if (p != null && p.apparel != null && p.apparel.WornApparel != null)
            {
                foreach (Apparel ap in p.apparel.WornApparel)
                {
                    ApparelProperties props = ApparelProperties.Get(ap.def);
                    if (props != null && props.carryWeightIncrease != 0)
                    {
                        float additional = props.carryWeightIncrease;
                        if (!props.qualityCarryWeightMults.NullOrEmpty())
                        {
                            ap.TryGetQuality(out QualityCategory qc);
                            additional *= props.qualityCarryWeightMults[(int)qc];
                        }
                        __result += additional;
                    }
                }
            }
        }

        /// <summary>
        /// Recolours specific apparel using colorGenerator when crafted.
        /// </summary>
        [HarmonyPatch(typeof(GenRecipe))]
        [HarmonyPatch("PostProcessProduct")]
        public static class GenRecipe_PostProcessProduct_Patch
        {
            [HarmonyPostfix]
            public static void LyniansPatch(Thing __result)
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
            [HarmonyPostfix]
            public static void LyniansPatch(Thing __result)
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
        /// Replaces the man in black pawnkind
        /// </summary>
        [HarmonyPatch(typeof(PawnGenerator))]
        [HarmonyPatch("GeneratePawn")]
        [HarmonyPatch(new Type[] { typeof(PawnGenerationRequest) })]
        public static class PawnGenerator_GeneratePawn_Patch
        {
            [HarmonyPrefix]
            public static void Lynians_ManInBlack_Patch(ref PawnGenerationRequest request)
            {
                if(request.Faction != null)
                {
                    FactionProperties props = FactionProperties.Get(request.Faction.def);
                    if (props != null && props.manInBlackReplacer != null && request.KindDef == PawnKindDef.Named("StrangerInBlack"))
                    {
                        request.KindDef = props.manInBlackReplacer;
                    }
                }
              
            }
        }

        /// <summary>
        /// Dobules the cooking speed stat for pawns affected by the cooking furrenzy hediff
        /// Also allows an increase beyond the normal limit of 160%
        /// </summary>
        [HarmonyPatch(typeof(StatExtension))]
        [HarmonyPatch("GetStatValue")]
        public static class StatExtension_GetStatValue_Patch
        {
            [HarmonyPostfix]
            public static void Lynians_CookingFrurrenzy_Patch(Thing thing, StatDef stat, ref float __result)
            {
                if (stat == StatDefOf.CookSpeed && thing is Pawn p && p.RaceProps.Humanlike)
                {
                    if (p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Mashed_Lynian_LynianCookingFurrenzy) != null)
                    {
                        __result *= 2f;
                    }
                }
            }
        }
    }
}
