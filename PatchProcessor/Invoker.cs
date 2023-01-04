using System;
using System.Linq;
using System.Reflection;
using ColorLibSL.Core;
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

        [PluginUnload]
        public void Unload()
        {
            ClearPatches();
        }

        public void InvokePatches()
        {
            Log.Info("Invocando parches...");

            try
            {
                var harmony = new Harmony("patches.invoker.infernal");
                
                var assembly = Assembly.Load("InfernalPatches");
                var types = assembly.GetTypes();
                var patchClasses = types.Where(t => t.GetCustomAttributes(typeof(HarmonyPatch), true).Length > 0);
                
                foreach (var patchClass in patchClasses)
                {
                    var harmonyPatch = patchClass.GetCustomAttributes(typeof(HarmonyPatch), true)[0] as HarmonyPatch;
                    var patchMethods = patchClass.GetMethods().Where(m => m.GetCustomAttributes(typeof(HarmonyPatch), true).Length > 0);
                    foreach (var patchMethod in patchMethods)
                    {
                        var harmonyPatchMethod = patchMethod.GetCustomAttributes(typeof(HarmonyPatch), true)[0] as HarmonyPatch;
                        var patch = new HarmonyMethod(patchMethod);

                        if (harmonyPatchMethod != null)
                        {
                            if (harmonyPatchMethod.info != null)
                            {
                                patch = new HarmonyMethod(harmonyPatchMethod.info.method);
                            }
                            else if (harmonyPatchMethod.info?.declaringType != null)
                            {
                                patch = new HarmonyMethod(harmonyPatchMethod.info?.declaringType, harmonyPatchMethod.info?.methodName);
                            }
                        }
                        if (harmonyPatch != null)
                        {
                            if (harmonyPatch.info != null)
                            {
                                harmony.Patch(harmonyPatch.info.method, patch);
                            }
                            else if (harmonyPatch.info?.declaringType != null)
                            {
                                harmony.Patch(harmonyPatch.info.method, harmonyPatch.info, patch);
                            }
                        }
                    }
                    Log.Info($"{LogUtils.GetColor(LogColor.Blue)}Metodo Parcheado: " + harmonyPatch?.info?.methodType + " " + harmonyPatch?.info?.methodName);
                    Log.Info($"{LogUtils.GetColor(LogColor.Blue)}Type Parcheado: " + harmonyPatch?.info?.declaringType);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{LogUtils.GetColor(LogColor.BrightRed)}Error al invocar los parches: " + ex);
            }
        }

        public void ClearPatches()
        {
            try
            {
                var harmony = new Harmony("patches.invoker.infernal");
                harmony.UnpatchAll("patches.invoker.infernal");
            }
            catch (Exception ex)
            {
                Log.Error($"{LogUtils.GetColor(LogColor.BrightRed)}Error al limpiar los parches: " + ex);
            }
        }
    }
}