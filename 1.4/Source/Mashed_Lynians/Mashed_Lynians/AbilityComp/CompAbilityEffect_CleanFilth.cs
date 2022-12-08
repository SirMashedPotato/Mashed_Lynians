using Verse;
using RimWorld;
using System.Collections.Generic;
using System.Linq;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_CleanFilth : CompAbilityEffect
    {
        public new CompProperties_AbilityCleanFilth Props
        {
            get
            {
                return (CompProperties_AbilityCleanFilth)this.props;
            }
        }


        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            Pawn user = parent.pawn;
            if (target.Pawn != null && target.Pawn == user)
            {
                IEnumerable<IntVec3> cells = GenRadial.RadialCellsAround(user.Position, parent.def.verbProperties.range, true);
                foreach(IntVec3 cell in cells)
                {
                    List<Thing> filth = cell.GetThingList(user.Map).Where(x => x is Filth).ToList();
                    if (!filth.NullOrEmpty())
                    {
                        for (int i = filth.Count-1; i >= 0; i--)
                        {
                            FleckMaker.ThrowDustPuff(cell, user.Map, 1f);
                            filth[i].Destroy();
                        }
                    }
                }
            }
        }
    }
}
