using Verse;
using System.Collections.Generic;
using RimWorld;
using System;

namespace Mashed_Lynians
{
    public class RaceProperties : DefModExtension
    {
        public bool isLynian = true;
        public bool isCatLike = false;
        public bool canUseFelvine = false;
        [Obsolete("Use xml to handle instead")]
        public List<HediffDef> hediffsToAdd;
        [Obsolete("Use xml to handle instead")]
        public List<AbilityDef> abilitiesToAdd;
        public List<AbilityDef> oneOfRandomAbility;
        public float oneOfRandomChance = 1f;
        public GeneDef hybridInheritedGene;

        public static RaceProperties Get(Def def) => def.GetModExtension<RaceProperties>();
    }
}
