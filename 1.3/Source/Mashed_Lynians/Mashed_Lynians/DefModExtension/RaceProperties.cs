using Verse;
using System.Collections.Generic;

namespace Mashed_Lynians
{
    public class RaceProperties : DefModExtension
    {
        public bool canUseFelvine = true;
        public List<HediffDef> hediffsToAdd;

        public static RaceProperties Get(Def def)
        {
            return def.GetModExtension<RaceProperties>();
        }
    }
}
