using UnityEngine;
using Verse;

namespace Mashed_Lynians
{
    [StaticConstructorOnStartup]
    public static class Assets
    {
        public static readonly Texture2D SkillPointsFillTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.4f, 0.5f, 0.6f));
        public static float RectPadding = 12f;
    }
}
