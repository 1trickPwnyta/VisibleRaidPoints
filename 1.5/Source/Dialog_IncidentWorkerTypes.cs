﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                listingStandard.CheckboxLabeled("VisibleRaidPoints_EntitySwarm".Translate(), ref VisibleRaidPointsSettings.EntitySwarm);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_FleshbeastAttack".Translate(), ref VisibleRaidPointsSettings.FleshbeastAttack);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_FleshmassHeart".Translate(), ref VisibleRaidPointsSettings.FleshmassHeart);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_GhoulAttack".Translate(), ref VisibleRaidPointsSettings.GhoulAttack);
            }
            listingStandard.CheckboxLabeled("VisibleRaidPoints_GiveQuest".Translate(), ref VisibleRaidPointsSettings.GiveQuest);
            if (ModsConfig.AnomalyActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_GorehulkAssault".Translate(), ref VisibleRaidPointsSettings.GorehulkAssault);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_HateChanters".Translate(), ref VisibleRaidPointsSettings.HateChanters);
            }
            listingStandard.CheckboxLabeled("VisibleRaidPoints_Infestation".Translate(), ref VisibleRaidPointsSettings.Infestation);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_InsectJelly".Translate(), ref VisibleRaidPointsSettings.InsectJelly);
            if (ModsConfig.RoyaltyActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_MechCluster".Translate(), ref VisibleRaidPointsSettings.MechCluster);
            }
            listingStandard.CheckboxLabeled("VisibleRaidPoints_PsychicEmanation".Translate(), ref VisibleRaidPointsSettings.PsychicEmanation);
            if (ModsConfig.AnomalyActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_PsychicRitualSiege".Translate(), ref VisibleRaidPointsSettings.PsychicRitualSiege);
            }
            listingStandard.CheckboxLabeled("VisibleRaidPoints_RaidEnemy".Translate(), ref VisibleRaidPointsSettings.RaidEnemy);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_RaidFriendly".Translate(), ref VisibleRaidPointsSettings.RaidFriendly);
            if (ModsConfig.AnomalyActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_ShamblerAssault".Translate(), ref VisibleRaidPointsSettings.ShamblerAssault);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_SightstealerArrival".Translate(), ref VisibleRaidPointsSettings.SightstealerArrival);
                listingStandard.CheckboxLabeled("VisibleRaidPoints_SightstealerSwarm".Translate(), ref VisibleRaidPointsSettings.SightstealerSwarm);
            }
            listingStandard.CheckboxLabeled("VisibleRaidPoints_TraderCaravanArrival".Translate(), ref VisibleRaidPointsSettings.TraderCaravanArrival);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_TravelerGroup".Translate(), ref VisibleRaidPointsSettings.TravelerGroup);
            listingStandard.CheckboxLabeled("VisibleRaidPoints_VisitorGroup".Translate(), ref VisibleRaidPointsSettings.VisitorGroup);
            if (ModsConfig.BiotechActive)
            {
                listingStandard.CheckboxLabeled("VisibleRaidPoints_WastepackInfestation".Translate(), ref VisibleRaidPointsSettings.WastepackInfestation);
            }

            listingStandard.End();
            Widgets.EndScrollView();
        }
    }
}