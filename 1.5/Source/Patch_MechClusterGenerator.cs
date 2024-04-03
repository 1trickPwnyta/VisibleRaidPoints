using RimWorld;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(MechClusterGenerator))]
    [HarmonyPatch(nameof(MechClusterGenerator.GenerateClusterSketch))]
    public static class Patch_MechClusterGenerator_GenerateClusterSketch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundCap = false;
            bool foundMin = false;

            yield return new CodeInstruction(OpCodes.Call, VisibleRaidPointsRefs.m_ThreatPointsBreakdown_SetMechClusterMax);

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundCap && instruction.opcode == OpCodes.Ldc_R4)
                {
                    yield return instruction;
                    foundCap = true;
                    continue;
                }

                if (foundCap && !foundMin && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_Mathf_Min)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Call, VisibleRaidPointsRefs.m_ThreatPointsBreakdown_SetFinalResult);
                    foundMin = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
