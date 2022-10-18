using System;
using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse.AI;

namespace Mashed_Lynians
{
    public class Gas_SleepBomb : Gas
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
                                if (!p.RaceProps.IsFlesh || p.RaceProps.needsRest == false)
                                {
                                    return;
                                }
                                float num = 0.028758334f;
                                if (num != 0f && !this.Destroyed)
                                {
                                    float num2 = Mathf.Lerp(0.85f, 1.15f, Rand.ValueSeeded(p.thingIDNumber ^ 74374237));
                                    num *= num2;
                                    p.needs.rest.CurLevel -= num * 3;

                                    if (p.needs.rest.CurLevel <= 0 && Rand.Chance(0.25f))
                                    {
                                        if (p.Drafted)
                                        {
                                            p.drafter.Drafted = false;
                                        }
                                        p.jobs.StartJob(JobMaker.MakeJob(JobDefOf.LayDown, p.Position), JobCondition.InterruptForced, null, false, true, null, new JobTag?(JobTag.SatisfyingNeeds), false, false);
                                    }
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
