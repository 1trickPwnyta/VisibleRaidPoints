using RimWorld;
using System.Collections.Generic;
using Verse;

namespace VisibleRaidPoints
{
    public static class LetterUtility
    {
        public static void ReceiveLetter(LetterStack letterStack, TaggedString label, TaggedString text, LetterDef textLetterDef, LookTargets lookTargets, Faction relatedFaction, Quest quest, List<ThingDef> hyperlinkThingDefs, string debugInfo, int delayTicks, bool playSound, bool incidentEnabled)
        {
            if (incidentEnabled)
            {
                ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetCurrent();

                if (VisibleRaidPointsSettings.ShowInLabel)
                {
                    label = $"({(int)breakdown.GetFinalResult()}) {label}";
                }

                if (VisibleRaidPointsSettings.ShowInText)
                {
                    text += $"\n\n{TextGenerator.GetThreatPointsIndicatorText(breakdown)}";
                }

                if (VisibleRaidPointsSettings.ShowBreakdown)
                {
                    TaggedString breakdownText = TextGenerator.GetThreatPointsBreakdownText(breakdown);

                    if (breakdownText != null)
                    {
                        text += $"\n\n{breakdownText}";
                    }
                }
            }
            letterStack.ReceiveLetter(label, text, textLetterDef, lookTargets, relatedFaction, quest, hyperlinkThingDefs, debugInfo, delayTicks, playSound);
        }

        public static void InjectThreatPoints(Letter letter, QuestPart_Letter quest)
        {
            ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetAssociated(quest);
            if (breakdown != null)
            {
                if (VisibleRaidPointsSettings.ShowInLabel)
                {
                    letter.Label = $"({(int)breakdown.GetFinalResult()}) {letter.Label}";
                }

                ChoiceLetter choiceLetter = letter as ChoiceLetter;
                if (choiceLetter != null)
                {
                    if (VisibleRaidPointsSettings.ShowInText)
                    {
                        choiceLetter.Text += $"\n\n{TextGenerator.GetThreatPointsIndicatorText(breakdown)}";
                    }

                    if (VisibleRaidPointsSettings.ShowBreakdown)
                    {
                        TaggedString breakdownText = TextGenerator.GetThreatPointsBreakdownText(breakdown);

                        if (breakdownText != null)
                        {
                            choiceLetter.Text += $"\n\n{breakdownText}";
                        }
                    }
                }
            }
        }
    }
}
