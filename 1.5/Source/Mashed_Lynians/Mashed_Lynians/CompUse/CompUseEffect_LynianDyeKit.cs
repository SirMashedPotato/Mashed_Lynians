using Verse;
using RimWorld;
using static AlienRace.AlienPartGenerator;

namespace Mashed_Lynians
{
    public class CompUseEffect_LynianDyeKit : CompUseEffect
    {

		public override void DoEffect(Pawn usedBy)
		{
			///For selecting colours
			///1- pops up colour picker ui when used, set both colours there
			///2- gizmo on item, set colours individually <- this one me thinks

			Comp_LynianDyeKit dyeComp = parent.TryGetComp<Comp_LynianDyeKit>();
			AlienComp alienComp = usedBy.TryGetComp<AlienComp>();
			alienComp.OverwriteColorChannel("skin", dyeComp.primaryColor, dyeComp.secondaryColor);
			usedBy.Drawer.renderer.graphics.SetAllGraphicsDirty();
		}

		public override bool CanBeUsedBy(Pawn p, out string failReason)
		{
			if (!Utility.PawnIsLynian(p))
			{
				failReason = "Mashed_Lynian_PawnNotLynian".Translate(p.Name);
				return false;
			}
			return base.CanBeUsedBy(p, out failReason);
		}
    }
}
