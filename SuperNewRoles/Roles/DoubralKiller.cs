using Hazel;
using SuperNewRoles.CustomRPC;
using SuperNewRoles.Mode;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HarmonyLib;
using SuperNewRoles.Buttons;
using System.Linq;

namespace SuperNewRoles.Roles
{
    public static class DoubralKiller
    {
        public static void resetFirstCoolDown()
        {
            RoleClass.DoubralKiller.FirstSuicideTime = RoleClass.DoubralKiller.FirstSuicideDefaultTime;
        }
        public static void resetSecondCoolDown()
        {
            HudManagerStartPatch.DoubralKillerSecondKillButton.MaxTimer = RoleClass.DoubralKiller.SecondKillTime;
            HudManagerStartPatch.DoubralKillerSecondKillButton.Timer = RoleClass.DoubralKiller.SecondKillTime;
            RoleClass.DoubralKiller.SecondSuicideTime = RoleClass.DoubralKiller.SecondSuicideDefaultTime;
        }
        public static void EndMeeting()
        {
            resetFirstCoolDown();
            resetSecondCoolDown();
        }
        public static void FixedUpdate()
        {
            bool FirstIsViewButtonText = false;
            bool SecondIsViewButtonText = false;
            static void Postfix()
            {
                SuperNewRolesPlugin.Logger.LogInfo(RoleClass.DoubralKiller.FirstSuicideTime);
                SuperNewRolesPlugin.Logger.LogInfo(RoleClass.DoubralKiller.SecondSuicideTime);
            }
            foreach (PlayerControl p in RoleClass.DoubralKiller.DoubralKillerPlayer)
            {
                if (p.isAlive())
                {
                    if (!RoleClass.IsMeeting)
                    {
                        if (PlayerControl.LocalPlayer.isRole(RoleId.DoubralKiller))
                        {
                            if (RoleClass.DoubralKiller.FirstIsSuicideView)
                            {
                                SecondIsViewButtonText = true;
                                RoleClass.DoubralKiller.SecondSuicideTime -= Time.fixedDeltaTime;
                                if (RoleClass.DoubralKiller.SecondSuicideTime <= 0)
                                {
                                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CustomRPC.RPCMurderPlayer, SendOption.Reliable, -1);
                                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                                    writer.Write(byte.MaxValue);
                                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                                    RPCProcedure.RPCMurderPlayer(PlayerControl.LocalPlayer.PlayerId, PlayerControl.LocalPlayer.PlayerId, byte.MaxValue);
                                }
                            }
                            if (RoleClass.DoubralKiller.SecondIsSuicideView)
                            {
                                SecondIsViewButtonText = true;
                                if (RoleClass.DoubralKiller.SecondSuicideTime <= 0)
                                {
                                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CustomRPC.RPCMurderPlayer, SendOption.Reliable, -1);
                                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                                    writer.Write(byte.MaxValue);
                                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                                    RPCProcedure.RPCMurderPlayer(PlayerControl.LocalPlayer.PlayerId, PlayerControl.LocalPlayer.PlayerId, byte.MaxValue);
                                }
                            }
                        }
                    }
                }
            }

            if (FirstIsViewButtonText && RoleClass.DoubralKiller.FirstIsSuicideView && PlayerControl.LocalPlayer.isAlive())
            {
                RoleClass.DoubralKiller.FirstSuicideKillText.text = string.Format(ModTranslation.getString("DoubralKillerFirstSuicideText"), ((int)RoleClass.DoubralKiller.FirstSuicideTime) + 1);
            }
            else
            {
                if (RoleClass.DoubralKiller.FirstSuicideKillText.text != "")
                {
                    RoleClass.DoubralKiller.FirstSuicideKillText.text = "";
                }
            }
            if (SecondIsViewButtonText && RoleClass.DoubralKiller.SecondIsSuicideView && PlayerControl.LocalPlayer.isAlive())
            {
                RoleClass.DoubralKiller.SecondSuicideKillText.text = string.Format(ModTranslation.getString("DoubralKillerSecondSuicideText"), ((int)RoleClass.DoubralKiller.SecondSuicideTime) + 1);
            }
            else
            {
                if (RoleClass.DoubralKiller.SecondSuicideKillText.text != "")
                {
                    RoleClass.DoubralKiller.SecondSuicideKillText.text = "";
                }
            }
        }
        public static void MurderPlayer(PlayerControl __instance, PlayerControl target)
        {
            if (__instance.isRole(RoleId.DoubralKiller))
            {
                if (__instance.PlayerId == PlayerControl.LocalPlayer.PlayerId)
                {
                    RoleClass.DoubralKiller.FirstSuicideTime = RoleClass.DoubralKiller.FirstSuicideDefaultTime;
                    RoleClass.DoubralKiller.FirstIsSuicideView = true;
                    RoleClass.DoubralKiller.SecondSuicideTime = RoleClass.DoubralKiller.SecondSuicideDefaultTime;
                    RoleClass.DoubralKiller.SecondIsSuicideView = true;
                }
                RoleClass.DoubralKiller.FirstIsSuicideViews[__instance.PlayerId] = true;
                RoleClass.DoubralKiller.SecondIsSuicideViews[__instance.PlayerId] = true;
                if (ModeHandler.isMode(ModeId.SuperHostRoles))
                {
                    RoleClass.DoubralKiller.FirstSuicideTimers[__instance.PlayerId] = RoleClass.DoubralKiller.FirstSuicideDefaultTime;
                    RoleClass.DoubralKiller.SecondSuicideTimers[__instance.PlayerId] = RoleClass.DoubralKiller.SecondSuicideDefaultTime;
                }
                else if (ModeHandler.isMode(ModeId.Default))
                {
                    if (__instance.PlayerId == PlayerControl.LocalPlayer.PlayerId)
                    {
                        __instance.SetKillTimerUnchecked(RoleClass.DoubralKiller.FirstKillTime);
                        __instance.SetKillTimerUnchecked(RoleClass.DoubralKiller.SecondKillTime);
                        RoleClass.DoubralKiller.FirstSuicideTime = RoleClass.DoubralKiller.FirstSuicideDefaultTime;
                        RoleClass.DoubralKiller.SecondSuicideTime = RoleClass.DoubralKiller.SecondSuicideDefaultTime;
                    }
                }
            }
        }
        public static void WrapUp()
        {
            if (RoleClass.DoubralKiller.IsMeetingReset)
            {
                RoleClass.DoubralKiller.FirstSuicideTime = RoleClass.DoubralKiller.FirstSuicideDefaultTime;
                RoleClass.DoubralKiller.SecondSuicideTime = RoleClass.DoubralKiller.SecondSuicideDefaultTime;
            }
        }
        public class DoubralKillerFixedPatch
        {
            public static PlayerControl DoubralKillersetTarget(bool onlyCrewmates = false, bool targetPlayersInVents = false, List<PlayerControl> untargetablePlayers = null, PlayerControl targetingPlayer = null)
            {
                PlayerControl result = null;
                float num = GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)];
                if (!ShipStatus.Instance) return result;
                if (targetingPlayer == null) targetingPlayer = PlayerControl.LocalPlayer;
                if (targetingPlayer.Data.IsDead || targetingPlayer.inVent) return result;

                if (untargetablePlayers == null)
                {
                    untargetablePlayers = new List<PlayerControl>();
                }

                Vector2 truePosition = targetingPlayer.GetTruePosition();
                Il2CppSystem.Collections.Generic.List<GameData.PlayerInfo> allPlayers = GameData.Instance.AllPlayers;
                for (int i = 0; i < allPlayers.Count; i++)
                {
                    GameData.PlayerInfo playerInfo = allPlayers[i];
                    if (!playerInfo.Disconnected && playerInfo.PlayerId != targetingPlayer.PlayerId && playerInfo.Object.isAlive())
                    {
                        PlayerControl @object = playerInfo.Object;
                        if (untargetablePlayers.Any(x => x == @object))
                        {
                            // if that player is not targetable: skip check
                            continue;
                        }

                        if (@object && (!@object.inVent || targetPlayersInVents))
                        {
                            Vector2 vector = @object.GetTruePosition() - truePosition;
                            float magnitude = vector.magnitude;
                            if (magnitude <= num && !PhysicsHelpers.AnyNonTriggersBetween(truePosition, vector.normalized, magnitude, Constants.ShipAndObjectsMask))
                            {
                                result = @object;
                                num = magnitude;
                            }
                        }
                    }
                }
                return result;
            }
        }
    }
}