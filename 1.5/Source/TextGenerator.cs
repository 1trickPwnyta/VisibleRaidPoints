using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
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

            List<float> additionalFactorsUsed = new List<float>();

            if (ThreatPointsBreakdown.StorytellerRandomFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_StorytellerRandomFactorDesc".Translate()}: {ThreatPointsBreakdown.StorytellerRandomFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(ThreatPointsBreakdown.StorytellerRandomFactor);
            }

            if (ThreatPointsBreakdown.RaidArrivalModeFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_RaidArrivalModeFactorDesc".Translate()}: {ThreatPointsBreakdown.RaidArrivalModeFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} {$"({ThreatPointsBreakdown.RaidArrivalModeDesc})".Colorize(ColoredText.SubtleGrayColor)}";
                additionalFactorsUsed.Add(ThreatPointsBreakdown.RaidArrivalModeFactor);
            }

            if (ThreatPointsBreakdown.RaidStrategyFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_RaidStrategyFactorDesc".Translate()}: {ThreatPointsBreakdown.RaidStrategyFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} {$"({ThreatPointsBreakdown.RaidStrategyDesc})".Colorize(ColoredText.SubtleGrayColor)}";
                additionalFactorsUsed.Add(ThreatPointsBreakdown.RaidStrategyFactor);
            }

            if (ThreatPointsBreakdown.RaidAgeRestrictionFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_RaidAgeRestrictionFactorDesc".Translate()}: {ThreatPointsBreakdown.RaidAgeRestrictionFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)} {$"({ThreatPointsBreakdown.RaidAgeRestrictionDesc})".Colorize(ColoredText.SubtleGrayColor)}";
                additionalFactorsUsed.Add(ThreatPointsBreakdown.RaidAgeRestrictionFactor);
            }

            if (ThreatPointsBreakdown.AmbushManhunterFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_AmbushManhunterFactorDesc".Translate()}: {ThreatPointsBreakdown.AmbushManhunterFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(ThreatPointsBreakdown.AmbushManhunterFactor);
            }

            if (ThreatPointsBreakdown.CaravanDemandFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_CaravanDemandFactorDesc".Translate()}: {ThreatPointsBreakdown.CaravanDemandFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(ThreatPointsBreakdown.CaravanDemandFactor);
            }

            if (ThreatPointsBreakdown.CrashedShipPartFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_CrashedShipPartFactorDesc".Translate()}: {ThreatPointsBreakdown.CrashedShipPartFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(ThreatPointsBreakdown.CrashedShipPartFactor);
            }

            if (ThreatPointsBreakdown.DeepDrillInfestationFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_DeepDrillInfestationFactorDesc".Translate()}: {ThreatPointsBreakdown.DeepDrillInfestationFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(ThreatPointsBreakdown.DeepDrillInfestationFactor);
            }

            if (ThreatPointsBreakdown.InfestationFactor > 0f)
            {
                text += $"\n\n{"VisibleRaidPoints_InfestationFactorDesc".Translate()}: {ThreatPointsBreakdown.InfestationFactor.ToString("0.00").Colorize(ColoredText.ImpactColor)}";
                additionalFactorsUsed.Add(ThreatPointsBreakdown.InfestationFactor);
            }

            if (additionalFactorsUsed.Count > 0)
            {
                // Running total pre-final
                text += $"\n\n{"VisibleRaidPoints_RunningTotalPreFinalDesc".Translate()}";
                text += $"\n{((int)ThreatPointsBreakdown.PostClamp).ToString().Colorize(ColoredText.FactionColor_Hostile)} ";
                foreach (float factor in additionalFactorsUsed)
                {
                    text += $"x {factor.ToString("0.00").Colorize(ColoredText.ImpactColor)} ";
                }
                text += $"= {((int)ThreatPointsBreakdown.PreMiscCalcs).ToString().Colorize(ColoredText.FactionColor_Hostile)}";
            }

            if (ThreatPointsBreakdown.AnimalInsanityMassCalc)
            {
                text += $"\n\n{"VisibleRaidPoints_AnimalInsanityMassCalcDesc".Translate()}";
                text += $"\n({((int)ThreatPointsBreakdown.PreMiscCalcs).ToString().Colorize(ColoredText.FactionColor_Hostile)} - 250) x 0.5 + 250 = {((int)ThreatPointsBreakdown.FinalResult).ToString().Colorize(ColoredText.FactionColor_Hostile)}";
            }

            if (ThreatPointsBreakdown.CrashedShipPartMin > 0f && ThreatPointsBreakdown.PreMiscCalcs < ThreatPointsBreakdown.CrashedShipPartMin)
            {
                text += $"\n\n{"VisibleRaidPoints_CrashedShipPartMinDesc".Translate(ThreatPointsBreakdown.CrashedShipPartMin)}";
            }

            if (ThreatPointsBreakdown.DeepDrillInfestationMin > 0f && ThreatPointsBreakdown.PreMiscCalcs < ThreatPointsBreakdown.DeepDrillInfestationMin)
            {
                text += $"\n\n{"VisibleRaidPoints_DeepDrillInfestationMinDesc".Translate(ThreatPointsBreakdown.DeepDrillInfestationMin)}";
            }

            if (ThreatPointsBreakdown.DeepDrillInfestationMax > 0f && ThreatPointsBreakdown.PreMiscCalcs > ThreatPointsBreakdown.DeepDrillInfestationMax)
            {
                text += $"\n\n{"VisibleRaidPoints_DeepDrillInfestationMaxDesc".Translate(ThreatPointsBreakdown.DeepDrillInfestationMax)}";
            }

            if (ThreatPointsBreakdown.MechClusterMax > 0f && ThreatPointsBreakdown.PreMiscCalcs > ThreatPointsBreakdown.MechClusterMax)
            {
                text += $"\n\n{"VisibleRaidPoints_MechClusterMaxDesc".Translate(ThreatPointsBreakdown.MechClusterMax)}";
            }

            if (ThreatPointsBreakdown.RaidStrategyMin > 0f && ThreatPointsBreakdown.PreMiscCalcs < ThreatPointsBreakdown.RaidStrategyMin)
            {
                text += $"\n\n{"VisibleRaidPoints_RaidStrategyMinDesc".Translate(ThreatPointsBreakdown.RaidStrategyMin)} {$"({ThreatPointsBreakdown.FactionDesc}, {ThreatPointsBreakdown.GroupKindDesc})".Colorize(ColoredText.SubtleGrayColor)}";
            }

            text += "\n\n----------------------";
            text += $"\n{"VisibleRaidPoints_BreakdownTotal".Translate()}: {((int)ThreatPointsBreakdown.FinalResult).ToString().Colorize(ColoredText.FactionColor_Hostile)}";

            return text;
        }
    }
}
