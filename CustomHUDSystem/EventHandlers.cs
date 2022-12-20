using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using UnityEngine;

namespace CustomHUDSystem
{
    public class EventHandlers
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoined(Player player)
        {
            if (!player.GameObject.TryGetComponent<Component>(out _))
                player.GameObject.AddComponent<Component>();
        }
        [PluginEvent(ServerEventType.RoundRestart)]
        public void OnRoundRestart()
        {
            foreach (Player player in Player.GetPlayers<Player>())
            {
                if(player.GameObject.TryGetComponent<Component>(out var component))
                        Object.Destroy(component);
            }
        }
    }
}