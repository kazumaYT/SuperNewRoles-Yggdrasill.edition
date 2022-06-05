using HarmonyLib;
using SuperNewRoles.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperNewRoles.Mode.SuperHostRoles.Roles
{
    class SuperBrothers
    {
        public static bool MSizeAlive()
        {
            foreach (PlayerControl p in RoleClass.MSizeBrother.MSizeBrotherPlayer)
            {
                if (p.isAlive())
                {
                    SuperNewRolesPlugin.Logger.LogInfo("SHRMが生きていると判定されました");
                    return true;
                }
            }
            SuperNewRolesPlugin.Logger.LogInfo("SHRMが生きていないと判定されました");
            return false;
        }

        [HarmonyPatch(typeof(HudManager), nameof(HudManager.SetHudActive))]
        class Lsize
        {
            public static void Postfix(HudManager __instance, [HarmonyArgument(0)] bool isActive)
            {
				bool isMSizeAlive = SuperBrothers.MSizeAlive();
                if (!AmongUsClient.Instance.AmHost) return;
                if (PlayerControl.LocalPlayer.isRole(CustomRPC.RoleId.LSizeYoungerBrother))
                {
                    __instance.KillButton.ToggleVisible(visible: isMSizeAlive = true);
                    __instance.SabotageButton.ToggleVisible(visible: isMSizeAlive = true);
                    __instance.ImpostorVentButton.ToggleVisible(visible: isMSizeAlive = true);
                }
            }
        }
        class Peach
        {
            public static void Postfix(HudManager __instance, [HarmonyArgument(0)] bool isActive)
            {
				bool isMSizeAlive = SuperBrothers.MSizeAlive();
                if (!AmongUsClient.Instance.AmHost) return;
                if (PlayerControl.LocalPlayer.isRole(CustomRPC.RoleId.Peach))
                {
                    __instance.KillButton.ToggleVisible(visible: isMSizeAlive = false);
                    __instance.SabotageButton.ToggleVisible(visible: isMSizeAlive = false);
                    __instance.ImpostorVentButton.ToggleVisible(visible: isMSizeAlive = false);
                }
            }
        }
    }
}
