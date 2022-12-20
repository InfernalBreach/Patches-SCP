using HarmonyLib;
using PluginAPI.Core.Attributes;

namespace Scp939Tweaks
{
    public class Scp939Tweaks
    {
        private static Harmony harmony;
        
        [PluginEntryPoint("Scp939Tweaks", "1.0.0", "Tweaks para el Scp939", "xNexusACS")]
        public void Load()
        {
            harmony = new Harmony($"scp939tweaks.nexus.infernal");
            harmony.PatchAll();
        }
    }
}