using System.Collections.Generic;
using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    class RoleRequirement_RaceLynian : RoleRequirement
    {
        public override string GetLabel(Precept_Role role)
        {
            if (labelCached == null)
            {
                labelCached = "Mashed_Lynian_IdeoRequirementLynian".Translate();
            }
            return labelCached;
        }

        public override bool Met(Pawn p, Precept_Role role)
        {
            return races.Contains(p.def);
        }

        public HashSet<ThingDef> races;

        [NoTranslate]
        private string labelCached;
    }
}
