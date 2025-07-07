using Verse;

namespace Mashed_Lynians
{
    public class PawnKindProperties : DefModExtension
    {
        public bool purchasableFromTrader = false;

        public static PawnKindProperties Get(Def def) => def.GetModExtension<PawnKindProperties>();
    }
}
