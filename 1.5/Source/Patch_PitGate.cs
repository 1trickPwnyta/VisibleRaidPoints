using RimWorld;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(PitGate))]
    [HarmonyPatch("TryFireIncident")]
    public static class Patch_PitGate_TryFireIncident
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_LetterStack_ReceiveLetter)
                {
                    yield return new CodeInstruction(OpCodes.Ldsfld, VisibleRaidPointsRefs.f_VisibleRaidPointsSettings_PitGateEmergence);
                    yield return new CodeInstruction(OpCodes.Call, VisibleRaidPointsRefs.m_LetterUtility_ReceiveLetter);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
