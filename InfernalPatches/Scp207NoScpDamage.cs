using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using CustomPlayerEffects;
using HarmonyLib;
using PlayerRoles;
using static HarmonyLib.AccessTools;

namespace InfernalPatches
{
    [HarmonyPatch(typeof(Scp207), nameof(Scp207.Update))]
    internal static class Scp207NoScpDamage
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var newInst = instructions.ToList();

            var index = newInst.FindIndex(x => x.opcode == OpCodes.Ret);
            var exitlabel = newInst[index + 1].labels[0];
            var newlabel = generator.DefineLabel();
            
            Label lab = generator.DefineLabel();

            newInst[index + 1].labels.Clear();
            newInst[index + 1].labels.Add(newlabel);

            newInst.InsertRange(index + 1, new[]
            {
                new CodeInstruction(OpCodes.Ldsfld, Field(typeof(Scp207NoScpDamage), nameof(ScpWithNoDamage))),
                new CodeInstruction(OpCodes.Ldarg_0).WithLabels(exitlabel),
                new CodeInstruction(OpCodes.Ldfld, Field(typeof(StatusEffectBase),nameof(StatusEffectBase.Hub))),
                new CodeInstruction(OpCodes.Ldfld, Field(typeof(ReferenceHub),nameof(ReferenceHub.roleManager))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(PlayerRoleManager), nameof(PlayerRoleManager.CurrentRole))),
                new CodeInstruction(OpCodes.Callvirt, Method(typeof(List<RoleTypeId>), nameof(List<RoleTypeId>.Contains))),
                new CodeInstruction(OpCodes.Brfalse_S, lab),
                new CodeInstruction(OpCodes.Ret)
            });

            for (int i = 0; i < newInst.Count; i++)
                yield return newInst[i];
        }
        private static readonly List<RoleTypeId> ScpWithNoDamage = new ()
        {
            RoleTypeId.Scp049, RoleTypeId.Scp096, RoleTypeId.Scp106, RoleTypeId.Scp173, RoleTypeId.Scp0492,
            RoleTypeId.Scp939
        };
    }
}