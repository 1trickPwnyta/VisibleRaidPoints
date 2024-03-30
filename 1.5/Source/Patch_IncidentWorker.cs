using Verse;
using RimWorld;
using HarmonyLib;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(IncidentWorker))]
    [HarmonyPatch("SendStandardLetter")]
    [HarmonyPatch(new[] { typeof(TaggedString), typeof(TaggedString), typeof(LetterDef), typeof(IncidentParms), typeof(LookTargets), typeof(NamedArgument[]) })]
    public static class Patch_IncidentWorker_SendStandardLetter
    {
        public static void Prefix(IncidentWorker __instance, ref TaggedString baseLetterLabel, ref TaggedString baseLetterText)
        {
            if (VisibleRaidPointsSettings.IncidentWorkerTypeEnabled(__instance.GetType()))
            {
                if (VisibleRaidPointsSettings.ShowInLabel)
                {
                    baseLetterLabel = $"({(int)ThreatPointsBreakdown.FinalResult}) {baseLetterLabel}";
                }

                if (VisibleRaidPointsSettings.ShowInText)
                {
                    baseLetterText += $"\n\n{TextGenerator.GetThreatPointsIndicatorText()}";
                }

                if (VisibleRaidPointsSettings.ShowBreakdown)
                {
                    TaggedString breakdown = TextGenerator.GetThreatPointsBreakdownText();

                    if (breakdown != null)
                    {
                        baseLetterText += $"\n\n{breakdown}";
                    }
                }
            }
        }
    }
}
