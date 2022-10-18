using RimWorld;

namespace Mashed_Lynians
{
    public class JobDriver_SmellFelvine : JobDriver_VisitJoyThing
    {
		protected override void WaitTickAction()
		{
			this.pawn.GainComfortFromCellIfPossible(false);
			JoyUtility.JoyTickCheckEnd(this.pawn, JoyTickFullJoyAction.EndJob);
		}
	}
}
