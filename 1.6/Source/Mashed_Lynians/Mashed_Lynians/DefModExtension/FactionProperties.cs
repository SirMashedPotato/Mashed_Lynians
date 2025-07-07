using Verse;

namespace Mashed_Lynians
{
    public class FactionProperties : DefModExtension
    {
        public PawnKindDef manInBlackReplacer;

        public static FactionProperties Get(Def def) => def.GetModExtension<FactionProperties>();
    }
}
