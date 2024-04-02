using RimWorld;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(IncidentWorker_Infestation))]
    [HarmonyPatch("TryExecuteWorker")]
    public static class Patch_IncidentWorker_Infestation_TryExecuteWorker
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool loadedFactor = false;
            bool foundMul = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!loadedFactor && instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_SimpleCurve_Evaluate)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_InfestationFactor);
                    loadedFactor = true;
                    continue;
                }

                if (loadedFactor && !foundMul && instruction.opcode == OpCodes.Mul)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_PreMiscCalcs);
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_FinalResult);
                    foundMul = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
