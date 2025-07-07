using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

namespace Mashed_Lynians
{
    [StaticConstructorOnStartup]
    public class Comp_UltimateMaskShield : CompShield
	{
		public new CompProperties_UltimateMaskShield Props => (CompProperties_UltimateMaskShield)props;

		private float EnergyMax =>  parent.GetStatValue(RimWorld.StatDefOf.EnergyShieldEnergyMax, true, -1);

		public override void CompDrawWornExtras()
		{
			if (IsApparel)
			{
				Draw();
			}
		}

		public override void PostPreApplyDamage(ref DamageInfo dinfo, out bool absorbed)
		{
			absorbed = false;
			if (ShieldState != ShieldState.Active)
			{
				return;
			}
			energy -= dinfo.Amount * Props.energyLossPerDamage;
			if (energy < 0f)
			{
				Break();
			}
			else
			{
				AbsorbedDamage(dinfo);
			}
			absorbed = true;
			return;
		}

		private void AbsorbedDamage(DamageInfo dinfo)
		{
			SoundDefOf.EnergyShield_AbsorbDamage.PlayOneShot(new TargetInfo(PawnOwner.Position, PawnOwner.Map, false));
			impactAngleVect = Vector3Utility.HorizontalVectorFromAngle(dinfo.Angle);
			Vector3 loc = PawnOwner.TrueCenter() + impactAngleVect.RotatedBy(180f) * 0.5f;
			float num = Mathf.Min(10f, 2f + dinfo.Amount / 10f);
			FleckMaker.Static(loc, PawnOwner.Map, FleckDefOf.ExplosionFlash, num);
			int num2 = (int)num;
			for (int i = 0; i < num2; i++)
			{
				FleckMaker.ThrowDustPuff(loc, PawnOwner.Map, Rand.Range(0.8f, 1.2f));
			}
			lastAbsorbDamageTick = Find.TickManager.TicksGame;
			KeepDisplaying();
		}

		private void Break()
		{
			float scale = Mathf.Lerp(Props.minDrawSize, Props.maxDrawSize, energy);
			EffecterDefOf.Shield_Break.SpawnAttached(parent, parent.MapHeld, scale);
			FleckMaker.Static(PawnOwner.TrueCenter(), PawnOwner.Map, FleckDefOf.ExplosionFlash, 12f);
			for (int i = 0; i < 6; i++)
			{
				FleckMaker.ThrowDustPuff(PawnOwner.TrueCenter() + Vector3Utility.HorizontalVectorFromAngle(Rand.Range(0, 360)) * Rand.Range(0.3f, 0.6f), PawnOwner.Map, Rand.Range(0.8f, 1.2f));
			}
			energy = 0f;
			ticksToReset = Props.startingTicksToReset;
		}

		private void Draw()
		{
			if (ShieldState == ShieldState.Active && ShouldDisplay)
			{
				float num = Mathf.Lerp(Props.minDrawSize, Props.maxDrawSize, energy/EnergyMax);
				Vector3 vector = PawnOwner.Drawer.DrawPos;
				vector.y = AltitudeLayer.MoteOverhead.AltitudeFor();
				int num2 = Find.TickManager.TicksGame - lastAbsorbDamageTick;
				if (num2 < 8)
				{
					float num3 = (8 - num2) / 8f * 0.05f;
					vector += impactAngleVect * num3;
					num -= num3;
				}
				Vector3 s = new Vector3(num, 1f, num);
				Matrix4x4 matrix = default;
				matrix.SetTRS(vector, Quaternion.AngleAxis(0, Vector3.up), s);
				Material BubbleMat = MaterialPool.MatFrom(Props.texPath, ShaderDatabase.MoteGlow);
				Graphics.DrawMesh(MeshPool.plane10, matrix, BubbleMat, 0);
			}
		}

		private Vector3 impactAngleVect;
		private int lastAbsorbDamageTick = -9999;
	}
}
