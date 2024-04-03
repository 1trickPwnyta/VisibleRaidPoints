using RimWorld;
using System.Collections.Generic;
using Verse;

namespace VisibleRaidPoints
{
    public static class TextGenerator
    {
        private static readonly float clampLow = 35f;
        private static readonly float pollutionRaidFactor = 1.5f;
        private static readonly float ambushManhunterFactor = 0.75f;
        private static readonly float crashedShipPartFactor = 0.9f;
        private static readonly float crashedShipPartMin = 300f;
        private static readonly float deepDrillInfestationMin = 200f;
        private static readonly float deepDrillInfestationMax = 1000f;
        private static readonly float mechClusterMax = 10000f;

        public static TaggedString GetThreatPointsIndicatorText(ThreatPointsBreakdown breakdown)
        {
            return $"{"VisibleRaidPoints_RaidPointsUsed".Translate()}: {((int)breakdown.FinalResult).ToString().Colorize(ColoredText.FactionColor_Hostile)}";
        }

        public static TaggedString GetThreatPointsBreakdownText(ThreatPointsBreakdown breakdown)
        {
            if (breakdown.PointsPerPawn == null)
            {
                Debug.Log("Points per pawn not initialized. Cannot provide threat points breakdown. Harmony transpiler patch probably failed.");
                return null;
            }

            TaggedString text = $"=== {"VisibleRaidPoints_PointsBreakdown".Translate()} ===";

            text += $"\n\n{"VisibleRaidPoints_BreakdownPlayerWealthForStorytellerDesc".Translate()}: {$"${(int)breakdown.PlayerWealthForStoryteller}".Colorize(ColoredText.CurrencyColor)} {$"({"VisibleRaidPoints_BreakdownPlayerWealthForStorytellerExpl".Translate()})".Colorize(ColoredText.SubtleGrayColor)}";
            text += $"\n{"VisibleRaidPoints_BreakdownPointsFromWealthDesc".Translate()}: {((int)breakdown.PointsFromWealth).ToString().Colorize(ColoredText.FactionColor_Hostile)}";

            text += "\n";
            if (breakdown.PointsPerPawn.Count > 0)
            {
                text += $"\n{"VisibleRaidPoints_BreakdownPointsPerPawnDesc".Translate()}: {$"({"VisibleRaidPoints_BreakdownPointsPerPawnExpl".Translate()})".Colorize(ColoredText.SubtleGrayColor)}";
            }
            foreach (ThreatPointsBreakdown.PawnPoints p in breakdown.PointsPerPawn)
            {
                if (p.Points > 0f)
                {
                    text += $"\n  {p.Name}: {p.Points.ToString(".0").Colorize(ColoredText.FactionColor_Hostile)}";
                }
            }
            text += $"\n{"VisibleRaidPoints_BreakdownPointsFromPawnsDesc".Translate()}: {((int)breakdown.GetPointsFromPawns()).ToString().Colorize(ColoredText.FactionColor_Hostile)}";

            if (breakdown.TargetRandomFactor != 1f)
            {
                text += $"\n\n{"VisibleRaidPoints_BreakdownTargetRandomFactorDesc".Translate()}: {breakdown.TargetRandomFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
            }

            text += $"\n\n{"VisibleRaidPoints_BreakdownAdaptationFactorDesc".Translate()}: {breakdown.AdaptationFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";

            text += $"\n\n{"VisibleRaidPoints_BreakdownThreatScaleDesc".Translate()}: {breakdown.ThreatScale.ToString("0.00").Colorize(ColoredText.ImpactColor)}";

            if (breakdown.GraceFactor != 1f)
            {
                text += $"\n\n{"VisibleRaidPoints_BreakdownGraceFactorDesc".Translate()}: {breakdown.GraceFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} {$"({"VisibleRaidPoints_BreakdownGraceFactorExpl".Translate()})".Colorize(ColoredText.SubtleGrayColor)}";
            }

            // Running total pre-clamp
            text += $"\n\n{"VisibleRaidPoints_RunningTotalPreClampDesc".Translate()}";
            text += $"\n({((int)breakdown.PointsFromWealth).ToString().Colorize(ColoredText.FactionColor_Hostile)} + {((int)breakdown.GetPointsFromPawns()).ToString().Colorize(ColoredText.FactionColor_Hostile)}) {(breakdown.TargetRandomFactor != 1f ? $"x {breakdown.TargetRandomFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} " : "")}x {breakdown.AdaptationFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} x {breakdown.ThreatScale.ToString("0.00").Colorize(ColoredText.ImpactColor)} {(breakdown.GraceFactor != 1f ? $"x {breakdown.GraceFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} " : "")}= {((int)breakdown.PreClamp).ToString().Colorize(ColoredText.FactionColor_Hostile)}";

            if (breakdown.PreClamp < clampLow)
            {
                text += $"\n\n{"VisibleRaidPoints_BreakdownClampLowDesc".Translate(clampLow)}";
            }

            if (breakdown.PostClamp < breakdown.PreClamp)
            {
                text += $"\n\n{"VisibleRaidPoints_BreakdownClampHighDesc".Translate(breakdown.PostClamp)}";
            }

            List<float> additionalFactorsUsed = new List<float>();

            if (breakdown.StorytellerRandomFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_StorytellerRandomFactorDesc".Translate()}: {breakdown.StorytellerRandomFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(breakdown.StorytellerRandomFactor);
            }

            if (breakdown.RaidArrivalModeDef != null && breakdown.RaidArrivalModeFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_RaidArrivalModeFactorDesc".Translate()}: {breakdown.RaidArrivalModeFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} {$"({breakdown.RaidArrivalModeDef.defName})".Colorize(ColoredText.SubtleGrayColor)}";
                additionalFactorsUsed.Add(breakdown.RaidArrivalModeFactor);
            }

            if (breakdown.RaidStrategyDef != null && breakdown.RaidStrategyFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_RaidStrategyFactorDesc".Translate()}: {breakdown.RaidStrategyFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} {$"({breakdown.RaidStrategyDef.defName})".Colorize(ColoredText.SubtleGrayColor)}";
                additionalFactorsUsed.Add(breakdown.RaidStrategyFactor);
            }

            if (breakdown.RaidAgeRestrictionDef != null)
            {
                text += $"\n\n{"VisibleRaidPoints_RaidAgeRestrictionFactorDesc".Translate()}: {breakdown.RaidAgeRestrictionDef.threatPointsFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} {$"({breakdown.RaidAgeRestrictionDef.defName})".Colorize(ColoredText.SubtleGrayColor)}";
                additionalFactorsUsed.Add(breakdown.RaidAgeRestrictionDef.threatPointsFactor);
            }

            if (breakdown.PollutionRaidFactor)
            {
                text += $"\n\n{"VisibleRaidPoints_PollutionRaidFactorDesc".Translate()}: {pollutionRaidFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(pollutionRaidFactor);
            }

            if (breakdown.AmbushManhunterFactor)
            {
                text += $"\n\n{"VisibleRaidPoints_AmbushManhunterFactorDesc".Translate()}: {ambushManhunterFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(ambushManhunterFactor);
            }

            if (breakdown.CaravanDemandFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_CaravanDemandFactorDesc".Translate()}: {breakdown.CaravanDemandFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(breakdown.CaravanDemandFactor);
            }

            if (breakdown.CrashedShipPartFactor)
            {
                text += $"\n\n{"VisibleRaidPoints_CrashedShipPartFactorDesc".Translate()}: {crashedShipPartFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(crashedShipPartFactor);
            }

            if (breakdown.DeepDrillInfestationFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_DeepDrillInfestationFactorDesc".Translate()}: {breakdown.DeepDrillInfestationFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(breakdown.DeepDrillInfestationFactor);
            }

            if (breakdown.InfestationFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_InfestationFactorDesc".Translate()}: {breakdown.InfestationFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(breakdown.InfestationFactor);
            }

            if (additionalFactorsUsed.Count > 0)
            {
                // Running total pre-final
                text += $"\n\n{"VisibleRaidPoints_RunningTotalPreFinalDesc".Translate()}";
                text += $"\n{((int)breakdown.PostClamp).ToString().Colorize(ColoredText.FactionColor_Hostile)} ";
                foreach (float factor in additionalFactorsUsed)
                {
                    text += $"x {factor.ToString("0.00").Colorize(ColoredText.ImpactColor)} ";
                }
                text += $"= {((int)breakdown.PreMiscCalcs).ToString().Colorize(ColoredText.FactionColor_Hostile)}";
            }

            if (breakdown.AnimalInsanityMassCalc)
            {
                text += $"\n\n{"VisibleRaidPoints_AnimalInsanityMassCalcDesc".Translate()}";
                text += $"\n({((int)breakdown.PreMiscCalcs).ToString().Colorize(ColoredText.FactionColor_Hostile)} - 250) x 0.5 + 250 = {((int)breakdown.FinalResult).ToString().Colorize(ColoredText.FactionColor_Hostile)}";
            }

            if (breakdown.CrashedShipPartFactor && breakdown.PreMiscCalcs < crashedShipPartMin)
            {
                text += $"\n\n{"VisibleRaidPoints_CrashedShipPartMinDesc".Translate(crashedShipPartMin)}";
            }

            if (breakdown.DeepDrillInfestationFactor > 0f)
            {
                if (breakdown.PreMiscCalcs < deepDrillInfestationMin)
                {
                    text += $"\n\n{"VisibleRaidPoints_DeepDrillInfestationMinDesc".Translate(deepDrillInfestationMin)}";
                }
                if (breakdown.PreMiscCalcs > deepDrillInfestationMax)
                {
                    text += $"\n\n{"VisibleRaidPoints_DeepDrillInfestationMaxDesc".Translate(deepDrillInfestationMax)}";
                }
            }

            if (breakdown.MechClusterMax && breakdown.PreMiscCalcs > mechClusterMax)
            {
                text += $"\n\n{"VisibleRaidPoints_MechClusterMaxDesc".Translate(mechClusterMax)}";
            }

            if (breakdown.RaidStrategyDef != null && breakdown.RaidFaction != null && breakdown.RaidGroupKindDef != null)
            {
                float raidStrategyMin = breakdown.RaidStrategyDef.Worker.MinimumPoints(breakdown.RaidFaction, breakdown.RaidGroupKindDef) * 1.05f;
                if (breakdown.PreMiscCalcs < raidStrategyMin)
                {
                    text += $"\n\n{"VisibleRaidPoints_RaidStrategyMinDesc".Translate(raidStrategyMin)} {$"({breakdown.RaidFaction.def.defName}, {breakdown.RaidGroupKindDef.defName})".Colorize(ColoredText.SubtleGrayColor)}";
                }
            }

            text += "\n\n----------------------";
            text += $"\n{"VisibleRaidPoints_BreakdownTotal".Translate()}: {((int)breakdown.FinalResult).ToString().Colorize(ColoredText.FactionColor_Hostile)}";

            return text;
        }
    }
}
