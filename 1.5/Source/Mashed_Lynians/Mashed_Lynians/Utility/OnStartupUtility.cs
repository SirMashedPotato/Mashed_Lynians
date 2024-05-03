using Verse;
using System.Collections.Generic;
using RimWorld;
using System.Linq;

namespace Mashed_Lynians
{
    [StaticConstructorOnStartup]
    public static class OnStartupUtility
    {
        public static HashSet<ThingDef> LynianRaces = new HashSet<ThingDef>();

        static OnStartupUtility()
        {
            FillRaceLists(DefDatabase<ThingDef>.AllDefsListForReading.Where(x => x.race != null).ToList());
        }

        public static void FillRaceLists(List<ThingDef> raceDefs)
        {
            foreach (ThingDef def in raceDefs)
            {
                RaceProperties props = RaceProperties.Get(def);
                if (props != null)
                {
                    if (props.isLynian)
                    {
                        LynianRaces.Add(def);
                    }
                }
            }
        }
    }
}
