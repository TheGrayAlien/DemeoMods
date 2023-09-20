﻿namespace HouseRules.Core
{
    using System;
    using Boardgame;
    using Boardgame.BoardEntities;
    using Boardgame.Data;
    using Boardgame.SerializableEvents;
    using DataKeys;
    using HarmonyLib;

    [Flags]
    public enum SyncableTrigger
    {
        /// <summary>Nothing that needs special syncing is modified.</summary>
        None = 0,

        /// <summary>New pieces are changed or have their attributes modified.</summary>
        NewPieceModified = 1,

        /// <summary>Status effect immunities are modified.</summary>
        StatusEffectImmunityModified = 2,

        /// <summary>Status effect data (e.g., values, duration, etc.) are modified.</summary>
        StatusEffectDataModified = 4,
    }

    internal static class BoardSyncer
    {
        private static GameContext _gameContext;
        private static bool _isSyncScheduled;
        private static bool _isMove;
        private static bool _isNewPlayer;

        /// <summary>
        /// Schedules a sync to be triggered at the next available opportunity.
        /// </summary>
        internal static void ScheduleSync()
        {
            if (!HR.IsRulesetActive)
            {
                throw new InvalidOperationException("Can not schedule sync without an active ruleset.");
            }

            _isSyncScheduled = true;
        }

        internal static void Patch(Harmony harmony)
        {
            harmony.Patch(
                original: AccessTools.Method(typeof(GameStartup), "InitializeGame"),
                postfix: new HarmonyMethod(typeof(BoardSyncer), nameof(GameStartup_InitializeGame_Postfix)));

            harmony.Patch(
                original: AccessTools.Method(typeof(SerializableEventQueue), "SendResponseEvent"),
                postfix: new HarmonyMethod(
                    typeof(BoardSyncer),
                    nameof(SerializableEventQueue_SendResponseEvent_Postfix)));

            harmony.Patch(
                original: AccessTools.Method(typeof(Piece), "IsImmuneToStatusEffect"),
                postfix: new HarmonyMethod(
                    typeof(BoardSyncer),
                    nameof(Piece_IsImmuneToStatusEffect_Postfix)));

            harmony.Patch(
                original: AccessTools.Method(typeof(EffectSink), "AddStatusEffect"),
                postfix: new HarmonyMethod(
                    typeof(BoardSyncer),
                    nameof(EffectSink_AddStatusEffect_Postfix)));
        }

        private static void GameStartup_InitializeGame_Postfix(GameStartup __instance)
        {
            var gameContext = Traverse.Create(__instance).Field<GameContext>("gameContext").Value;
            _gameContext = gameContext;
        }

        private static void SerializableEventQueue_SendResponseEvent_Postfix(SerializableEvent serializableEvent)
        {
            if (!HR.IsRulesetActive)
            {
                return;
            }

            var isNewPieceCheckRequired = (HR.SelectedRuleset.ModifiedSyncables & SyncableTrigger.NewPieceModified) > 0;
            if (!_isSyncScheduled && isNewPieceCheckRequired && CanRepresentNewSpawn(serializableEvent))
            {
                _isSyncScheduled = true;
            }

            if (_isSyncScheduled && IsSyncOpportunity(serializableEvent))
            {
                SyncBoard();
            }
        }

        private static void Piece_IsImmuneToStatusEffect_Postfix()
        {
            var isEffectImmunityCheckRequired = (HR.SelectedRuleset.ModifiedSyncables & SyncableTrigger.StatusEffectImmunityModified) > 0;
            if (isEffectImmunityCheckRequired)
            {
                // CoreMod.Logger.Msg("(StateChange) Immunity");
                _isSyncScheduled = true;
            }
        }

        private static void EffectSink_AddStatusEffect_Postfix()
        {
            var isEffectDataCheckRequired = (HR.SelectedRuleset.ModifiedSyncables & SyncableTrigger.StatusEffectDataModified) > 0;
            if (isEffectDataCheckRequired)
            {
                // CoreMod.Logger.Msg("(StateChange) Effect");
                _isSyncScheduled = true;
            }
        }

        private static bool CanRepresentNewSpawn(SerializableEvent serializableEvent)
        {
            // string whatUp = serializableEvent.ToString();
            switch (serializableEvent.type)
            {
                case SerializableEvent.Type.NewPlayerJoin:
                    // CoreMod.Logger.Msg($"---NewPlayer--- {whatUp}");
                    _isNewPlayer = true;
                    return false;
                case SerializableEvent.Type.UpdateGameHub:
                    if (_isNewPlayer)
                    {
                        // CoreMod.Logger.Msg($"***NewPlayer*** UpdateFog -> {whatUp}");
                        _isNewPlayer = false;
                        _gameContext.serializableEventQueue.SendResponseEvent(new SerializableEventUpdateFog());
                        return false;
                    }

                    // CoreMod.Logger.Msg($"------ {whatUp}");
                    return false;
                case SerializableEvent.Type.OnMoved:
                    if (!_isMove)
                    {
                        var pieceId = Traverse.Create(serializableEvent).Field<int>("pieceId").Value;
                        Piece thisPiece = _gameContext.pieceAndTurnController.GetPiece(pieceId);
                        if (thisPiece.IsPlayer())
                        {
                            // CoreMod.Logger.Msg($"---OnMoved--- {thisPiece.GetPieceConfig().PieceNameLocalizationKey} {whatUp}");
                            _isMove = true;
                            return false;
                        }
                    }

                    // CoreMod.Logger.Msg($"------ {whatUp}");
                    return false;
                case SerializableEvent.Type.Move:
                case SerializableEvent.Type.Interact:
                case SerializableEvent.Type.NPCStartInteraction:
                    if (_gameContext.pieceAndTurnController.IsPlayersTurn())
                    {
                        // CoreMod.Logger.Msg($"---PlayerMove--- {whatUp}");
                        _isMove = true;
                        return false;
                    }

                    // CoreMod.Logger.Msg($"------ {whatUp}");
                    return false;
                case SerializableEvent.Type.SpawnPiece:
                case SerializableEvent.Type.SetBoardPieceID:
                case SerializableEvent.Type.SlimeFusion:
                case SerializableEvent.Type.UpdateFogAndSpawn:
                    // CoreMod.Logger.Msg($"<<<>>> {whatUp}");
                    return true;
                case SerializableEvent.Type.EndAction:
                case SerializableEvent.Type.EndTurn:
                    if (_isMove)
                    {
                        // CoreMod.Logger.Msg($"***EndAction/EndTurn*** UpdateFog -> {whatUp}");
                        _isMove = false;
                        _gameContext.serializableEventQueue.SendResponseEvent(new SerializableEventUpdateFog());
                        return false;
                    }

                    // CoreMod.Logger.Msg($"------ {whatUp}");
                    return false;
                case SerializableEvent.Type.EndRound:
                    // CoreMod.Logger.Msg($"<<<EndRound>>> {whatUp}");
                    return true;
                case SerializableEvent.Type.OnAbilityUsed:
                    return CanRepresentNewSpawn((SerializableEventOnAbilityUsed)serializableEvent);
                case SerializableEvent.Type.PieceDied:
                    return CanRepresentNewSpawn((SerializableEventPieceDied)serializableEvent);
                default:
                    // CoreMod.Logger.Msg($"---Event--- {whatUp}");
                    return false;
            }
        }

        private static bool CanRepresentNewSpawn(SerializableEventOnAbilityUsed onAbilityUsedEvent)
        {
            // string whatUp = onAbilityUsedEvent.ToString();
            var abilityKey = Traverse.Create(onAbilityUsedEvent).Field<AbilityKey>("abilityKey").Value;
            switch (abilityKey)
            {
                case AbilityKey.Grab:
                    if (!_gameContext.pieceAndTurnController.IsPlayersTurn())
                    {
                        var targetTile = Traverse.Create(onAbilityUsedEvent).Field<IntPoint2D>("targetTile").Value;
                        Piece wasGrabbed = _gameContext.pieceAndTurnController.FindPieceWithPosition(targetTile);
                        if (wasGrabbed.IsPlayer())
                        {
                            // CoreMod.Logger.Msg($"---Grab--- {wasGrabbed.GetPieceConfig().PieceNameLocalizationKey} {whatUp}");
                            _isMove = true;
                            return false;
                        }

                        // CoreMod.Logger.Msg($"------ {whatUp}");
                        return false;
                    }

                    // CoreMod.Logger.Msg($"------ {whatUp}");
                    return false;
                case AbilityKey.RevealPath:
                case AbilityKey.DetectEnemies:
                case AbilityKey.BeastWhisperer:
                case AbilityKey.HurricaneAnthem:
                case AbilityKey.Lure:
                case AbilityKey.BoobyTrap:
                case AbilityKey.RepeatingBallista:
                case AbilityKey.TheBehemoth:
                case AbilityKey.HealingWard:
                case AbilityKey.RaiseRoots:
                case AbilityKey.CallCompanion:
                case AbilityKey.DigRatsNest:
                case AbilityKey.Barricade:
                case AbilityKey.MagicBarrier:
                    // CoreMod.Logger.Msg($"<<<Spawn>>> {whatUp}");
                    return true;
            }

            var abilityName = abilityKey.ToString();
            var isSpawnAbility = abilityName.Contains("Spawn");
            var isLampAbility = abilityName.Contains("Lamp");
            var isSummonAbility = abilityName.Contains("Summon");

            if (isSpawnAbility || isLampAbility || isSummonAbility)
            {
                // CoreMod.Logger.Msg("<<<Ability>>> Summon Spawn/Creature/Lamp");
                return true;
            }

            return false;
        }

        private static bool CanRepresentNewSpawn(SerializableEventPieceDied pieceDiedEvent)
        {
            foreach (var pieceId in pieceDiedEvent.deadPieces)
            {
                if (!_gameContext.pieceAndTurnController.TryGetPiece(pieceId, out var piece))
                {
                    continue;
                }

                if (piece.boardPieceId == BoardPieceId.SpiderEgg || piece.boardPieceId.ToString().Contains("SandPile") || piece.boardPieceId == BoardPieceId.ScabRat || piece.boardPieceId.ToString().Contains("Giant"))
                {
                    // CoreMod.Logger.Msg("<<<Piece Died>>>");
                    return true;
                }
            }

            return false;
        }

        private static bool IsSyncOpportunity(SerializableEvent serializableEvent)
        {
            if (_gameContext.pieceAndTurnController.GetCurrentIndexFromTurnQueue() >= 0 && !_gameContext.pieceAndTurnController.IsPlayersTurn())
            {
                return serializableEvent.type == SerializableEvent.Type.EndTurn;
            }

            return true;
        }

        private static void SyncBoard()
        {
            // CoreMod.Logger.Msg("<<< Recovery >>>");
            _isMove = false;
            _isNewPlayer = false;
            _isSyncScheduled = false;
            _gameContext.serializableEventQueue.SendResponseEvent(new SerializableEventUpdateFog());
            _gameContext.serializableEventQueue.SendResponseEvent(SerializableEvent.CreateRecovery());
        }
    }
}