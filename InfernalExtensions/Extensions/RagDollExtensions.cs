using System;
using Mirror;
using PlayerRoles;
using PlayerRoles.Ragdolls;
using UnityEngine;

namespace InfernalExtensions.Extensions
{
    public class RagDollExtensions
    {
        public static int CleanUpTime
        {
            get => RagdollManager.CleanupTime;
            set => RagdollManager.CleanupTime = value;
        }
        public bool AllowCleanUp => NetworkInfo.ExistenceTime < CleanUpTime;
        
        public BasicRagdoll Base { get; }
        
        public GameObject GameObject => Base.gameObject;
        
        public RagdollData NetworkInfo
        {
            get => Base.NetworkInfo;
            set => Base.NetworkInfo = value;
        }
        public bool IsCleanedUp => Base._cleanedUp;
        
        public string Name => Base.name;
        public DateTime CreationTime => new((long)NetworkInfo.CreationTime);
        
        public RoleTypeId Role => NetworkInfo.RoleType;
        
        public bool AllowRecall => NetworkInfo.ExistenceTime > PlayerRoles.PlayableScps.Scp049.Scp049ResurrectAbility.HumanCorpseDuration;
        
        public Vector3 Position
        {
            get => Base.transform.position;
            set
            {
                NetworkServer.UnSpawn(GameObject);

                Base.transform.position = value;

                NetworkServer.Spawn(GameObject);
            }
        }
        public Quaternion Rotation
        {
            get => Base.transform.rotation;
            set
            {
                NetworkServer.UnSpawn(GameObject);

                Base.transform.rotation = value;

                NetworkServer.Spawn(GameObject);
            }
        }
        public Vector3 Scale
        {
            get => Base.transform.localScale;
            set
            {
                NetworkServer.UnSpawn(GameObject);

                Base.transform.localScale = value;

                NetworkServer.Spawn(GameObject);
            }
        }
        public override string ToString() => $"{Base.Info.OwnerHub} ({Name}) [{Base.Info.Handler.ServerLogsText}] *{Role}* |{CreationTime}| ={AllowRecall}=";
    }
}