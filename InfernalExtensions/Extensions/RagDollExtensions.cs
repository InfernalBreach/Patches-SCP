using System;
using Mirror;
using PlayerRoles;
using PlayerRoles.Ragdolls;
using UnityEngine;

namespace InfernalExtensions.Extensions
{
    public static class RagDollExtensions
    {
        public static int CleanUpTime
        {
            get => RagdollManager.CleanupTime;
            set => RagdollManager.CleanupTime = value;
        }
        public static bool AllowCleanUp => NetworkInfo.ExistenceTime < CleanUpTime;
        
        public static BasicRagdoll Base { get; }
        
        public static GameObject GameObject => Base.gameObject;
        
        public static RagdollData NetworkInfo
        {
            get => Base.NetworkInfo;
            set => Base.NetworkInfo = value;
        }
        public static bool IsCleanedUp => Base._cleanedUp;
        
        public static string Name => Base.name;
        public static DateTime CreationTime => new((long)NetworkInfo.CreationTime);
        
        public static RoleTypeId Role => NetworkInfo.RoleType;
        
        public static bool AllowRecall => NetworkInfo.ExistenceTime > PlayerRoles.PlayableScps.Scp049.Scp049ResurrectAbility.HumanCorpseDuration;
        
        public static Vector3 Position
        {
            get => Base.transform.position;
            set
            {
                NetworkServer.UnSpawn(GameObject);

                Base.transform.position = value;

                NetworkServer.Spawn(GameObject);
            }
        }
        public static Quaternion Rotation
        {
            get => Base.transform.rotation;
            set
            {
                NetworkServer.UnSpawn(GameObject);

                Base.transform.rotation = value;

                NetworkServer.Spawn(GameObject);
            }
        }
        public static Vector3 Scale
        {
            get => Base.transform.localScale;
            set
            {
                NetworkServer.UnSpawn(GameObject);

                Base.transform.localScale = value;

                NetworkServer.Spawn(GameObject);
            }
        }
        public static string ToString() => $"{Base.Info.OwnerHub} ({Name}) [{Base.Info.Handler.ServerLogsText}] *{Role}* |{CreationTime}| ={AllowRecall}=";
    }
}