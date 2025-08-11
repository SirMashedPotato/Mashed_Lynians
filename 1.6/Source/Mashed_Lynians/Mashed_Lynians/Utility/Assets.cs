using UnityEngine;
using Verse;

namespace Mashed_Lynians
{
    [StaticConstructorOnStartup]
    public static class Assets
    {
        public static readonly Texture2D SkillPointsFillTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.6f, 0.7f, 0.8f));
        public static float RectPadding = 12f;
    }
}
