using Verse;

namespace Mashed_Lynians
{
    public class RaceProperties : DefModExtension
    {
        public bool canUseFelvine = true;
        public static RaceProperties Get(Def def)
        {
            return def.GetModExtension<RaceProperties>();
        }
    }
}
