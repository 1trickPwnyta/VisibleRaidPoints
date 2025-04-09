using RimWorld;
using System.Collections.Generic;
using Verse;

namespace VisibleRaidPoints
{
    public static class LetterUtility
    {
        public static void ReceiveLetter(LetterStack letterStack, TaggedString label, TaggedString text, LetterDef textLetterDef, LookTargets lookTargets, Faction relatedFaction, Quest quest, List<ThingDef> hyperlinkThingDefs, string debugInfo, int delayTicks, bool playSound, bool incidentEnabled)
        {
            ChoiceLetter letter = LetterMaker.MakeLetter(label, text, textLetterDef, lookTargets, relatedFaction, quest, hyperlinkThingDefs);
            if (incidentEnabled)
            {
                ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetCurrent();
                ThreatPointsBreakdown.Clear();
                if (breakdown != null)
                {
                    ThreatPointsBreakdown.Associate(letter, breakdown);
                }
            }
            letterStack.ReceiveLetter(letter, debugInfo, delayTicks, playSound);
        }

        public static void InjectThreatPoints(Letter letter, QuestPart_Letter quest)
        {
            if (letter is ChoiceLetter)
            {
                ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetAssociated(quest);
                if (breakdown != null)
                {
                    ThreatPointsBreakdown.Associate((ChoiceLetter)letter, breakdown);
                }
            }
        }

        public static void AssociateWithBreakdown(ChoiceLetter letter, IncidentDef def, IncidentParms parms)
        {
            if (VisibleRaidPointsSettings.IncidentWorkerTypeEnabled(def.workerClass) && parms.points > 0f)
            {
                ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetAssociated(parms);
                if (breakdown == null)
                {
                    breakdown = ThreatPointsBreakdown.GetCurrent();
                    ThreatPointsBreakdown.Clear();
                }
                if (breakdown != null)
                {
                    ThreatPointsBreakdown.Associate(letter, breakdown);
                }
                else
                {
                    throw new System.Exception("Tried to associate letter with null breakdown.");
                }
            }
        }

        public static void AssociateWithBreakdown(ArchivedDialog dialog, bool enabled)
        {
            if (enabled)
            {
                ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetCurrent();
                ThreatPointsBreakdown.Clear();
                if (breakdown != null)
                {
                    ThreatPointsBreakdown.Associate(dialog, breakdown);
                }
                else
                {
                    throw new System.Exception("Tried to associate dialog with null breakdown.");
                }
            }
        }

        public static string GetModifiedLabel(string label, Letter letter)
        {
            if (letter is ChoiceLetter)
            {
                ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetAssociated((ChoiceLetter)letter);
                if (breakdown != null)
                {
                    if (VisibleRaidPointsSettings.ShowInLabel)
                    {
                        label = $"({(int)breakdown.GetFinalResult()}) {label}";
                    }
                }
            }
            return label;
        }

        private static TaggedString GetModifiedText(TaggedString text, ThreatPointsBreakdown breakdown)
        {
            if (breakdown != null)
            {
                if (VisibleRaidPointsSettings.ShowInText)
                {
                    text += $"\n\n{TextGenerator.GetThreatPointsIndicatorText(breakdown)}";
                }
                if (VisibleRaidPointsSettings.ShowBreakdown)
                {
                    text += $"\n\n=== {"VisibleRaidPoints_PointsBreakdown".Translate()} ===";
                    text += $"\n\n{TextGenerator.GetThreatPointsBreakdownText(breakdown)}";
                }
            }
            return text;
        }

        public static TaggedString GetModifiedText(TaggedString text, ChoiceLetter letter)
        {
            ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetAssociated(letter);
            return GetModifiedText(text, breakdown);
        }

        public static string GetModifiedText(string text, ArchivedDialog dialog)
        {
            ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetAssociated(dialog);
            return GetModifiedText(text, breakdown);
        }

        public static DiaOption GetBreakdownOption(ThreatPointsBreakdown breakdown)
        {
            DiaOption option = new DiaOption("VisibleRaidPoints_PointsBreakdown".Translate());
            option.action = delegate ()
            {
                Find.WindowStack.Add(new Dialog_MessageBox(TextGenerator.GetThreatPointsBreakdownText(breakdown), null, null, null, null, "VisibleRaidPoints_PointsBreakdown".Translate()));
            };
            return option;
        }

        public static void AddBreakdownOption(DiaNode node, bool enabled)
        {
            if (enabled)
            {
                ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetCurrent();
                if (breakdown != null && !VisibleRaidPointsSettings.ShowBreakdown && VisibleRaidPointsSettings.ShowBreakdownLink)
                {
                    DiaOption option = GetBreakdownOption(breakdown);
                    node.options.Add(option);
                }
            }
        }

        public static void AddBreakdownOption(DiaNode node, ArchivedDialog dialog)
        {
            ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetAssociated(dialog);
            if (breakdown != null && !VisibleRaidPointsSettings.ShowBreakdown && VisibleRaidPointsSettings.ShowBreakdownLink)
            {
                DiaOption option = GetBreakdownOption(breakdown);
                node.options.Add(option);
            }
        }
    }
}
