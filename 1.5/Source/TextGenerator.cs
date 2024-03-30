using Verse;

namespace VisibleRaidPoints
{
    public static class TextGenerator
    {
        public static TaggedString GetThreatPointsIndicatorText()
        {
            return $"{"VisibleRaidPoints_RaidPointsUsed".Translate()}: {((int)ThreatPointsBreakdown.FinalResult).ToString().Colorize(ColoredText.FactionColor_Hostile)}";
        }

        public static TaggedString GetThreatPointsBreakdownText()
        {
            if (ThreatPointsBreakdown.PointsPerPawn == null)
            {
                Debug.Log("Points per pawn not initialized. Cannot provide threat points breakdown. Harmony transpiler patch probably failed.");
                return null;
            }

            if (Find.Storyteller == null || Find.Storyteller.difficulty == null)
            {
                Debug.Log("Storyteller or difficulty settings not available. Cannot provide threat points breakdown. This shouldn't happen unless version < 1.3.");
                return null;
            }

            float clampLow = 35f;

            TaggedString text = $"=== {"VisibleRaidPoints_PointsBreakdown".Translate()} ===";

            text += $"\n\n{"VisibleRaidPoints_BreakdownPlayerWealthForStorytellerDesc".Translate()}: {$"${(int)ThreatPointsBreakdown.PlayerWealthForStoryteller}".Colorize(ColoredText.CurrencyColor)} {$"({"VisibleRaidPoints_BreakdownPlayerWealthForStorytellerExpl".Translate()})".Colorize(ColoredText.SubtleGrayColor)}";
            text += $"\n{"VisibleRaidPoints_BreakdownPointsFromWealthDesc".Translate()}: {((int)ThreatPointsBreakdown.PointsFromWealth).ToString().Colorize(ColoredText.FactionColor_Hostile)}";

            text += "\n";
            if (ThreatPointsBreakdown.PointsPerPawn.Count > 0)
            {
                text += $"\n{"VisibleRaidPoints_BreakdownPointsPerPawnDesc".Translate()}: {$"({"VisibleRaidPoints_BreakdownPointsPerPawnExpl".Translate()})".Colorize(ColoredText.SubtleGrayColor)}";
            }
            foreach (ThreatPointsBreakdown.PawnPoints p in ThreatPointsBreakdown.PointsPerPawn)
            {
                if (p.Points > 0f)
                {
                    text += $"\n  {p.Name}: {p.Points.ToString(".0").Colorize(ColoredText.FactionColor_Hostile)}";
                }
            }
            text += $"\n{"VisibleRaidPoints_BreakdownPointsFromPawnsDesc".Translate()}: {((int)ThreatPointsBreakdown.PointsFromPawns).ToString().Colorize(ColoredText.FactionColor_Hostile)}";

            if (ThreatPointsBreakdown.TargetRandomFactor != 1f)
            {
                text += $"\n\n{"VisibleRaidPoints_BreakdownTargetRandomFactorDesc".Translate()}: {ThreatPointsBreakdown.TargetRandomFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
            }

            text += $"\n\n{"VisibleRaidPoints_BreakdownAdaptationFactorDesc".Translate()}: {ThreatPointsBreakdown.AdaptationFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";

            text += $"\n\n{"VisibleRaidPoints_BreakdownThreatScaleDesc".Translate()}: {Find.Storyteller.difficulty.threatScale.ToString("0.00").Colorize(ColoredText.ImpactColor)}";

            if (ThreatPointsBreakdown.GraceFactor != 1f)
            {
                text += $"\n\n{"VisibleRaidPoints_BreakdownGraceFactorDesc".Translate()}: {ThreatPointsBreakdown.GraceFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} {$"({"VisibleRaidPoints_BreakdownGraceFactorExpl".Translate()})".Colorize(ColoredText.SubtleGrayColor)}";
            }

            // Running total pre-clamp
            text += $"\n\n{"VisibleRaidPoints_RunningTotalPreClampDesc".Translate()}";
            text += $"\n({((int)ThreatPointsBreakdown.PointsFromWealth).ToString().Colorize(ColoredText.FactionColor_Hostile)} + {((int)ThreatPointsBreakdown.PointsFromPawns).ToString().Colorize(ColoredText.FactionColor_Hostile)}) {(ThreatPointsBreakdown.TargetRandomFactor != 1f ? $"x {ThreatPointsBreakdown.TargetRandomFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} " : "")}x {ThreatPointsBreakdown.AdaptationFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} x {Find.Storyteller.difficulty.threatScale.ToString("0.00").Colorize(ColoredText.ImpactColor)} {(ThreatPointsBreakdown.GraceFactor != 1f ? $"x {ThreatPointsBreakdown.GraceFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} " : "")}= {((int)ThreatPointsBreakdown.PreClamp).ToString().Colorize(ColoredText.FactionColor_Hostile)}";

            if (ThreatPointsBreakdown.PreClamp < clampLow)
            {
                text += $"\n\n{"VisibleRaidPoints_BreakdownClampLowDesc".Translate(clampLow)}";
            }

            if (ThreatPointsBreakdown.PostClamp < ThreatPointsBreakdown.PreClamp)
            {
                text += $"\n\n{"VisibleRaidPoints_BreakdownClampHighDesc".Translate(ThreatPointsBreakdown.PostClamp)}";
            }

            if (ThreatPointsBreakdown.StorytellerRandomFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_StorytellerRandomFactorDesc".Translate()}: {ThreatPointsBreakdown.StorytellerRandomFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
            }

            if (ThreatPointsBreakdown.RaidArrivalModeFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_RaidArrivalModeFactorDesc".Translate()}: {ThreatPointsBreakdown.RaidArrivalModeFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} {$"({ThreatPointsBreakdown.RaidArrivalModeDesc})".Colorize(ColoredText.SubtleGrayColor)}";
            }

            if (ThreatPointsBreakdown.RaidStrategyFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_RaidStrategyFactorDesc".Translate()}: {ThreatPointsBreakdown.RaidStrategyFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} {$"({ThreatPointsBreakdown.RaidStrategyDesc})".Colorize(ColoredText.SubtleGrayColor)}";
            }

            if (ThreatPointsBreakdown.RaidAgeRestrictionFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_RaidAgeRestrictionFactorDesc".Translate()}: {ThreatPointsBreakdown.RaidAgeRestrictionFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} {$"({ThreatPointsBreakdown.RaidAgeRestrictionDesc})".Colorize(ColoredText.SubtleGrayColor)}";
            }

            if (ThreatPointsBreakdown.AmbushManhunterFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_AmbushManhunterFactorDesc".Translate()}: {ThreatPointsBreakdown.AmbushManhunterFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
            }

            if (ThreatPointsBreakdown.StorytellerRandomFactor > 0f || ThreatPointsBreakdown.RaidArrivalModeFactor > 0f || ThreatPointsBreakdown.RaidStrategyFactor > 0f || ThreatPointsBreakdown.RaidAgeRestrictionFactor > 0f || ThreatPointsBreakdown.AmbushManhunterFactor > 0f)
            {
                // Running total pre-final
                text += $"\n\n{"VisibleRaidPoints_RunningTotalPreFinalDesc".Translate()}";
                text += $"\n{((int)ThreatPointsBreakdown.PostClamp).ToString().Colorize(ColoredText.FactionColor_Hostile)} {(ThreatPointsBreakdown.StorytellerRandomFactor > 0f ? $"x {ThreatPointsBreakdown.StorytellerRandomFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} " : "")}{(ThreatPointsBreakdown.RaidArrivalModeFactor > 0f ? $"x {ThreatPointsBreakdown.RaidArrivalModeFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} " : "")}{(ThreatPointsBreakdown.RaidStrategyFactor > 0f ? $"x {ThreatPointsBreakdown.RaidStrategyFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} " : "")}{(ThreatPointsBreakdown.RaidAgeRestrictionFactor > 0f ? $"x {ThreatPointsBreakdown.RaidAgeRestrictionFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} " : "")}{(ThreatPointsBreakdown.AmbushManhunterFactor > 0f ? $"x {ThreatPointsBreakdown.AmbushManhunterFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} " : "")}= {((int)ThreatPointsBreakdown.PreMiscCalcs).ToString().Colorize(ColoredText.FactionColor_Hostile)}";
            }

            if (ThreatPointsBreakdown.AnimalInsanityMassCalc)
            {
                text += $"\n\n{"VisibleRaidPoints_AnimalInsanityMassCalcDesc".Translate()}";
                text += $"\n({((int)ThreatPointsBreakdown.PreMiscCalcs).ToString().Colorize(ColoredText.FactionColor_Hostile)} - 250) x 0.5 + 250 = {((int)ThreatPointsBreakdown.FinalResult).ToString().Colorize(ColoredText.FactionColor_Hostile)}";
            }

            text += "\n\n----------------------";
            text += $"\n{"VisibleRaidPoints_BreakdownTotal".Translate()}: {((int)ThreatPointsBreakdown.FinalResult).ToString().Colorize(ColoredText.FactionColor_Hostile)}";

            return text;
        }
    }
}
