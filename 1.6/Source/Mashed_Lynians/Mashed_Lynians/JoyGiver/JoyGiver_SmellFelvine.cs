using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;
using RimWorld;

namespace Mashed_Lynians
{
    public class JoyGiver_SmellFelvine : JoyGiver
    {
        public override float GetChance(Pawn pawn)
        {
			return Mashed_Lynians_ModSettings.EnableSmellingFelvine &&  Utility.PawnCanUseFelvine(pawn) ? base.GetChance(pawn) : 0f;
        }

        public override Job TryGiveJob(Pawn pawn)
        {
            bool allowedOutside = JoyUtility.EnjoyableOutsideNow(pawn, null);
			Job result;
			try
			{
				candidates.AddRange(pawn.Map.listerThings.ThingsOfDef(def.thingDefs.First()).Where(delegate (Thing thing)
				{
					if ((!allowedOutside && !thing.Position.Roofed(thing.Map)) || !pawn.CanReserveAndReach(thing, PathEndMode.Touch, Danger.None, 1, -1, null, false) || !thing.IsPoliticallyProper(pawn))
					{
						return false;
					}
					Plant plant = thing as Plant;
                    if (!plant.HarvestableNow)
                    {
						return false;
                    }
					Room room = thing.GetRoom(RegionType.Set_All);
					return room != null && ((room.Role != RoomRoleDefOf.Bedroom && room.Role != RoomRoleDefOf.Barracks && room.Role != RoomRoleDefOf.PrisonCell && room.Role != RoomRoleDefOf.PrisonBarracks && room.Role != RoomRoleDefOf.Hospital) || (pawn.ownership != null && pawn.ownership.OwnedRoom != null && pawn.ownership.OwnedRoom == room));
				}));
				if (candidates.NullOrEmpty())
				{
					result = null;
				}
				else
				{
					result = JobMaker.MakeJob(def.jobDef, candidates.RandomElement());
				}
			}
			finally
			{
				candidates.Clear();
			}
			return result;

		}
        private static readonly List<Thing> candidates = new List<Thing>();
    }
}
