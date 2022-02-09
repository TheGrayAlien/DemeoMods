﻿namespace HouseRules.Essentials
{
    using System.Collections.Generic;
    using HouseRules.Essentials.Rules;
    using HouseRules.Essentials.Rulesets;
    using HouseRules.Types;
    using MelonLoader;

    internal class EssentialsMod : MelonMod
    {
        internal static readonly MelonLogger.Instance Logger = new MelonLogger.Instance("HouseRules:Essentials");

        public override void OnApplicationStart()
        {
            RegisterRuleTypes();
            RegisterRulesets();
        }

        private static void RegisterRuleTypes()
        {
            HR.Rulebook.Register(typeof(SampleRule));
            HR.Rulebook.Register(typeof(AbilityAoeAdjustedRule));
            HR.Rulebook.Register(typeof(AbilityDamageAdjustedRule));
            HR.Rulebook.Register(typeof(AbilityActionCostAdjustedRule));
            HR.Rulebook.Register(typeof(ActionPointsAdjustedRule));
            HR.Rulebook.Register(typeof(CardEnergyFromAttackMultipliedRule));
            HR.Rulebook.Register(typeof(CardEnergyFromRecyclingMultipliedRule));
            HR.Rulebook.Register(typeof(CardLimitModifiedRule));
            HR.Rulebook.Register(typeof(CardSellValueMultipliedRule));
            HR.Rulebook.Register(typeof(EnemyAttackScaledRule));
            HR.Rulebook.Register(typeof(EnemyDoorOpeningDisabledRule));
            HR.Rulebook.Register(typeof(EnemyHealthScaledRule));
            HR.Rulebook.Register(typeof(EnemyRespawnDisabledRule));
            HR.Rulebook.Register(typeof(GoldPickedUpMultipliedRule));
            HR.Rulebook.Register(typeof(GoldPickedUpScaledRule));
            HR.Rulebook.Register(typeof(LevelPropertiesModifiedRule));
            HR.Rulebook.Register(typeof(PieceConfigAdjustedRule));
            HR.Rulebook.Register(typeof(RatNestsSpawnGoldRule));
            HR.Rulebook.Register(typeof(StartCardsModifiedRule));
            HR.Rulebook.Register(typeof(StartHealthAdjustedRule));
        }

        private static void RegisterRulesets()
        {
            HR.Rulebook.Register(SampleRuleset.Create());
        }
    }
}