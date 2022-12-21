using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Pickups;
using PlayerRoles;
using PlayerStatsSystem;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using Respawning;

namespace Logs;

public class EventHandlers
{
    [PluginEvent(ServerEventType.PlayerJoined)]
    public void OnJoin(Player player)
    {
        WebhookSender.AddMessage($"`Conexion ✨` >> {player.Nickname.DiscordParse()} ({player.UserId}) [{player.IpAddress}]", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.PlayerInteractDoor)]
    public void OnDoorInteract(Player player, DoorVariant door, bool canOpen)
    {
        WebhookSender.AddMessage(
            $"`Interaccion 🚪` >> {player.Nickname.DiscordParse()} ha interactuado con la puerta {door} [{(canOpen ? "No esta bloqueada" : "Esta bloqueada")}]", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.PlayerChangeRole)]
    public void OnChangingRole(Player player, PlayerRoleBase oldRole, RoleTypeId newRole, RoleChangeReason changeReason)
    {
        WebhookSender.AddMessage($"`Role 🎭` >> {player.Nickname.DiscordParse()} ha cambiado de role a {newRole} [Era {oldRole.RoleTypeId}]", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.RoundStart)]
    public void OnRoundStart()
    {
        WebhookSender.AddMessage("`🏁🟢🏁 Nueva Ronda Comenzada!`", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.WaitingForPlayers)]
    public void OnWaiting()
    {
        WebhookSender.AddMessage("`🏁🟡🏁 Esperando Jugadores!`", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.RoundEnd)]
    public void OnRoundEnd()
    {
        WebhookSender.AddMessage("`🏁🔴🏁 Ronda Finalizada!`", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.PlayerLeft)]
    public void OnLeft(Player player)
    {
        WebhookSender.AddMessage($"`Desconexion ⛔` >> {player.Nickname.DiscordParse()} ({player.UserId}) [{player.IpAddress}] como {player.Role}", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.LczDecontaminationStart)]
    public void OnStartDecont()
    {
        WebhookSender.AddMessage($"`Descontaminacion ☢` >> Descontaminacion de LCZ Comenzada", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.PlayerInteractElevator)]
    public void OnElevatorInteract(Player player)
    {
        WebhookSender.AddMessage($"`Interaccion 🚪` >> {player.Nickname.DiscordParse()} ha interactuado con un ascensor", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.Scp914KnobChange)]
    public void OnChangeKnob(Player player)
    {
        WebhookSender.AddMessage($"`SCP-914 🎛️` >> {player.Nickname.DiscordParse()} ha cambiado el modo de la 914", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.PlayerChangeItem)]
    public void OnChangingItem(Player player, ushort oldItem, ushort newItem)
    {
        if (player.ReferenceHub.isServer)
            return;
        
        WebhookSender.AddMessage(
            $"`Cambio de Item 🎮` >> {player.Nickname.DiscordParse()} ({player.UserId}) ha cambio de item activo a {newItem} [Era {oldItem}]", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.PlayerSearchedPickup)]
    public void OnPickingItem(Player player, ItemPickupBase item)
    {
        WebhookSender.AddMessage($"`Interaccion 📦` >> {player.Nickname.DiscordParse()} ha recogido {item.Info.ItemId} [{item.Info.Serial}]", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.WarheadStart)]
    public void OnStartWarhead(bool isAutomatic, Player player)
    {
        WebhookSender.AddMessage($"`Nuke ☢` >> {player.Nickname.DiscordParse()} ha iniciado la detonacion de la nuke [{(isAutomatic ? "No es detonacion automatica" : "Es detonacion automatica")}]", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.WarheadStop)]
    public void OnStopWarhead(Player player)
    {
        WebhookSender.AddMessage($"`Nuke ☢` >> {player.Nickname.DiscordParse()} ha detenido la detonacion de la nuke", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.TeamRespawn)]
    public void OnRespawn(SpawnableTeamType team)
    {
        WebhookSender.AddMessage($"`Respawn 🔄` >> Respawn de {team}", WebhookType.GameLogs);
    }

    [PluginEvent(ServerEventType.PlayerDeath)]
    public void OnDying(Player player, Player attacker, DamageHandlerBase damageHandler)
    {
        if (player == null || attacker == null || damageHandler == null)
            return;
        
        WebhookSender.AddMessage($"`Muerte ☠` >> {attacker.Nickname.DiscordParse()} ({attacker.Role}) ha matado a {player.Nickname.DiscordParse()}", WebhookType.GameLogs);
    }
}