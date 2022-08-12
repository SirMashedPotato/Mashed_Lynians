using Verse;
using RimWorld;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Mashed_Lynians
{
    class CompAbilityEffect_Pilfer : CompAbilityEffect
    {
        public new CompProperties_Pilfer Props
        {
            get
            {
                return (CompProperties_Pilfer)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target.Thing != null && target.Thing is Pawn)
            {
                Pawn p = target.Thing as Pawn;
                if (!p.inventory.innerContainer.NullOrEmpty())
                {
                    float chance = parent.pawn.GetStatValue(Props.stat);
                    if (Rand.Chance(chance))
                    {
                        Thing pilferedItem;
                        if (!Props.guaranteedPilfers.NullOrEmpty())
                        {
                            List<Thing> list = p.inventory.innerContainer.Where(x => Props.guaranteedPilfers.Contains(x.def)).ToList();
                            if (list.Any())
                            {
                                pilferedItem = list.RandomElement();
                                FinalisePilfering(pilferedItem, p, parent.pawn);
                                parent.pawn.health.AddHediff(Props.guaranteedPilferHediff);
                                return;
                            }
                          
                        }
                        pilferedItem = p.inventory.innerContainer.RandomElement();
                        if (pilferedItem != null)
                        {
                            FinalisePilfering(pilferedItem, p, parent.pawn);
                            return;
                        }
                    }
                    else
                    {
                        //TODO chance goes unnoticed
                        //TODO damage relations
                        Messages.Message("Mashed_Lynian_PilferFail".Translate(parent.pawn.Name), parent.pawn, MessageTypeDefOf.NegativeEvent);
                    }
                }
            }
        }

        public static void FinalisePilfering(Thing pilferedItem, Pawn target, Pawn pilferer)
        {
            if (pilferedItem.stackCount > 1)
            {
                pilferedItem.SplitOff((int)(pilferedItem.stackCount * 0.9f));
            }
            target.inventory.innerContainer.TryDrop(pilferedItem, ThingPlaceMode.Near, out Thing newThing);
            pilferer.records.Increment(RecordDefOf.Mashed_Lynian_PilferedNumber);
            pilferer.records.AddTo(RecordDefOf.Mashed_Lynian_PilferedValue, newThing.MarketValue * newThing.stackCount);
            Messages.Message("Mashed_Lynian_PilferSuccess".Translate(pilferer.Name, newThing.Label), newThing, MessageTypeDefOf.PositiveEvent);
        }

        public override string ExtraTooltipPart()
        {
            return "Mashed_Lynian_PilferDetails".Translate(parent.pawn.GetStatValue(Props.stat).ToStringPercent());
        }

        public override string ExtraLabelMouseAttachment(LocalTargetInfo target)
        {
            return "Mashed_Lynian_PilferDetails".Translate(parent.pawn.GetStatValue(Props.stat).ToStringPercent());
        }
    }
}
