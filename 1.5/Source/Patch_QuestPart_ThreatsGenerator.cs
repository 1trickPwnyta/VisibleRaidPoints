using HarmonyLib;
using RimWorld;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(QuestPart_ThreatsGenerator))]
    [HarmonyPatch(nameof(QuestPart_ThreatsGenerator.MakeIntervalIncidents))]
    public static class Patch_QuestPart_ThreatsGenerator
    {
        public static void Prefix(QuestPart_ThreatsGenerator __instance)
        {
            __instance.parms.SetQuestName(__instance.quest.name);
        }
    }
}
