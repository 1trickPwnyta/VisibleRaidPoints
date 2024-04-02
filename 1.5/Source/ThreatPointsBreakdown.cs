using System.Collections.Generic;

namespace VisibleRaidPoints
{
    public static class ThreatPointsBreakdown
    {
        public class PawnPoints
        {
            public string Name;
            public float Points;
        }

        public static float PlayerWealthForStoryteller;
        public static float PointsFromWealth;
        public static List<PawnPoints> PointsPerPawn;
        public static float PointsFromPawns;
        public static float TargetRandomFactor;
        public static float AdaptationFactor;
        public static float GraceFactor;
        public static float PreClamp;
        public static float PostClamp;
        public static float StorytellerRandomFactor;
        public static float RaidArrivalModeFactor;
        public static string RaidArrivalModeDesc;
        public static float RaidStrategyFactor;
        public static string RaidStrategyDesc;
        public static float RaidAgeRestrictionFactor;
        public static string RaidAgeRestrictionDesc;
        public static float AmbushManhunterFactor;
        public static float CaravanDemandFactor;
        public static float CrashedShipPartFactor;
        public static float DeepDrillInfestationFactor;
        public static float InfestationFactor;
        public static float PreMiscCalcs;
        public static bool AnimalInsanityMassCalc;
        public static float CrashedShipPartMin;
        public static float DeepDrillInfestationMin;
        public static float DeepDrillInfestationMax;
        public static float MechClusterMax;
        public static float RaidStrategyMin;
        public static string FactionDesc;
        public static string GroupKindDesc;
        public static float FinalResult;

        public static void Clear()
        {
            PlayerWealthForStoryteller = 0f;
            PointsFromWealth = 0f;
            PointsPerPawn = new List<PawnPoints>();
            PointsFromPawns = 0f;
            TargetRandomFactor = 0f;
            AdaptationFactor = 0f;
            GraceFactor = 0f;
            PreClamp = 0f;
            PostClamp = 0f;
            StorytellerRandomFactor = 0f;
            RaidArrivalModeFactor = 0f;
            RaidArrivalModeDesc = null;
            RaidStrategyFactor = 0f;
            RaidStrategyDesc = null;
            RaidAgeRestrictionFactor = 0f;
            RaidAgeRestrictionDesc = null;
            AmbushManhunterFactor = 0f;
            CaravanDemandFactor = 0f;
            CrashedShipPartFactor = 0f;
            DeepDrillInfestationFactor = 0f;
            InfestationFactor = 0f;
            PreMiscCalcs = 0f;
            AnimalInsanityMassCalc = false;
            CrashedShipPartMin = 0f;
            DeepDrillInfestationMin = 0f;
            DeepDrillInfestationMax = 0f;
            MechClusterMax = 0f;
            RaidStrategyMin = 0f;
            FactionDesc = null;
            GroupKindDesc = null;
            FinalResult = 0f;
        }

        public static void SetPawnPointsName(string name)
        {
            PawnPoints last;
            if (PointsPerPawn.Count == 0 || PointsPerPawn[PointsPerPawn.Count - 1].Name != null)
            {
                PointsPerPawn.Add(new PawnPoints());
            }
            last = PointsPerPawn[PointsPerPawn.Count - 1];
            last.Name = name;
        }

        public static void SetPawnPointsPoints(float points)
        {
            PawnPoints last;
            if (PointsPerPawn.Count == 0)
            {
                PointsPerPawn.Add(new PawnPoints());
            }
            last = PointsPerPawn[PointsPerPawn.Count - 1];
            last.Points = points;
        }
    }
}