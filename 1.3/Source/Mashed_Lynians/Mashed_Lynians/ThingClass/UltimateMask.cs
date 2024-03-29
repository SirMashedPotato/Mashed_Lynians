﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

namespace Mashed_Lynians
{
    [StaticConstructorOnStartup]
    public class UltimateMask : Apparel
	{
		private float EnergyMax
		{
			get
			{
				return this.GetStatValue(RimWorld.StatDefOf.EnergyShieldEnergyMax, true);
			}
		}

		private float EnergyGainPerTick
		{
			get
			{
				return this.GetStatValue(RimWorld.StatDefOf.EnergyShieldRechargeRate, true) / 60f;
			}
		}

		public float Energy
		{
			get
			{
				return this.energy;
			}
		}

		public ShieldState ShieldState
		{
			get
			{
				if (this.ticksToReset > 0)
				{
					return ShieldState.Resetting;
				}
				return ShieldState.Active;
			}
		}

		private bool ShouldDisplay
		{
			get
			{
				Pawn wearer = base.Wearer;
				return wearer.Spawned && !wearer.Dead && !wearer.Downed && (wearer.InAggroMentalState || wearer.Drafted || (wearer.Faction.HostileTo(Faction.OfPlayer) && !wearer.IsPrisoner) || Find.TickManager.TicksGame < this.lastKeepDisplayTick + this.KeepDisplayingTicks);
			}
		}

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<float>(ref this.energy, "energy", 0f, false);
			Scribe_Values.Look<int>(ref this.ticksToReset, "ticksToReset", -1, false);
			Scribe_Values.Look<int>(ref this.lastKeepDisplayTick, "lastKeepDisplayTick", 0, false);
		}

		public override IEnumerable<Gizmo> GetWornGizmos()
		{
			foreach (Gizmo gizmo in base.GetWornGizmos())
			{
				yield return gizmo;
			}
			if (Find.Selector.SingleSelectedThing == base.Wearer)
			{
				yield return new Gizmo_UltimateMaskStatus
				{
					shield = this
				};
			}
			yield break;
		}

		public override float GetSpecialApparelScoreOffset()
		{
			return this.EnergyMax * this.ApparelScorePerEnergyMax;
		}

		public override void Tick()
		{
			base.Tick();
			if (base.Wearer == null)
			{
				this.energy = 0f;
				return;
			}
			if (this.ShieldState == ShieldState.Resetting)
			{
				this.ticksToReset--;
				if (this.ticksToReset <= 0)
				{
					this.Reset();
					return;
				}
			}
			else if (this.ShieldState == ShieldState.Active)
			{
				this.energy += this.EnergyGainPerTick;
				if (this.energy > this.EnergyMax)
				{
					this.energy = this.EnergyMax;
				}
			}
		}

		public override bool CheckPreAbsorbDamage(DamageInfo dinfo)
		{
			if (this.ShieldState != ShieldState.Active)
			{
				return false;
			}
			this.energy -= dinfo.Amount * this.EnergyLossPerDamage;
			if (this.energy < 0f)
			{
				this.Break();
			}
			else
			{
				this.AbsorbedDamage(dinfo);
			}
			return true;
		}

		public void KeepDisplaying()
		{
			this.lastKeepDisplayTick = Find.TickManager.TicksGame;
		}

		private void AbsorbedDamage(DamageInfo dinfo)
		{
			SoundDefOf.EnergyShield_AbsorbDamage.PlayOneShot(new TargetInfo(base.Wearer.Position, base.Wearer.Map, false));
			this.impactAngleVect = Vector3Utility.HorizontalVectorFromAngle(dinfo.Angle);
			Vector3 loc = base.Wearer.TrueCenter() + this.impactAngleVect.RotatedBy(180f) * 0.5f;
			float num = Mathf.Min(10f, 2f + dinfo.Amount / 10f);
			FleckMaker.Static(loc, base.Wearer.Map, FleckDefOf.ExplosionFlash, num);
			int num2 = (int)num;
			for (int i = 0; i < num2; i++)
			{
				FleckMaker.ThrowDustPuff(loc, base.Wearer.Map, Rand.Range(minSize, maxSize));
			}
			this.lastAbsorbDamageTick = Find.TickManager.TicksGame;
			this.KeepDisplaying();
		}

		private void Break()
		{
			SoundDefOf.EnergyShield_Broken.PlayOneShot(new TargetInfo(base.Wearer.Position, base.Wearer.Map, false));
			FleckMaker.Static(base.Wearer.TrueCenter(), base.Wearer.Map, FleckDefOf.ExplosionFlash, 12f);
			for (int i = 0; i < 6; i++)
			{
				FleckMaker.ThrowDustPuff(base.Wearer.TrueCenter() + Vector3Utility.HorizontalVectorFromAngle((float)Rand.Range(0, 360)) * Rand.Range(0.3f, 0.6f), base.Wearer.Map, Rand.Range(minSize, maxSize));
			}
			this.energy = 0f;
			this.ticksToReset = this.StartingTicksToReset;
		}

		private void Reset()
		{
			if (base.Wearer.Spawned)
			{
				if (base.Wearer.Spawned)
				{
					SoundDefOf.EnergyShield_Reset.PlayOneShot(new TargetInfo(base.Wearer.Position, base.Wearer.Map, false));
					FleckMaker.ThrowLightningGlow(base.Wearer.TrueCenter(), base.Wearer.Map, 3f);
				}
				this.ticksToReset = -1;
				this.energy = this.EnergyOnReset;
			}
		}

		public override void DrawWornExtras()
		{
			if (this.ShieldState == ShieldState.Active && this.ShouldDisplay)
			{
				float num = Mathf.Lerp(minSize, maxSize, this.energy);
				Vector3 vector = base.Wearer.Drawer.DrawPos;
				vector.y = AltitudeLayer.MoteOverhead.AltitudeFor();
				int num2 = Find.TickManager.TicksGame - this.lastAbsorbDamageTick;
				if (num2 < JitterDurationTicks)
				{
					float num3 = (float)(JitterDurationTicks - num2) / 8f * MaxDamagedJitterDist;
					vector += this.impactAngleVect * num3;
					num -= num3;
				}
				//float angle = (float)Rand.Range(0, 360);
				Vector3 s = new Vector3(num, 1f, num);
				Matrix4x4 matrix = default(Matrix4x4);
				matrix.SetTRS(vector, Quaternion.AngleAxis(0, Vector3.up), s);
				Graphics.DrawMesh(MeshPool.plane10, matrix, UltimateMask.BubbleMat, 0);
			}
		}

		private float maxSize = 1.5f;
		private float minSize = 0.5f;
		private float energy;
		private int ticksToReset = -1;
		private int lastKeepDisplayTick = -9999;
		private Vector3 impactAngleVect;
		private int lastAbsorbDamageTick = -9999;
		private const float MaxDamagedJitterDist = 0.05f;
		private const int JitterDurationTicks = 8;
		private int StartingTicksToReset = 3200;
		private float EnergyOnReset = 0.2f;
		private float EnergyLossPerDamage = 0.033f;
		private int KeepDisplayingTicks = 1000;
        private float ApparelScorePerEnergyMax = 0.25f;
		private static readonly Material BubbleMat = MaterialPool.MatFrom("Other/Mashed_UltimateMaskShield", ShaderDatabase.Transparent);
	}
}
