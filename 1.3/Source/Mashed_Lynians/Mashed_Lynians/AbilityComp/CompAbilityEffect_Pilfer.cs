using Verse;
using RimWorld;
using System.Linq;
using System.Collections.Generic;

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
                Pawn targetPawn = target.Thing as Pawn;
                Pawn user = parent.pawn;
                if (!targetPawn.inventory.innerContainer.NullOrEmpty())
                {
                    float chance = user.GetStatValue(StatDefOf.Mashed_Lynian_PilferChance);
                    if (Rand.Chance(chance))
                    {
                        Thing pilferedItem;
                        if (!Props.guaranteedPilfers.NullOrEmpty())
                        {
                            List<Thing> list = targetPawn.inventory.innerContainer.Where(x => Props.guaranteedPilfers.Contains(x.def)).ToList();
                            if (list.Any())
                            {
                                pilferedItem = list.RandomElement();
                                FinalisePilfering(pilferedItem, targetPawn, user);
                                user.health.AddHediff(HediffDefOf.Mashed_Lynian_PilferedFelvine);
                                return;
                            }
                          
                        }
                        pilferedItem = targetPawn.inventory.innerContainer.RandomElement();
                        if (pilferedItem != null)
                        {
                            FinalisePilfering(pilferedItem, targetPawn, user);
                            return;
                        }
                    }
                    else
                    {
                        if (Rand.Chance(AvoidFailChance(user)))
                        {
                            Messages.Message("Mashed_Lynian_PilferFail".Translate(user.Name), parent.pawn, MessageTypeDefOf.NeutralEvent);
                        } 
                        else
                        {
                            Messages.Message("Mashed_Lynian_PilferCaught".Translate(user.Name), parent.pawn, MessageTypeDefOf.NegativeEvent);
                            if (targetPawn.RaceProps.Humanlike && targetPawn.needs.mood != null)
                            {
                                targetPawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.Mashed_Lynian_AttemptedPilfer, user);
                            }
                            if (targetPawn.Faction != null && targetPawn.Faction != Faction.OfPlayer)
                            {
                                Faction.OfPlayer.TryAffectGoodwillWith(targetPawn.Faction, -15, true, true, HistoryEventDefOf.Mashed_Lynian_AttemptedPilfer);
                            }
                            else
                            {
                                if (targetPawn.RaceProps.Humanlike && Rand.Chance(0.5f))
                                {
                                    targetPawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk);
                                }
                                else
                                {
                                    targetPawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.PanicFlee);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void FinalisePilfering(Thing pilferedItem, Pawn target, Pawn pilferer)
        {
            int count = 1;
            if (pilferedItem.stackCount > 1)
            {
                count = (int)(pilferedItem.stackCount * 0.25f);
            }
            target.inventory.innerContainer.TryDrop(pilferedItem, ThingPlaceMode.Near, count, out Thing newThing);
            pilferer.records.Increment(RecordDefOf.Mashed_Lynian_PilferedNumber);
            pilferer.records.AddTo(RecordDefOf.Mashed_Lynian_PilferedValue, newThing.MarketValue * newThing.stackCount);
            Messages.Message("Mashed_Lynian_PilferSuccess".Translate(pilferer.Name, newThing.Label), newThing, MessageTypeDefOf.PositiveEvent);
        }

        public override string ExtraTooltipPart()
        {
            return "Mashed_Lynian_PilferDetails".Translate(parent.pawn.GetStatValue(StatDefOf.Mashed_Lynian_PilferChance).ToStringPercent(), (1f - AvoidFailChance(parent.pawn)).ToStringPercent());
        }

        public override string ExtraLabelMouseAttachment(LocalTargetInfo target)
        {
            return ExtraTooltipPart();
        }

        public float AvoidFailChance(Pawn pawn)
        {
            return pawn.GetStatValue(StatDefOf.Mashed_Lynian_PilferChance) / 3;
        }
    }
}
