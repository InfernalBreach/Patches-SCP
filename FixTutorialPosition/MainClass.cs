using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using UnityEngine;

namespace FixTutorialPosition
{
    public class MainClass
    {
        [PluginEntryPoint("FixTutorialPosition", "1.0.0", "Teleport Tutorials to the tower again", "xNexusACS")]
        public void Enable()
        {
            EventManager.RegisterEvents(this);
        }

        [PluginEvent(ServerEventType.PlayerChangeRole)]
        public void OnPlayerChangeRole(Player player, PlayerRoleBase oldRole, RoleTypeId newRole,
            RoleChangeReason changeReason)
        {
            if (newRole is RoleTypeId.Tutorial && oldRole.RoleTypeId is not RoleTypeId.Tutorial)
                player.Position = new Vector3(40.297f, 1014.110f, -31.918f);
        }
    }
}