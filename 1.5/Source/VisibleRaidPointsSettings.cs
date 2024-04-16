using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace VisibleRaidPoints
{
    public class VisibleRaidPointsSettings : ModSettings
    {
        private static readonly int IncidentWorkerTypeCountVanilla = 12;
        private static readonly int IncidentWorkerTypeCountRoyalty = 1;
        private static readonly int IncidentWorkerTypeCountIdeology = 0;
        private static readonly int IncidentWorkerTypeCountBiotech = 1;
        private static readonly int IncidentWorkerTypeCountAnomaly = 13;

        public static int GetIncidentWorkerTypeCount()
        {
            int count = IncidentWorkerTypeCountVanilla;
            if (ModsConfig.RoyaltyActive)
            {
                count += IncidentWorkerTypeCountRoyalty;
            }
            if (ModsConfig.IdeologyActive)
            {
                count += IncidentWorkerTypeCountIdeology;
            }
            if (ModsConfig.BiotechActive)
            {
                count += IncidentWorkerTypeCountBiotech;
            }
            if (ModsConfig.AnomalyActive)
            {
                count += IncidentWorkerTypeCountAnomaly;
            }
            return count;
        }

        public static bool ShowInLabel;
        public static bool ShowInText;
        public static bool ShowBreakdown;
        public static bool AggressiveAnimals;
        public static bool AmbushEnemyFaction;
        public static bool AmbushManhunterPack;
        public static bool AnimalInsanityMass;
        public static bool CaravanDemand;
        public static bool ChimeraAssault;
        public static bool CrashedShipPart;
        public static bool CropBlight;
        public static bool DeepDrillInfestation;
        public static bool DevourerAssault;
        public static bool DevourerWaterAssault;
        public static bool FleshbeastAttack;
        public static bool FleshmassHeart;
        public static bool GhoulAttack;
        public static bool GorehulkAssault;
        public static bool HateChanters;
        public static bool Infestation;
        public static bool MechCluster;
        public static bool PsychicDrone;
        public static bool PsychicRitualSiege;
        public static bool RaidEnemy;
        public static bool RaidFriendly;
        public static bool ShamblerAssault;
        public static bool ShamblerSwarm;
        public static bool SightstealerArrival;
        public static bool SightstealerSwarm;
        public static bool WastepackInfestation;

        public static bool IncidentWorkerTypeEnabled(Type type)
        {
            if (AggressiveAnimals && type == typeof(IncidentWorker_AggressiveAnimals)) return true;
            if (AmbushEnemyFaction && type == typeof(IncidentWorker_Ambush_EnemyFaction)) return true;
            if (AmbushManhunterPack && type == typeof(IncidentWorker_Ambush_ManhunterPack)) return true;
            if (AnimalInsanityMass && type == typeof(IncidentWorker_AnimalInsanityMass)) return true;
            if (CaravanDemand && type == typeof(IncidentWorker_CaravanDemand)) return true;
            if (ChimeraAssault && type == typeof(IncidentWorker_ChimeraAssault)) return true;
            if (CrashedShipPart && type == typeof(IncidentWorker_CrashedShipPart)) return true;
            if (CropBlight && type == typeof(IncidentWorker_CropBlight)) return true;
            if (DeepDrillInfestation && type == typeof(IncidentWorker_DeepDrillInfestation)) return true;
            if (DevourerAssault && type == typeof(IncidentWorker_DevourerAssault)) return true;
            if (DevourerWaterAssault && type == typeof(IncidentWorker_DevourerWaterAssault)) return true;
            if (FleshbeastAttack && type == typeof(IncidentWorker_FleshbeastAttack)) return true;
            if (FleshmassHeart && type == typeof(IncidentWorker_FleshmassHeart)) return true;
            if (GhoulAttack && type == typeof(IncidentWorker_GhoulAttack)) return true;
            if (GorehulkAssault && type == typeof(IncidentWorker_GorehulkAssault)) return true;
            if (HateChanters && type == typeof(IncidentWorker_HateChanters)) return true;
            if (Infestation && type == typeof(IncidentWorker_Infestation)) return true;
            if (MechCluster && type == typeof(IncidentWorker_MechCluster)) return true;
            if (PsychicDrone && type == typeof(IncidentWorker_PsychicDrone)) return true;
            if (PsychicRitualSiege && type == typeof(IncidentWorker_PsychicRitualSiege)) return true;
            if (RaidEnemy && type == typeof(IncidentWorker_RaidEnemy)) return true;
            if (RaidFriendly && type == typeof(IncidentWorker_RaidFriendly)) return true;
            if (ShamblerAssault && type == typeof(IncidentWorker_ShamblerAssault)) return true;
            if (ShamblerSwarm && (type == typeof(IncidentWorker_ShamblerSwarm) || type == typeof(IncidentWorker_ShamblerSwarmAnimals))) return true;
            if (SightstealerArrival && type == typeof(IncidentWorker_SightstealerArrival)) return true;
            if (SightstealerSwarm && type == typeof(IncidentWorker_SightstealerSwarm)) return true;
            if (WastepackInfestation && type == typeof(IncidentWorker_WastepackInfestation)) return true;
            return false;
        }

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();

            listingStandard.Begin(inRect);

            listingStandard.CheckboxLabeled("VisibleRaidPoints_ShowPointsInLetterLabel".Translate(), ref ShowInLabel);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_ShowPointsInLetterText".Translate(), ref ShowInText);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_ShowBreakdownInLetterText".Translate(), ref ShowBreakdown);
            if (listingStandard.ButtonTextLabeled("VisibleRaidPoints_IncidentWorkerTypes".Translate(), "VisibleRaidPoints_Modify".Translate()))
            {
                Find.WindowStack.Add(new Dialog_IncidentWorkerTypes());
            }

            listingStandard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref ShowInLabel, "ShowInLabel", false);
            Scribe_Values.Look(ref ShowInText, "ShowInText", true);
            Scribe_Values.Look(ref ShowBreakdown, "ShowBreakdown", false);
            Scribe_Values.Look(ref AggressiveAnimals, "AggressiveAnimals", true);
            Scribe_Values.Look(ref AmbushEnemyFaction, "AmbushEnemyFaction", true);
            Scribe_Values.Look(ref AmbushManhunterPack, "AmbushManhunterPack", true);
            Scribe_Values.Look(ref AnimalInsanityMass, "AnimalInsanityMass", false);
            Scribe_Values.Look(ref CaravanDemand, "CaravanDemand", false);
            Scribe_Values.Look(ref ChimeraAssault, "ChimeraAssault", true);
            Scribe_Values.Look(ref CrashedShipPart, "CrashedShipPart", true);
            Scribe_Values.Look(ref CropBlight, "CropBlight", false);
            Scribe_Values.Look(ref DeepDrillInfestation, "DeepDrillInfestation", true);
            Scribe_Values.Look(ref DevourerAssault, "DevourerAssault", true);
            Scribe_Values.Look(ref DevourerWaterAssault, "DevourerWaterAssault", true);
            Scribe_Values.Look(ref FleshbeastAttack, "FleshbeastAttack", true);
            Scribe_Values.Look(ref FleshmassHeart, "FleshmassHeart", true);
            Scribe_Values.Look(ref GhoulAttack, "GhoulAttack", false);
            Scribe_Values.Look(ref GorehulkAssault, "GorehulkAssault", true);
            Scribe_Values.Look(ref HateChanters, "HateChanters", true);
            Scribe_Values.Look(ref Infestation, "Infestation", true);
            Scribe_Values.Look(ref MechCluster, "MechCluster", true);
            Scribe_Values.Look(ref PsychicDrone, "PsychicDrone", false);
            Scribe_Values.Look(ref PsychicRitualSiege, "PsychicRitualSiege", true);
            Scribe_Values.Look(ref RaidEnemy, "RaidEnemy", true);
            Scribe_Values.Look(ref RaidFriendly, "RaidFriendly", false);
            Scribe_Values.Look(ref ShamblerAssault, "ShamblerAssault", true);
            Scribe_Values.Look(ref ShamblerSwarm, "ShamblerSwarm", true);
            Scribe_Values.Look(ref SightstealerArrival, "SightstealerArrival", true);
            Scribe_Values.Look(ref SightstealerSwarm, "SightstealerSwarm", true);
            Scribe_Values.Look(ref WastepackInfestation, "WastepackInfestation", true);
        }
    }
}
