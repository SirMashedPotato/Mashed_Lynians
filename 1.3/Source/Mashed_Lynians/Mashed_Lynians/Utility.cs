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
    }
}
