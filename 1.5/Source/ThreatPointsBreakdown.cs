using RimWorld;
using System.Collections.Generic;
using Verse;

namespace VisibleRaidPoints
{
    public class ThreatPointsBreakdown : IExposable
    {
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

        private static ThreatPointsBreakdown current;
        private static Dictionary<IncidentParms, ThreatPointsBreakdown> incidentAssociations = new Dictionary<IncidentParms, ThreatPointsBreakdown>();

        public float PlayerWealthForStoryteller = 0f;
        public float PointsFromWealth = 0f;
        public List<PawnPoints> PointsPerPawn = new List<PawnPoints>();
        public float TargetRandomFactor = 0f;
        public float AdaptationFactor = 0f;
        public float GraceFactor = 0f;
        public float ThreatScale = 0f;
        public float PreClamp = 0f;
        public float PostClamp = 0f;
        public float StorytellerRandomFactor = 0f;
        public PawnsArrivalModeDef RaidArrivalModeDef = null;
        public float RaidArrivalModeFactor = 0f;
        public RaidStrategyDef RaidStrategyDef = null;
        public float RaidStrategyFactor = 0f;
        public RaidAgeRestrictionDef RaidAgeRestrictionDef = null;
        public bool PollutionRaidFactor = false;
        public bool AmbushManhunterFactor = false;
        public float CaravanDemandFactor = 0f;
        public bool CrashedShipPartFactor = false;
        public float DeepDrillInfestationFactor = 0f;
        public float InfestationFactor = 0f;
        public float PreMiscCalcs = 0f;
        public bool AnimalInsanityMassCalc = false;
        public bool MechClusterMax = false;
        public Faction RaidFaction = null;
        public PawnGroupKindDef RaidGroupKindDef = null;
        public float FinalResult = 0f;

        public static void Clear()
        {
            current = new ThreatPointsBreakdown();
        }

        public static ThreatPointsBreakdown GetCurrent()
        {
            if (current == null)
            {
                Clear();
            }
            return current;
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

        public static void SetPlayerWealthForStoryteller(float playerWealthForStoryteller)
        {
            GetCurrent().PlayerWealthForStoryteller = playerWealthForStoryteller;
        }

        public static void SetPointsFromWealth(float pointsFromWealth)
        {
            GetCurrent().PointsFromWealth = pointsFromWealth;
        }

        public static void SetPawnPointsName(string name)
        {
            PawnPoints last;
            if (current.PointsPerPawn.Count == 0 || current.PointsPerPawn[current.PointsPerPawn.Count - 1].Name != null)
            {
                current.PointsPerPawn.Add(new PawnPoints());
            }
            last = current.PointsPerPawn[current.PointsPerPawn.Count - 1];
            last.Name = name;
        }

        public static void SetPawnPointsPoints(float points)
        {
            PawnPoints last;
            if (current.PointsPerPawn.Count == 0)
            {
                current.PointsPerPawn.Add(new PawnPoints());
            }
            last = current.PointsPerPawn[current.PointsPerPawn.Count - 1];
            last.Points = points;
        }

        public static void SetTargetRandomFactor(float targetRandomFactor)
        {
            GetCurrent().TargetRandomFactor = targetRandomFactor;
        }

        public static void SetAdaptationFactor(float adaptationFactor)
        {
            GetCurrent().AdaptationFactor = adaptationFactor;
        }

        public static void SetGraceFactor(float graceFactor)
        {
            GetCurrent().GraceFactor = graceFactor;
        }

        public static void SetThreatScale(float threatScale)
        {
            GetCurrent().ThreatScale = threatScale;
        }

        public static void SetPreClamp(float preClamp)
        {
            GetCurrent().PreClamp = preClamp;
        }

        public static void SetPostClamp(float postClamp)
        {
            GetCurrent().PostClamp = postClamp;
        }

        public static void SetStorytellerRandomFactor(float storytellerRandomFactor)
        {
            GetCurrent().StorytellerRandomFactor = storytellerRandomFactor;
        }

        public static void SetRaidArrivalModeDef(PawnsArrivalModeDef raidArrivalModeDef)
        {
            GetCurrent().RaidArrivalModeDef = raidArrivalModeDef;
        }

        public static void SetRaidArrivalModeFactor(float raidArrivalModeFactor)
        {
            GetCurrent().RaidArrivalModeFactor = raidArrivalModeFactor;
        }

        public static void SetRaidStrategyDef(RaidStrategyDef raidStrategyDef)
        {
            GetCurrent().RaidStrategyDef = raidStrategyDef;
        }

        public static void SetRaidStrategyFactor(float raidStrategyFactor)
        {
            GetCurrent().RaidStrategyFactor = raidStrategyFactor;
        }

        public static void SetRaidAgeRestrictionDef(RaidAgeRestrictionDef raidAgeRestrictionDef)
        {
            GetCurrent().RaidAgeRestrictionDef = raidAgeRestrictionDef;
        }

        public static void SetPollutionRaidFactor()
        {
            GetCurrent().PollutionRaidFactor = true;
        }

        public static void SetAmbushManhunterFactor()
        {
            GetCurrent().AmbushManhunterFactor = true;
        }

        public static void SetCaravanDemandFactor(float caravanDemandFactor)
        {
            GetCurrent().CaravanDemandFactor = caravanDemandFactor;
        }

        public static void SetCrashedShipPartFactor()
        {
            GetCurrent().CrashedShipPartFactor = true;
        }

        public static void SetDeepDrillInfestationFactor(float deepDrillInfestationFactor)
        {
            GetCurrent().DeepDrillInfestationFactor = deepDrillInfestationFactor;
        }

        public static void SetInfestationFactor(float infestationFactor)
        {
            GetCurrent().InfestationFactor = infestationFactor;
        }

        public static void SetPreMiscCalcs(float preMiscCalcs)
        {
            GetCurrent().PreMiscCalcs = preMiscCalcs;
        }

        public static void SetAnimalInsanityMassCalc()
        {
            GetCurrent().AnimalInsanityMassCalc = true;
        }

        public static void SetMechClusterMax()
        {
            GetCurrent().MechClusterMax = true;
        }

        public static void SetRaidFaction(Faction raidFaction)
        {
            GetCurrent().RaidFaction = raidFaction;
        }

        public static void SetRaidGroupKindDef(PawnGroupKindDef raidGroupKindDef)
        {
            GetCurrent().RaidGroupKindDef = raidGroupKindDef;
        }

        public static void SetFinalResult(float finalResult)
        {
            GetCurrent().FinalResult = finalResult;
        }

        public float GetPointsFromPawns()
        {
            float pointsFromPawns = 0f;
            foreach (PawnPoints pawnPoints in PointsPerPawn)
            {
                pointsFromPawns += pawnPoints.Points;
            }
            return pointsFromPawns;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref PlayerWealthForStoryteller, "PlayerWealthForStoryteller");
            Scribe_Values.Look(ref PointsFromWealth, "PointsFromWealth");
            Scribe_Collections.Look(ref PointsPerPawn, "PointsPerPawn", LookMode.Deep);
            Scribe_Values.Look(ref TargetRandomFactor, "TargetRandomFactor");
            Scribe_Values.Look(ref AdaptationFactor, "AdaptationFactor");
            Scribe_Values.Look(ref GraceFactor, "GraceFactor");
            Scribe_Values.Look(ref ThreatScale, "ThreatScale");
            Scribe_Values.Look(ref PreClamp, "PreClamp");
            Scribe_Values.Look(ref PostClamp, "PostClamp");
            Scribe_Values.Look(ref StorytellerRandomFactor, "StorytellerRandomFactor");
            Scribe_Defs.Look(ref RaidArrivalModeDef, "RaidArrivalModeDef");
            Scribe_Values.Look(ref RaidArrivalModeFactor, "RaidArrivalModeFactor");
            Scribe_Defs.Look(ref RaidStrategyDef, "RaidStrategyDef");
            Scribe_Values.Look(ref RaidStrategyFactor, "RaidStrategyFactor");
            Scribe_Defs.Look(ref RaidAgeRestrictionDef, "RaidAgeRestrictionDef");
            Scribe_Values.Look(ref PollutionRaidFactor, "PollutionRaidFactor");
            Scribe_Values.Look(ref AmbushManhunterFactor, "AmbushManhunterFactor");
            Scribe_Values.Look(ref CaravanDemandFactor, "CaravanDemandFactor");
            Scribe_Values.Look(ref CrashedShipPartFactor, "CrashedShipPartFactor");
            Scribe_Values.Look(ref DeepDrillInfestationFactor, "DeepDrillInfestationFactor");
            Scribe_Values.Look(ref InfestationFactor, "InfestationFactor");
            Scribe_Values.Look(ref PreMiscCalcs, "PreMiscCalcs");
            Scribe_Values.Look(ref AnimalInsanityMassCalc, "AnimalInsanityMassCalc");
            Scribe_Values.Look(ref MechClusterMax, "MechClusterMax");
            Scribe_References.Look(ref RaidFaction, "RaidFaction");
            Scribe_Defs.Look(ref RaidGroupKindDef, "RaidGroupKindDef");
            Scribe_Values.Look(ref FinalResult, "FinalResult");
        }
    }
}