﻿using HarmonyLib;
using RimWorld;
using RimWorld.QuestGen;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;

namespace VisibleRaidPoints
{
    public static class VisibleRaidPointsRefs
    {
        public static readonly MethodInfo m_ThreatPointsBreakdown_Clear = AccessTools.Method(typeof(ThreatPointsBreakdown), "Clear");
        public static readonly MethodInfo m_IIncidentTarget_get_PlayerWealthForStoryteller = AccessTools.Method(typeof(IIncidentTarget), "get_PlayerWealthForStoryteller");
        public static readonly MethodInfo m_IIncidentTarget_get_PlayerPawnsForStoryteller = AccessTools.Method(typeof(IIncidentTarget), "get_PlayerPawnsForStoryteller");
        public static readonly MethodInfo m_IEnumeratorPawn_get_Current = AccessTools.Method(typeof(IEnumerator<Pawn>), "get_Current");
        public static readonly MethodInfo m_Pawn_get_LabelShort = AccessTools.Method(typeof(Pawn), "get_LabelShort");
        public static readonly MethodInfo m_Mathf_Lerp_float_float_float = AccessTools.Method(typeof(Mathf), "Lerp", new[] { typeof(float), typeof(float), typeof(float) });
        public static readonly MethodInfo m_FloatRange_get_RandomInRange = AccessTools.Method(typeof(FloatRange), "get_RandomInRange");
        public static readonly MethodInfo m_Mathf_Clamp = AccessTools.Method(typeof(Mathf), nameof(Mathf.Clamp), new[] { typeof(float), typeof(float), typeof(float) });
        public static readonly MethodInfo m_Mathf_Max = AccessTools.Method(typeof(Mathf), nameof(Mathf.Max), new[] { typeof(float), typeof(float) });
        public static readonly MethodInfo m_Mathf_Min = AccessTools.Method(typeof(Mathf), nameof(Mathf.Min), new[] { typeof(float), typeof(float) });
        public static readonly MethodInfo m_TaggedString_op_Implicit_string = AccessTools.Method(typeof(TaggedString), "op_Implicit", new[] { typeof(string) });
        public static readonly MethodInfo m_IncidentWorker_CaravanDemand_GenerateMessageText = AccessTools.Method(typeof(IncidentWorker_CaravanDemand), "GenerateMessageText");
        public static readonly MethodInfo m_CaravanDemandUtility_GenerateCaravanDemandMessageText = AccessTools.Method(typeof(CaravanDemandUtility), nameof(CaravanDemandUtility.GenerateCaravanDemandMessageText));
        public static readonly MethodInfo m_Rand_Range = AccessTools.Method(typeof(Rand), nameof(Rand.Range), new[] { typeof(float), typeof(float) });
        public static readonly MethodInfo m_SimpleCurve_Evaluate = AccessTools.Method(typeof(SimpleCurve), nameof(SimpleCurve.Evaluate));
        public static readonly MethodInfo m_Find_get_LetterStack = AccessTools.Method(typeof(Find), "get_LetterStack");
        public static readonly MethodInfo m_LetterStack_ReceiveLetter = AccessTools.Method(typeof(LetterStack), nameof(LetterStack.ReceiveLetter), new[] { typeof(TaggedString), typeof(TaggedString), typeof(LetterDef), typeof(LookTargets), typeof(Faction), typeof(Quest), typeof(List<ThingDef>), typeof(string), typeof(int), typeof(bool) });
        public static readonly MethodInfo m_LetterUtility_ReceiveLetter = AccessTools.Method(typeof(LetterUtility), nameof(LetterUtility.ReceiveLetter));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetInitialValue = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetInitialValue));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetOperationType = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetOperationType));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetOperationValue = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetOperationValue));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetOperationDescription = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetOperationDescription));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetOperationRunningTotal = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetOperationRunningTotal));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPlayerWealthForStoryteller = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetPlayerWealthForStoryteller));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPawnPointsName = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetPawnPointsName));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPawnPointsPoints = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetPawnPointsPoints));
        public static readonly MethodInfo m_TextGenerator_GetAmbushManhunterFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetAmbushManhunterFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetAnimalInsanityMassCalcDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetAnimalInsanityMassCalcDesc));
        public static readonly MethodInfo m_TextGenerator_GetCaravanDemandFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetCaravanDemandFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetCrashedShipPartFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetCrashedShipPartFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetCrashedShipPartMinDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetCrashedShipPartMinDesc));
        public static readonly MethodInfo m_TextGenerator_GetDeepDrillInfestationFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetDeepDrillInfestationFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetDeepDrillInfestationMinDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetDeepDrillInfestationMinDesc));
        public static readonly MethodInfo m_TextGenerator_GetDeepDrillInfestationMaxDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetDeepDrillInfestationMaxDesc));
        public static readonly MethodInfo m_TextGenerator_GetInfestationFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetInfestationFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetFactionMinDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetFactionMinDesc));
        public static readonly MethodInfo m_TextGenerator_GetRaidArrivalModeFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetRaidArrivalModeFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetRaidStrategyFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetRaidStrategyFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetRaidAgeRestrictionFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetRaidAgeRestrictionFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetRaidStrategyMinDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetRaidStrategyMinDesc));
        public static readonly MethodInfo m_TextGenerator_GetMechClusterMaxDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetMechClusterMaxDesc));
        public static readonly MethodInfo m_TextGenerator_GetSightstealerSwarmFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetSightstealerSwarmFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetGorehulkAssaultMinDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetGorehulkAssaultMinDesc));
        public static readonly MethodInfo m_TextGenerator_GetGorehulkAssaultMinFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetGorehulkAssaultMinFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetEntitySwarmRandomFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetEntitySwarmRandomFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetEntitySwarmMinDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetEntitySwarmMinDesc));
        public static readonly MethodInfo m_TextGenerator_GetFleshbeastAttackFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetFleshbeastAttackFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetNoctolithsDamagedFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetNoctolithsDamagedFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetDevourerAssaultMinDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetDevourerAssaultMinDesc));
        public static readonly MethodInfo m_TextGenerator_GetDevourerAssaultMinFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetDevourerAssaultMinFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetDevourerWaterAssaultFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetDevourerWaterAssaultFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetChimeraAssaultMinDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetChimeraAssaultMinDesc));
        public static readonly MethodInfo m_TextGenerator_GetChimeraAssaultMinFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetChimeraAssaultMinFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetHateChantersMinDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetHateChantersMinDesc));
        public static readonly MethodInfo m_TextGenerator_GetHateChantersMinFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetHateChantersMinFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetPollutionRaidFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetPollutionRaidFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetStorytellerRandomFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetStorytellerRandomFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetPointsFromWealthDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetPointsFromWealthDesc));
        public static readonly MethodInfo m_TextGenerator_GetPointsFromPawnsDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetPointsFromPawnsDesc));
        public static readonly MethodInfo m_TextGenerator_GetTargetRandomFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetTargetRandomFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetAdaptationFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetAdaptationFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetThreatScaleDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetThreatScaleDesc));
        public static readonly MethodInfo m_TextGenerator_GetGraceFactorDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetGraceFactorDesc));
        public static readonly MethodInfo m_TextGenerator_GetClampLowDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetClampLowDesc));
        public static readonly MethodInfo m_TextGenerator_GetClampHighDesc = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GetClampHighDesc));

        public static readonly FieldInfo f_IncidentParms_points = AccessTools.Field(typeof(IncidentParms), nameof(IncidentParms.points));
        public static readonly FieldInfo f_RaidAgeRestrictionDef_threatPointsFactor = AccessTools.Field(typeof(RaidAgeRestrictionDef), nameof(RaidAgeRestrictionDef.threatPointsFactor));
        public static readonly FieldInfo f_Difficulty_threatScale = AccessTools.Field(typeof(Difficulty), nameof(Difficulty.threatScale));
        public static readonly FieldInfo f_QuestPart_RandomRaid_faction = AccessTools.Field(typeof(QuestPart_RandomRaid), nameof(QuestPart_RandomRaid.faction));
        public static readonly FieldInfo f_PawnGroupMakerParms_groupKind = AccessTools.Field(typeof(PawnGroupMakerParms), nameof(PawnGroupMakerParms.groupKind));
        public static readonly FieldInfo f_QuestPart_Noctoliths_points = AccessTools.Field(typeof(QuestPart_Noctoliths), "points");
        public static readonly FieldInfo f_QuestPart_Noctoliths_damagedCount = AccessTools.Field(typeof(QuestPart_Noctoliths), "damagedCount");
        public static readonly FieldInfo f_VisibleRaidPointsSettings_PitGateEmergence = AccessTools.Field(typeof(VisibleRaidPointsSettings), nameof(VisibleRaidPointsSettings.PitGateEmergence));
        public static readonly FieldInfo f_VisibleRaidPointsSettings_NoctolAttack = AccessTools.Field(typeof(VisibleRaidPointsSettings), nameof(VisibleRaidPointsSettings.NoctolAttack));
        public static readonly FieldInfo f_VisibleRaidPointsSettings_HateChanters = AccessTools.Field(typeof(VisibleRaidPointsSettings), nameof(VisibleRaidPointsSettings.HateChanters));
    }
}
