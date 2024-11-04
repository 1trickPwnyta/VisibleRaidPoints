using Verse;
using RimWorld;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(QuestPart_Letter))]
    [HarmonyPatch(nameof(QuestPart_Letter.Notify_QuestSignalReceived))]
    public static class Patch_QuestPart_Letter_Notify_QuestSignalReceived
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_LetterStack_ReceiveLetter_Letter)
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_2);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, VisibleRaidPointsRefs.m_LetterUtility_InjectThreatPoints);
                }

                yield return instruction;
            }
        }
    }

    [HarmonyPatch(typeof(QuestPart_Letter))]
    [HarmonyPatch(nameof(QuestPart_Letter.ExposeData))]
    public static class Patch_QuestPart_Letter_ExposeData
    {
        public static void Postfix(QuestPart_Letter __instance)
        {
            ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetAssociated(__instance);
            Scribe_Deep.Look(ref breakdown, "ThreatPointsBreakdown");
            ThreatPointsBreakdown.Associate(__instance, breakdown);
        }
    }
}
