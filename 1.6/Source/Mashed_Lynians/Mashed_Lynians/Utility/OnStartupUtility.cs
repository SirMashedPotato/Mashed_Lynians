using Verse;
using System.Collections.Generic;
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
            RemoveFacialAnimationComps();
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

        public static void RemoveFacialAnimationComps()
        {
            if (!ModsConfig.IsActive("nals.facialanimation") || !Mashed_Lynians_ModSettings.RemoveFacialAnimationComps)
            {
                return;
            }
            Log.Message("[Mashed's Lynians] detected [NL] Facial Animation. Removing FacialAnimation comps from Lynian races");
            foreach (ThingDef race in LynianRaces)
            {
                for (int i = race.comps.Count - 1; i >= 0; i--)
                {
                    if (race.comps[i].compClass.Namespace == "FacialAnimation")
                    {
                        race.comps.Remove(race.comps[i]);
                    }
                }
            }
        }
    }
}
