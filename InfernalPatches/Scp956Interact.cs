using HarmonyLib;
using PlayerRoles;
using PluginAPI.Core;

namespace InfernalPatches
{
    [HarmonyPatch(typeof(Scp956Pinata), nameof(Scp956Pinata.UpdateAi))]
    public static class Scp956Interact
    {
        public static bool Prefix(Scp956Pinata __instance)
        {
            if (Player.Get<Player>(__instance._foundTarget.Hub).Role is RoleTypeId.Tutorial)
            {
                __instance.CancelInvoke();
                return true;
            }
            return false;
        }
    }
}