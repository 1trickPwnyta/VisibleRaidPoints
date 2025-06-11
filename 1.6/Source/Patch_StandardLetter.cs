using HarmonyLib;
using System.Collections.Generic;
using Verse;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(StandardLetter))]
    [HarmonyPatch("get_Choices")]
    public static class Patch_StandardLetter_get_Choices
    {
        public static void Postfix(StandardLetter __instance, ref IEnumerable<DiaOption> __result)
        {
            ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetAssociated(__instance);
            if (breakdown != null && !VisibleRaidPointsSettings.ShowBreakdown && VisibleRaidPointsSettings.ShowBreakdownLink)
            {
                DiaOption option = LetterUtility.GetBreakdownOption(breakdown);
                __result = __result.AddItem(option);
            }
        }
    }
}
