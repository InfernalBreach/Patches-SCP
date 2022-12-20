using System;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;

namespace CustomHUDSystem
{
    public class MainClass
    {
        public static Random random = new Random();
        
        [PluginEntryPoint("CustomHUD", "1.0.0", "Implementa un HUD nuevo a los jugadores", "xNexusACS")]
        public void Load()
        {
            EventManager.RegisterEvents<EventHandlers>(this);
        }
    }
}