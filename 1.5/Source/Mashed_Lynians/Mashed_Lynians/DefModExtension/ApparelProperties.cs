using Verse;
using System.Collections.Generic;

namespace Mashed_Lynians
{
    public class ApparelProperties : DefModExtension
    {
        public float carryWeightIncrease = 0f;
        public List<float> qualityCarryWeightMults;

        public bool isBoaboaMask = false;
        public bool isShakalakaMask = false;
        public bool isGajalakaMask = false;
        public bool isGoldenGajalakaMask = false;

        public bool treatAsCatLike = false;

        public bool overrideColour = false;

        public static ApparelProperties Get(Def def)
        {
            return def.GetModExtension<ApparelProperties>();
        }

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string text in base.ConfigErrors())
            {
                yield return text;
            }

            if (!qualityCarryWeightMults.NullOrEmpty() && qualityCarryWeightMults.Count != 7)
            {
                yield return "qualityCarryWeightMults does not contain exactly 7 values";
            }
        }
    }
}
