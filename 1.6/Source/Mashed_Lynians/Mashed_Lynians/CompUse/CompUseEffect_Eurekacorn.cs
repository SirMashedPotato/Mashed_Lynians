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

            Comp_EurekacornTracker comp_Eurekacorn = usedBy.GetComp<Comp_EurekacornTracker>();
            comp_Eurekacorn.GainSkillPoints(Props.skillPointCount);

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

            Comp_EurekacornTracker comp_Eurekacorn = p.GetComp<Comp_EurekacornTracker>();
            if (comp_Eurekacorn == null)
            {
                Log.Error(p.Label + " is missing Comp_EurekacornTracker");
                return false;
            }

            if (!comp_Eurekacorn.CanGainSkillPoints(Props.skillPointCount))
            {
                return "Mashed_Lynians_Eurekacorn_LimitMet".Translate(p);

            }

            return true;
        }

        public override string TransformLabel(string label)
        {
            return base.TransformLabel(label);
        }
    }
}
