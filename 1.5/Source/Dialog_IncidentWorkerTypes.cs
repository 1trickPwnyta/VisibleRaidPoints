using RimWorld;
using UnityEngine;
using Verse;

namespace VisibleRaidPoints
{
    public class Dialog_IncidentWorkerTypes : Window
    {
        private Vector2 scrollPos;

        public Dialog_IncidentWorkerTypes()
        {
            this.doCloseX = true;
            this.doCloseButton = true;
        }

        public override void DoWindowContents(Rect inRect)
        {
            Rect rect = new Rect(inRect);
            rect.yMax -= Window.CloseButSize.y;
            rect.yMin += 8f;
            Listing_Standard listingStandard = new Listing_Standard(rect, () => scrollPos);
            Rect viewRect = new Rect(0f, 0f, inRect.width - 16f, 24f * VisibleRaidPointsSettings.GetIncidentWorkerTypeCount());
            Widgets.BeginScrollView(rect, ref scrollPos, viewRect);
            listingStandard.Begin(viewRect);
            
            listingStandard.CheckboxLabeled("VisibleRaidPoints_AggressiveAnimals".Translate(), ref VisibleRaidPointsSettings.AggressiveAnimals);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_AmbushEnemyFaction".Translate(), ref VisibleRaidPointsSettings.AmbushEnemyFaction);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_AmbushManhunterPack".Translate(), ref VisibleRaidPointsSettings.AmbushManhunterPack);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_AnimalInsanityMass".Translate(), ref VisibleRaidPointsSettings.AnimalInsanityMass);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_CaravanDemand".Translate(), ref VisibleRaidPointsSettings.CaravanDemand);
            if (ModsConfig.AnomalyActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_ChimeraAssault".Translate(), ref VisibleRaidPointsSettings.ChimeraAssault);
            }
            listingStandard.CheckboxLabeled("VisibleRaidPoints_CrashedShipPart".Translate(), ref VisibleRaidPointsSettings.CrashedShipPart);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_CropBlight".Translate(), ref VisibleRaidPointsSettings.CropBlight);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_DeepDrillInfestation".Translate(), ref VisibleRaidPointsSettings.DeepDrillInfestation);
            if (ModsConfig.AnomalyActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_DevourerAssault".Translate(), ref VisibleRaidPointsSettings.DevourerAssault);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_DevourerWaterAssault".Translate(), ref VisibleRaidPointsSettings.DevourerWaterAssault);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_FleshbeastAttack".Translate(), ref VisibleRaidPointsSettings.FleshbeastAttack);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_FleshmassHeart".Translate(), ref VisibleRaidPointsSettings.FleshmassHeart);
            }
            if (ModsConfig.AnomalyActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_GorehulkAssault".Translate(), ref VisibleRaidPointsSettings.GorehulkAssault);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_HateChanters".Translate(), ref VisibleRaidPointsSettings.HateChanters);
            }
            listingStandard.CheckboxLabeled("VisibleRaidPoints_Infestation".Translate(), ref VisibleRaidPointsSettings.Infestation);
            if (ModsConfig.RoyaltyActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_MechCluster".Translate(), ref VisibleRaidPointsSettings.MechCluster);
            }
            if (ModsConfig.AnomalyActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_NoctolAttack".Translate(), ref VisibleRaidPointsSettings.NoctolAttack);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_PitGateEmergence".Translate(), ref VisibleRaidPointsSettings.PitGateEmergence);
            }
            listingStandard.CheckboxLabeled("VisibleRaidPoints_PsychicDrone".Translate(), ref VisibleRaidPointsSettings.PsychicDrone);
            if (ModsConfig.AnomalyActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_PsychicRitualSiege".Translate(), ref VisibleRaidPointsSettings.PsychicRitualSiege);
            }
            listingStandard.CheckboxLabeled("VisibleRaidPoints_RaidEnemy".Translate(), ref VisibleRaidPointsSettings.RaidEnemy);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_RaidFriendly".Translate(), ref VisibleRaidPointsSettings.RaidFriendly);
            if (ModsConfig.AnomalyActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_ShamblerAssault".Translate(), ref VisibleRaidPointsSettings.ShamblerAssault);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_ShamblerSwarm".Translate(), ref VisibleRaidPointsSettings.ShamblerSwarm);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_SightstealerSwarm".Translate(), ref VisibleRaidPointsSettings.SightstealerSwarm);
            }
            if (ModsConfig.BiotechActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_WastepackInfestation".Translate(), ref VisibleRaidPointsSettings.WastepackInfestation);
            }

            listingStandard.End();
            Widgets.EndScrollView();
        }
    }
}
