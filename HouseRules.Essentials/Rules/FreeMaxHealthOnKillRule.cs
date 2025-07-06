namespace HouseRules.Essentials.Rules
{
    using System.Collections.Generic;
    using Boardgame;
    using Boardgame.BoardEntities;
    using Boardgame.BoardEntities.AI;
    using Boardgame.TurnOrder;
    using DataKeys;
    using HarmonyLib;
    using HouseRules.Core.Types;

    public sealed class FreeMaxHealthOnKillRule : Rule, IConfigWritable<Dictionary<BoardPieceId, int>>,
        IPatchable, IMultiplayerSafe, IDisableOnReconnect
    {
        public override string Description => "Players heals and gain max health per kill";

        private readonly Dictionary<BoardPieceId, int> _adjustments;
        private static Dictionary<BoardPieceId, int> _globalAdjustments;
        private static bool _isActivated;
        private static List<Piece> _playerPieces;
        private static Piece tempPiece;

        public FreeMaxHealthOnKillRule(Dictionary<BoardPieceId, int> adjustments)
        {
            _adjustments = adjustments;
        }

        public Dictionary<BoardPieceId, int> GetConfigObject() => _adjustments;

        protected override void OnActivate(Context context)
        {
            _globalAdjustments = _adjustments;
            _isActivated = true;
        }

        protected override void OnDeactivate(Context context)
        {
            _isActivated = false;
        }

        private static void Patch(Harmony harmony)
        {
            harmony.Patch(
                original: AccessTools.Method(typeof(MotherTracker), "TrackUnitDefeated"),
                prefix: new HarmonyMethod(
                    typeof(FreeMaxHealthOnKillRule),
                    nameof(MotherTracker_TrackUnitDefeated_Prefix)));

            harmony.Patch(
                original: AccessTools.Constructor(typeof(RearrangePlayerTurnOrder), new[] { typeof(TurnQueue) }),
                postfix: new HarmonyMethod(
                    typeof(FreeMaxHealthOnKillRule),
                    nameof(RearrangePlayerTurnOrder_Constructor_Postfix)));
        }

        private static void RearrangePlayerTurnOrder_Constructor_Postfix(
            RearrangePlayerTurnOrder __instance,
            TurnQueue turnQueue)
        {
            if (!_isActivated)
            {
                return;
            }

            _playerPieces = turnQueue.GetPlayerPieces();
        }

        private static void MotherTracker_TrackUnitDefeated_Prefix(Piece defeatedUnit, Piece attackerUnit)
        {
            if (!_isActivated)
            {
                return;
            }

            if (tempPiece != null)
            {
                if (attackerUnit.HasPieceType(PieceType.Prop))
                {
                    attackerUnit = tempPiece;
                }
                else
                {
                    tempPiece = null;
                }
            }

            if (defeatedUnit.HasPieceType(PieceType.Prop))
            {
                tempPiece = attackerUnit;
                return;
            }

            if (!defeatedUnit.IsCreature())
            {
                return;
            }

            if (!attackerUnit.IsPlayer())
            {
                Piece piece2;
                PieceAI pieceAI = attackerUnit.pieceAI;
                var gameContext = Traverse.Create(typeof(GameHub)).Field<GameContext>("gameContext").Value;
                if (attackerUnit.boardPieceId == BoardPieceId.WarlockMinion && attackerUnit.GetHealth() > 0)
                {
                    if (pieceAI == null)
                    {
                        return;
                    }
                    else if (pieceAI.memory.TryGetAssociatedPiece(gameContext.pieceAndTurnController, out piece2))
                    {
                        attackerUnit = piece2;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (attackerUnit.boardPieceId == BoardPieceId.SellswordArbalestierActive)
                {
                    if (pieceAI == null)
                    {
                        return;
                    }
                    else if (pieceAI.memory.TryGetAssociatedPiece(gameContext.pieceAndTurnController, out piece2))
                    {
                        attackerUnit = piece2;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (attackerUnit.boardPieceId == BoardPieceId.Verochka)
                {
                    foreach (var piece in _playerPieces)
                    {
                        if (piece.boardPieceId == BoardPieceId.HeroHunter)
                        {
                            attackerUnit = piece;
                        }
                    }
                }
                else if (attackerUnit.boardPieceId == BoardPieceId.Tornado)
                {
                    foreach (var piece in _playerPieces)
                    {
                        if (piece.boardPieceId == BoardPieceId.HeroBard)
                        {
                            attackerUnit = piece;
                        }
                    }
                }
                else if (attackerUnit.boardPieceId == BoardPieceId.GrapplingTotem)
                {
                    foreach (var piece in _playerPieces)
                    {
                        if (piece.boardPieceId == BoardPieceId.HeroBarbarian)
                        {
                            attackerUnit = piece;
                        }
                    }
                }
                else if (attackerUnit.boardPieceId == BoardPieceId.SwordOfAvalon)
                {
                    foreach (var piece in _playerPieces)
                    {
                        if (piece.boardPieceId == BoardPieceId.HeroRogue)
                        {
                            attackerUnit = piece;
                        }
                    }
                }
                else if (attackerUnit.boardPieceId == BoardPieceId.SmiteWard)
                {
                    foreach (var piece in _playerPieces)
                    {
                        if (piece.boardPieceId == BoardPieceId.HeroGuardian)
                        {
                            attackerUnit = piece;
                        }
                    }
                }
                else if (attackerUnit.HasEffectState(EffectStateType.ConfusedPermanentVisualOnly) && (attackerUnit.boardPieceId == BoardPieceId.IceElemental || attackerUnit.boardPieceId == BoardPieceId.FireElemental))
                {
                    foreach (var piece in _playerPieces)
                    {
                        if (piece.boardPieceId == BoardPieceId.HeroSorcerer)
                        {
                            attackerUnit = piece;
                        }
                    }
                }
                else
                {
                    return;
                }

                if (!attackerUnit.IsPlayer())
                {
                    return;
                }
            }

            foreach (var replacement in _globalAdjustments)
            {
                if (attackerUnit.boardPieceId == replacement.Key)
                {
                    attackerUnit.effectSink.TrySetStatMaxValue(Stats.Type.Health, attackerUnit.GetMaxHealth() + replacement.Value);
                    attackerUnit.effectSink.TrySetStatBaseValue(Stats.Type.Health, attackerUnit.GetHealth() + replacement.Value);
                    attackerUnit.DisableEffectState(EffectStateType.Heal);
                    attackerUnit.EnableEffectState(EffectStateType.Heal, 1);
                }
            }
        }
    }
}
