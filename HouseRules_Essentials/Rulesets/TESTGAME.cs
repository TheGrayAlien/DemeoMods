﻿namespace HouseRules.Essentials.Rulesets
{
    using System.Collections.Generic;
    using DataKeys;
    using global::Types;
    using HouseRules.Essentials.Rules;
    using HouseRules.Types;

    internal static class TESTGAME
    {
        internal static Ruleset Create()
        {
            const string name = "TEST Demeo Revolutions";
            const string description = "Everything that has a beginning has an end.";

            var spawnCategoriesRule = new SpawnCategoryOverriddenRule(new Dictionary<BoardPieceId, List<int>>
            {
                { BoardPieceId.ScarabSandPile, new List<int> { 1, 1, 1 } },
                { BoardPieceId.LargeCorruption, new List<int> { 3, 1, 1 } },
                { BoardPieceId.ReptileArcher, new List<int> { 3, 1, 1 } },
                { BoardPieceId.SandScorpion, new List<int> { 4, 1, 1 } },
                { BoardPieceId.ServantOfAlfaragh, new List<int> { 3, 1, 1 } },
                { BoardPieceId.GoldSandPile, new List<int> { 2, 1, 1 } },
                { BoardPieceId.SmallCorruption, new List<int> { 3, 2, 1 } },
                { BoardPieceId.GeneralRonthian, new List<int> { 3, 1, 1 } },
                { BoardPieceId.TheUnseen, new List<int> { 4, 2, 1 } },
                { BoardPieceId.ElvenArcher, new List<int> { 4, 1, 1 } },
                { BoardPieceId.ElvenHound, new List<int> { 4, 2, 1 } },
                { BoardPieceId.RootHound, new List<int> { 4, 2, 1 } },
                { BoardPieceId.TheUnspoken, new List<int> { 4, 2, 1 } },
                { BoardPieceId.ElvenCultist, new List<int> { 4, 2, 1 } },
                { BoardPieceId.Bandit, new List<int> { 4, 1, 1 } },
                { BoardPieceId.DruidArcher, new List<int> { 4, 2, 1 } },
                { BoardPieceId.DruidHoundMaster, new List<int> { 4, 1, 1 } },
                { BoardPieceId.GoblinChieftan, new List<int> { 4, 1, 1 } },
                { BoardPieceId.GoblinMadUn, new List<int> { 4, 1, 1 } },
                { BoardPieceId.RootBeast, new List<int> { 4, 2, 1 } },
                { BoardPieceId.ScabRat, new List<int> { 4, 1, 1 } },
                { BoardPieceId.Spider, new List<int> { 4, 2, 1 } },
                { BoardPieceId.Rat, new List<int> { 4, 2, 1 } },
                { BoardPieceId.TheUnheard, new List<int> { 4, 2, 1 } },
                { BoardPieceId.Slimeling, new List<int> { 4, 1, 1 } },
                { BoardPieceId.Thug, new List<int> { 4, 1, 1 } },
                { BoardPieceId.ElvenMystic, new List<int> { 4, 2, 1 } },
                { BoardPieceId.ElvenPriest, new List<int> { 4, 1, 1 } },
                { BoardPieceId.ElvenSkirmisher, new List<int> { 4, 2, 1 } },
                { BoardPieceId.GoblinFighter, new List<int> { 4, 2, 1 } },
                { BoardPieceId.GoblinRanger, new List<int> { 4, 2, 1 } },
                { BoardPieceId.SpiderEgg, new List<int> { 3, 1, 1 } },
                { BoardPieceId.SporeFungus, new List<int> { 3, 1, 1 } },
                { BoardPieceId.RatNest, new List<int> { 3, 1, 1 } },
                { BoardPieceId.CultMemberElder, new List<int> { 4, 1, 1 } },
                { BoardPieceId.RootMage, new List<int> { 4, 2, 1 } },
                { BoardPieceId.KillerBee, new List<int> { 4, 1, 1 } },
                { BoardPieceId.ChestGoblin, new List<int> { 2, 1, 1 } },
                { BoardPieceId.EarthElemental, new List<int> { 2, 1, 1 } },
                { BoardPieceId.Sigataur, new List<int> { 2, 1, 1 } },
                { BoardPieceId.GiantSlime, new List<int> { 2, 1, 1 } },
                { BoardPieceId.FireElemental, new List<int> { 2, 1, 1 } },
                { BoardPieceId.ElvenMarauder, new List<int> { 2, 1, 1 } },
                { BoardPieceId.IceElemental, new List<int> { 2, 1, 1 } },
                { BoardPieceId.GiantSpider, new List<int> { 2, 1, 1 } },
                { BoardPieceId.Mimic, new List<int> { 2, 1, 1 } },
                { BoardPieceId.Cavetroll, new List<int> { 2, 1, 2 } },
                { BoardPieceId.RootGolem, new List<int> { 2, 1, 2 } },
                { BoardPieceId.Brookmare, new List<int> { 2, 1, 2 } },
                { BoardPieceId.Gorgon, new List<int> { 2, 1, 2 } },
                { BoardPieceId.SilentSentinel, new List<int> { 2, 1, 2 } },
                { BoardPieceId.Wyvern, new List<int> { 2, 1, 2 } },
                { BoardPieceId.BigBoiMutant, new List<int> { 2, 1, 1 } },
                { BoardPieceId.ReptileMutantWizard, new List<int> { 3, 1, 1 } },
            });

            /*var myEntranceDeckFloor1 = new Dictionary<BoardPieceId, int>
            {
                { BoardPieceId.LargeCorruption, 3 },
                { BoardPieceId.ReptileArcher, 3 },
                { BoardPieceId.SandScorpion, 4 },
                { BoardPieceId.ServantOfAlfaragh, 3 },
                { BoardPieceId.GoldSandPile, 2 },
                { BoardPieceId.SmallCorruption, 3 },
                { BoardPieceId.GeneralRonthian, 3 },
                { BoardPieceId.TheUnseen, 4 },
                { BoardPieceId.ElvenArcher, 4 },
                { BoardPieceId.ElvenHound, 4 },
                { BoardPieceId.RootHound, 0 },
                { BoardPieceId.TheUnspoken, 4 },
                { BoardPieceId.ElvenCultist, 4 },
                { BoardPieceId.Bandit, 4 },
                { BoardPieceId.GoblinMadUn, 4 },
                { BoardPieceId.RootBeast, 4 },
                { BoardPieceId.ScabRat, 4 },
                { BoardPieceId.Spider, 4 },
                { BoardPieceId.Rat, 0 },
                { BoardPieceId.RootGolem, 2 },
                { BoardPieceId.Brookmare, 2 },
                { BoardPieceId.Gorgon, 2 },
                { BoardPieceId.SilentSentinel, 2 },
                { BoardPieceId.Wyvern, 2 },
                { BoardPieceId.BigBoiMutant, 2 },
                { BoardPieceId.ReptileMutantWizard, 3 },
            };
            var myExitDeckFloor1 = new Dictionary<BoardPieceId, int>
            {
                { BoardPieceId.DruidArcher, 4 },
                { BoardPieceId.DruidHoundMaster, 4 },
                { BoardPieceId.GoblinChieftan, 4 },
                { BoardPieceId.TheUnheard, 4 },
                { BoardPieceId.Slimeling, 0 },
                { BoardPieceId.Thug, 4 },
                { BoardPieceId.ElvenMystic, 4 },
                { BoardPieceId.ElvenPriest, 4 },
                { BoardPieceId.ElvenSkirmisher, 4 },
                { BoardPieceId.GoblinFighter, 0 },
                { BoardPieceId.GoblinRanger, 4 },
                { BoardPieceId.SpiderEgg, 3 },
                { BoardPieceId.SporeFungus, 3 },
                { BoardPieceId.RatNest, 3 },
                { BoardPieceId.CultMemberElder, 4 },
                { BoardPieceId.RootMage, 4 },
                { BoardPieceId.KillerBee, 4 },
                { BoardPieceId.ChestGoblin, 2 },
                { BoardPieceId.EarthElemental, 2 },
                { BoardPieceId.Sigataur, 2 },
                { BoardPieceId.GiantSlime, 2 },
                { BoardPieceId.FireElemental, 2 },
                { BoardPieceId.ElvenMarauder, 2 },
                { BoardPieceId.IceElemental, 2 },
                { BoardPieceId.GiantSpider, 2 },
                { BoardPieceId.Mimic, 2 },
                { BoardPieceId.Cavetroll, 2 },
            };
            var myEntranceDeckFloor2 = new Dictionary<BoardPieceId, int>
            {
                { BoardPieceId.LargeCorruption, 3 },
                { BoardPieceId.ReptileArcher, 3 },
                { BoardPieceId.SandScorpion, 4 },
                { BoardPieceId.ServantOfAlfaragh, 3 },
                { BoardPieceId.GoldSandPile, 2 },
                { BoardPieceId.SmallCorruption, 3 },
                { BoardPieceId.GeneralRonthian, 3 },
                { BoardPieceId.TheUnseen, 4 },
                { BoardPieceId.ElvenArcher, 4 },
                { BoardPieceId.ElvenHound, 0 },
                { BoardPieceId.RootHound, 4 },
                { BoardPieceId.TheUnspoken, 4 },
                { BoardPieceId.ElvenCultist, 4 },
                { BoardPieceId.Bandit, 4 },
                { BoardPieceId.GoblinMadUn, 4 },
                { BoardPieceId.RootBeast, 4 },
                { BoardPieceId.ScabRat, 4 },
                { BoardPieceId.Spider, 0 },
                { BoardPieceId.Rat, 4 },
                { BoardPieceId.RootGolem, 2 },
                { BoardPieceId.Brookmare, 2 },
                { BoardPieceId.Gorgon, 2 },
                { BoardPieceId.SilentSentinel, 2 },
                { BoardPieceId.Wyvern, 2 },
                { BoardPieceId.BigBoiMutant, 2 },
                { BoardPieceId.ReptileMutantWizard, 3 },
            };
            var myExitDeckFloor2 = new Dictionary<BoardPieceId, int>
            {
                { BoardPieceId.DruidArcher, 4 },
                { BoardPieceId.DruidHoundMaster, 4 },
                { BoardPieceId.GoblinChieftan, 4 },
                { BoardPieceId.TheUnheard, 4 },
                { BoardPieceId.Slimeling, 4 },
                { BoardPieceId.Thug, 4 },
                { BoardPieceId.ElvenMystic, 4 },
                { BoardPieceId.ElvenPriest, 4 },
                { BoardPieceId.ElvenSkirmisher, 4 },
                { BoardPieceId.GoblinFighter, 4 },
                { BoardPieceId.GoblinRanger, 0 },
                { BoardPieceId.SpiderEgg, 3 },
                { BoardPieceId.SporeFungus, 3 },
                { BoardPieceId.RatNest, 3 },
                { BoardPieceId.CultMemberElder, 4 },
                { BoardPieceId.RootMage, 4 },
                { BoardPieceId.KillerBee, 0 },
                { BoardPieceId.ChestGoblin, 2 },
                { BoardPieceId.EarthElemental, 2 },
                { BoardPieceId.Sigataur, 2 },
                { BoardPieceId.GiantSlime, 2 },
                { BoardPieceId.FireElemental, 2 },
                { BoardPieceId.ElvenMarauder, 2 },
                { BoardPieceId.IceElemental, 2 },
                { BoardPieceId.GiantSpider, 2 },
                { BoardPieceId.Mimic, 2 },
                { BoardPieceId.Cavetroll, 2 },

            };
            var myBossDeck = new Dictionary<BoardPieceId, int>
            {
                { BoardPieceId.TheUnheard, 4 },
                { BoardPieceId.Slimeling, 5 },
                { BoardPieceId.TheUnseen, 5 },
                { BoardPieceId.ElvenArcher, 4 },
                { BoardPieceId.ElvenHound, 4 },
                { BoardPieceId.RootHound, 4 },
                { BoardPieceId.TheUnspoken, 4 },
                { BoardPieceId.ElvenMystic, 4 },
                { BoardPieceId.ElvenPriest, 4 },
                { BoardPieceId.ElvenSkirmisher, 4 },
                { BoardPieceId.CultMemberElder, 4 },
                { BoardPieceId.KillerBee, 0 },
                { BoardPieceId.Spider, 0 },
                { BoardPieceId.EarthElemental, 2 },
                { BoardPieceId.Sigataur, 2 },
                { BoardPieceId.GiantSlime, 2 },
                { BoardPieceId.FireElemental, 2 },
                { BoardPieceId.ElvenMarauder, 2 },
                { BoardPieceId.IceElemental, 2 },
                { BoardPieceId.GiantSpider, 2 },
                { BoardPieceId.BigBoiMutant, 2 },
                { BoardPieceId.ReptileMutantWizard, 2 },
                { BoardPieceId.Mimic, 2 },
                { BoardPieceId.Cavetroll, 2 },
                { BoardPieceId.RootGolem, 2 },
                { BoardPieceId.Brookmare, 2 },
                { BoardPieceId.Gorgon, 2 },
                { BoardPieceId.SilentSentinel, 2 },
                { BoardPieceId.Wyvern, 2 },
            };
            var myMonsterDeckConfig = new MyMonsterDeckOverriddenRule.DeckConfig
            {
                EntranceDeckFloor1 = myEntranceDeckFloor1,
                ExitDeckFloor1 = myExitDeckFloor1,
                EntranceDeckFloor2 = myEntranceDeckFloor2,
                ExitDeckFloor2 = myExitDeckFloor2,
                BossDeck = myBossDeck,
                KeyHolderFloor1 = BoardPieceId.Mimic,
                KeyHolderFloor2 = BoardPieceId.Wyvern,
            };
            var myMonsterDeckRule = new MyMonsterDeckOverriddenRule(myMonsterDeckConfig);*/

            var warlockCards = new List<StartCardsModifiedRule.CardConfig>
            {
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Torch, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.God, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.EnergyPotion, ReplenishFrequency = 0 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.DetectEnemies, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.RevealPath, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Teleportation, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Fireball, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.MinionCharge, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.MagicMissile, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.MagicMissile, ReplenishFrequency = 1 },
            };

            var bardCards = new List<StartCardsModifiedRule.CardConfig>
            {
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Torch, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.God, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.EnergyPotion, ReplenishFrequency = 0 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.DetectEnemies, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.RevealPath, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Teleportation, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Fireball, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.CourageShanty, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.EnemyFlashbang, ReplenishFrequency = 2 },
            };
            var guardianCards = new List<StartCardsModifiedRule.CardConfig>
            {
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Torch, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.God, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.EnergyPotion, ReplenishFrequency = 0 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.DetectEnemies, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.RevealPath, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Teleportation, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.WhirlwindAttack, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Fireball, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Grab, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.ReplenishArmor, ReplenishFrequency = 1 },
            };
            var hunterCards = new List<StartCardsModifiedRule.CardConfig>
            {
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Torch, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.God, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.EnergyPotion, ReplenishFrequency = 0 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.DetectEnemies, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.RevealPath, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Teleportation, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Fireball, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Arrow, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.EnemyFireball, ReplenishFrequency = 1 },
            };
            var assassinCards = new List<StartCardsModifiedRule.CardConfig>
            {
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Torch, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.God, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.EnergyPotion, ReplenishFrequency = 0 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.DetectEnemies, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.RevealPath, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Teleportation, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Fireball, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Sneak, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.DiseasedBite, ReplenishFrequency = 2 },
            };
            var sorcererCards = new List<StartCardsModifiedRule.CardConfig>
            {
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Torch, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.God, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.EnergyPotion, ReplenishFrequency = 0 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.DetectEnemies, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.RevealPath, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Teleportation, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Fireball, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Zap, ReplenishFrequency = 1 },
                new StartCardsModifiedRule.CardConfig { Card = AbilityKey.Electricity, ReplenishFrequency = 1 },
            };
            var startingCardsRule = new StartCardsModifiedRule(new Dictionary<BoardPieceId, List<StartCardsModifiedRule.CardConfig>>
            {
                { BoardPieceId.HeroWarlock, warlockCards },
                { BoardPieceId.HeroBard, bardCards },
                { BoardPieceId.HeroGuardian, guardianCards },
                { BoardPieceId.HeroHunter, hunterCards },
                { BoardPieceId.HeroRogue, assassinCards },
                { BoardPieceId.HeroSorcerer, sorcererCards },
            });

            var piecesAdjustedRule = new PieceConfigAdjustedRule(new List<PieceConfigAdjustedRule.PieceProperty>
            {
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroWarlock, Property = "MoveRange", Value = 20 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroBard, Property = "MoveRange", Value = 20 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroGuardian, Property = "MoveRange", Value = 20 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroSorcerer, Property = "MoveRange", Value = 20 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroHunter, Property = "MoveRange", Value = 20 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroRogue, Property = "MoveRange", Value = 20 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroWarlock, Property = "StartHealth", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroBard, Property = "StartHealth", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroGuardian, Property = "StartHealth", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroRogue, Property = "StartHealth", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroHunter, Property = "StartHealth", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroSorcerer, Property = "StartHealth", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroSorcerer, Property = "AttackDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroRogue, Property = "AttackDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroWarlock, Property = "AttackDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroGuardian, Property = "AttackDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroHunter, Property = "AttackDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroBard, Property = "AttackDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Verochka, Property = "AttackDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.WarlockMinion, Property = "AttackDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroBard, Property = "CriticalHitDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroGuardian, Property = "CriticalHitDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroRogue, Property = "CriticalHitDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroHunter, Property = "CriticalHitDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroWarlock, Property = "CriticalHitDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroSorcerer, Property = "CriticalHitDamage", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Mimic, Property = "BerserkBelowHealth", Value = 0.99f },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Mimic, Property = "StartHealth", Value = 17 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Mimic, Property = "StartArmor", Value = 5 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Mimic, Property = "AttackDamage", Value = 4 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.WarlockMinion, Property = "MoveRange", Value = 20 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.WarlockMinion, Property = "ActionPoint", Value = 6 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Verochka, Property = "StartHealth", Value = 99 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Barricade, Property = "StartHealth", Value = 8 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Lure, Property = "StartHealth", Value = 12 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.ElvenQueen, Property = "StartHealth", Value = 67 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Wyvern, Property = "StartHealth", Value = 25 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroWarlock, Property = "ActionPoint", Value = 9 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroBard, Property = "ActionPoint", Value = 9 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroGuardian, Property = "ActionPoint", Value = 9 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroRogue, Property = "ActionPoint", Value = 9 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroHunter, Property = "ActionPoint", Value = 9 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.HeroSorcerer, Property = "ActionPoint", Value = 9 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Mimic, Property = "PowerIndex", Value = 3 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Wyvern, Property = "PowerIndex", Value = 3 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.SilentSentinel, Property = "PowerIndex", Value = 3 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.EarthElemental, Property = "PowerIndex", Value = 3 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Brookmare, Property = "PowerIndex", Value = 3 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.BigBoiMutant, Property = "PowerIndex", Value = 3 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.ReptileMutantWizard, Property = "PowerIndex", Value = 3 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Cavetroll, Property = "PowerIndex", Value = 3 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.ElvenMarauder, Property = "PowerIndex", Value = 3 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.GiantSlime, Property = "PowerIndex", Value = 3 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.GiantSpider, Property = "PowerIndex", Value = 3 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.Sigataur, Property = "PowerIndex", Value = 3 },
                new PieceConfigAdjustedRule.PieceProperty { Piece = BoardPieceId.RootGolem, Property = "PowerIndex", Value = 3 },
            });

            var allowedCardsRule = new CardAdditionOverriddenRule(new Dictionary<BoardPieceId, List<AbilityKey>>
            {
                {
                    BoardPieceId.HeroGuardian, new List<AbilityKey>
                    {
                        AbilityKey.EnergyPotion,
                        AbilityKey.Bone,
                        AbilityKey.WebBomb,
                        AbilityKey.Regroup,
                        AbilityKey.OneMoreThing,
                        AbilityKey.PanicPowder,
                        AbilityKey.BottleOfLye,
                        AbilityKey.Teleportation,
                        AbilityKey.StrengthPotion,
                        AbilityKey.SwiftnessPotion,
                        AbilityKey.HeavensFury,
                        AbilityKey.HealingPotion,
                        AbilityKey.ScrollElectricity,
                        AbilityKey.ScrollTsunami,
                        AbilityKey.LuckPotion,
                        AbilityKey.IceImmunePotion,
                        AbilityKey.FireImmunePotion,
                        AbilityKey.ExtraActionPotion,
                        AbilityKey.DamageResistPotion,
                        AbilityKey.WaterBottle,
                        AbilityKey.HealingWard,
                        AbilityKey.WhirlwindAttack,
                        AbilityKey.WarCry,
                        AbilityKey.PiercingThrow,
                        AbilityKey.Charge,
                        AbilityKey.HealingWard,
                        AbilityKey.WhirlwindAttack,
                        AbilityKey.WarCry,
                        AbilityKey.TheBehemoth,
                        AbilityKey.PiercingThrow,
                        AbilityKey.Charge,
                        AbilityKey.WhirlwindAttack,
                        AbilityKey.WarCry,
                        AbilityKey.TheBehemoth,
                        AbilityKey.PiercingThrow,
                        AbilityKey.Charge,
                    }
                },
                {
                    BoardPieceId.HeroBard, new List<AbilityKey>
                    {
                        AbilityKey.EnergyPotion,
                        AbilityKey.Bone,
                        AbilityKey.Barricade,
                        AbilityKey.WebBomb,
                        AbilityKey.Regroup,
                        AbilityKey.OneMoreThing,
                        AbilityKey.BottleOfLye,
                        AbilityKey.Teleportation,
                        AbilityKey.SwiftnessPotion,
                        AbilityKey.HeavensFury,
                        AbilityKey.HealingPotion,
                        AbilityKey.ScrollElectricity,
                        AbilityKey.MagicPotion,
                        AbilityKey.LuckPotion,
                        AbilityKey.IceImmunePotion,
                        AbilityKey.FireImmunePotion,
                        AbilityKey.ExtraActionPotion,
                        AbilityKey.DamageResistPotion,
                        AbilityKey.WaterBottle,
                        AbilityKey.ScrollOfCharm,
                        AbilityKey.SongOfRecovery,
                        AbilityKey.SongOfResilience,
                        AbilityKey.BlockAbilities,
                        AbilityKey.PiercingVoice,
                        AbilityKey.ShatteringVoice,
                        AbilityKey.HurricaneAnthem,
                        AbilityKey.SongOfRecovery,
                        AbilityKey.ScrollOfCharm,
                        AbilityKey.SongOfResilience,
                        AbilityKey.PiercingVoice,
                        AbilityKey.ShatteringVoice,
                        AbilityKey.HurricaneAnthem,
                        AbilityKey.ScrollOfCharm,
                        AbilityKey.SongOfRecovery,
                        AbilityKey.SongOfResilience,
                        AbilityKey.PiercingVoice,
                        AbilityKey.ShatteringVoice,
                        AbilityKey.HurricaneAnthem,
                    }
                },
                {
                    BoardPieceId.HeroHunter, new List<AbilityKey>
                    {
                        AbilityKey.Regroup,
                        AbilityKey.EnergyPotion,
                        AbilityKey.RepeatingBallista,
                        AbilityKey.PanicPowder,
                        AbilityKey.BottleOfLye,
                        AbilityKey.Teleportation,
                        AbilityKey.StrengthPotion,
                        AbilityKey.HeavensFury,
                        AbilityKey.HealingPotion,
                        AbilityKey.ScrollElectricity,
                        AbilityKey.LuckPotion,
                        AbilityKey.FireImmunePotion,
                        AbilityKey.ExtraActionPotion,
                        AbilityKey.DamageResistPotion,
                        AbilityKey.WaterBottle,
                        AbilityKey.ScrollOfCharm,
                        AbilityKey.BeastWhisperer,
                        AbilityKey.HailOfArrows,
                        AbilityKey.CallCompanion,
                        AbilityKey.PoisonedTip,
                        AbilityKey.HuntersMark,
                        AbilityKey.Lure,
                        AbilityKey.ScrollOfCharm,
                        AbilityKey.BeastWhisperer,
                        AbilityKey.HailOfArrows,
                        AbilityKey.CallCompanion,
                        AbilityKey.PoisonedTip,
                        AbilityKey.HuntersMark,
                        AbilityKey.Lure,
                        AbilityKey.BeastWhisperer,
                        AbilityKey.HailOfArrows,
                        AbilityKey.CallCompanion,
                        AbilityKey.HuntersMark,
                        AbilityKey.Lure,
                    }
                },
                {
                    BoardPieceId.HeroRogue, new List<AbilityKey>
                    {
                        AbilityKey.EnergyPotion,
                        AbilityKey.Bone,
                        AbilityKey.WebBomb,
                        AbilityKey.Regroup,
                        AbilityKey.OneMoreThing,
                        AbilityKey.PanicPowder,
                        AbilityKey.BottleOfLye,
                        AbilityKey.Teleportation,
                        AbilityKey.StrengthPotion,
                        AbilityKey.HeavensFury,
                        AbilityKey.HealingPotion,
                        AbilityKey.ScrollElectricity,
                        AbilityKey.ScrollTsunami,
                        AbilityKey.LuckPotion,
                        AbilityKey.IceImmunePotion,
                        AbilityKey.FireImmunePotion,
                        AbilityKey.ExtraActionPotion,
                        AbilityKey.DamageResistPotion,
                        AbilityKey.WaterBottle,
                        AbilityKey.Blink,
                        AbilityKey.PoisonBomb,
                        AbilityKey.CoinFlip,
                        AbilityKey.CursedDagger,
                        AbilityKey.BoobyTrap,
                        AbilityKey.FlashBomb,
                        AbilityKey.Blink,
                        AbilityKey.PoisonBomb,
                        AbilityKey.CursedDagger,
                        AbilityKey.FlashBomb,
                        AbilityKey.Blink,
                        AbilityKey.PoisonBomb,
                        AbilityKey.CoinFlip,
                        AbilityKey.CursedDagger,
                    }
                },
                {
                    BoardPieceId.HeroSorcerer, new List<AbilityKey>
                    {
                        AbilityKey.EnergyPotion,
                        AbilityKey.Bone,
                        AbilityKey.Regroup,
                        AbilityKey.PanicPowder,
                        AbilityKey.Barricade,
                        AbilityKey.BottleOfLye,
                        AbilityKey.Teleportation,
                        AbilityKey.SwiftnessPotion,
                        AbilityKey.HeavensFury,
                        AbilityKey.HealingPotion,
                        AbilityKey.ScrollElectricity,
                        AbilityKey.ScrollTsunami,
                        AbilityKey.MagicPotion,
                        AbilityKey.LuckPotion,
                        AbilityKey.IceImmunePotion,
                        AbilityKey.FireImmunePotion,
                        AbilityKey.ExtraActionPotion,
                        AbilityKey.DamageResistPotion,
                        AbilityKey.Banish,
                        AbilityKey.Fireball,
                        AbilityKey.Freeze,
                        AbilityKey.MagicShield,
                        AbilityKey.Vortex,
                        AbilityKey.Banish,
                        AbilityKey.Fireball,
                        AbilityKey.Freeze,
                        AbilityKey.MagicShield,
                        AbilityKey.MagicBarrier,
                        AbilityKey.Vortex,
                        AbilityKey.Banish,
                        AbilityKey.Fireball,
                        AbilityKey.Freeze,
                        AbilityKey.MagicBarrier,
                        AbilityKey.Vortex,
                    }
                },
                {
                    BoardPieceId.HeroWarlock, new List<AbilityKey>
                    {
                        AbilityKey.EnergyPotion,
                        AbilityKey.Bone,
                        AbilityKey.WebBomb,
                        AbilityKey.Barricade,
                        AbilityKey.RepeatingBallista,
                        AbilityKey.Regroup,
                        AbilityKey.PanicPowder,
                        AbilityKey.BottleOfLye,
                        AbilityKey.SwiftnessPotion,
                        AbilityKey.HeavensFury,
                        AbilityKey.HealingPotion,
                        AbilityKey.ScrollElectricity,
                        AbilityKey.MagicPotion,
                        AbilityKey.LuckPotion,
                        AbilityKey.IceImmunePotion,
                        AbilityKey.FireImmunePotion,
                        AbilityKey.ExtraActionPotion,
                        AbilityKey.DamageResistPotion,
                        AbilityKey.WaterBottle,
                        AbilityKey.Deflect,
                        AbilityKey.GuidingLight,
                        AbilityKey.Implode,
                        AbilityKey.MissileSwarm,
                        AbilityKey.Deflect,
                        AbilityKey.GuidingLight,
                        AbilityKey.Implode,
                        AbilityKey.MissileSwarm,
                        AbilityKey.Portal,
                        AbilityKey.Deflect,
                        AbilityKey.GuidingLight,
                        AbilityKey.MissileSwarm,
                        AbilityKey.Portal,
                    }
                },
            });

            var statusEffectRule = new StatusEffectConfigRule(new List<StatusEffectData>
            {
                new StatusEffectData
                {
                    effectStateType = EffectStateType.TorchPlayer,
                    durationTurns = 20,
                    damagePerTurn = 0,
                    stacks = false,
                    clearOnNewLevel = false,
                    tickWhen = StatusEffectsConfig.TickWhen.EndTurn,
                },
                new StatusEffectData
                {
                    effectStateType = EffectStateType.FireImmunity,
                    durationTurns = 12,
                    damagePerTurn = 0,
                    stacks = false,
                    clearOnNewLevel = false,
                    tickWhen = StatusEffectsConfig.TickWhen.StartTurn,
                },
                new StatusEffectData
                {
                    effectStateType = EffectStateType.IceImmunity,
                    durationTurns = 12,
                    damagePerTurn = 0,
                    stacks = false,
                    clearOnNewLevel = false,
                    tickWhen = StatusEffectsConfig.TickWhen.StartTurn,
                },
                new StatusEffectData
                {
                    effectStateType = EffectStateType.Invulnerable3,
                    durationTurns = 2,
                    damagePerTurn = 0,
                    stacks = false,
                    clearOnNewLevel = false,
                    tickWhen = StatusEffectsConfig.TickWhen.StartTurn,
                },
                new StatusEffectData
                {
                    effectStateType = EffectStateType.MarkOfAvalon,
                    durationTurns = 2,
                    damagePerTurn = 0,
                    stacks = false,
                    clearOnNewLevel = false,
                    tickWhen = StatusEffectsConfig.TickWhen.EndTurn,
                },
                new StatusEffectData
                {
                    effectStateType = EffectStateType.ExtraEnergy,
                    durationTurns = 3,
                    damagePerTurn = 0,
                    stacks = false,
                    clearOnNewLevel = false,
                    tickWhen = StatusEffectsConfig.TickWhen.Never,
                },
            });

            var pieceAbilityRule = new PieceAbilityListOverriddenRule(new Dictionary<BoardPieceId, List<AbilityKey>>
            {
                { BoardPieceId.EarthElemental, new List<AbilityKey> { AbilityKey.EnemyMelee, AbilityKey.EnemyKnockbackMelee, AbilityKey.EarthShatter, AbilityKey.EnemyJavelin } },
                { BoardPieceId.Mimic, new List<AbilityKey> { AbilityKey.EnemyMelee, AbilityKey.AcidSpit, AbilityKey.Grab } },
                { BoardPieceId.RootMage, new List<AbilityKey> { AbilityKey.EnemyMelee, AbilityKey.TeleportEnemy } },
                { BoardPieceId.ChestGoblin, new List<AbilityKey> { AbilityKey.EnemyMelee, AbilityKey.EnemyStealGold } },
                { BoardPieceId.KillerBee, new List<AbilityKey> { AbilityKey.EnemyMelee, AbilityKey.ThornPowder } },
                { BoardPieceId.CultMemberElder, new List<AbilityKey> { AbilityKey.EnemyMelee, AbilityKey.Weaken, AbilityKey.EnemyFireball } },
                { BoardPieceId.Wyvern, new List<AbilityKey> { AbilityKey.EnemyMelee, AbilityKey.DiseasedBite, AbilityKey.LightningBolt, AbilityKey.LeapHeavy } },
                { BoardPieceId.SilentSentinel, new List<AbilityKey> { AbilityKey.EnemyMelee, AbilityKey.LeapHeavy, AbilityKey.Grab } },
                { BoardPieceId.ElvenArcher, new List<AbilityKey> { AbilityKey.EnemyMelee, AbilityKey.EnemyArrowSnipe, AbilityKey.EnemyFrostball } },
                { BoardPieceId.ElvenCultist, new List<AbilityKey> { AbilityKey.EnemyMelee, AbilityKey.LeechMelee } },
                { BoardPieceId.TheUnseen, new List<AbilityKey> { AbilityKey.EnemyMelee, AbilityKey.Zap } },
                { BoardPieceId.ElvenQueen, new List<AbilityKey> { AbilityKey.SummonBossMinions, AbilityKey.LightningBolt, AbilityKey.Shockwave, AbilityKey.EnemyFrostball } },
                { BoardPieceId.BigBoiMutant, new List<AbilityKey> { AbilityKey.EnemyKnockbackMelee, AbilityKey.Shockwave, AbilityKey.LeapHeavy } },
                { BoardPieceId.GoblinFighter, new List<AbilityKey> { AbilityKey.EnemyMelee, AbilityKey.EnemyFlashbang } },
            });

            var pieceBehaviourListRule = new PieceBehavioursListOverriddenRule(new Dictionary<BoardPieceId, List<Behaviour>>
            {
                { BoardPieceId.EarthElemental, new List<Behaviour> { Behaviour.Patrol, Behaviour.AttackPlayer, Behaviour.EarthShatter, Behaviour.RangedAttackHighPrio } },
                { BoardPieceId.Mimic, new List<Behaviour> { Behaviour.Patrol, Behaviour.AttackPlayer, Behaviour.RangedAttackHighPrio } },
                { BoardPieceId.RootMage, new List<Behaviour> { Behaviour.Patrol, Behaviour.AttackPlayer, Behaviour.CastOnTeam } },
                { BoardPieceId.KillerBee, new List<Behaviour> { Behaviour.Patrol, Behaviour.AttackPlayer, Behaviour.RangedAttackHighPrio } },
                { BoardPieceId.ChestGoblin, new List<Behaviour> { Behaviour.Patrol, Behaviour.FollowPlayerMeleeAttacker, Behaviour.AttackAndRetreat } },
                { BoardPieceId.CultMemberElder, new List<Behaviour> { Behaviour.Patrol, Behaviour.AttackAndRetreat, Behaviour.RangedSpellCaster } },
                { BoardPieceId.SilentSentinel, new List<Behaviour> { Behaviour.Patrol, Behaviour.AttackPlayer, Behaviour.RangedSpellCaster } },
                { BoardPieceId.ElvenArcher, new List<Behaviour> { Behaviour.Patrol, Behaviour.RangedSpellCaster, Behaviour.FollowPlayerRangedAttacker } },
                { BoardPieceId.TheUnseen, new List<Behaviour> { Behaviour.Patrol, Behaviour.AttackPlayer, Behaviour.RangedSpellCaster } },
                { BoardPieceId.GoblinFighter, new List<Behaviour> { Behaviour.Patrol, Behaviour.AttackPlayer, Behaviour.RangedAttackHighPrio } },
            });

            var pieceImmunityRule = new PieceImmunityListAdjustedRule(new Dictionary<BoardPieceId, List<EffectStateType>>
            {
                { BoardPieceId.HeroSorcerer, new List<EffectStateType> { EffectStateType.Stunned } },
                { BoardPieceId.HeroHunter, new List<EffectStateType> { EffectStateType.Frozen } },
                { BoardPieceId.Verochka, new List<EffectStateType> { EffectStateType.Frozen } },
                { BoardPieceId.HeroGuardian, new List<EffectStateType> { EffectStateType.Weaken } },
                { BoardPieceId.HeroBard, new List<EffectStateType> { EffectStateType.Diseased } },
                { BoardPieceId.HeroRogue, new List<EffectStateType> { EffectStateType.Tangled, EffectStateType.Blinded } },
                { BoardPieceId.HeroWarlock, new List<EffectStateType> { EffectStateType.CorruptedRage, EffectStateType.Undefined } },
                { BoardPieceId.WarlockMinion, new List<EffectStateType> { EffectStateType.CorruptedRage, EffectStateType.Undefined } },
            });

            var applyEffectOnHitRule = new ApplyEffectOnHitAdjustedRule(new Dictionary<BoardPieceId, EffectStateType>
            {
                { BoardPieceId.HealingBeacon, EffectStateType.Diseased },
                { BoardPieceId.EyeOfAvalon, EffectStateType.Wet },
                { BoardPieceId.Lure, EffectStateType.Thorns },
                { BoardPieceId.SmiteWard, EffectStateType.AbilityBlocked },
                { BoardPieceId.Verochka, EffectStateType.MarkOfAvalon },
                { BoardPieceId.WarlockMinion, EffectStateType.ExposeEnergy },
            });

            var pieceUseWhenKilledRule = new PieceUseWhenKilledOverriddenRule(new Dictionary<BoardPieceId, List<AbilityKey>>
            {
                { BoardPieceId.ChestGoblin, new List<AbilityKey> { AbilityKey.EnemyDropStolenGoods, AbilityKey.DropChest } },
                { BoardPieceId.EarthElemental, new List<AbilityKey> { AbilityKey.Explosion, AbilityKey.DeathDropJavelin } },
            });

            var abilityActionCostRule = new AbilityActionCostAdjustedRule(new Dictionary<AbilityKey, bool>
            {
                { AbilityKey.Zap, false },
                { AbilityKey.LightningBolt, false },
                { AbilityKey.Sneak, false },
                { AbilityKey.Grab, false },
                { AbilityKey.Arrow, false },
                { AbilityKey.CourageShanty, false },
                { AbilityKey.MinionCharge, false },
                { AbilityKey.SpellPowerPotion, false },
                { AbilityKey.PVPBlink, false },
                { AbilityKey.Weaken, false },
                { AbilityKey.LeapHeavy, false },
                { AbilityKey.SpawnRandomLamp, false },
                { AbilityKey.FretsOfFire, false },
                { AbilityKey.Fireball, false },
                { AbilityKey.RevealPath, false },
                { AbilityKey.DetectEnemies, false },
                { AbilityKey.WhirlwindAttack, false },
                { AbilityKey.God, false },
                { AbilityKey.Teleportation, false },
            });

            var abilityHealOverriddenRule = new AbilityHealOverriddenRule(new Dictionary<AbilityKey, int>
            {
                { AbilityKey.HealingPotion, 5 },
                { AbilityKey.Rejuvenation, 8 },
                { AbilityKey.AltarHeal, 8 },
                { AbilityKey.WaterBottle, 2 },
                { AbilityKey.TurretHealProjectile, 2 },
            });

            var abilityDamageAllRule = new AbilityDamageAllOverriddenRule(new Dictionary<AbilityKey, List<int>>
            {
                { AbilityKey.ShatteringVoice, new List<int> { 3, 6, 3, 6 } },
                { AbilityKey.PiercingVoice, new List<int> { 2, 4, 2, 4 } },
                { AbilityKey.Arrow, new List<int> { 3, 8, 3, 8 } },
                { AbilityKey.Electricity, new List<int> { 3, 5, 1, 2 } },
                { AbilityKey.PVPBlink, new List<int> { 6, 12, 6, 12 } },
                { AbilityKey.FretsOfFire, new List<int> { 4, 8, 4, 8 } },
                { AbilityKey.Fireball, new List<int> { 99, 99, 99, 99 } },
                { AbilityKey.WhirlwindAttack, new List<int> { 99, 99, 99, 99 } },
            });

            var turnOrderRule = new TurnOrderOverriddenRule(new TurnOrderOverriddenRule.Scores
            { Bard = 15, Warlock = 14, Guardian = 13, Sorcerer = 12, Hunter = 11, Assassin = 10, Downed = -6, Javelin = 12 });

            var freeAbilityOnCritRule = new FreeAbilityOnCritRule(new Dictionary<BoardPieceId, AbilityKey>
            {
                { BoardPieceId.HeroHunter, AbilityKey.Bone },
                { BoardPieceId.HeroSorcerer, AbilityKey.WaterBottle },
                { BoardPieceId.HeroWarlock, AbilityKey.SpellPowerPotion },
                { BoardPieceId.HeroBard, AbilityKey.PanicPowder },
            });

            var freeHealOnHitRule = new FreeHealOnHitRule(new List<BoardPieceId> { BoardPieceId.HeroRogue, BoardPieceId.HeroWarlock });
            var freeHealOnCritRule = new FreeHealOnCritRule(new List<BoardPieceId> { BoardPieceId.HeroRogue, BoardPieceId.HeroWarlock, BoardPieceId.HeroBard });
            var freeActionPointsOnCritRule = new FreeActionPointsOnCritRule(new List<BoardPieceId> { BoardPieceId.HeroGuardian, BoardPieceId.HeroRogue });
            var freeReplenishablesOnCritRule = new FreeReplenishablesOnCritRule(new List<BoardPieceId> { BoardPieceId.HeroBard, BoardPieceId.HeroRogue, BoardPieceId.HeroGuardian, BoardPieceId.HeroSorcerer, BoardPieceId.HeroHunter, BoardPieceId.HeroWarlock });
            var backstabConfigRule = new BackstabConfigOverriddenRule(new List<BoardPieceId> { BoardPieceId.HeroBard, BoardPieceId.HeroRogue });
            var abilityBackstabRule = new AbilityBackstabAdjustedRule(new Dictionary<AbilityKey, bool>
            {
                { AbilityKey.PiercingVoice, true },
                { AbilityKey.ShatteringVoice, true },
                { AbilityKey.DiseasedBite, true },
            });

            var abilityStealthDamageRule = new AbilityStealthDamageOverriddenRule(new Dictionary<AbilityKey, int>
            {
                { AbilityKey.DiseasedBite, 2 },
                { AbilityKey.PoisonBomb, 1 },
                { AbilityKey.PlayerMelee, 2 },
                { AbilityKey.FretsOfFire, 1 },
            });

            var enemyCooldownRule = new EnemyCooldownOverriddenRule(new Dictionary<AbilityKey, int>
            {
                { AbilityKey.Zap, 2 },
                { AbilityKey.LightningBolt, 4 },
                { AbilityKey.LeapHeavy, 2 },
                { AbilityKey.EnemyFrostball, 4 },
                { AbilityKey.Shockwave, 4 },
                { AbilityKey.EnemyFireball, 2 },
                { AbilityKey.EnemyFlashbang, 2 },
                { AbilityKey.Petrify, 2 },
            });

            var aoeAdjustedRule = new AbilityAoeAdjustedRule(new Dictionary<AbilityKey, int>
            {
                { AbilityKey.SongOfRecovery, 2 },
                { AbilityKey.SongOfResilience, 2 },
                { AbilityKey.FlashBomb, 1 },
                { AbilityKey.WarCry, 1 },
                { AbilityKey.WhirlwindAttack, 1 },
                { AbilityKey.Deflect, 2 },
                { AbilityKey.BlindingLight, 1 },
                { AbilityKey.BlockAbilities, 1 },
                { AbilityKey.Fireball, 2 },
            });

            // var pieceExtraImmunities = new PieceExtraImmunitiesRule(true);
            var partyElectricity = new PartyElectricityDamageOverriddenRule(true);
            var petsFocusHuntersMarkRule = new PetsFocusHunterMarkRule(true);
            var enemyRespawnDisabledRule = new EnemyRespawnDisabledRule(true);
            var cardEnergyFromAttackRule = new CardEnergyFromAttackMultipliedRule(0.75f);
            var cardEnergyFromRecyclingRule = new CardEnergyFromRecyclingMultipliedRule(0.66f);
            var enemyHealthScaledRule = new EnemyHealthScaledRule(1.0f);
            var enemyAttackScaledRule = new EnemyAttackScaledRule(1.0f);
            var levelSequenceOverriddenRule = new LevelSequenceOverriddenRule(new List<string>
            {
                "ElvenFloor17",
                "SewersFloor08",
                "SewersFloor11",
                "ForestFloor02",
                "ForestFloor01",
                "ElvenFloor14",
                "fixhydra",
            });

            var levelPropertiesRule = new LevelPropertiesModifiedRule(new Dictionary<string, int>
            {
                { "BigGoldPileChance", 30 },
                { "FloorOneHealingFountains", 1 },
                { "FloorOnePotionStand", 0 },
                { "FloorOneMerchant", 0 },
                { "FloorOneLootChests", 2 },
                { "FloorOneGoldMaxAmount", 450 },
                { "FloorTwoHealingFountains", 1 },
                { "FloorTwoPotionStand", 1 },
                { "FloorTwoMerchant", 1 },
                { "FloorTwoLootChests", 3 },
                { "FloorTwoGoldMaxAmount", 550 },
                { "FloorThreeHealingFountains", 1 },
                { "FloorThreePotionStand", 0 },
                { "FloorThreeMerchant", 0 },
                { "FloorThreeLootChests", 1 },
            });

            return Ruleset.NewInstance(
                name,
                description,
                piecesAdjustedRule,
                spawnCategoriesRule,
                // myMonsterDeckRule,
                startingCardsRule,
                allowedCardsRule,
                statusEffectRule,
                pieceAbilityRule,
                pieceBehaviourListRule,
                pieceImmunityRule,
                applyEffectOnHitRule,
                pieceUseWhenKilledRule,
                abilityActionCostRule,
                abilityHealOverriddenRule,
                backstabConfigRule,
                turnOrderRule,
                freeHealOnHitRule,
                freeHealOnCritRule,
                freeReplenishablesOnCritRule,
                freeActionPointsOnCritRule,
                freeAbilityOnCritRule,
                abilityBackstabRule,
                abilityStealthDamageRule,
                enemyCooldownRule,
                aoeAdjustedRule,
                abilityDamageAllRule,
                partyElectricity,
                petsFocusHuntersMarkRule,
                enemyRespawnDisabledRule,
                cardEnergyFromAttackRule,
                cardEnergyFromRecyclingRule,
                enemyHealthScaledRule,
                enemyAttackScaledRule,
                levelSequenceOverriddenRule,
                levelPropertiesRule);
        }
    }
}

