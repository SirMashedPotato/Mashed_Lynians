using RimWorld;
using UnityEngine;
using Verse;

namespace Mashed_Lynians
{
    public class Tab_UpgradeKnowledge
    {
        public static void DoCell(Rect inRect, LynianKnowledgeDef knowledgeDef, Comp_EurekacornTracker compEurekacornTracker)
        {
            Widgets.DrawBoxSolidWithOutline(inRect, Widgets.WindowBGFillColor, Color.grey, 1);
            Rect mainRect = inRect.ContractedBy(Assets.RectPadding);
            RectDivider rectDivider = new RectDivider(mainRect, mainRect.GetHashCode(), null);

            RectDivider labelRect = rectDivider.NewRow(Text.LineHeight, VerticalJustification.Top);
            Widgets.Label(labelRect.NewCol(knowledgeDef.LabelCap.GetWidthCached(), HorizontalJustification.Left), knowledgeDef.LabelCap);

            GUI.DrawTexture(labelRect.NewCol(labelRect.Rect.height, HorizontalJustification.Right), ContentFinder<Texture2D>.Get(knowledgeDef.iconTexPath));

            var font = Text.Font;
            Text.Font = GameFont.Tiny;
            Widgets.Label(rectDivider.NewRow(Text.LineHeight * 3f, VerticalJustification.Top), knowledgeDef.description);
            Text.Font = font;

            DoProgressBar(mainRect, knowledgeDef, compEurekacornTracker);
        }

        private static void DoProgressBar(Rect inRect, LynianKnowledgeDef knowledgeDef, Comp_EurekacornTracker compEurekacornTracker)
        {
            Rect mainRect = inRect;
            mainRect.height = inRect.height / 3;
            mainRect.y += mainRect.height * 2;

            float knowledgeProgress = knowledgeDef.Progress(compEurekacornTracker);
            float requiredKnowledge = knowledgeDef.knowledgeCost;
            float fillPercent = (float)knowledgeProgress / requiredKnowledge;
            Widgets.FillableBar(mainRect, fillPercent, Assets.SkillPointsFillTex, Texture2D.grayTexture, true);
            var anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(mainRect, "Progress".Translate() + " (" + (int)knowledgeProgress + " / " + requiredKnowledge + ")");
            Text.Anchor = anchor;
        }
    }
}
