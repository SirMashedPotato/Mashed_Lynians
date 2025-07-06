using System;
using UnityEngine;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace Mashed_Lynians
{
    /// <summary>
    /// Largely based on Dialog_GlowerColorPicker
    /// </summary>
    public class Dialog_DyeColorPicker : Window
    {
        public override Vector2 InitialSize => new Vector2(640f, 360f);

        public Dialog_DyeColorPicker(CompUseEffect_LynianDyeKit dyeComp, Color color, bool primaryColor)
        {
            this.dyeComp = dyeComp;
            this.primaryColor = primaryColor;
            this.color = color;
            oldColor = color;
            r = color.r;
            g = color.g;
            b = color.b;
            forcePause = true;
            absorbInputAroundWindow = true;
            closeOnClickedOutside = true;
            closeOnAccept = false;
        }

        public override void DoWindowContents(Rect inRect)
        {
            using (TextBlock.Default())
            {
                RectDivider rect = new RectDivider(inRect, 195906069, null);
                HeaderRow(ref rect);
                rect.NewRow(0f, VerticalJustification.Top);
                BottomButtons(ref rect);
                rect.NewRow(0f, VerticalJustification.Bottom);

                Listing_Standard listing_Standard = new Listing_Standard
                {
                    ColumnWidth = 340f
                };
                listing_Standard.Begin(rect);
                r = (float)Math.Round(listing_Standard.SliderLabeled("Mashed_Lynian_ColorRed".Translate(r * 100), r, 0, 1) * 100) / 100;
                g = (float)Math.Round(listing_Standard.SliderLabeled("Mashed_Lynian_ColorGreen".Translate(g * 100), g, 0, 1) * 100) / 100;
                b = (float)Math.Round(listing_Standard.SliderLabeled("Mashed_Lynian_ColorBlue".Translate(b * 100), b, 0, 1) * 100) / 100;
                color = new Color(r, g, b, 1);
                listing_Standard.End();

                ColorPalette(ref rect, ref r, ref g, ref b);

                rect.NewRow(140f, VerticalJustification.Top);
                ColorReadback(rect, color, oldColor);
            }
        }

        private static void HeaderRow(ref RectDivider layout)
        {
            using (new TextBlock(GameFont.Medium))
            {
                TaggedString taggedString = "ChooseAColor".Translate().CapitalizeFirst();
                RectDivider rect = layout.NewRow(Text.CalcHeight(taggedString, layout.Rect.width), VerticalJustification.Top);
                Widgets.Label(rect, taggedString);
            }
        }

        private void BottomButtons(ref RectDivider layout)
        {
            RectDivider rectDivider = layout.NewRow(ButSize.y, VerticalJustification.Bottom);
            if (Widgets.ButtonText(rectDivider.NewCol(ButSize.x, HorizontalJustification.Left), "Cancel".Translate(), true, true, true, null))
            {
                Close(true);
            }
            if (Widgets.ButtonText(rectDivider.NewCol(ButSize.x, HorizontalJustification.Left), "Mashed_Lynian_Randomise".Translate(), true, true, true, null))
            {
                r = (float)Math.Round(Rand.Range(0f, 1f) * 100) / 100;
                g = (float)Math.Round(Rand.Range(0f, 1f) * 100) / 100;
                b = (float)Math.Round(Rand.Range(0f, 1f) * 100) / 100;
            }
            if (Widgets.ButtonText(rectDivider.NewCol(ButSize.x, HorizontalJustification.Right), "Accept".Translate(), true, true, true, null))
            {
                color.a = 1;
                if (primaryColor)
                {
                    dyeComp.primaryColor = color;
                } 
                else
                {
                    dyeComp.secondaryColor = color;
                }
               
                Close(true);
            }
        }

        private static void ColorPalette(ref RectDivider layout, ref float r, ref float g, ref float b)
        {
            if (cachedColorDefList.NullOrEmpty())
            {
                foreach(ColorDef def in DefDatabase<ColorDef>.AllDefsListForReading)
                {
                    if (def.modContentPack.IsCoreMod)
                    {
                        cachedColorDefList.Add(def.color);
                    }
                }
            }

            using (new TextBlock(TextAnchor.MiddleLeft))
            {
                Color setColor = new Color(r, g, b, 1);
                RectDivider rectDivider = layout;
                RectDivider rect = rectDivider.NewCol(250f, HorizontalJustification.Right);
                Widgets.ColorSelector(rect, ref setColor, cachedColorDefList, out _, null, 22, 2);
                r = setColor.r;
                g = setColor.g;
                b = setColor.b;
            }
        }

        private static void ColorReadback(Rect rect, Color color, Color oldColor)
        {
            rect.SplitVertically((rect.width - 26f) / 2f, out Rect parent, out Rect parent2);
            RectDivider rectDivider = new RectDivider(parent, 195906069, null);
            TaggedString label = "CurrentColor".Translate().CapitalizeFirst();
            TaggedString label2 = "OldColor".Translate().CapitalizeFirst();
            float width = Mathf.Max(new float[]
            {
                100f,
                label.GetWidthCached(),
                label2.GetWidthCached()
            });
            RectDivider rect2 = rectDivider.NewRow(Text.LineHeight, VerticalJustification.Top);
            Widgets.Label(rect2.NewCol(width, HorizontalJustification.Left), label);
            Widgets.DrawBoxSolid(rect2, color);
            RectDivider rect3 = rectDivider.NewRow(Text.LineHeight, VerticalJustification.Top);
            Widgets.Label(rect3.NewCol(width, HorizontalJustification.Left), label2);
            Widgets.DrawBoxSolid(rect3, oldColor);
            RectDivider rect4 = new RectDivider(parent2, 195906069, null);
            rect4.NewCol(26f, HorizontalJustification.Left);
        }

        public override void PreClose()
        {
            cachedColorDefList.Clear();
            base.PreClose();
        }

        private float r;
        private float g;
        private float b;

        private bool primaryColor;
        private Color color;
        private Color oldColor;
        private CompUseEffect_LynianDyeKit dyeComp;

        ///UI shite
        protected static readonly Vector2 ButSize = new Vector2(150f, 38f);
        private static List<Color> cachedColorDefList = new List<Color> { };
    }
}
