﻿using Verse;
using RimWorld;
using static AlienRace.AlienPartGenerator;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Mashed_Lynians
{
    public class CompUseEffect_LynianDyeKit : CompUseEffect
    {
        public CompProperties_UseEffectLynianDyeKit Props => (CompProperties_UseEffectLynianDyeKit)props;

        public Color primaryColor = Color.white;
        public Color secondaryColor = Color.white;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref primaryColor, "dyeKit_PrimaryColor");
            Scribe_Values.Look(ref secondaryColor, "dyeKit_SecondaryColor");
        }

        public override void PostPostMake()
        {
            primaryColor = RandomColor();
            secondaryColor = RandomColor();
            base.PostPostMake();
        }

        /* ==========[Use effect]========== */

        public override void DoEffect(Pawn usedBy)
		{
            AlienComp alienComp = usedBy.TryGetComp<AlienComp>();
            alienComp.OverwriteColorChannel("skin", primaryColor, secondaryColor);
            alienComp.RegenerateAddonsForced();
            usedBy.Drawer.renderer.SetAllGraphicsDirty();
        }

        public override AcceptanceReport CanBeUsedBy(Pawn p)
		{
			if (!Utility.PawnIsLynian(p))
			{
				return "Mashed_Lynian_PawnNotLynian".Translate(p.Name);
			}
			return true;
		}

        /* ==========[Old dye kit comp]========== */

        private Color RandomColor()
        {
            Color color = new Color
            {
                r = (float)Math.Round(Rand.Range(0f, 1f) * 100) / 100,
                g = (float)Math.Round(Rand.Range(0f, 1f) * 100) / 100,
                b = (float)Math.Round(Rand.Range(0f, 1f) * 100) / 100,
                a = 1
            };
            return color;
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo gizmo in base.CompGetGizmosExtra())
            {
                yield return gizmo;
            }

            ///Selecting colours
            yield return new Command_Action
            {
                defaultLabel = "Mashed_Lynian_ChangePrimaryColor".Translate(),
                defaultDesc = "Mashed_Lynian_ChangeColorDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/Mashed_Lynian_LynianDyeIcon", true),
                defaultIconColor = primaryColor,
                action = delegate ()
                {
                    Dialog_DyeColorPicker window = new Dialog_DyeColorPicker(this, primaryColor, true);
                    Find.WindowStack.Add(window);
                }
            };

            yield return new Command_Action
            {
                defaultLabel = "Mashed_Lynian_ChangeSecondaryColor".Translate(),
                defaultDesc = "Mashed_Lynian_ChangeColorDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/Mashed_Lynian_LynianDyeIcon", true),
                defaultIconColor = secondaryColor,
                action = delegate ()
                {
                    Dialog_DyeColorPicker window = new Dialog_DyeColorPicker(this, secondaryColor, false);
                    Find.WindowStack.Add(window);
                }
            };

            ///Copying colours
            TargetingParameters targeting = new TargetingParameters()
            {
                canTargetHumans = true,
                canTargetItems = true,
                canTargetBuildings = false,
                mapObjectTargetsMustBeAutoAttackable = false,
                canTargetSelf = false,
                validator = delegate (TargetInfo targ)
                {
                    return targ.Thing != null && (targ.Thing.def == ThingDefOf.Mashed_Lynian_LynianDyeKit || Utility.ThingIsLynian(targ.Thing));
                }
            };

            yield return new Command_Target
            {
                defaultLabel = "Mashed_Lynian_CopyColorPrimary".Translate(),
                defaultDesc = "Mashed_Lynian_CopyColorPrimaryDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/Mashed_Lynian_CopyColorPrimary", true),
                targetingParams = targeting,
                action = delegate (LocalTargetInfo targ)
                {
                    CopyColor(targ.Thing, true);
                }
            };

            yield return new Command_Target
            {
                defaultLabel = "Mashed_Lynian_CopyColorSecondary".Translate(),
                defaultDesc = "Mashed_Lynian_CopyColorSecondaryDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/Mashed_Lynian_CopyColorSecondary", true),
                targetingParams = targeting,
                action = delegate (LocalTargetInfo targ)
                {
                    CopyColor(targ.Thing, false);
                }
            };

            ///Swapping colours
            yield return new Command_Action
            {
                defaultLabel = "Mashed_Lynian_SwapColor".Translate(),
                defaultDesc = "Mashed_Lynian_SwapColorDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/Mashed_Lynian_SwapColor", true),
                action = delegate ()
                {
                    Color temp = primaryColor;
                    primaryColor = secondaryColor;
                    secondaryColor = temp;
                }
            };
        }

        private void CopyColor(Thing source, bool primaryColor = true)
        {
            if (Utility.ThingIsLynian(source))
            {
                AlienComp alienComp = source.TryGetComp<AlienComp>();
                alienComp.ColorChannels.TryGetValue("skin", out ExposableValueTuple<Color, Color> colors);
                if (primaryColor)
                {
                    this.primaryColor = colors.first;
                }
                else
                {
                    secondaryColor = colors.second;
                }
            }
            else
            {
                CompUseEffect_LynianDyeKit sourceComp = source.TryGetComp<CompUseEffect_LynianDyeKit>();
                if (primaryColor)
                {
                    this.primaryColor = sourceComp.primaryColor;
                }
                else
                {
                    secondaryColor = sourceComp.secondaryColor;
                }
            }
        }
    }
}
