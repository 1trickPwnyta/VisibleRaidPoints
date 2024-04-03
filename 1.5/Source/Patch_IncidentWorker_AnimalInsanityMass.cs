using RimWorld;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Linq;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(IncidentWorker_AnimalInsanityMass))]
    [HarmonyPatch("TryExecuteWorker")]
    public static class Patch_IncidentWorker_AnimalInsanityMass_TryExecuteWorker
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            for (int i = 0; i < instructionsList.Count; i++)
            {
                CodeInstruction instruction = instructionsList[i];
                if (instruction.opcode == OpCodes.Add)
                {
                    CodeInstruction nextInstruction = instructionsList[i + 1];
                    if (nextInstruction.opcode == OpCodes.Stfld)
                    {
                        yield return instruction;
                        yield return new CodeInstruction(OpCodes.Dup);
                        yield return new CodeInstruction(OpCodes.Call, VisibleRaidPointsRefs.m_ThreatPointsBreakdown_SetFinalResult);
                        yield return new CodeInstruction(OpCodes.Call, VisibleRaidPointsRefs.m_ThreatPointsBreakdown_SetAnimalInsanityMassCalc);
                    }
                }
                else
                {
                    yield return instruction;
                }
            }
        }
    }
}
