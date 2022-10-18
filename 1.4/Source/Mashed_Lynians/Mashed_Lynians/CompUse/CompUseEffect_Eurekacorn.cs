using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompUseEffect_Eurekacorn : CompUseEffect_Artifact
    {
		new public CompProperties_UseEffectEurekacorn Props
		{
			get
			{
				return (CompProperties_UseEffectEurekacorn)this.props;
			}
		}

		public override void DoEffect(Pawn usedBy)
		{
			base.DoEffect(usedBy);
			usedBy.abilities.GainAbility(Props.ability);
			Messages.Message("Mashed_Lynian_EurekacornGainedAbility".Translate(usedBy.Name, Props.ability.label),usedBy , MessageTypeDefOf.PositiveEvent);
		}

		public override bool CanBeUsedBy(Pawn p, out string failReason)
		{
			if (!Utility.PawnIsLynian(p))
			{
				failReason = "Mashed_Lynian_PawnNotLynian".Translate(p.Name);
				return false;
			}
			if (p.abilities.GetAbility(Props.ability) != null)
			{
				failReason = "Mashed_Lynian_EurekacornHasAbility".Translate(p.Name, Props.ability.label);
				return false;
			}
			return base.CanBeUsedBy(p, out failReason);
		}
	}
}
