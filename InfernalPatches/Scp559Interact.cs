using HarmonyLib;
using PlayerRoles;

namespace InfernalPatches
{
    [HarmonyPatch(typeof(Scp559Cake), nameof(Scp559Cake.ServerInteract))]
    public static class Scp559Interact
    {
        public static bool Prefix(Scp559Cake __instance, ReferenceHub ply)
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