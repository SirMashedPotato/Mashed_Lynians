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
				RecomputePosition();
				return groundPos;
			}
		}

		static PawnDigger()
		{
			AnimationCurve animationCurve = new AnimationCurve();
			animationCurve.AddKey(0f, 0f);
			animationCurve.AddKey(0.1f, 0.15f);
			animationCurve.AddKey(1f, 1f);
            FlightSpeed = new Func<float, float>(animationCurve.Evaluate);
		}

		private void RecomputePosition()
		{
			if (positionLastComputedTick == ticksFlying)
			{
				return;
			}
			positionLastComputedTick = ticksFlying;
			float arg = (float)ticksFlying / ticksFlightTime;
			float num = FlightSpeed(arg);
			groundPos = Vector3.Lerp(startVec, DestinationPos, num);
		}

		public override void DynamicDrawPhaseAt(DrawPhase phase, Vector3 drawLoc, bool flip = false)
		{
			RecomputePosition();
            if (!Find.TickManager.Paused)
            {
                if (Rand.Chance(0.5f))
                {
					FleckMaker.ThrowDustPuffThick(groundPos, Map, 1f, Color.white);
				}
			}
		}

        protected override void RespawnPawn()
		{
			LandingEffects();
			base.RespawnPawn();
		}
		
		private void LandingEffects()
		{
            soundLanding?.PlayOneShot(new TargetInfo(Position, Map, false));
			FleckMaker.ThrowDustPuffThick(DestinationPos, Map, 2f, Color.white);
		}

		public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
		{
			Effecter effecter = flightEffecter;
			effecter?.Cleanup();
			base.Destroy(mode);
		}

		private static readonly Func<float, float> FlightSpeed;
		private readonly Effecter flightEffecter;
		private int positionLastComputedTick = -1;
		private Vector3 groundPos;
	}
}
