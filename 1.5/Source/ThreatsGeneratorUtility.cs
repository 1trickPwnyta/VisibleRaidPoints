using RimWorld;
using System.Collections.Generic;

namespace VisibleRaidPoints
{
    public static class ThreatsGeneratorUtility
    {
        private static Dictionary<ThreatsGeneratorParams, string> questNames = new Dictionary<ThreatsGeneratorParams, string>();

        public static string GetQuestName(this ThreatsGeneratorParams parms)
        {
            return questNames.ContainsKey(parms) ? questNames[parms] : null;
        }

        public static void SetQuestName(this ThreatsGeneratorParams parms, string name)
        {
            questNames[parms] = name;
        }
    }
}
