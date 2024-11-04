using RimWorld;
using HarmonyLib;
using RimWorld.QuestGen;
using Verse;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(QuestGen_Misc))]
    [HarmonyPatch(nameof(QuestGen_Misc.Letter))]
    public static class Patch_QuestGen_Misc_Letter
    {
        public static void Postfix(LetterDef letterDef, QuestPart_Letter __result)
        {
            if (letterDef == LetterDefOf.ThreatBig && __result != null)
            {
                ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetCurrent();
                if (breakdown.GetFinalResult() > 0)
                {
                    ThreatPointsBreakdown.Associate(__result, breakdown);
                }
            }
        }
    }
}
