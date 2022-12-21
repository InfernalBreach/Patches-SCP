using HarmonyLib;
using MEC;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;

namespace Logs
{
    public class MainClass
    {
        public static MainClass singleton;

        public Harmony harmony;

        [PluginEntryPoint("Logs", "1.0.0", "Loguea los eventos del servidor en discord", "xNexusACS")]
        public void Load()
        {
            singleton = this;
            harmony = new Harmony("logs.nexus.infernal");
            harmony.PatchAll();
            
            WebhookSender.AddMessage("`SERVIDOR BETA CONECTADO ✨`", WebhookType.GameLogs);
            
            Timing.RunCoroutine(WebhookSender.ManageQueue());
            
            EventManager.RegisterEvents<EventHandlers>(this);
        }

        [PluginUnload]
        public void Unload()
        {
            EventManager.UnregisterEvents<EventHandlers>(this);
            
            singleton = null;
            harmony.UnpatchAll(harmony.Id);
        }
        
        [PluginConfig] public Config Config;
    }
}