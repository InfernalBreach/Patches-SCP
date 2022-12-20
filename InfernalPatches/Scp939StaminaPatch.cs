using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles;
using PlayerStatsSystem;
using PluginAPI.Core;

namespace InfernalPatches
{
    [HarmonyPatch(typeof(StaminaStat), nameof(StaminaStat.ModifyAmount))]
    public class Scp939StaminaPatch
    {
        /*public static void Prefix(StaminaStat __instance, float f)
        {
            if (Player.TryGet(__instance.Hub, out var playerToChange))
            {
                if (playerToChange is not null && playerToChange.Role is RoleTypeId.Scp939)
                {
                    float max = 2f;

                    var maxV = typeof(StaminaStat).GetProperty("MaxValue", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                    maxV?.SetValue(__instance, max);
                }
            }
        }*/
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            
            Label label = generator.DefineLabel();
            
            newInstructions.InsertRange(0, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Scp939StaminaPatch), nameof(NewStaminaProccess))),
                new CodeInstruction(OpCodes.Brtrue_S, label),
            });
            
            for (int i = 0; i < newInstructions.Count; i++)
            {
                if (newInstructions[i].opcode == OpCodes.Ldarg_0 && newInstructions[i + 1].opcode == OpCodes.Ldfld && newInstructions[i + 2].opcode == OpCodes.Ldfld && newInstructions[i + 3].opcode == OpCodes.Ldc_R4 && newInstructions[i + 4].opcode == OpCodes.Ble_Un_S)
                {
                    newInstructions[i + 4].labels.Add(label);
                    break;
                }
            }
            return newInstructions;
        }

        public void NewStaminaProccess()
        {
            var playerRole = typeof(Player).GetProperty("Role", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            var role = playerRole?.GetValue(this);
            
            if (role is RoleTypeId.Scp939)
            {
                var playerMaxStamina = typeof(StaminaStat).GetProperty("MaxValue", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                var maxStamina = playerMaxStamina?.GetValue(this);
                
                playerMaxStamina?.SetValue(maxStamina, 2f);
            }
        }
    }
}