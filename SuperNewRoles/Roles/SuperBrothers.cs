using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Hazel;
using SuperNewRoles.Patches;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using SuperNewRoles;
using SuperNewRoles.Roles;
using SuperNewRoles.Helpers;

namespace SuperNewRoles.Roles
{
    public class LSizeYoungerBrother
    {
        public class MurderPatch
        {
            public static void Postfix(PlayerControl __instance)
            {
                if (PlayerControl.LocalPlayer.PlayerId == __instance.PlayerId && PlayerControl.LocalPlayer.isRole(CustomRPC.RoleId.MSizeBrother))
                {
                    PlayerControl.LocalPlayer.SetKillTimerUnchecked(RoleClass.LSizeYoungerBrother.KillCoolTime);
                }
            }
        }
        public static void SetLSizeYoungerBrotherButton()
        {
            if (PlayerControl.LocalPlayer.isRole(CustomRPC.RoleId.LSizeYoungerBrother))
            {
                foreach (PlayerControl p in RoleClass.MSizeBrother.MSizeBrotherPlayer)
                {
                    if (p.isAlive())
                    {
                        SuperNewRolesPlugin.Logger.LogInfo("Mが生きていると判定されました");
                        HudManager.Instance.ImpostorVentButton.gameObject.SetActive(false);
                        HudManager.Instance.SabotageButton.gameObject.SetActive(false);
                        HudManager.Instance.KillButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        SuperNewRolesPlugin.Logger.LogInfo("Mが生きていないと判定されました");
                        HudManager.Instance.ImpostorVentButton.gameObject.SetActive(true);
                        HudManager.Instance.SabotageButton.gameObject.SetActive(true);
                        HudManager.Instance.KillButton.gameObject.SetActive(true);
                    }
                }
            }
        }

        public class FixedUpdate
        {
            public static void Postfix()
            {
                SetLSizeYoungerBrotherButton();
            }
        }
    }



    public class Peach
    {
        public class MurderPatch
        {
            public static void Postfix(PlayerControl __instance)
            {
                if (PlayerControl.LocalPlayer.PlayerId == __instance.PlayerId && PlayerControl.LocalPlayer.isRole(CustomRPC.RoleId.Peach))
                {
                    PlayerControl.LocalPlayer.SetKillTimerUnchecked(RoleClass.Peach.KillCoolTime);
                }
            }
        }
        public static void SetPeachButton()
        {
            if (PlayerControl.LocalPlayer.isRole(CustomRPC.RoleId.Peach))
            {
                foreach (PlayerControl p in RoleClass.MSizeBrother.MSizeBrotherPlayer)
                {
                    if (p.isDead())
                    {
                        SuperNewRolesPlugin.Logger.LogInfo("Mが生きていないと判定されました");
                        HudManager.Instance.ImpostorVentButton.gameObject.SetActive(false);
                        HudManager.Instance.SabotageButton.gameObject.SetActive(false);
                        HudManager.Instance.KillButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        SuperNewRolesPlugin.Logger.LogInfo("Mが生きていると判定されました");
                        HudManager.Instance.ImpostorVentButton.gameObject.SetActive(true);
                        HudManager.Instance.SabotageButton.gameObject.SetActive(true);
                        HudManager.Instance.KillButton.gameObject.SetActive(true);
                    }
                }
            }
        }

        public class FixedUpdate
        {
            public static void Postfix()
            {
                SetPeachButton();
            }
        }
    }
}