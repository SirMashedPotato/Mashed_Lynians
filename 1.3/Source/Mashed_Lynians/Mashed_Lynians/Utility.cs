using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public static class Utility
    {
        public static bool CanUseFelvine(Pawn pawn)
        {
            return CanUseFelvine(pawn as Thing);
        }

        public static bool CanUseFelvine(Thing thing)
        {
            RaceProperties props = RaceProperties.Get(thing.def);
            return props != null && props.canUseFelvine;
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

        public static readonly List<PawnKindDef> lynianColonistKindList = new List<PawnKindDef>
        {
            PawnKindDefOf.Mashed_Lynian_BoaboaColonist, PawnKindDefOf.Mashed_Lynian_FelyneColonist, PawnKindDefOf.Mashed_Lynian_GajalakaColonist,
            PawnKindDefOf.Mashed_Lynian_GrimalkyneColonist, PawnKindDefOf.Mashed_Lynian_MelynxColonist, PawnKindDefOf.Mashed_Lynian_ShakalakaColonist,
            PawnKindDefOf.Mashed_Lynian_UrukiColonist
        };
    }
}
