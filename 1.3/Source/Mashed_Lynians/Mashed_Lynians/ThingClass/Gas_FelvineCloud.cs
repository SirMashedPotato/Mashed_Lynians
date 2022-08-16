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
                                RaceProperties props = RaceProperties.Get(p.def);
                                if (props != null && props.canUseFelvine)
                                {
                                    HealthUtility.AdjustSeverity(p, HediffDefOf.Mashed_Lynian_FelvineHighFrenzy, 0.025f);
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
