using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public static class Utility
    {
        public static bool PawnCanUseFelvine(Pawn pawn)
        {
            if (WearingCatLikeMask(pawn))
            {
                return true;
            }
            if (ModsConfig.BiotechActive && pawn.genes != null)
            {
                if (pawn.genes.HasActiveGene(GeneDefOf.AddictionImmune_Mashed_Lynian_FelvineChemical) 
                    || pawn.genes.HasActiveGene(GeneDefOf.AddictionResistant_Mashed_Lynian_FelvineChemical)
                    || pawn.genes.HasActiveGene(GeneDefOf.AddictionImmune_Mashed_Lynian_FelvineChemical))
                {
                    return true;
                }
            }

            RaceProperties props = RaceProperties.Get(pawn.def);
            return props != null && props.canUseFelvine;
        }

        public static bool WearingCatLikeMask(Pawn pawn)
        {
            if (Mashed_Lynians_ModSettings.EnableCatLikeMasks && pawn.apparel != null)
            {
                Thing apparel = pawn.apparel.FirstApparelOnBodyPartGroup(BodyPartGroupDefOf.FullHead);
                if (apparel != null)
                {
                    ApparelProperties props = ApparelProperties.Get(apparel.def);
                    if (props != null && props.treatAsCatLike)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        public static bool PawnIsLynian(Pawn pawn)
        {
            return ThingIsLynian(pawn);
        }

        public static bool ThingIsLynian(Thing thing)
        {
            RaceProperties props = RaceProperties.Get(thing.def);
            return props != null && props.isLynian;
        }

        public static bool PawnIsCatLike(Pawn pawn)
        {
            return ThingIsCatLike(pawn) || WearingCatLikeMask(pawn);
        }

        public static bool ThingIsCatLike(Thing thing)
        {
            RaceProperties props = RaceProperties.Get(thing.def);
            return props != null && props.isCatLike;
        }

        public static bool IsRecruitCheck(Pawn pawn)
        {
            PawnKindProperties props = PawnKindProperties.Get(pawn.kindDef);
            if (props != null && props.purchasableFromTrader)
            {
                return true;
            }
            return false;
        }
    }
}
