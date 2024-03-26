using RimWorld.Planet;
using Verse;
using RimWorld;

namespace Mashed_Lynians
{
    public class GenStep_ShakalakaWanderer : GenStep_Scatterer
	{
		public override int SeedPart
		{
			get
			{
				return 931842770;
			}
		}

		protected override bool CanScatterAt(IntVec3 c, Map map)
		{
			return base.CanScatterAt(c, map) && c.Standable(map);
		}

		protected override void ScatterAt(IntVec3 loc, Map map, GenStepParams parms, int count = 1)
		{
			Pawn pawn;
			if (parms.sitePart != null && parms.sitePart.things != null && parms.sitePart.things.Any)
			{
				pawn = (Pawn)parms.sitePart.things.Take(parms.sitePart.things[0]);
			}
			else
			{
				DownedRefugeeComp component = map.Parent.GetComponent<DownedRefugeeComp>();
				if (component != null && component.pawn.Any)
				{
					pawn = component.pawn.Take(component.pawn[0]);
				}
				else
				{
                    if (ModsConfig.BiotechActive)
                    {
						pawn = SiteUtility.GenerateChildPawn(map.Tile, PawnKindDefOf.Mashed_Lynian_ShakalakaWanderer);
                    } 
					else
                    {
						pawn = DownedRefugeeQuestUtility.GenerateRefugee(map.Tile, PawnKindDefOf.Mashed_Lynian_ShakalakaWanderer, 0f);
					}
				}
			}
			HealthUtility.DamageUntilDowned(pawn, false);
			HealthUtility.DamageLegsUntilIncapableOfMoving(pawn, false);
			GenSpawn.Spawn(pawn, loc, map, WipeMode.Vanish);
			pawn.mindState.WillJoinColonyIfRescued = true;
			MapGenerator.rootsToUnfog.Add(loc);
			MapGenerator.SetVar<CellRect>("RectOfInterest", CellRect.CenteredOn(loc, 1, 1));
		}
	}
}
