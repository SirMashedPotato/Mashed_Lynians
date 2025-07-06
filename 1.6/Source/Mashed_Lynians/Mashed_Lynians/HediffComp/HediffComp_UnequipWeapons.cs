using Verse;

namespace Mashed_Lynians
{
    public class HediffComp_UnequipWeapons : HediffComp
    {
        public override void CompPostMake()
        {
            base.CompPostMake();
            Pawn.equipment.DropAllEquipment(Pawn.Position);
        }
    }
}
