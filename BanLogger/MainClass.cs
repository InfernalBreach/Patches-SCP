using CommandSystem;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;

namespace BanLogger;

public class MainClass
{
    [PluginEntryPoint("BanLogger", "1.0.0", "Loguea Baneos por webhook a discord", "xNexusACS")]
    public void Enable()
    {
        WebhookController = new WebhookController(this);
        WebhookController.Start();
        EventManager.RegisterEvents(this);
    }

    public WebhookController WebhookController { get; private set; }
    
    [PluginConfig] 
    public Config Config;

    /*[PluginEvent(ServerEventType.PlayerBanned)]
    public void OnBanned(Player player, ICommandSender issuer, string reason, long duration)
    {
        WebhookController.SendMessage(new BanInfo(issuer, player.Nickname, player.UserId, reason, duration));
    }*/

    [PluginEvent(ServerEventType.PlayerKicked)]
    public void OnKicked(Player player, Player issuer, string reason)
    {
        WebhookController.SendMessage(new BanInfo(issuer, player, reason, 0));
    }
}