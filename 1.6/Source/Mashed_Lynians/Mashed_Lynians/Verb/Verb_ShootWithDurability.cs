using Verse;

namespace Mashed_Lynians
{
    public class Verb_ShootWithDurability : Verb_Shoot
    {
        protected override bool TryCastShot()
        {
            if (base.TryCastShot())
            {
                if (EquipmentSource != null)
                {
                    EquipmentSource.HitPoints -= 15;
                    if (EquipmentSource.HitPoints <= 0)
                    {
                        SelfConsume();
                    }
                }
            }
            return true;
        }

        private void SelfConsume()
        {
            if (EquipmentSource != null && !EquipmentSource.Destroyed)
            {
                EquipmentSource.Destroy(DestroyMode.Vanish);
            }
        }
    }
}
