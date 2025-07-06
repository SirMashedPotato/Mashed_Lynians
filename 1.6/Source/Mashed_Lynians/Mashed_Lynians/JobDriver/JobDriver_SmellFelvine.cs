using RimWorld;

namespace Mashed_Lynians
{
    public class JobDriver_SmellFelvine : JobDriver_VisitJoyThing
    {
        protected override void WaitTickAction(int delta)
        {
            pawn.GainComfortFromCellIfPossible(delta, false);
            JoyUtility.JoyTickCheckEnd(pawn, delta, JoyTickFullJoyAction.EndJob);
        }
    }
}
