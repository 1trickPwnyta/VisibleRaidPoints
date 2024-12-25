using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Verse;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(ArchivedDialog))]
    [HarmonyPatch("RimWorld.IArchivable.OpenArchived")]
    public static class Patch_ArchivedDialog_RimWorld_IArchivable_OpenArchived
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == VisibleRaidPointsRefs.f_ArchivedDialog_text)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, VisibleRaidPointsRefs.m_LetterUtility_GetModifiedText_ArchivedDialog);
                    continue;
                }

                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == VisibleRaidPointsRefs.m_Find_get_WindowStack)
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_0);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, VisibleRaidPointsRefs.m_LetterUtility_AddBreakdownOption_ArchivedDialog);
                }

                yield return instruction;
            }
        }
    }

    [HarmonyPatch(typeof(ArchivedDialog))]
    [HarmonyPatch("RimWorld.IArchivable.get_ArchivedTooltip")]
    public static class Patch_ArchivedDialog_RimWorld_IArchivable_get_ArchivedTooltip
    {
        public static void Postfix(ArchivedDialog __instance, string ___text, ref string __result)
        {
            __result = LetterUtility.GetModifiedText(___text, __instance);
        }
    }

    [HarmonyPatch(typeof(ArchivedDialog))]
    [HarmonyPatch("RimWorld.IArchivable.get_ArchivedLabel")]
    public static class Patch_ArchivedDialog_RimWorld_IArchivable_get_ArchivedLabel
    {
        public static void Postfix(ArchivedDialog __instance, string ___text, ref string __result)
        {
            __result = LetterUtility.GetModifiedText(___text, __instance).Flatten();
        }
    }

    [HarmonyPatch(typeof(ArchivedDialog))]
    [HarmonyPatch(nameof(ArchivedDialog.ExposeData))]
    public static class Patch_ArchivedDialog_ExposeData
    {
        public static void Postfix(ArchivedDialog __instance)
        {
            ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetAssociated(__instance);
            Scribe_Deep.Look(ref breakdown, "ThreatPointsBreakdown");
            ThreatPointsBreakdown.Associate(__instance, breakdown);
        }
    }
}
