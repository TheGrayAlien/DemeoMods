namespace HouseRules.Essentials.Rulesets
{
    using System.Collections.Generic;
    using DataKeys;
    using HouseRules.Core.Types;
    using HouseRules.Essentials.Rules;

    internal static class PotionCommotionRuleset
    {
        internal static Ruleset Create()
        {
            const string name = "Potion Commotion";
            const string description = "Nothing but potions in the cards you get given. Enemies do not respawn.";
            const string longdesc = "";

            var allowedCards = new List<AbilityKey>
            {
                AbilityKey.AdamantPotion,
                AbilityKey.BottleOfLye,
                AbilityKey.ExtraActionPotion,
                AbilityKey.FireImmunePotion,
                AbilityKey.HealingPotion,
                AbilityKey.IceImmunePotion,
                AbilityKey.LuckPotion,
                AbilityKey.MagicPotion,
                AbilityKey.SpellPowerPotion,
                AbilityKey.StrengthPotion,
                AbilityKey.SwiftnessPotion,
                AbilityKey.VigorPotion,
                AbilityKey.WaterBottle,
            };

            var allowedCardsRule = new CardAdditionOverriddenRule(new Dictionary<BoardPieceId, List<AbilityKey>>
            {
                { BoardPieceId.HeroBarbarian, allowedCards },
                { BoardPieceId.HeroBard, allowedCards },
                { BoardPieceId.HeroGuardian, allowedCards },
                { BoardPieceId.HeroHunter, allowedCards },
                { BoardPieceId.HeroRogue, allowedCards },
                { BoardPieceId.HeroSorcerer, allowedCards },
                { BoardPieceId.HeroWarlock, allowedCards },
            });

            var abilityActionCostRule = new AbilityActionCostAdjustedRule(new Dictionary<AbilityKey, bool>
            {
                { AbilityKey.Zap, false },
                { AbilityKey.Overcharge, false },
                { AbilityKey.LightningBolt, false },
            });

            var enemyRespanDisabled = new EnemyRespawnDisabledRule(true);

            var levelPropertiesRule = new LevelPropertiesModifiedRule(new Dictionary<string, int>
            {
                { "FloorOneElvenSummoners", 0 },
                { "FloorTwoElvenSummoners", 0 },
                { "FloorThreeElvenSummoners", 0 },
            });

            return Ruleset.NewInstance(
                name,
                description,
                longdesc,
                levelPropertiesRule,
                allowedCardsRule,
                abilityActionCostRule,
                enemyRespanDisabled);
        }
    }
}
