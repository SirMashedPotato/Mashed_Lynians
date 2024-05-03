using Verse;

namespace Mashed_Lynians
{
    public class Mashed_Lynians_ModSettings : ModSettings
    {
        private static Mashed_Lynians_ModSettings _instance;

        /* ==========[GETTERS]========== */
        public static bool EnableAcornKnight => _instance.Mashed_Lynians_EnableAcornKnight;
        public static bool EnableDigSkillRequirement => _instance.Mashed_Lynians_EnableDigSkillRequirement;
        public static bool EnableHairballs => _instance.Mashed_Lynians_EnableHairballs;
        public static bool EnableSmellingFelvine => _instance.Mashed_Lynians_EnableSmellingFelvine;
        public static bool EnableCatLikeMasks => _instance.Mashed_Lynians_EnableCatLikeMasks;

        /* ==========[VARIABLES]========== */
        public bool Mashed_Lynians_EnableAcornKnight = true;
        public bool Mashed_Lynians_EnableDigSkillRequirement = true;
        public bool Mashed_Lynians_EnableHairballs = true;
        public bool Mashed_Lynians_EnableSmellingFelvine = true;
        public bool Mashed_Lynians_EnableCatLikeMasks = true;

        public Mashed_Lynians_ModSettings()
        {
            _instance = this;
        }

        /* ==========[SAVING]========== */
        public override void ExposeData()
        {
            Scribe_Values.Look(ref Mashed_Lynians_EnableAcornKnight, "Mashed_Lynians_EnableAcornKnight", true);
            Scribe_Values.Look(ref Mashed_Lynians_EnableDigSkillRequirement, "Mashed_Lynians_EnableDigSkillRequirement", true);
            Scribe_Values.Look(ref Mashed_Lynians_EnableHairballs, "Mashed_Lynians_EnableHairballs", true);
            Scribe_Values.Look(ref Mashed_Lynians_EnableSmellingFelvine, "Mashed_Lynians_EnableSmellingFelvine", true);
            Scribe_Values.Look(ref Mashed_Lynians_EnableCatLikeMasks, "Mashed_Lynians_EnableCatLikeMasks", true);
        }
    }
}
