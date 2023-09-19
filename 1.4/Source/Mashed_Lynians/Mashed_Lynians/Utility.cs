using System.Collections.Generic;
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
                if (pawn.genes.HasGene(GeneDefOf.AddictionImmune_Mashed_Lynian_FelvineChemical) 
                    || pawn.genes.HasGene(GeneDefOf.AddictionResistant_Mashed_Lynian_FelvineChemical)
                    || pawn.genes.HasGene(GeneDefOf.AddictionImmune_Mashed_Lynian_FelvineChemical))
                {
                    return true;
                }
            }

            RaceProperties props = RaceProperties.Get(pawn.def);
            return props != null && props.canUseFelvine;
        }

        public static bool WearingCatLikeMask(Pawn pawn)
        {
            if (pawn.apparel != null)
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
            return PawnIsLynian(pawn as Thing);
        }

        public static bool PawnIsLynian(Thing thing)
        {
            RaceProperties props = RaceProperties.Get(thing.def);
            return props != null && props.isLynian;
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

        public static readonly List<PawnKindDef> lynianColonistKindList = new List<PawnKindDef>
        {
            PawnKindDefOf.Mashed_Lynian_BoaboaColonist, PawnKindDefOf.Mashed_Lynian_FelyneColonist, PawnKindDefOf.Mashed_Lynian_GajalakaColonist,
            PawnKindDefOf.Mashed_Lynian_GrimalkyneColonist, PawnKindDefOf.Mashed_Lynian_MelynxColonist, PawnKindDefOf.Mashed_Lynian_ShakalakaColonist,
            PawnKindDefOf.Mashed_Lynian_UrukiColonist
        };
    }
}
