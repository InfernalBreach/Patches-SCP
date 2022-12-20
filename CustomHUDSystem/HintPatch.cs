using HarmonyLib;
using Hints;

namespace CustomHUDSystem
{
    [HarmonyPatch(typeof(HintDisplay), nameof(HintDisplay.Show))]
    public static class HintPatch
    {
        public static bool Prefix(Hint hint)
        {
            if(hint.GetType() == typeof(TranslationHint))
                return false;

            if(hint._effects != null && hint._effects.Length > 0)
                return false;

            return true;
        }
    }
}