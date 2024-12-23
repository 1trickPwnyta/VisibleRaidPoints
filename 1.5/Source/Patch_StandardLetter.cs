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
            if (breakdown != null && !VisibleRaidPointsSettings.ShowBreakdown)
            {
                DiaOption option = new DiaOption("VisibleRaidPoints_PointsBreakdown".Translate());
                option.action = delegate ()
                {
                    Find.WindowStack.Add(new Dialog_MessageBox(TextGenerator.GetThreatPointsBreakdownText(breakdown), null, null, null, null, "VisibleRaidPoints_PointsBreakdown".Translate()));
                };
                __result = __result.AddItem(option);
            }
        }
    }
}
