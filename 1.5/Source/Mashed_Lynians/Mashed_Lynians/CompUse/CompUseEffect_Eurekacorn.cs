using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompUseEffect_Eurekacorn : CompUseEffect
	{
		public CompProperties_UseEffectEurekacorn Props
		{
			get
			{
				return (CompProperties_UseEffectEurekacorn)props;
			}
		}

		public override void DoEffect(Pawn usedBy)
		{
			base.DoEffect(usedBy);
            if (Props.ability != null)
            {
				usedBy.abilities.GainAbility(Props.ability);
				Messages.Message("Mashed_Lynian_EurekacornGainedAbility".Translate(usedBy.Name, Props.ability.label), usedBy, MessageTypeDefOf.PositiveEvent);
			}
			if (Props.hediff != null)
            {
				usedBy.health.AddHediff(Props.hediff);
				Messages.Message("Mashed_Lynian_EurekacornGainedHediff".Translate(usedBy.Name, Props.hediff.label), usedBy, MessageTypeDefOf.PositiveEvent);
			}
            if (Props.fillHunger)
            {
				Need hunger = usedBy.needs.TryGetNeed(NeedDefOf.Food);
                if (hunger != null)
                {
					hunger.CurLevel = hunger.MaxLevel;
                }
            }
		}

		public override bool CanBeUsedBy(Pawn p, out string failReason)
		{
			if (!Utility.PawnIsLynian(p))
			{
				failReason = "Mashed_Lynian_PawnNotLynian".Translate(p.Name);
				return false;
			}
			if (Props.ability != null && p.abilities.GetAbility(Props.ability) != null)
			{
				failReason = "Mashed_Lynian_EurekacornHasAbility".Translate(p.Name, Props.ability.label);
				return false;
			}
			if (Props.hediff != null && p.health.hediffSet.GetFirstHediffOfDef(Props.hediff) != null)
			{
				failReason = "Mashed_Lynian_EurekacornHasHediff".Translate(p.Name, Props.hediff.label);
				return false;
			}
			return base.CanBeUsedBy(p, out failReason);
		}

        public override string TransformLabel(string label)
        {
            return base.TransformLabel(label);
        }
    }
}
