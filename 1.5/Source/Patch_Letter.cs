using HarmonyLib;
using Verse;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(Letter))]
    [HarmonyPatch("RimWorld.IArchivable.get_ArchivedLabel")]
    public static class Patch_Letter_RimWorld_IArchivable_get_ArchivedLabel
    {
        public static void Postfix(Letter __instance, ref string __result)
        {
            __result = LetterUtility.GetModifiedLabel(__result, __instance);
        }
    }

    [HarmonyPatch(typeof(Letter))]
    [HarmonyPatch("get_Label")]
    public static class Patch_Letter_get_Label
    {
        public static void Postfix(Letter __instance, ref TaggedString __result)
        {
            __result = LetterUtility.GetModifiedLabel(__result, __instance);
        }
    }

    [HarmonyPatch(typeof(Letter))]
    [HarmonyPatch("PostProcessedLabel")]
    public static class Patch_Letter_PostProcessedLabel
    {
        public static void Postfix(Letter __instance, ref string __result)
        {
            __result = LetterUtility.GetModifiedLabel(__result, __instance);
        }
    }
}
