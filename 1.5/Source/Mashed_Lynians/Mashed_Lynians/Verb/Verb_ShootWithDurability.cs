using Verse;

namespace Mashed_Lynians
{
    public class Verb_ShootWithDurability : Verb_Shoot
    {
        protected override bool TryCastShot()
        {
            if (base.TryCastShot())
            {
                if (base.EquipmentSource != null)
                {
                    base.EquipmentSource.HitPoints -= 15;
                    if (base.EquipmentSource.HitPoints <= 0)
                    {
                        this.SelfConsume();
                    }
                }
            }
            return true;
        }

        private void SelfConsume()
        {
            if (base.EquipmentSource != null && !base.EquipmentSource.Destroyed)
            {
                base.EquipmentSource.Destroy(DestroyMode.Vanish);
            }
        }
    }
}
