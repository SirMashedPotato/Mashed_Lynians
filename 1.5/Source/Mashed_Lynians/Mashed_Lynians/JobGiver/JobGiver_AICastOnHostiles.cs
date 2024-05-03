using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;
using RimWorld;

namespace Mashed_Lynians
{
    public class JobGiver_AICastOnHostiles : JobGiver_AICastAbility
    {
		protected override LocalTargetInfo GetTarget(Pawn caster, Ability ability)
		{
			potentialTargets.Clear();
			IEnumerable<Thing> hostiles = from x in caster.Map.attackTargetsCache.GetPotentialTargetsFor(caster)
										  select x.Thing;
			if (hostiles.EnumerableNullOrEmpty())
			{
				return LocalTargetInfo.Invalid;
			}
			foreach (Pawn pawn in caster.Map.mapPawns.AllPawnsSpawned)
			{
				if (pawn.HostileTo(caster) && pawn.Position.InHorDistOf(caster.Position, MaxDistanceFromCaster) && ability.CanApplyOn(new LocalTargetInfo(pawn)))
				{
					potentialTargets.Add(pawn);
				}
			}
			if (potentialTargets.TryRandomElementByWeight(delegate (Pawn x)
            {
				float num = MaxSquareDistanceFromTarget;
				foreach (Thing thing2 in hostiles)
                {
					if (thing2.Spawned)
                    {
						float num2 = thing2.Position.DistanceToSquared(x.Position);
                        if (num2 < num)
                        {
							num = num2;
                        }
                    }
                }
				return DistanceSquaredToTargetSelectionWeightCurve.Evaluate(num);
            }, out Pawn thing))
            {
				return new LocalTargetInfo(thing);
            }
			return LocalTargetInfo.Invalid;
		}

		private const float MaxDistanceFromCaster = 30f;
		private const float MaxSquareDistanceFromTarget = 625f;
		private static readonly SimpleCurve DistanceSquaredToTargetSelectionWeightCurve = new SimpleCurve
		{
			{
				new CurvePoint(100f, 1f),
				true
			},
			{
				new CurvePoint(MaxSquareDistanceFromTarget/2, 0.1f),
				true
			},
			{
				new CurvePoint(MaxSquareDistanceFromTarget, 0f),
				true
			}
		};
		private static List<Pawn> potentialTargets = new List<Pawn>();
	}
}
