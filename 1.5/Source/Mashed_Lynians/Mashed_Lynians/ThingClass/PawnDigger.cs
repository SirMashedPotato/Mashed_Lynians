using System;
using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

namespace Mashed_Lynians
{
    public class PawnDigger : PawnFlyer
    {
		/// <summary>
		/// TODO possibly rework this for changes in 1.5
		/// </summary>
		public override Vector3 DrawPos
		{
			get
			{
				this.RecomputePosition();
				return this.groundPos;
			}
		}

		static PawnDigger()
		{
			AnimationCurve animationCurve = new AnimationCurve();
			animationCurve.AddKey(0f, 0f);
			animationCurve.AddKey(0.1f, 0.15f);
			animationCurve.AddKey(1f, 1f);
			PawnDigger.FlightSpeed = new Func<float, float>(animationCurve.Evaluate);
		}

		private void RecomputePosition()
		{
			if (this.positionLastComputedTick == this.ticksFlying)
			{
				return;
			}
			this.positionLastComputedTick = this.ticksFlying;
			float arg = (float)this.ticksFlying / (float)this.ticksFlightTime;
			float num = PawnDigger.FlightSpeed(arg);
			this.groundPos = Vector3.Lerp(this.startVec, base.DestinationPos, num);
		}

		/* TODO fuck
		public override void DrawAt(Vector3 drawLoc, bool flip = false)
		{
			this.RecomputePosition();
            if (!Find.TickManager.Paused)
            {
                if (Rand.Chance(0.5f))
                {
					FleckMaker.ThrowDustPuffThick(this.groundPos, base.Map, 1f, Color.white);
				}
			}
		}
		*/

        protected override void RespawnPawn()
		{
			this.LandingEffects();
			base.RespawnPawn();
		}
		
		private void LandingEffects()
		{
			if (this.soundLanding != null)
			{
				this.soundLanding.PlayOneShot(new TargetInfo(base.Position, base.Map, false));
			}
			FleckMaker.ThrowDustPuffThick(base.DestinationPos, base.Map, 2f, Color.white);
		}

		public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
		{
			Effecter effecter = this.flightEffecter;
			if (effecter != null)
			{
				effecter.Cleanup();
			}
			base.Destroy(mode);
		}

		private static readonly Func<float, float> FlightSpeed;
		private Effecter flightEffecter;
		private int positionLastComputedTick = -1;
		private Vector3 groundPos;
	}
}
