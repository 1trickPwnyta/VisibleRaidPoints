using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using RimWorld.QuestGen;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(QuestNode_Root_PollutionRaid))]
    [HarmonyPatch("RunInt")]
    public static class Patch_QuestNode_Root_PollutionRaid_RunInt
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            yield return new CodeInstruction(OpCodes.Call, VisibleRaidPointsRefs.m_ThreatPointsBreakdown_SetPollutionRaidFactor);

            foreach (CodeInstruction instruction in instructions)
            {
                yield return instruction;
            }
        }
    }
}
