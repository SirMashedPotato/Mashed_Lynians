using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace Mashed_Lynians
{
    public class ThoughtWorker_MissingMask_GenericThought : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (GenDate.DaysPassedSinceSettle < 15)
            {
                return false;
            }
            RaceProperties rp = RaceProperties.Get(p.def);
            if (rp == null || !rp.isLynian)
            {
                return false;
            }
            List<Apparel> wornApparel = p.apparel.WornApparel.Where(x => x.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.FullHead)).ToList();
            return wornApparel.NullOrEmpty();
        }
    }
}
