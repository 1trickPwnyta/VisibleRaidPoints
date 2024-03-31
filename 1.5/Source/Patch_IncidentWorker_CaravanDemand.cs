using RimWorld;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(IncidentWorker_CaravanDemand))]
    [HarmonyPatch("TryExecuteWorker")]
    public static class Patch_IncidentWorker_CaravanDemand_TryExecuteWorker
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundFactor = false;
            bool gotResult = false;
            bool foundTextGeneration = false;
            bool foundCast = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundFactor && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_FloatRange_get_RandomInRange)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_CaravanDemandFactor);
                    foundFactor = true;
                    continue;
                }

                if (foundFactor && !gotResult && instruction.opcode == OpCodes.Mul)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_PreMiscCalcs);
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stsfld, VisibleRaidPointsRefs.f_ThreatPointsBreakdown_FinalResult);
                    gotResult = true;
                    continue;
                }

                if (!foundTextGeneration && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_IncidentWorker_CaravanDemand_GenerateMessageText)
                {
                    instruction.operand = VisibleRaidPointsRefs.m_TextGenerator_GenerateCaravanDemandMessageText;
                    foundTextGeneration = true;
                }

                if (foundTextGeneration && !foundCast && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_TaggedString_op_Implicit_string)
                {
                    foundCast = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
