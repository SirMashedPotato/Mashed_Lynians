using System;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace Mashed_Lynians
{
    public class Gas_FelvineCloud : Gas
    {
        private int tickerInterval = 0;
        private int tickerMax = 120;

        public override void Tick()
        {
            base.Tick();

            try
            {
                if (tickerInterval >= tickerMax)
                {
                    HashSet<Thing> hashSet = new HashSet<Thing>(this.Position.GetThingList(this.Map));
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
                    tickerInterval = 0;
                }
                tickerInterval++;
            }
            catch (NullReferenceException e)
            {

            }
        }
    }
}
