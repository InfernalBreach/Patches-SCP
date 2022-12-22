﻿using Christmas.Scp2536;
using HarmonyLib;
using PlayerRoles;

namespace InfernalPatches
{
    [HarmonyPatch(typeof(Scp2536GiftController), nameof(Scp2536GiftController.ServerInteract))]
    public static class Scp2536GiftInteract
    {
        public static bool Prefix(Scp2536GiftController __instance, ReferenceHub ply)
        {
            if (ply.roleManager.CurrentRole.RoleTypeId is RoleTypeId.Tutorial)
            {
                __instance.CancelInvoke();
                return true;
            }

            return false;
        }
    }
}