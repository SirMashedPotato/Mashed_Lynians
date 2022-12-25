using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_EffecterOnSelf : CompAbilityEffect
	{
		public new CompProperties_AbilityEffecterOnSelf Props
		{
			get
			{
				return (CompProperties_AbilityEffecterOnSelf)this.props;
			}
		}
		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
            if (target.Pawn != null && target.Pawn == parent.pawn)
            {
				Effecter effecter;

				effecter = Props.effecterDef.Spawn(target.Thing, parent.pawn.Map, Props.scale);

				if (Props.maintainForTicks > 0)
				{
					parent.AddEffecterToMaintain(effecter, target.Cell, Props.maintainForTicks, null);
					return;
				}
				effecter.Cleanup();
			}
		}
	}
}
