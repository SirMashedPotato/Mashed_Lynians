using UnityEngine;
using Verse;

namespace Mashed_Lynians
{
    public class Mashed_Lynians_Mod : Mod
    {
        Mashed_Lynians_ModSettings settings;

        public Mashed_Lynians_Mod(ModContentPack contentPack) : base(contentPack)
        {
            settings = GetSettings<Mashed_Lynians_ModSettings>();
        }

        public override string SettingsCategory() => "Mashed_Lynians_ModName".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);

            listing_Standard.CheckboxLabeled("Mashed_Lynians_EnableAcornKnight".Translate(), ref settings.Mashed_Lynians_EnableAcornKnight);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("Mashed_Lynians_EnableDigSkillRequirement".Translate(), ref settings.Mashed_Lynians_EnableDigSkillRequirement);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("Mashed_Lynians_EnableHairballs".Translate(), ref settings.Mashed_Lynians_EnableHairballs);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("Mashed_Lynians_EnableSmellingFelvine".Translate(), ref settings.Mashed_Lynians_EnableSmellingFelvine);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("Mashed_Lynians_EnableCatLikeMasks".Translate(), ref settings.Mashed_Lynians_EnableCatLikeMasks);
            listing_Standard.Gap();


            listing_Standard.End();
            base.DoSettingsWindowContents(inRect);
        }
    }
}
