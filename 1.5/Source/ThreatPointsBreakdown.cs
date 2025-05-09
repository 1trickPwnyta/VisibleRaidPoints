﻿using RimWorld;
using System.Collections.Generic;
using Verse;

namespace VisibleRaidPoints
{
    public class ThreatPointsBreakdown : IExposable
    {
        public enum OperationType
        {
            Add,
            Mul,
            Min,
            Max
        }

        public class PointsOperation : IExposable
        {
            public OperationType? Operation;
            public float Value;
            public TaggedString Description;
            public float RunningTotal;

            public void ExposeData()
            {
                Scribe_Values.Look(ref Operation, "Operation");
                Scribe_Values.Look(ref Value, "Value");
                Scribe_Values.Look(ref Description, "Description");
                Scribe_Values.Look(ref RunningTotal, "RunningTotal");
            }
        }

        public class PawnPoints : IExposable
        {
            public string Name;
            public float Points;

            public void ExposeData()
            {
                Scribe_Values.Look(ref Name, "Name");
                Scribe_Values.Look(ref Points, "Points");
            }
        }

        private static ThreatPointsBreakdown current = new ThreatPointsBreakdown();
        private static Dictionary<ChoiceLetter, ThreatPointsBreakdown> letterAssociations = new Dictionary<ChoiceLetter, ThreatPointsBreakdown>();
        private static Dictionary<IncidentParms, ThreatPointsBreakdown> incidentAssociations = new Dictionary<IncidentParms, ThreatPointsBreakdown>();
        private static Dictionary<QuestPart_Letter, ThreatPointsBreakdown> questAssociations = new Dictionary<QuestPart_Letter, ThreatPointsBreakdown>();
        private static Dictionary<ArchivedDialog, ThreatPointsBreakdown> dialogAssociations = new Dictionary<ArchivedDialog, ThreatPointsBreakdown>();

        public float InitialValue = 0f;
        public List<PointsOperation> Operations = new List<PointsOperation>();
        public float PlayerWealthForStoryteller = 0f;
        public List<PawnPoints> PointsPerPawn = new List<PawnPoints>();
        public string CallingMethodName = null;

        public static void Clear()
        {
            current = new ThreatPointsBreakdown();
        }

        public static ThreatPointsBreakdown GetCurrent()
        {
            return current;
        }

        public static void Associate(ChoiceLetter letter, ThreatPointsBreakdown breakdown)
        {
            if (!letterAssociations.ContainsKey(letter))
            {
                letterAssociations.Add(letter, breakdown);
            }
            else
            {
                letterAssociations[letter] = breakdown;
            }
        }

        public static void Associate(IncidentParms parms, ThreatPointsBreakdown breakdown)
        {
            if (!incidentAssociations.ContainsKey(parms))
            {
                incidentAssociations.Add(parms, breakdown);
            }
            else
            {
                incidentAssociations[parms] = breakdown;
            }
        }

        public static void Associate(QuestPart_Letter quest, ThreatPointsBreakdown breakdown)
        {
            if (!questAssociations.ContainsKey(quest))
            {
                questAssociations.Add(quest, breakdown);
            }
            else
            {
                questAssociations[quest] = breakdown;
            }
        }

        public static void Associate(ArchivedDialog dialog, ThreatPointsBreakdown breakdown)
        {
            if (!dialogAssociations.ContainsKey(dialog))
            {
                dialogAssociations.Add(dialog, breakdown);
            }
            else
            {
                dialogAssociations[dialog] = breakdown;
            }
        }

        public static ThreatPointsBreakdown GetAssociated(ChoiceLetter letter)
        {
            if (letterAssociations.ContainsKey(letter))
            {
                return letterAssociations[letter];
            }
            else
            {
                return null;
            }
        }

        public static ThreatPointsBreakdown GetAssociated(IncidentParms parms)
        {
            if (incidentAssociations.ContainsKey(parms))
            {
                return incidentAssociations[parms];
            }
            else
            {
                return null;
            }
        }

        public static ThreatPointsBreakdown GetAssociated(QuestPart_Letter quest)
        {
            if (questAssociations.ContainsKey(quest))
            {
                return questAssociations[quest];
            }
            else
            {
                return null;
            }
        }

        public static ThreatPointsBreakdown GetAssociated(ArchivedDialog dialog)
        {
            if (dialogAssociations.ContainsKey(dialog))
            {
                return dialogAssociations[dialog];
            }
            else
            {
                return null;
            }
        }

        public static void SetInitialValue(float initialValue)
        {
            current.InitialValue = initialValue;
            current.CallingMethodName = (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
        }

        public static void SetOperationType(OperationType operationType)
        {
            PointsOperation last;
            if (current.Operations.Count == 0 || current.Operations.GetLast().Operation != null)
            {
                current.Operations.Add(new PointsOperation());
            }
            last = current.Operations.GetLast();
            last.Operation = operationType;
        }

        public static void SetOperationValue(float value)
        {
            PointsOperation last;
            if (current.Operations.Count == 0)
            {
                current.Operations.Add(new PointsOperation());
            }
            last = current.Operations.GetLast();
            last.Value = value;
        }

        public static void SetOperationDescription(TaggedString description)
        {
            PointsOperation last;
            if (current.Operations.Count == 0)
            {
                current.Operations.Add(new PointsOperation());
            }
            last = current.Operations.GetLast();
            last.Description = description;
        }

        public static void SetOperationRunningTotal(float runningTotal)
        {
            PointsOperation last;
            if (current.Operations.Count == 0)
            {
                current.Operations.Add(new PointsOperation());
            }
            last = current.Operations.GetLast();
            last.RunningTotal = runningTotal;
        }

        public static void SetPlayerWealthForStoryteller(float playerWealthForStoryteller)
        {
            current.PlayerWealthForStoryteller = playerWealthForStoryteller;
        }

        public static void SetPawnPointsName(string name)
        {
            PawnPoints last;
            if (current.PointsPerPawn.Count == 0 || current.PointsPerPawn.GetLast().Name != null)
            {
                current.PointsPerPawn.Add(new PawnPoints());
            }
            last = current.PointsPerPawn.GetLast();
            last.Name = name;
        }

        public static void SetPawnPointsPoints(float points)
        {
            PawnPoints last;
            if (current.PointsPerPawn.Count == 0)
            {
                current.PointsPerPawn.Add(new PawnPoints());
            }
            last = current.PointsPerPawn.GetLast();
            last.Points = points;
        }

        public float GetFinalResult()
        {
            if (Operations.Count > 0)
            {
                return Operations.GetLast().RunningTotal;
            }
            else
            {
                return InitialValue;
            }
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref InitialValue, "InitialValue");
            Scribe_Collections.Look(ref Operations, "Operations");
            Scribe_Values.Look(ref PlayerWealthForStoryteller, "PlayerWealthForStoryteller");
            Scribe_Collections.Look(ref PointsPerPawn, "PointsPerPawn", LookMode.Deep);
            Scribe_Values.Look(ref CallingMethodName, "CallingMethodName");
        }
    }
}