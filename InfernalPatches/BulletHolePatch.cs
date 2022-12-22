using HarmonyLib;
using InventorySystem.Items.Firearms.Modules;
using UnityEngine;
using gCore = GamemodeCore.GamemodeCore;

namespace InfernalPatches
{
    [HarmonyPatch(typeof(StandardHitregBase), nameof(StandardHitregBase.PlaceBulletholeDecal))]
    public static class BulletHolePatch
    {
        public static bool Prefix(StandardHitregBase __instance, Ray ray, RaycastHit hit)
        {
            if (gCore.Instance.CurrentEvent != null && !gCore.Instance.CurrentEvent.BulletHolesAllowed)
                return false;
            return true;
        }
    }
}