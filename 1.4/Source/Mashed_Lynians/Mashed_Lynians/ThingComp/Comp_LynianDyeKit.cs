using UnityEngine;
using Verse;
using RimWorld;
using System.Collections.Generic;
using System;
using static AlienRace.AlienPartGenerator;

namespace Mashed_Lynians
{
    public class Comp_LynianDyeKit : ThingComp
	{
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
                    return targ.Thing != null && (targ.Thing.def == ThingDefOf.Mashed_Lynian_LynianDyeKit || Utility.PawnIsLynian(targ.Thing));
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
            if (Utility.PawnIsLynian(source))
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
                Comp_LynianDyeKit sourceComp = source.TryGetComp<Comp_LynianDyeKit>();
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

        public Color primaryColor = Color.white;
        public Color secondaryColor = Color.white;
    }
}
