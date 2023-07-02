﻿namespace HouseRules.Essentials.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Boardgame;
    using Boardgame.BoardEntities;
    using Boardgame.Data;
    using Boardgame.SerializableEvents;
    using DataKeys;
    using HarmonyLib;
    using HouseRules.Types;

    public sealed class PotionAdditionOverriddenRule : Rule, IConfigWritable<Dictionary<BoardPieceId, List<AbilityKey>>>,
        IPatchable, IMultiplayerSafe
    {
        public override string Description => "Potion Racks loot is adjusted";

        private static Dictionary<BoardPieceId, List<AbilityKey>> _globalpotionCards;
        private static bool _isActivated;
        private static int _numPlayers;
        private static bool _isPotionStand;
        private static int _numAlags;
        private readonly Dictionary<BoardPieceId, List<AbilityKey>> _potionCards;

        public PotionAdditionOverriddenRule(Dictionary<BoardPieceId, List<AbilityKey>> potionCards)
        {
            _potionCards = potionCards;
        }

        public Dictionary<BoardPieceId, List<AbilityKey>> GetConfigObject() => _potionCards;

        protected override void OnActivate(GameContext gameContext)
        {
            _globalpotionCards = _potionCards;
            _isActivated = true;
        }

        protected override void OnDeactivate(GameContext gameContext)
        {
            _numAlags = 0;
            _isActivated = false;
        }

        private static void Patch(Harmony harmony)
        {
            harmony.Patch(
                original: AccessTools.Method(typeof(Interactable), "OnInteraction", new[] { typeof(int), typeof(IntPoint2D), typeof(GameContext), typeof(int) }),
                prefix: new HarmonyMethod(
                    typeof(PotionAdditionOverriddenRule),
                    nameof(Interactable_OnInteraction_Prefix)));

            harmony.Patch(
                original: AccessTools.Method(typeof(SerializableEventQueue), "RespondToRequest"),
                prefix: new HarmonyMethod(
                    typeof(PotionAdditionOverriddenRule),
                    nameof(SerializableEventQueue_RespondToRequest_Prefix)));
        }

        public static class RandomProvider
        {
            private static int seed = Environment.TickCount;

            private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(
                () => new Random(Interlocked.Increment(ref seed)));

            public static Random GetThreadRandom()
            {
                return randomWrapper.Value;
            }
        }

        private static void Interactable_OnInteraction_Prefix(
            GameContext gameContext,
            IntPoint2D targetTile)
        {
            if (!_isActivated)
            {
                return;
            }

            if (!gameContext.pieceAndTurnController.GetInteractableAtPosition(targetTile))
            {
                return;
            }

            var interactable = gameContext.pieceAndTurnController.GetInteractableAtPosition(targetTile);
            if (interactable.type == Interactable.Type.PotionStand)
            {
                _numPlayers = gameContext.pieceAndTurnController.GetNumberOfPlayerPieces();
                _isPotionStand = true;
            }
        }

        private static void SerializableEventQueue_RespondToRequest_Prefix(
            SerializableEventQueue __instance,
            ref SerializableEvent request)
        {
            if (!_isActivated)
            {
                return;
            }

            if (!_isPotionStand)
            {
                return;
            }

            if (_numPlayers > 1)
            {
                _numPlayers--;
            }
            else
            {
                _isPotionStand = false;
            }

            if (request.type != SerializableEvent.Type.AddCardToPiece)
            {
                return;
            }

            var addCardToPieceEvent = (SerializableEventAddCardToPiece)request;
            var gameContext = Traverse.Create(__instance).Property<GameContext>("gameContext").Value;
            var pieceId = Traverse.Create(addCardToPieceEvent).Field<int>("pieceId").Value;
            var cardSource = Traverse.Create(addCardToPieceEvent).Field<int>("cardSource").Value;

            if (cardSource != (int)MotherTracker.Context.Energy && cardSource != (int)MotherTracker.Context.Chest)
            {
                return;
            }

            if (!gameContext.pieceAndTurnController.TryGetPiece(pieceId, out var piece))
            {
                return;
            }

            if (!piece.IsPlayer())
            {
                return;
            }

            if (!_globalpotionCards.TryGetValue(piece.boardPieceId, out var replacementAbilityKeys))
            {
                return;
            }

            int rand;
            if (piece.GetStat(Stats.Type.InnateCounterDamageExtraDamage) == 69 || HR.SelectedRuleset.Name.Contains("Demeo Revolutions"))
            {
                int randNum = RandomProvider.GetThreadRandom().Next(101);
                if (randNum > 90)
                {
                    if (_numAlags < 2)
                    {
                        // Rejuv and Damage Resist Potions
                        _numAlags++;
                        rand = RandomProvider.GetThreadRandom().Next(0, 2);
                    }
                    else
                    {
                        rand = RandomProvider.GetThreadRandom().Next(2, replacementAbilityKeys.Count);
                    }
                }
                else
                {
                    rand = RandomProvider.GetThreadRandom().Next(2, replacementAbilityKeys.Count);
                }
            }
            else
            {
                rand = RandomProvider.GetThreadRandom().Next(2, replacementAbilityKeys.Count);
            }

            var replacementAbilityKey2 = replacementAbilityKeys[rand];
            Traverse.Create(addCardToPieceEvent).Field<AbilityKey>("card").Value = replacementAbilityKey2;

            return;
        }
    }
}
