using RimWorld;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(IncidentWorker_Raid))]
    [HarmonyPatch("AdjustedRaidPoints")]
    public static class Patch_IncidentWorker_Raid_AdjustedRaidPoints
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundRaidArrivalModeFactor = false;
            bool foundAgeRestrictionFactor = false;
            bool foundMax = false;

            yield return new CodeInstruction(OpCodes.Ldarg_3);
            yield return new CodeInstruction(OpCodes.Ldfld, VisibleRaidPointsRefs.f_Faction_def);
            yield return new CodeInstruction(OpCodes.Ldfld, VisibleRaidPointsRefs.f_Def_defName);
            yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_FactionDesc);
            yield return new CodeInstruction(OpCodes.Ldarg_S, 4);
            yield return new CodeInstruction(OpCodes.Ldfld, VisibleRaidPointsRefs.f_Def_defName);
            yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_GroupKindDesc);

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_SimpleCurve_Evaluate)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    if (!foundRaidArrivalModeFactor)
                    {
                        yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_RaidArrivalModeFactor);
                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                        yield return new CodeInstruction(OpCodes.Ldfld, VisibleRaidPointsRefs.f_Def_defName);
                        yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_RaidArrivalModeDesc);
                        foundRaidArrivalModeFactor = true;
                    } 
                    else
                    {
                        yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_RaidStrategyFactor);
                        yield return new CodeInstruction(OpCodes.Ldarg_2);
                        yield return new CodeInstruction(OpCodes.Ldfld, VisibleRaidPointsRefs.f_Def_defName);
                        yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_RaidStrategyDesc);
                    }
                }
                else if (instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == VisibleRaidPointsRefs.f_RaidAgeRestrictionDef_threatPointsFactor)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_RaidAgeRestrictionFactor);
                    yield return new CodeInstruction(OpCodes.Ldarg_S, 5);
                    yield return new CodeInstruction(OpCodes.Ldfld, VisibleRaidPointsRefs.f_Def_defName);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_RaidAgeRestrictionDesc);
                    foundAgeRestrictionFactor = true;
                }
                else if (foundAgeRestrictionFactor && !foundMax && instruction.opcode == OpCodes.Ldarg_0)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_PreMiscCalcs);
                }
                else if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_Mathf_Max)
                {
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_RaidStrategyMin);
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_FinalResult);
                    foundMax = true;
                }
                else
                {
                    yield return instruction;
                }
            }
        }
    }
}
