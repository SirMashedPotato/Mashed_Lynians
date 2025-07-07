using RimWorld;
using Verse;

namespace Mashed_Lynians
{
    [DefOf]
    public static class GeneDefOf
    {
        static GeneDefOf() =>  DefOfHelper.EnsureInitializedInCtor(typeof(GeneDefOf));

        [MayRequireBiotech]
        public static GeneDef ChemicalDependency_Mashed_Lynian_FelvineChemical;
        [MayRequireBiotech]
        public static GeneDef AddictionResistant_Mashed_Lynian_FelvineChemical;
        [MayRequireBiotech]
        public static GeneDef AddictionImmune_Mashed_Lynian_FelvineChemical;
    }
}