using RimWorld;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(IncidentWorker_CrashedShipPart))]
    [HarmonyPatch("TryExecuteWorker")]
    public static class Patch_IncidentWorker_CrashedShipPart_TryExecuteWorker
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundPoints = false;
            bool loadedFactor = false;
            bool loadedMin = false;
            bool foundMax = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundPoints && instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == VisibleRaidPointsRefs.f_IncidentParms_points)
                {
                    foundPoints = true;
                }

                if (foundPoints && !loadedFactor && instruction.opcode == OpCodes.Ldc_R4)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_CrashedShipPartFactor);
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
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_CrashedShipPartMin);
                    loadedMin = true;
                    continue;
                }

                if (loadedMin && !foundMax && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_Mathf_Max)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_FinalResult);
                    foundMax = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
