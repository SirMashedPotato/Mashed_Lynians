using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Mashed_Lynians
{
    public class Page_EurekacornMenu : Page
    {
        public const int columnCount = 2;
        public const int altColumnCount = 3;
        public const float rectLimitY = 45f;
        public const float RectPadding = 12f;

        public static float rowHeight = Text.LineHeight * 6f;

        private static Vector2 scrollPosition = Vector2.zero;
        private static SelectedTab curTab = SelectedTab.Ability;
        private readonly List<TabRecord> tabs = new List<TabRecord>();


        public Comp_EurekacornTracker compEurekacornTracker;
        public Pawn pawn;

        public override string PageTitle => "Mashed_Lynians_EurekacornMenu_Label".Translate().CapitalizeFirst() + ": " + pawn.NameShortColored;

        public enum SelectedTab
        {
            Ability,
            Skill,
            Trait,
            Knowledge
        }

        public Page_EurekacornMenu(Pawn p, Comp_EurekacornTracker comp)
        {
            pawn = p;
            compEurekacornTracker = comp;
            ReadySettingsTabs();
        }

        private void ReadySettingsTabs()
        {
            tabs.Add(new TabRecord("Mashed_Lynians_EurekacornTab_Abilities".Translate(), delegate
            {
                curTab = SelectedTab.Ability;
            }, () => curTab == SelectedTab.Ability));

            tabs.Add(new TabRecord("Skills".Translate(), delegate
            {
                curTab = SelectedTab.Skill;
            }, () => curTab == SelectedTab.Skill));

            tabs.Add(new TabRecord("Traits".Translate(), delegate
            {
                curTab = SelectedTab.Trait;
            }, () => curTab == SelectedTab.Trait));

            tabs.Add(new TabRecord("Mashed_Lynians_EurekacornTab_Knowledge".Translate(), delegate
            {
                curTab = SelectedTab.Knowledge;
            }, () => curTab == SelectedTab.Knowledge));
        }

        public override void DoWindowContents(Rect inRect)
        {
            DrawPageTitle(inRect);
            Widgets.ButtonImage(new Rect(inRect.width - 30f, 0f, 30f, 30f), TexButton.Info, false, "Mashed_Lynians_EurekacornMenu_Info".Translate().Resolve());

            inRect.yMin += rectLimitY;
            Rect consumedHeartsRect = inRect;
            consumedHeartsRect.height = Text.LineHeight * 2f;
            DoSkillPointsBar(consumedHeartsRect);

            Rect mainRect = new Rect(inRect.x, inRect.yMin + 45f, inRect.width, inRect.height - 45f);
            TabDrawer.DrawTabs(new Rect(mainRect.x, mainRect.yMin + 45f, mainRect.width, mainRect.height - 45f), tabs);

            mainRect.yMin += rectLimitY;
            DoBottomButtons(mainRect, showNext: false);
            mainRect.height -= rectLimitY;
            Widgets.DrawMenuSection(mainRect);
            mainRect = mainRect.ContractedBy(RectPadding);

            switch (curTab)
            {
                case SelectedTab.Ability:
                    DoUpgradeGrid(mainRect, 2);
                    break;

                case SelectedTab.Skill:
                    DoUpgradeGrid(mainRect, 2);
                    break;

                case SelectedTab.Trait:
                    DoUpgradeGrid(mainRect, 2);
                    break;

                case SelectedTab.Knowledge:
                    DoUpgradeGrid(mainRect, 2);
                    break;
            }

            Widgets.EndScrollView();
        }

        public void DoSkillPointsBar(Rect inRect)
        {
            int skillPointCount = compEurekacornTracker.SkillPointCount;
            int maxLevel = compEurekacornTracker.MaxSkillPoints;
            float fillPercent = (float)skillPointCount / maxLevel;
            Widgets.FillableBar(inRect, fillPercent, OnStartupUtility.SkillPointsFillTex, Texture2D.grayTexture, true);
            var anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(inRect, "Mashed_Lynians_SKillPoints".Translate() + " (" + skillPointCount + " / " + maxLevel + ")");
            Text.Anchor = anchor;
        }

        public void DoUpgradeGrid(Rect inRect, int listCount)
        {
            Rect scrollRect = inRect;
            Rect innerRect = scrollRect;
            innerRect.width -= 30f;
            int finalColumnCount = curTab == SelectedTab.Trait ? altColumnCount : columnCount;

            float cellWidth = (innerRect.width / finalColumnCount) - (RectPadding / 3f);
            float cellHeight = rowHeight;
            float rowCount = ((float)listCount / finalColumnCount);
            if (rowCount % 1 != 0)
            {
                rowCount += 0.5f;
            }
            innerRect.height = Mathf.Round(rowCount) * (cellHeight + RectPadding);

            Widgets.BeginScrollView(scrollRect, ref scrollPosition, innerRect);
            int row = 0;
            int column = 0;
            Rect upgradeRect = new Rect(innerRect.x, innerRect.y, cellWidth, cellHeight);

            for (int i = 0; i < listCount; i++)
            {
                DoUpgradeCelll(upgradeRect, i);
                if (++column >= finalColumnCount)
                {
                    upgradeRect.y += ((RectPadding / 2f) + cellHeight);
                    upgradeRect.x = innerRect.x;
                    column = 0;
                    row++;
                }
                else
                {
                    upgradeRect.x += ((RectPadding / 2f) + cellWidth);
                }
            }
        }

        public void DoUpgradeCelll(Rect inRect, int index)
        {
            Rect rightRect = inRect;
            Rect leftRect = inRect;
            rightRect.width = rightRect.height;
            leftRect.width -= rightRect.width + (RectPadding / 2f);
            rightRect.x += leftRect.width + (RectPadding / 2f);
            /*
            switch (curTab)
            {
                case SelectedTab.Ability:
                    Tab_UpgradeAbility.DoCell(leftRect, rightRect, AbilityList[index], compLycanthrope);
                    break;

                case SelectedTab.Skill:
                    Tab_UpgradeClaw.DoCell(inRect, ClawList[index], compLycanthrope);
                    break;

                case SelectedTab.Trait:
                    Tab_UpgradeTotem.DoCell(leftRect, rightRect, TotemList[index], compLycanthrope, ref upgradeAmountList);
                    break;

                case SelectedTab.Knowledge:
                    Tab_UpgradeTrait.DoCell(inRect, TraitList[index], compLycanthrope);
                    break;
            }
            */
        }
    }
}
