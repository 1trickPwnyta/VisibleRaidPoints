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
        public static readonly MethodInfo m_SimpleCurve_Evaluate = AccessTools.Method(typeof(SimpleCurve), "Evaluate");
        public static readonly MethodInfo m_IIncidentTarget_get_PlayerPawnsForStoryteller = AccessTools.Method(typeof(IIncidentTarget), "get_PlayerPawnsForStoryteller");
        public static readonly MethodInfo m_IEnumeratorPawn_get_Current = AccessTools.Method(typeof(IEnumerator<Pawn>), "get_Current");
        public static readonly MethodInfo m_Pawn_get_LabelShort = AccessTools.Method(typeof(Pawn), "get_LabelShort");
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPawnPointsName = AccessTools.Method(typeof(ThreatPointsBreakdown), "SetPawnPointsName");
        public static readonly MethodInfo m_ThreatPointsBreakdown_SetPawnPointsPoints = AccessTools.Method(typeof(ThreatPointsBreakdown), "SetPawnPointsPoints");
        public static readonly MethodInfo m_Mathf_Lerp_float_float_float = AccessTools.Method(typeof(Mathf), "Lerp", new[] { typeof(float), typeof(float), typeof(float) });
        public static readonly MethodInfo m_FloatRange_get_RandomInRange = AccessTools.Method(typeof(FloatRange), "get_RandomInRange");
        public static readonly MethodInfo m_Mathf_Clamp = AccessTools.Method(typeof(Mathf), nameof(Mathf.Clamp), new[] { typeof(float), typeof(float), typeof(float) });
        public static readonly MethodInfo m_Mathf_Max = AccessTools.Method(typeof(Mathf), nameof(Mathf.Max), new[] { typeof(float), typeof(float) });
        public static readonly MethodInfo m_TaggedString_op_Implicit_string = AccessTools.Method(typeof(TaggedString), "op_Implicit", new[] { typeof(string) });
        public static readonly MethodInfo m_IncidentWorker_CaravanDemand_GenerateMessageText = AccessTools.Method(typeof(IncidentWorker_CaravanDemand), "GenerateMessageText");
        public static readonly MethodInfo m_TextGenerator_GenerateCaravanDemandMessageText = AccessTools.Method(typeof(TextGenerator), nameof(TextGenerator.GenerateCaravanDemandMessageText));

        public static readonly FieldInfo f_IncidentParms_points = AccessTools.Field(typeof(IncidentParms), nameof(IncidentParms.points));
        public static readonly FieldInfo f_ThreatPointsBreakdown_PlayerWealthForStoryteller = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.PlayerWealthForStoryteller));
        public static readonly FieldInfo f_ThreatPointsBreakdown_PointsFromWealth = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.PointsFromWealth));
        public static readonly FieldInfo f_ThreatPointsBreakdown_PointsFromPawns = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.PointsFromPawns));
        public static readonly FieldInfo f_ThreatPointsBreakdown_TargetRandomFactor = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.TargetRandomFactor));
        public static readonly FieldInfo f_ThreatPointsBreakdown_AdaptationFactor = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.AdaptationFactor));
        public static readonly FieldInfo f_ThreatPointsBreakdown_GraceFactor = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.GraceFactor));
        public static readonly FieldInfo f_ThreatPointsBreakdown_PreClamp = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.PreClamp));
        public static readonly FieldInfo f_ThreatPointsBreakdown_PostClamp = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.PostClamp));
        public static readonly FieldInfo f_ThreatPointsBreakdown_StorytellerRandomFactor = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.StorytellerRandomFactor));
        public static readonly FieldInfo f_ThreatPointsBreakdown_RaidArrivalModeFactor = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.RaidArrivalModeFactor));
        public static readonly FieldInfo f_ThreatPointsBreakdown_RaidArrivalModeDesc = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.RaidArrivalModeDesc));
        public static readonly FieldInfo f_ThreatPointsBreakdown_RaidStrategyFactor = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.RaidStrategyFactor));
        public static readonly FieldInfo f_ThreatPointsBreakdown_RaidStrategyDesc = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.RaidStrategyDesc));
        public static readonly FieldInfo f_ThreatPointsBreakdown_RaidAgeRestrictionFactor = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.RaidAgeRestrictionFactor));
        public static readonly FieldInfo f_ThreatPointsBreakdown_RaidAgeRestrictionDesc = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.RaidAgeRestrictionDesc));
        public static readonly FieldInfo f_ThreatPointsBreakdown_AmbushManhunterFactor = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.AmbushManhunterFactor));
        public static readonly FieldInfo f_ThreatPointsBreakdown_CaravanDemandFactor = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.CaravanDemandFactor));
        public static readonly FieldInfo f_ThreatPointsBreakdown_CrashedShipPartFactor = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.CrashedShipPartFactor));
        public static readonly FieldInfo f_ThreatPointsBreakdown_PreMiscCalcs = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.PreMiscCalcs));
        public static readonly FieldInfo f_ThreatPointsBreakdown_AnimalInsanityMassCalc = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.AnimalInsanityMassCalc));
        public static readonly FieldInfo f_ThreatPointsBreakdown_CrashedShipPartMin = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.CrashedShipPartMin));
        public static readonly FieldInfo f_ThreatPointsBreakdown_FinalResult = AccessTools.Field(typeof(ThreatPointsBreakdown), nameof(ThreatPointsBreakdown.FinalResult));
        public static readonly FieldInfo f_Def_defName = AccessTools.Field(typeof(Def), "defName");
        public static readonly FieldInfo f_RaidAgeRestrictionDef_threatPointsFactor = AccessTools.Field(typeof(RaidAgeRestrictionDef), nameof(RaidAgeRestrictionDef.threatPointsFactor));
    }
}
