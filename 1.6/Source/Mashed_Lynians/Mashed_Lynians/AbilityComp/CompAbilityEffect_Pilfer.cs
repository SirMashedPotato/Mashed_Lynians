using Verse;
using RimWorld;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static RimWorld.PsychicRitualRoleDef;

namespace Mashed_Lynians
{
    public class CompAbilityEffect_Pilfer : CompAbilityEffect
    {
        public new CompProperties_AbilityPilfer Props => (CompProperties_AbilityPilfer)props;

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
                        Thing thingToPilfer;
                        if (!Props.guaranteedPilfers.NullOrEmpty() && Utility.PawnCanUseFelvine(user))
                        {
                            List<Thing> list = targetPawn.inventory.innerContainer.Where(x => Props.guaranteedPilfers.Contains(x.def)).ToList();
                            if (list.Any())
                            {
                                thingToPilfer = list.RandomElement();
                                FinalisePilfering(thingToPilfer, targetPawn, user);
                                user.health.AddHediff(HediffDefOf.Mashed_Lynian_PilferedFelvine);
                                return;
                            }
                          
                        }
                        thingToPilfer = targetPawn.inventory.innerContainer.RandomElement();
                        if (thingToPilfer != null)
                        {
                            FinalisePilfering(thingToPilfer, targetPawn, user);
                            return;
                        }
                    }
                    else
                    {
                        if (Rand.Chance(AvoidFailChance(user, targetPawn)))
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
                            if (targetPawn.Faction != null)
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

        private void FinalisePilfering(Thing thingToPilfer, Pawn target, Pawn pilferer)
        {
            int count = 1;
            if (thingToPilfer.stackCount > 1)
            {
                count = Mathf.Clamp((int)(thingToPilfer.stackCount * 0.25f), 1, thingToPilfer.stackCount);
            }
            target.inventory.innerContainer.TryTransferToContainer(thingToPilfer, parent.pawn.inventory.innerContainer, count, out Thing pilferedThing);

            pilferer.records.Increment(RecordDefOf.Mashed_Lynian_PilferedNumber);
            pilferer.records.AddTo(RecordDefOf.Mashed_Lynian_PilferedValue, pilferedThing.MarketValue * pilferedThing.stackCount);
            Messages.Message("Mashed_Lynian_PilferSuccess".Translate(pilferer.Name, pilferedThing.Label), pilferedThing, MessageTypeDefOf.PositiveEvent);
        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            if (target.Pawn == null)
            {
                return false;
            }

            if (target.Pawn.inventory.innerContainer.NullOrEmpty())
            {
                return false;
            }

            return base.Valid(target, throwMessages);
        }

        public override string ExtraTooltipPart()
        {
            return "Mashed_Lynian_PilferDetails".Translate(parent.pawn.GetStatValue(StatDefOf.Mashed_Lynian_PilferChance).ToStringPercent(), (1f - AvoidFailChance(parent.pawn)).ToStringPercent());
        }

        public override string ExtraLabelMouseAttachment(LocalTargetInfo target)
        {
            if (target.Pawn != null)
            {
                string str = "Mashed_Lynian_PilferDetails".Translate(parent.pawn.GetStatValue(StatDefOf.Mashed_Lynian_PilferChance).ToStringPercent(), (1f - AvoidFailChance(parent.pawn, target.Pawn)).ToStringPercent());
                if (!target.Pawn.Awake())
                {
                    str += "Mashed_Lynian_PilferDetails_Sleeping".Translate();
                }
                return str;
            }
            return ExtraTooltipPart();
        }

        public float AvoidFailChance(Pawn pawn)
        {
            return pawn.GetStatValue(StatDefOf.Mashed_Lynian_PilferChance) / 2;
        }

        public float AvoidFailChance(Pawn pawn, Pawn target)
        {
            float div = target.Awake() ? 2 : 1.2f;
            return pawn.GetStatValue(StatDefOf.Mashed_Lynian_PilferChance) / div;
        }
    }
}
