using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Cryptography;
using HarmonyLib;
using InfernalPatches.Common;
using LiteNetLib;
using LiteNetLib.Utils;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace InfernalPatches
{
    [HarmonyPatch(typeof(CustomLiteNetLib4MirrorTransport), nameof(CustomLiteNetLib4MirrorTransport.ProcessConnectionRequest))]
    internal static class ConnectionRequestPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int index = newInstructions.FindIndex(instruction => instruction.OperandIs(Method(typeof(CustomLiteNetLib4MirrorTransport), nameof(CustomLiteNetLib4MirrorTransport.CheckIpRateLimit))));
            int insertionIndex = index - 1;
            int returnIndex = index + 2;

            Label returnLabel = (Label)newInstructions[returnIndex].operand;

            newInstructions.InsertRange(insertionIndex, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_S, 8).MoveLabelsFrom(newInstructions[insertionIndex]),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Call, Method(typeof(ConnectionRequestPatch), nameof(ValidateRequest))),
                new(OpCodes.Brfalse_S, returnLabel),
            });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];
            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }

        private static bool ValidateRequest(byte[] array, ConnectionRequest request)
        {
            NetDataReader reader = new NetDataReader(request.Data.RawData);
            reader._position = 30;
            PreAuth preAuthData = PreAuth.ReadPreAuth(reader);
            if (preAuthData == null)
            {
                CustomLiteNetLib4MirrorTransport.RequestWriter.Reset();
                CustomLiteNetLib4MirrorTransport.RequestWriter.Put((byte)RejectionReason.Custom);
                CustomLiteNetLib4MirrorTransport.RequestWriter.Put("[InfernalBreach]\nTu conexion ha sido rechazada por que el PreAuth mandado de tu cliente es invalido, reinicia el juego o coloca ar en la consola del juego, puedes abrirla con la Ñ");
                return false;
            }

            string s = Encoding.Default.GetString(array);
            if (!ECDSA.VerifyBytes($"{s};{preAuthData.Flags};{preAuthData.Region};{preAuthData.Expiration}", preAuthData.Signature, ServerConsole.PublicKey))
            {
                CustomLiteNetLib4MirrorTransport.RequestWriter.Reset();
                CustomLiteNetLib4MirrorTransport.RequestWriter.Put((byte)RejectionReason.Custom);
                CustomLiteNetLib4MirrorTransport.RequestWriter.Put("[InfernalBreach]\nTu conexion ha sido rechazada por que el PreAuth mandado de tu cliente es invalido, reinicia el juego o coloca ar en la consola del juego, puedes abrirla con la Ñ");
                request.RejectForce(CustomLiteNetLib4MirrorTransport.RequestWriter);
                return false;
            }

            return true;
        }
    }
}