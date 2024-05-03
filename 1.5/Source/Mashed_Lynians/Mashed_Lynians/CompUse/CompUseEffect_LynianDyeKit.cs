using Verse;
using RimWorld;
using static AlienRace.AlienPartGenerator;

namespace Mashed_Lynians
{
    public class CompUseEffect_LynianDyeKit : CompUseEffect
    {

		public override void DoEffect(Pawn usedBy)
		{
			Comp_LynianDyeKit dyeComp = parent.TryGetComp<Comp_LynianDyeKit>();
			AlienComp alienComp = usedBy.TryGetComp<AlienComp>();
			alienComp.OverwriteColorChannel("skin", dyeComp.primaryColor, dyeComp.secondaryColor);
			usedBy.Drawer.renderer.SetAllGraphicsDirty();
		}

		public override AcceptanceReport CanBeUsedBy(Pawn p)
		{
			if (!Utility.PawnIsLynian(p))
			{
				return "Mashed_Lynian_PawnNotLynian".Translate(p.Name);
			}
			return true;
		}
    }
}
