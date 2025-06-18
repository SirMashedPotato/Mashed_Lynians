using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    [DefOf]
    public static class TerrainAffordanceDefOf
    {

        static TerrainAffordanceDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TerrainAffordanceDefOf));
        }

        public static TerrainAffordanceDef Diggable;
    }
}
