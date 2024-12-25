using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace VisibleRaidPoints
{
    [HarmonyPatch(typeof(ChoiceLetter))]
    [HarmonyPatch("get_Text")]
    public static class Patch_ChoiceLetter_get_Text
    {
        public static void Postfix(ChoiceLetter __instance, ref TaggedString __result)
        {
            __result = LetterUtility.GetModifiedText(__result, __instance);
        }
    }

    [HarmonyPatch(typeof(ChoiceLetter))]
    [HarmonyPatch("GetMouseoverText")]
    public static class Patch_ChoiceLetter_GetMouseoverText
    {
        public static void Postfix(ChoiceLetter __instance, TaggedString ___text, ref string __result)
        {
            __result = LetterUtility.GetModifiedText(___text, __instance).Resolve();
        }
    }

    [HarmonyPatch(typeof(ChoiceLetter))]
    [HarmonyPatch(nameof(ChoiceLetter.OpenLetter))]
    public static class Patch_ChoiceLetter_OpenLetter
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == VisibleRaidPointsRefs.f_ChoiceLetter_text)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, VisibleRaidPointsRefs.m_LetterUtility_GetModifiedText_ChoiceLetter);
                    continue;
                }

                yield return instruction;
            }
        }
    }

    [HarmonyPatch(typeof(ChoiceLetter))]
    [HarmonyPatch(nameof(ChoiceLetter.ExposeData))]
    public static class Patch_ChoiceLetter_ExposeData
    {
        public static void Postfix(ChoiceLetter __instance)
        {
            ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetAssociated(__instance);
            Scribe_Deep.Look(ref breakdown, "ThreatPointsBreakdown");
            ThreatPointsBreakdown.Associate(__instance, breakdown);
        }
    }
}
