﻿using RimWorld;
using HarmonyLib;
using RimWorld.QuestGen;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(QuestGen_Misc))]
    [HarmonyPatch(nameof(QuestGen_Misc.Letter))]
    public static class Patch_QuestGen_Misc_Letter
    {
        public static void Postfix(string label, QuestPart_Letter __result)
        {
            if (label == "[raidArrivedLetterLabel]" && __result != null)
            {
                ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetCurrent();
                ThreatPointsBreakdown.Clear();
                if (breakdown.GetFinalResult() > 0)
                {
                    ThreatPointsBreakdown.Associate(__result, breakdown);
                }
            }
        }
    }
}
