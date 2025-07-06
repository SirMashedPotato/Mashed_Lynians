using System;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace Mashed_Lynians
{
    public class Gas_FelvineCloud : Gas
    {
        private readonly int tickInterval = 120;

        protected override void TickInterval(int delta)
        {
            base.TickInterval(delta);
            try
            {
                if (this.IsHashIntervalTick(tickInterval, delta))
                {
                    HashSet<Thing> hashSet = new HashSet<Thing>(Position.GetThingList(Map));
                    if (hashSet != null)
                    {
                        foreach (Thing thing in hashSet)
                        {
                            if (thing is Pawn)
                            {
                                Pawn p = thing as Pawn;
                                if (Utility.PawnCanUseFelvine(p))
                                {
                                    float factor = 0.025f;
                                    //simulate ingesting felvine
                                    HealthUtility.AdjustSeverity(p, HediffDefOf.Mashed_Lynian_FelvineTolerance, factor / 3);
                                    HealthUtility.AdjustSeverity(p, HediffDefOf.Mashed_Lynian_FelvineHighFrenzy, factor);
                                }
                            }
                        }
                    }
                }
            }
            catch (NullReferenceException)
            {

            }
        }
    }
}
