using System;
using HarmonyLib;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;

namespace CustomHUDSystem
{
    public class MainClass
    {
        private static Harmony harmony;
        
        public static Random random = new Random();
        
        [PluginEntryPoint("CustomHUD", "1.0.0", "Implementa un HUD nuevo a los jugadores", "xNexusACS")]
        public void Load()
        {
            harmony = new Harmony("hud.nexus.infernal");
            harmony.PatchAll();
            
            EventManager.RegisterEvents<EventHandlers>(this);
        }
    }
}