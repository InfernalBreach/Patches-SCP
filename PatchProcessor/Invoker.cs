using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;

namespace PatchProcessor
{
    public class Invoker
    {
        [PluginEntryPoint("PatchInvoker", "1.0.0", "Invoca los parches de otro proyecto", "xNexusACS")]
        public void Load()
        {
            InvokePatches();
        }

        public void InvokePatches()
        {
            Log.Info("Invocando parches...");

            try
            {
                var assembly = Assembly.Load("InfernalPatches");
                var types = assembly.GetTypes();
                var patchClasses = types.Where(t => t.GetCustomAttributes(typeof(HarmonyPatch), false).Length > 0);
                
                var harmony = new Harmony("patches.invoker.infernal");
                
                foreach (var patchClass in patchClasses)
                    harmony.PatchAll();
            }
            catch (Exception ex)
            {
                Log.Error("Error al invocar los parches: " + ex);
            }
        }
    }
}