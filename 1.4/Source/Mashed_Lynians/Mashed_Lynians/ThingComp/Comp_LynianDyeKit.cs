using UnityEngine;
using Verse;
using RimWorld;
using System.Collections.Generic;
using System;

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

            yield return new Command_ColorIcon
            {
                defaultLabel = "GlowerChangeColor".Translate() + ", 1",
                defaultDesc = "GlowerChangeColorDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/ChangeColor", true),
                color = primaryColor,
                action = delegate ()
                {
                    Dialog_DyeColorPicker window = new Dialog_DyeColorPicker(this, primaryColor, true);
                    Find.WindowStack.Add(window);
                }

            };

            yield return new Command_ColorIcon
            {
                defaultLabel = "GlowerChangeColor".Translate() + ", 2",
                defaultDesc = "GlowerChangeColorDescription".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Commands/ChangeColor", true),
                color = secondaryColor,
                action = delegate ()
                {
                    Dialog_DyeColorPicker window = new Dialog_DyeColorPicker(this, secondaryColor, false);
                    Find.WindowStack.Add(window);
                }

            };
        }

        public Color primaryColor = Color.white;
        public Color secondaryColor = Color.white;
    }
}
