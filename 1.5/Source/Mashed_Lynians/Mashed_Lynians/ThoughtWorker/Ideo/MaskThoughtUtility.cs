using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace Mashed_Lynians
{
    public static class MaskThoughtUtility
    {
        public static bool Valid(Pawn p) => ValidNow(p) && Utility.PawnIsLynian(p);

        public static bool ValidNow(Pawn p)
        {
            return ExpectationsUtility.CurrentExpectationFor(p).order > 0;
        }

        public static bool WearingValidMask_Boaboa(Pawn p)
        {
            List<Apparel> wornApparel = p.apparel.WornApparel.Where(x => x.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.FullHead)).ToList();
            foreach (Apparel ap in wornApparel)
            {
                ApparelProperties props = ApparelProperties.Get(ap.def);
                if (props != null && props.isBoaboaMask)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool WearingValidMask_Gajalaka(Pawn p)
        {
            List<Apparel> wornApparel = p.apparel.WornApparel.Where(x => x.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.FullHead)).ToList();
            foreach (Apparel ap in wornApparel)
            {
                ApparelProperties props = ApparelProperties.Get(ap.def);
                if (props != null && props.isGajalakaMask)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool WearingValidMask_Shakalaka(Pawn p)
        {
            List<Apparel> wornApparel = p.apparel.WornApparel.Where(x => x.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.FullHead)).ToList();
            foreach (Apparel ap in wornApparel)
            {
                ApparelProperties props = ApparelProperties.Get(ap.def);
                if (props != null && props.isShakalakaMask)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool WearingValidMask_GoldenGajalaka(Pawn p)
        {
            List<Apparel> wornApparel = p.apparel.WornApparel.Where(x => x.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.FullHead)).ToList();
            foreach (Apparel ap in wornApparel)
            {
                ApparelProperties props = ApparelProperties.Get(ap.def);
                if (props != null && props.isGoldenGajalakaMask)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

   
