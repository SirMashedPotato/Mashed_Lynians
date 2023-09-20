using Verse;
using System.Collections.Generic;
using RimWorld;

namespace Mashed_Lynians
{
    public class RaceProperties : DefModExtension
    {
        public bool isLynian = true;
        public bool isCatLike = false;
        public bool canUseFelvine = false;
        public List<HediffDef> hediffsToAdd;
        public List<AbilityDef> abilitiesToAdd;
        public List<AbilityDef> oneOfRandomAbility;
        public float oneOfRandomChance = 1f;
        public GeneDef hybridInheritedGene;

        public static RaceProperties Get(Def def)
        {
            return def.GetModExtension<RaceProperties>();
        }
    }
}
