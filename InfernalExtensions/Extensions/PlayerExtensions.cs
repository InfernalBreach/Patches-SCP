using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Mirror;
using PlayerRoles;
using PluginAPI.Core;
using UnityEngine;

namespace InfernalExtensions.Extensions
{
    public static class PlayerExtensions
    {
        private static ReferenceHub referenceHub;
        
        private static readonly Dictionary<Type, MethodInfo> WriterExtensionsValue = new();
        private static readonly Dictionary<string, ulong> SyncVarDirtyBitsValue = new();
        private static readonly ReadOnlyDictionary<Type, MethodInfo> ReadOnlyWriterExtensionsValue = new(WriterExtensionsValue);
        private static readonly ReadOnlyDictionary<string, ulong> ReadOnlySyncVarDirtyBitsValue = new(SyncVarDirtyBitsValue);
        private static MethodInfo setDirtyBitsMethodInfoValue;
        private static MethodInfo sendSpawnMessageMethodInfoValue;

        public static ReferenceHub ReferenceHub => referenceHub;

        public static Dictionary<GameObject, Player> Dictionary { get; } = new(20, new ReferenceHub.GameObjectComparer());
        
        public static IEnumerable<Player> List => Dictionary.Values;
        
        public static PlayerRoleManager RoleManager => ReferenceHub.roleManager;
        
        public static Dictionary<string, object> SessionVariables { get; } = new();
        
        public static NetworkIdentity NetworkIdentity => ReferenceHub.networkIdentity;
        
        public static bool IsInvisible
        {
            get => RoleExtensions.FirstPersonController.FpcModule.Motor.IsInvisible;
            set => RoleExtensions.FirstPersonController.FpcModule.Motor.IsInvisible = true;
        }
        
        public static bool IsInDarknes => RoleExtensions.FirstPersonController.InDarkness;
        
        public static bool IsHost => ReferenceHub.isServer;
        
        public static Vector3 Scale
        {
            get => ReferenceHub.transform.localScale;
            set
            {
                try
                {
                    ReferenceHub.transform.localScale = value;

                    foreach (Player target in List)
                        ServerExtensions.SendSpawnMessage?.Invoke(null, new object[] { NetworkIdentity, target.Connection });
                }
                catch (Exception exception)
                {
                    Log.Error($"{nameof(Scale)} error: {exception}");
                }
            }
        }
        public static void SendFakeSyncVar(this Player target, NetworkIdentity behaviorOwner, Type targetType, string propertyName, object value)
        {
            void CustomSyncVarGenerator(NetworkWriter targetWriter)
            {
                targetWriter.WriteUInt64(SyncVarDirtyBits[propertyName]);
                WriterExtensions[value.GetType()]?.Invoke(null, new[] { targetWriter, value });
            }

            PooledNetworkWriter writer = NetworkWriterPool.GetWriter();
            PooledNetworkWriter writer2 = NetworkWriterPool.GetWriter();

            MakeCustomSyncWriter(behaviorOwner, targetType, null, CustomSyncVarGenerator, writer, writer2);
            target.ReferenceHub.networkIdentity.connectionToClient.Send(new UpdateVarsMessage() { netId = behaviorOwner.netId, payload = writer.ToArraySegment() });

            NetworkWriterPool.Recycle(writer);
            NetworkWriterPool.Recycle(writer2);
        }
        private static void MakeCustomSyncWriter(NetworkIdentity behaviorOwner, Type targetType, Action<NetworkWriter> customSyncObject, Action<NetworkWriter> customSyncVar, NetworkWriter owner, NetworkWriter observer)
        {
            byte behaviorDirty = 0;
            NetworkBehaviour behaviour = null;
            
            for (int i = 0; i < behaviorOwner.NetworkBehaviours.Length; i++)
            {
                if (behaviorOwner.NetworkBehaviours[i].GetType() == targetType)
                {
                    behaviour = behaviorOwner.NetworkBehaviours[i];
                    behaviorDirty = (byte)i;
                    break;
                }
            }
            owner.WriteByte(behaviorDirty);
            
            int position = owner.Position;
            owner.WriteInt32(0);
            int position2 = owner.Position;
            
            if (customSyncObject is not null)
                customSyncObject.Invoke(owner);
            else
                behaviour.SerializeObjectsDelta(owner);
            
            customSyncVar?.Invoke(owner);
            
            int position3 = owner.Position;
            owner.Position = position;
            owner.WriteInt32(position3 - position2);
            owner.Position = position3;
            
            if (behaviour.syncMode != SyncMode.Observers)
            {
                ArraySegment<byte> arraySegment = owner.ToArraySegment();
                observer.WriteBytes(arraySegment.Array, position, owner.Position - position);
            }
        }
        public static ReadOnlyDictionary<string, ulong> SyncVarDirtyBits
        {
            get
            {
                if (SyncVarDirtyBitsValue.Count == 0)
                {
                    foreach (PropertyInfo property in typeof(ServerConsole).Assembly.GetTypes()
                                 .SelectMany(x => x.GetProperties())
                                 .Where(m => m.Name.StartsWith("Network")))
                    {
                        MethodInfo setMethod = property.GetSetMethod();

                        if (setMethod is null)
                            continue;

                        MethodBody methodBody = setMethod.GetMethodBody();

                        if (methodBody is null)
                            continue;

                        byte[] bytecodes = methodBody.GetILAsByteArray();

                        if (!SyncVarDirtyBitsValue.ContainsKey($"{property.Name}"))
                            SyncVarDirtyBitsValue.Add($"{property.Name}", bytecodes[bytecodes.LastIndexOf((byte)OpCodes.Ldc_I8.Value) + 1]);
                    }
                }

                return ReadOnlySyncVarDirtyBitsValue;
            }
        }
        public static ReadOnlyDictionary<Type, MethodInfo> WriterExtensions
        {
            get
            {
                if (WriterExtensionsValue.Count == 0)
                {
                    foreach (MethodInfo method in typeof(NetworkWriterExtensions).GetMethods().Where(x => !x.IsGenericMethod && (x.GetParameters()?.Length == 2)))
                        WriterExtensionsValue.Add(method.GetParameters().First(x => x.ParameterType != typeof(NetworkWriter)).ParameterType, method);

                    foreach (MethodInfo method in typeof(GeneratedNetworkCode).GetMethods().Where(x => !x.IsGenericMethod && (x.GetParameters()?.Length == 2) && (x.ReturnType == typeof(void))))
                        WriterExtensionsValue.Add(method.GetParameters().First(x => x.ParameterType != typeof(NetworkWriter)).ParameterType, method);

                    foreach (Type serializer in typeof(ServerConsole).Assembly.GetTypes().Where(x => x.Name.EndsWith("Serializer")))
                    {
                        foreach (MethodInfo method in serializer.GetMethods().Where(x => (x.ReturnType == typeof(void)) && x.Name.StartsWith("Write")))
                            WriterExtensionsValue.Add(method.GetParameters().First(x => x.ParameterType != typeof(NetworkWriter)).ParameterType, method);
                    }
                }

                return ReadOnlyWriterExtensionsValue;
            }
        }
    }
}