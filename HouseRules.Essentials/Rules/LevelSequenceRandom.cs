namespace HouseRules.Essentials.Rules
{
    using System.Collections.Generic;
    using System.Linq;
    using Boardgame;
    using Boardgame.SerializableEvents;
    using Boardgame.SerializableEvents.CustomEventHandlers;
    using HarmonyLib;
    using HouseRules.Core.Types;
    using UnityEngine;

    public sealed class LevelSequenceRandomRule : Rule, IConfigWritable<List<string>>, IPatchable, IMultiplayerSafe, IDisableOnReconnect
    {
        public override string Description => "The adventure's map order is randomly generated from all locations";

        private static bool isRandomMaps;
        private static bool isFastForward;
        private static bool isSkipLevel1;
        private static List<string> _globalAdjustments;
        private static List<string> _randomMaps = new List<string> { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };

        private static bool _isActivated;
        private readonly List<string> _adjustments;

        private readonly List<string> elvenFloors = Enumerable.Range(1, 17).Select(i => $"ElvenFloor{i:00}").ToList();
        private readonly List<string> forestFloors = Enumerable.Range(1, 9).Select(i => $"ForestFloor{i:00}").ToList();
        private readonly List<string> sewersFloors = Enumerable.Range(1, 12).Select(i => $"SewersFloor{i:00}").ToList();
        private readonly List<string> desertFloors = Enumerable.Range(1, 10).Select(i => $"DesertFloor{i:00}").ToList();
        private readonly List<string> townsFloors = Enumerable.Range(1, 8).Select(i => $"TownsFloor{i:00}").ToList();

        private readonly List<string> shopFloors = new List<string>
        {
            "ShopFloor02",
            "ForestShopFloor",
            "SewersShopFloor",
            "DesertShopFloor",
            "TownsShopFloor"
        };

        public LevelSequenceOverriddenRule(List<string> adjustments)
        {
            _adjustments = adjustments;
        }

        public List<string> GetConfigObject() => _adjustments;

        protected override void OnActivate(GameContext gameContext)
        {
            _globalAdjustments = _adjustments;
            _isActivated = true;
        }

        protected override void OnPreGameCreated(GameContext gameContext)
        {
            ReplaceExistingProperties(_globalAdjustments, gameContext);
        }

        private static void Patch(Harmony harmony)
        {
            harmony.Patch(
                original: AccessTools.Method(typeof(LevelSequenceConfiguration), "GetSequenceDefinition"),
                prefix: new HarmonyMethod(typeof(LevelSequenceOverriddenRule), nameof(LevelSequenceConfiguration_GetSequenceDefinition_Prefix))
            );

            harmony.Patch(
                original: AccessTools.Method(typeof(PlayAgainEventHandler), "AfterResponse"),
                prefix: new HarmonyMethod(typeof(LevelSequenceOverriddenRule), nameof(PlayAgainEventHandler_AfterResponse_Prefix))
            );
        }

        private static bool LevelSequenceConfiguration_GetSequenceDefinition_Prefix(ref SequenceDefinition __result, int index, LevelSequence.GameType gameType)
        {
            if (!_isActivated)
            {
                return true;
            }

            var gameContext = Traverse.Create(typeof(GameHub)).Field<GameContext>("gameContext").Value;
            var sequenceDefinitions = gameContext.levelSequenceConfiguration.sequenceDefinitions.GetSequenceFromId(gameType, out _);

            if (index >= 0 && index < sequenceDefinitions.Length)
            {
                return true;
            }

            __result = gameContext.levelLoaderAndInitializer.GetLevelSequence().CurrentLevelIsLastLevel
                ? sequenceDefinitions[sequenceDefinitions.Length - 1]
                : sequenceDefinitions[sequenceDefinitions.Length - 3];

            return false;
        }

        private static bool PlayAgainEventHandler_AfterResponse_Prefix(PlayAgainEventHandler __instance, SerializableEventQueue eventQueue)
        {
            if (!_isActivated)
            {
                return true;
            }

            var gameContext = Traverse.Create(typeof(GameHub)).Field<GameContext>("gameContext").Value;
            var newGameType = Traverse.Create(__instance).Field<PostGameControllerBase>("postGameController").Value.gameType;

            var gsmLevelSequence = gameContext.levelSequenceConfiguration.GetNewLevelSequence(-1, newGameType, LevelSequence.ControlType.OneHero);
            var originalSequence = Traverse.Create(gsmLevelSequence).Field<string[]>("levels").Value;

            if (isRandomMaps)
            {
                UpdateRandomMapsFinalLevel(newGameType);
                Traverse.Create(gsmLevelSequence).Field<string[]>("levels").Value = _randomMaps.Prepend(originalSequence[0]).ToArray();
            }
            else
            {
                UpdateGlobalAdjustmentsFinalLevel(newGameType);
                Traverse.Create(gsmLevelSequence).Field<string[]>("levels").Value = _globalAdjustments.Prepend(originalSequence[0]).ToArray();
            }

            var gameState = gameContext.gameStateMachine.GetCurrentGameState();
            eventQueue.SendEventRequest(new SerializableEventStartNewGame(gsmLevelSequence, gameState));
            return false;
        }

        private static void UpdateRandomMapsFinalLevel(LevelSequence.GameType gameType)
        {
            if (gameType == LevelSequence.GameType.Desert)
            {
                _randomMaps[4] = "DesertBossFloor01";
            }
            else if (_randomMaps[4] == "DesertBossFloor01")
            {
                _randomMaps[4] = "DesertFloor10";
            }
            else if (gameType == LevelSequence.GameType.Town)
            {
                _randomMaps[4] = "TownsBossFloor01";
            }
            else if (_randomMaps[4] == "TownsBossFloor01")
            {
                _randomMaps[4] = "TownsFloor02";
            }
        }

        private static void UpdateGlobalAdjustmentsFinalLevel(LevelSequence.GameType gameType)
        {
            if (gameType == LevelSequence.GameType.Desert)
            {
                _globalAdjustments[4] = "DesertBossFloor01";
            }
            else if (gameType == LevelSequence.GameType.Town)
            {
                _globalAdjustments[4] = "TownsBossFloor01";
            }
        }

        private void ReplaceExistingProperties(List<string> replacements, GameContext gameContext)
        {
            var gsmLevelSequence = Traverse.Create(gameContext.gameStateMachine).Field<LevelSequence>("levelSequence").Value;
            var originalSequence = Traverse.Create(gsmLevelSequence).Field<string[]>("levels").Value;

            if (replacements.Count == 5 && replacements[1].Contains("Shop") && replacements[3].Contains("Shop"))
            {
                isRandomMaps = false;
                UpdateGlobalAdjustmentsFinalLevel(gsmLevelSequence.gameType);

                HouseRulesEssentialsBase.LogWarning("User configured specific level sequence loaded");
                HouseRulesEssentialsBase.LogWarning($"Map1: {replacements[0]} Shop1: {replacements[1]} Map2: {replacements[2]} Shop2: {replacements[3]} Map3: {replacements[4]}");
                Traverse.Create(gsmLevelSequence).Field<string[]>("levels").Value = replacements.Prepend(originalSequence[0]).ToArray();
            }
            else
            {
                isRandomMaps = true;
                GenerateRandomMapSequence(gsmLevelSequence.gameType);
                Traverse.Create(gsmLevelSequence).Field<string[]>("levels").Value = _randomMaps.Prepend(originalSequence[0]).ToArray();
            }
        }

        private void GenerateRandomMapSequence(LevelSequence.GameType gameType)
        {
            List<string> levels = new List<string>();

            levels.Add(GetRandomLevel());
            levels.Add(shopFloors[Random.Range(0, shopFloors.Count)]);
            levels.Add(GetRandomLevel());
            levels.Add(shopFloors[Random.Range(0, shopFloors.Count)]);
            levels.Add(GetRandomLevel());

            if (gameType == LevelSequence.GameType.Desert)
            {
                levels[4] = "DesertBossFloor01";
            }
            else if (gameType == LevelSequence.GameType.Town)
            {
                levels[4] = "TownsBossFloor01";
            }

            _randomMaps = levels;
        }

        private string GetRandomLevel()
        {
            var allLevels = new List<string>();
            allLevels.AddRange(elvenFloors);
            allLevels.AddRange(forestFloors);
            allLevels.AddRange(sewersFloors);
            allLevels.AddRange(desertFloors);
            allLevels.AddRange(townsFloors);

            return allLevels[Random.Range(0, allLevels.Count)];
        }
    }
}