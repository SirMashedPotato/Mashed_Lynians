using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class CompUseEffect_Eurekacorn : CompUseEffect
	{
		public CompProperties_UseEffectEurekacorn Props => (CompProperties_UseEffectEurekacorn)props;

		public override void DoEffect(Pawn usedBy)
		{
			base.DoEffect(usedBy);
            //TODO
            if (Props.fillHunger)
            {
				Need hunger = usedBy.needs.TryGetNeed(NeedDefOf.Food);
                if (hunger != null)
                {
					hunger.CurLevel = hunger.MaxLevel;
                }
            }
		}

		public override AcceptanceReport CanBeUsedBy(Pawn p)
		{
			if (!Utility.PawnIsLynian(p))
			{
                return "Mashed_Lynian_PawnNotLynian".Translate(p.Name);
			}
            return true;
        }

        public override string TransformLabel(string label)
        {
            return base.TransformLabel(label);
        }
    }
}
