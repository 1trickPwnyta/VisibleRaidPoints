using RimWorld;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(IncidentWorker_DeepDrillInfestation))]
    [HarmonyPatch("TryExecuteWorker")]
    public static class Patch_IncidentWorker_DeepDrillInfestation_TryExecuteWorker
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundPoints = false;
            bool loadedFactor = false;
            bool loadedMin = false;
            bool loadedMax = false;
            bool foundClamp = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundPoints && instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == VisibleRaidPointsRefs.f_IncidentParms_points)
                {
                    foundPoints = true;
                }

                if (foundPoints && !loadedFactor && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_Rand_Range)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_DeepDrillInfestationFactor);
                    loadedFactor = true;
                    continue;
                }

                if (loadedFactor && !loadedMin && instruction.opcode == OpCodes.Mul)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_PreMiscCalcs);
                    continue;
                }

                if (loadedFactor && !loadedMin && instruction.opcode == OpCodes.Ldc_R4)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_DeepDrillInfestationMin);
                    loadedMin = true;
                    continue;
                }

                if (loadedMin && !loadedMax && instruction.opcode == OpCodes.Ldc_R4)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_DeepDrillInfestationMax);
                    loadedMax = true;
                    continue;
                }

                if (loadedMax && !foundClamp && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_Mathf_Clamp)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_FinalResult);
                    foundClamp = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
