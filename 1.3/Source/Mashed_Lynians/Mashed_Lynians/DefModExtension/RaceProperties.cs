using Verse;
using System.Collections.Generic;

namespace Mashed_Lynians
{
    public class RaceProperties : DefModExtension
    {
        public bool isLynian = true;
        public bool canUseFelvine = false;
        public List<HediffDef> hediffsToAdd;

        public static RaceProperties Get(Def def)
        {
            return def.GetModExtension<RaceProperties>();
        }
    }
}
