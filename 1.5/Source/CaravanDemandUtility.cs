using RimWorld.Planet;
using RimWorld;
using System;
using System.Collections.Generic;
using Verse;
using HarmonyLib;

namespace VisibleRaidPoints
{
    public static class CaravanDemandUtility
    {
        public static TaggedString GenerateCaravanDemandMessageText(IncidentWorker_CaravanDemand instance, Faction enemyFaction, int attackerCount, List<ThingCount> demands, Caravan caravan, out TaggedString originalText)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            originalText = (string)typeof(IncidentWorker_CaravanDemand).Method("GenerateMessageText").Invoke(instance, new object[] { enemyFaction, attackerCount, demands, caravan });
            TaggedString text = originalText;

            if (VisibleRaidPointsSettings.CaravanDemand)
            {
                ThreatPointsBreakdown breakdown = ThreatPointsBreakdown.GetCurrent();

                if (breakdown != null)
                {
                    if (VisibleRaidPointsSettings.ShowInText)
                    {
                        text += $"\n\n{TextGenerator.GetThreatPointsIndicatorText(breakdown)}";
                    }

                    if (VisibleRaidPointsSettings.ShowBreakdown)
                    {
                        text += $"\n\n=== {"VisibleRaidPoints_PointsBreakdown".Translate()} ===";
                        text += $"\n\n{TextGenerator.GetThreatPointsBreakdownText(breakdown)}";
                    }
                }
            }

            return text;
        }
    }
}
