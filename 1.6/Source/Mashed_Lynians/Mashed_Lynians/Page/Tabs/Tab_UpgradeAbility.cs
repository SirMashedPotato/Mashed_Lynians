using UnityEngine;
using Verse;

namespace Mashed_Lynians
{
    public class Tab_UpgradeAbility
    {
        public static void DoCell(Rect leftRect, Rect rightRect, LynianAbilityDef abilityDef, Comp_EurekacornTracker compEurekacornTracker)
        {
            Pawn pawn = compEurekacornTracker.parent as Pawn;
            DoLeftRect(leftRect, abilityDef, compEurekacornTracker, pawn);
            DoRightRect(rightRect, abilityDef);
        }

        private static void DoLeftRect(Rect inRect, LynianAbilityDef abilityDef, Comp_EurekacornTracker compEurekacornTracker, Pawn pawn)
        {
            AcceptanceReport acceptanceReport = abilityDef.PawnRequirementsMet(compEurekacornTracker, pawn);

            Widgets.DrawBoxSolidWithOutline(inRect, Widgets.WindowBGFillColor, Color.grey, 1);
            Rect mainRect = inRect.ContractedBy(Assets.RectPadding);
            RectDivider rectDivider = new RectDivider(mainRect, mainRect.GetHashCode(), null);

            RectDivider labelRect = rectDivider.NewRow(Text.LineHeight, VerticalJustification.Top);
            Widgets.Label(labelRect.NewCol(abilityDef.LabelCap.GetWidthCached(), HorizontalJustification.Left), abilityDef.LabelCap);
            if (abilityDef.AlreadyUnlocked(pawn))
            {
                TaggedString levelLabel = "Mashed_Lynians_Unlocked".Translate();
                Widgets.Label(labelRect.NewCol(levelLabel.GetWidthCached(), HorizontalJustification.Right), levelLabel);
            }
            else if (!acceptanceReport)
            {
                TaggedString levelLabel = "Locked".Translate();
                Widgets.Label(labelRect.NewCol(levelLabel.GetWidthCached(), HorizontalJustification.Right), levelLabel);
            }

            var font = Text.Font;
            Text.Font = GameFont.Tiny;
            RectDivider descRect = rectDivider.NewRow(Text.LineHeight * 3f, VerticalJustification.Top);
            Widgets.Label(descRect, abilityDef.description);

            Rect lowerRect = mainRect;
            lowerRect.height = Text.LineHeight * 1.5f;
            lowerRect.y = inRect.y + inRect.height - lowerRect.height - Assets.RectPadding;

            lowerRect.SplitVerticallyWithMargin(out Rect lowerInfoRect, out Rect lowerButtonRect, Assets.RectPadding);

            Text.Font = font;

            if (abilityDef.requiredKnowledgeDef != null)
            {
                RectDivider knowledgeRect = new RectDivider(lowerInfoRect, lowerInfoRect.GetHashCode());
                GUI.DrawTexture(knowledgeRect.NewCol(knowledgeRect.Rect.height), ContentFinder<Texture2D>.Get(abilityDef.requiredKnowledgeDef.iconTexPath));
                Widgets.Label(knowledgeRect.NewCol(abilityDef.requiredKnowledgeDef.labelShort.GetWidthCached()), abilityDef.requiredKnowledgeDef.labelShort);
            }

            if (!abilityDef.AlreadyUnlocked(pawn))
            {
                if (!acceptanceReport)
                {
                    Rect descriptionRect = mainRect;
                    descriptionRect.height = Text.LineHeight;
                    descriptionRect.width = descriptionRect.height;
                    descriptionRect.y += mainRect.height - descriptionRect.height;
                    descriptionRect.x += mainRect.width - descriptionRect.width;
                    Widgets.ButtonImage(descriptionRect, TexButton.Info, true, acceptanceReport.Reason.CapitalizeFirst());
                    return;
                }

                if (abilityDef.skillPointCost > 0)
                {
                    lowerButtonRect.width = 130f;
                    lowerButtonRect.x = inRect.x + inRect.width - lowerButtonRect.width - Assets.RectPadding;

                    bool canPurchase = abilityDef.CanPurchase(compEurekacornTracker);
                    string unlockLabel = "Mashed_Lynians_UnlockLabel".Translate(compEurekacornTracker.SkillPointCount, abilityDef.skillPointCost);
                    if (Widgets.ButtonText(lowerButtonRect, unlockLabel, true, canPurchase, active: canPurchase))
                    {
                        abilityDef.Purchase(compEurekacornTracker, pawn);
                    }
                }
            }
        }

        private static void DoRightRect(Rect inRect, LynianAbilityDef abilityDef)
        {
            Widgets.DrawBoxSolidWithOutline(inRect, Widgets.WindowBGFillColor, Color.grey, 1);
            Rect mainRect = inRect.ContractedBy(Assets.RectPadding);
            GUI.DrawTexture(mainRect, ContentFinder<Texture2D>.Get(abilityDef.backgroundTexPath));
            Rect iconRect = mainRect.ContractedBy(Assets.RectPadding / 2f);
            GUI.DrawTexture(iconRect, abilityDef.abilityDef.uiIcon);
        }
    }
}
