using HarmonyLib;
using RimWorld;
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
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPawnPointsName = AccessTools.Method(typeof(ThreatPointsBreakdown), "SetPawnPointsName");
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPawnPointsPoints = AccessTools.Method(typeof(ThreatPointsBreakdown), "SetPawnPointsPoints");
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
        public static readonly MethodInfo m_ThreatPointsBreakdown_GetCurrent = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.GetCurrent));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPreMiscCalcs = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetPreMiscCalcs));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetAmbushManhunterFactor = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetAmbushManhunterFactor));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetFinalResult = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetFinalResult));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetAnimalInsanityMassCalc = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetAnimalInsanityMassCalc));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetCaravanDemandFactor = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetCaravanDemandFactor));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetCrashedShipPartFactor = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetCrashedShipPartFactor));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetDeepDrillInfestationFactor = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetDeepDrillInfestationFactor));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetInfestationFactor = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetInfestationFactor));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetRaidFaction = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetRaidFaction));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetRaidGroupKindDef = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetRaidGroupKindDef));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetRaidArrivalModeFactor = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetRaidArrivalModeFactor));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetRaidArrivalModeDef = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetRaidArrivalModeDef));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetRaidStrategyFactor = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetRaidStrategyFactor));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetRaidStrategyDef = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetRaidStrategyDef));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetRaidAgeRestrictionDef = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetRaidAgeRestrictionDef));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPollutionRaidFactor = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetPollutionRaidFactor));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetMechClusterMax = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetMechClusterMax));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetStorytellerRandomFactor = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetStorytellerRandomFactor));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPlayerWealthForStoryteller = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetPlayerWealthForStoryteller));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPointsFromWealth = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetPointsFromWealth));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetTargetRandomFactor = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetTargetRandomFactor));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetAdaptationFactor = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetAdaptationFactor));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetThreatScale = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetThreatScale));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetGraceFactor = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetGraceFactor));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPreClamp = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetPreClamp));
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPostClamp = AccessTools.Method(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.SetPostClamp));

        public static readonly FieldInfo f_IncidentParms_points = AccessTools.Field(typeof(IncidentParms), nameof(IncidentParms.points));
        public static readonly FieldInfo f_RaidAgeRestrictionDef_threatPointsFactor = AccessTools.Field(typeof(RaidAgeRestrictionDef), nameof(RaidAgeRestrictionDef.threatPointsFactor));
        public static readonly FieldInfo f_Difficulty_threatScale = AccessTools.Field(typeof(Difficulty), nameof(Difficulty.threatScale));
    }
}
