namespace HouseRules.Essentials.Rules
{
    using System.Collections.Generic;
    using Boardgame;
    using Boardgame.BoardEntities;
    using Boardgame.BoardEntities.Abilities;
    using DataKeys;
    using HarmonyLib;
    using HouseRules.Core;
    using HouseRules.Core.Types;

    public sealed class FreeReplenishablesOnCritRule : Rule, IConfigWritable<List<BoardPieceId>>, IPatchable, IMultiplayerSafe
    {
        public override string Description => "Some Heroes can replenish abilities by getting critical hits";

        private static List<BoardPieceId> _globalAdjustments;
        private static bool _isActivated;

        private readonly List<BoardPieceId> _adjustments;

        public FreeReplenishablesOnCritRule(List<BoardPieceId> adjustments)
        {
            _adjustments = adjustments;
        }

        public List<BoardPieceId> GetConfigObject() => _adjustments;

        protected override void OnActivate(GameContext gameContext)
        {
            _globalAdjustments = _adjustments;
            _isActivated = true;
        }

        protected override void OnDeactivate(GameContext gameContext) => _isActivated = false;

        private static void Patch(Harmony harmony)
        {
            harmony.Patch(
                original: AccessTools.Method(typeof(Ability), "GenerateAttackDamage"),
                postfix: new HarmonyMethod(
                    typeof(FreeReplenishablesOnCritRule),
                    nameof(Ability_GenerateAttackDamage_Postfix)));
        }

        private static void Ability_GenerateAttackDamage_Postfix(Piece source, Dice.Outcome diceResult)
        {
            if (!_isActivated)
            {
                return;
            }

            if (!source.IsPlayer())
            {
                return;
            }

            if (diceResult != Dice.Outcome.Crit)
            {
                return;
            }

            if (!_globalAdjustments.Contains(source.boardPieceId))
            {
                return;
            }

            if (source.GetStat(Stats.Type.InnateCounterDamageExtraDamage) != 69 && !HR.SelectedRuleset.Name.Contains("Revolutions"))
            {
                return;
            }

            if (HR.SelectedRuleset.Name.Contains("PROGRESSIVE") || HR.SelectedRuleset.Name.Equals("TEST GAME"))
            {
                if (source.GetStatMax(Stats.Type.CritChance) < 4)
                {
                    return;
                }
            }

            Inventory.Item value;
            if (source.boardPieceId == BoardPieceId.HeroRogue)
            {
                for (int i = 0; i < source.inventory.Items.Count; i++)
                {
                    value = source.inventory.Items[i];
                    if (value.abilityKey == AbilityKey.DiseasedBite)
                    {
                        if (value.IsReplenishing)
                        {
                            if (value.replenishCooldown < 0)
                            {
                                value.replenishCooldown = 3;
                                source.inventory.Items[i] = value;
                            }

                            value.replenishCooldown -= 1;
                            if (value.replenishCooldown < 1)
                            {
                                value.flags &= (Inventory.ItemFlag)(-3);
                            }

                            source.inventory.Items[i] = value;
                            source.AddGold(0);
                        }

                        break;
                    }
                }
            }
            else if (source.boardPieceId == BoardPieceId.HeroBarbarian)
            {
                for (int i = 0; i < source.inventory.Items.Count; i++)
                {
                    value = source.inventory.Items[i];
                    if (value.abilityKey == AbilityKey.Grapple)
                    {
                        if (value.IsReplenishing)
                        {
                            AbilityFactory.TryGetAbility(AbilityKey.Grapple, out var abilityG);
                            source.effectSink.RemoveStatusEffect(EffectStateType.UsedHookThisTurn);
                            abilityG.effectsPreventingUse.Clear();
                            source.inventory.RemoveDisableCooldownFlags();
                            value.flags &= (Inventory.ItemFlag)(-3);
                            source.inventory.Items[i] = value;
                            source.AddGold(0);
                        }
                    }
                    else if (value.abilityKey == AbilityKey.Net)
                    {
                        if (value.IsReplenishing)
                        {
                            if (value.replenishCooldown < 0)
                            {
                                value.replenishCooldown = 3;
                                source.inventory.Items[i] = value;
                            }

                            value.replenishCooldown -= 1;
                            if (value.replenishCooldown < 1)
                            {
                                value.flags &= (Inventory.ItemFlag)(-3);
                            }

                            source.inventory.Items[i] = value;
                            source.AddGold(0);
                        }

                        break;
                    }
                }
            }
            else if (source.boardPieceId == BoardPieceId.HeroBard)
            {
                for (int i = 0; i < source.inventory.Items.Count; i++)
                {
                    value = source.inventory.Items[i];
                    if (value.abilityKey == AbilityKey.CourageShanty)
                    {
                        if (value.IsReplenishing)
                        {
                            value.flags &= (Inventory.ItemFlag)(-3);
                            source.inventory.Items[i] = value;
                            source.AddGold(0);
                        }
                    }
                    else if (value.abilityKey == AbilityKey.EnemyFlashbang)
                    {
                        if (value.IsReplenishing)
                        {
                            if (value.replenishCooldown < 0)
                            {
                                value.replenishCooldown = 3;
                                source.inventory.Items[i] = value;
                            }

                            value.replenishCooldown -= 1;
                            if (value.replenishCooldown < 1)
                            {
                                value.flags &= (Inventory.ItemFlag)(-3);
                            }

                            source.inventory.Items[i] = value;
                            source.AddGold(0);
                        }
                    }
                }
            }
            else if (source.boardPieceId == BoardPieceId.HeroWarlock)
            {
                for (int i = 0; i < source.inventory.Items.Count; i++)
                {
                    value = source.inventory.Items[i];
                    if (value.abilityKey == AbilityKey.MinionCharge)
                    {
                        if (value.IsReplenishing)
                        {
                            value.flags &= (Inventory.ItemFlag)(-3);
                            source.inventory.Items[i] = value;
                            source.AddGold(0);
                        }

                        break;
                    }
                }
            }
            else if (source.boardPieceId == BoardPieceId.HeroSorcerer)
            {
                if (!source.effectSink.HasEffectState(EffectStateType.Overcharge))
                {
                    AbilityFactory.TryGetAbility(AbilityKey.Zap, out var abilityZ);
                    source.effectSink.RemoveStatusEffect(EffectStateType.Discharge);
                    abilityZ.effectsPreventingUse.Clear();
                    source.inventory.RemoveDisableCooldownFlags();

                    for (int i = 0; i < source.inventory.Items.Count; i++)
                    {
                        value = source.inventory.Items[i];
                        if (value.abilityKey == AbilityKey.Zap)
                        {
                            if (value.IsReplenishing)
                            {
                                value.flags &= (Inventory.ItemFlag)(-3);
                                source.inventory.Items[i] = value;
                                source.AddGold(0);
                            }

                            break;
                        }
                    }
                }
            }
            else if (source.boardPieceId == BoardPieceId.HeroGuardian)
            {
                for (int i = 0; i < source.inventory.Items.Count; i++)
                {
                    value = source.inventory.Items[i];
                    if (value.abilityKey == AbilityKey.Grab)
                    {
                        if (value.IsReplenishing)
                        {
                            value.flags &= (Inventory.ItemFlag)(-3);
                            source.inventory.Items[i] = value;
                            source.AddGold(0);
                        }

                        break;
                    }
                }
            }
            else if (source.boardPieceId == BoardPieceId.HeroHunter)
            {
                for (int i = 0; i < source.inventory.Items.Count; i++)
                {
                    value = source.inventory.Items[i];
                    if (value.abilityKey == AbilityKey.Arrow)
                    {
                        if (value.IsReplenishing)
                        {
                            value.flags &= (Inventory.ItemFlag)(-3);
                            source.inventory.Items[i] = value;
                            source.AddGold(0);
                        }

                        break;
                    }
                }
            }
        }
    }
}
