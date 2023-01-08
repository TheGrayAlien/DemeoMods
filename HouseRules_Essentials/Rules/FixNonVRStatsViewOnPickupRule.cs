﻿namespace HouseRules.Essentials.Rules
{
    using System.Collections.Generic;
    using System.Text;
    using Boardgame;
    using Boardgame.BoardEntities;
    using Boardgame.NonVR;
    using Boardgame.NonVR.Ui;
    using Boardgame.Ui;
    using DataKeys;
    using HarmonyLib;
    using HouseRules.Types;
    using UnityEngine;

    public sealed class FixNonVRStatsViewOnPickupRule : Rule, IPatchable, IMultiplayerSafe
    {
        public override string Description => "Show more stats for PC-Edition when player piece selected";

        private static void Patch(Harmony harmony)
        {
            harmony.Patch(
                original: AccessTools.Method(typeof(NonVrInfoPanelController), "OnSelectPiece"),
                postfix: new HarmonyMethod(
                    typeof(FixNonVRStatsViewOnPickupRule),
                    nameof(NonVrInfoPanelController_OnSelectPiece_Postfix)));
        }

        private static void NonVrInfoPanelController_OnSelectPiece_Postfix(PiceDetectionData selectData)
        {
            if (selectData.Grabbable)
            {
                if (selectData.HasHudInstantiator)
                {
                    GrabbedPieceHudInstantiator data = selectData.HudInstantiator;
                    Piece myPiece = data.MyPiece;

                    if (!myPiece.IsPlayer())
                    {
                        return;
                    }

                    var pieceNameController = Traverse.Create(data).Field<IPieceNameController>("pieceNameController").Value;
                    int strength = myPiece.GetStat(Stats.Type.Strength);
                    int maxstrength = myPiece.GetStatMax(Stats.Type.Strength);
                    int speed = myPiece.GetStat(Stats.Type.Speed);
                    int maxspeed = myPiece.GetStatMax(Stats.Type.Speed);
                    int magic = myPiece.GetStat(Stats.Type.MagicBonus);
                    int maxmagic = myPiece.GetStatMax(Stats.Type.MagicBonus);
                    int resist = myPiece.GetStat(Stats.Type.DamageResist);
                    int maxresist = myPiece.GetStatMax(Stats.Type.DamageResist);
                    int numdowns = myPiece.GetStat(Stats.Type.DownedCounter);
                    bool hasbonuses = true;
                    bool hasimmunities = true;
                    var pieceConfig = Traverse.Create(data).Field<PieceConfigData>("pieceConfig").Value;
                    EffectStateType[] immuneToStatusEffects = pieceConfig.ImmuneToStatusEffects;

                    if (immuneToStatusEffects == null || immuneToStatusEffects.Length == 0)
                    {
                        hasimmunities = false;
                    }

                    if (strength == 0 && speed == 0 && magic == 0 && resist == 0)
                    {
                        hasbonuses = false;
                    }

                    Color lightblue = new Color(0f, 0.75f, 1f);
                    Color lightgreen = new Color(0f, 1f, 0.5f);
                    Color orange = new Color(1f, 0.499f, 0f);
                    Color pink = new Color(0.984f, 0.3765f, 0.498f);
                    Color gold = new Color(1f, 1f, 0.6f);
                    string name = pieceNameController.GetPieceName();
                    var sb = new StringBuilder();
                    sb.AppendLine(ColorizeString($"<u>{name}</u>", Color.yellow));
                    sb.Append(ColorizeString("Downed times remaining: ", pink));
                    switch (numdowns)
                    {
                        case 0:
                            sb.AppendLine(ColorizeString("3", Color.green));
                            break;
                        case 1:
                            sb.AppendLine(ColorizeString("2", gold));
                            break;
                        case 2:
                            sb.AppendLine(ColorizeString("1", orange));
                            break;
                        case 3:
                            sb.AppendLine(ColorizeString("0", Color.red));
                            break;
                    }

                    sb.AppendLine();
                    sb.Append(ColorizeString("--", Color.gray));
                    sb.Append(ColorizeString(" Bonus Stats ", Color.white));
                    sb.AppendLine(ColorizeString("--", Color.gray));

                    if (hasbonuses)
                    {
                        if (strength > 0)
                        {
                            sb.Append(ColorizeString("Strength: ", Color.cyan));
                            sb.AppendLine(ColorizeString($"{strength}/{maxstrength}", lightgreen));
                        }

                        if (speed > 0)
                        {
                            sb.Append(ColorizeString("Swiftness: ", Color.cyan));
                            sb.AppendLine(ColorizeString($"{speed}/{maxspeed}", lightgreen));
                        }

                        if (magic > 0)
                        {
                            sb.Append(ColorizeString("Magic: ", Color.cyan));
                            sb.AppendLine(ColorizeString($"{magic}/{maxmagic}", lightgreen));
                        }

                        if (resist > 0)
                        {
                            sb.Append(ColorizeString("Damage Resist: ", Color.cyan));
                            sb.AppendLine(ColorizeString($"{resist}/{maxresist}", lightgreen));
                        }
                    }
                    else
                    {
                        sb.AppendLine(ColorizeString("None", Color.cyan));
                    }

                    sb.AppendLine();
                    sb.Append(ColorizeString("--", Color.gray));
                    sb.Append(ColorizeString(" Immunities ", Color.white));
                    sb.AppendLine(ColorizeString("--", Color.gray));

                    if (!hasimmunities)
                    {
                        sb.AppendLine(ColorizeString("None", lightblue));
                        GameUI.ShowCameraMessage(sb.ToString(), 5);
                        return;
                    }

                    int num = immuneToStatusEffects.Length;
                    bool weak = false;
                    List<string> list = new List<string>();

                    for (int i = 0; i < num; i++)
                    {
                        string localizedTitle = StatusEffectsConfig.GetLocalizedTitle(immuneToStatusEffects[i]);
                        if (!list.Contains(localizedTitle))
                        {
                            if (localizedTitle.Contains("Netted"))
                            {
                                localizedTitle = ColorizeString("Netted", lightblue);
                            }
                            else if (localizedTitle.Contains("Weaken"))
                            {
                                if (weak)
                                {
                                    continue;
                                }

                                weak = true;
                            }

                            if (i != 0)
                            {
                                sb.Append(ColorizeString(", ", lightblue));
                            }

                            sb.Append(ColorizeString($"{localizedTitle}", lightblue));
                        }
                    }

                    if (HR.SelectedRuleset.Name.Contains("Demeo Revolutions"))
                    {
                        switch (myPiece.boardPieceId)
                        {
                            case BoardPieceId.HeroGuardian:
                                sb.Append(ColorizeString(", Fire", lightblue));
                                break;
                            case BoardPieceId.HeroSorcerer:
                                sb.Append(ColorizeString(", Electricity", lightblue));
                                break;
                            case BoardPieceId.HeroWarlock:
                                sb.Append(ColorizeString(", Corruption", lightblue));
                                break;
                            case BoardPieceId.HeroHunter:
                                sb.Append(ColorizeString(", Ice", lightblue));
                                break;
                            case BoardPieceId.HeroBarbarian:
                                sb.Append(ColorizeString(", Slime", lightblue));
                                break;
                        }
                    }

                    // Pad lines to raise text higher on PC-Edition screen
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.AppendLine();
                    GameUI.ShowCameraMessage(sb.ToString(), 5);
                }
            }
        }

        private static string ColorizeString(string text, Color color)
        {
            return string.Concat(new string[]
            {
        "<color=#",
        ColorUtility.ToHtmlStringRGB(color),
        ">",
        text,
        "</color>",
            });
        }
    }
}
