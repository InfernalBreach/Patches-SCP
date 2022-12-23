using PlayerRoles;
using UnityEngine;

namespace InfernalExtensions.Internal
{
    public class SpawnLocation
    {
        public SpawnLocation(RoleTypeId roleType, Vector3 position, float horizontalRotation)
        {
            RoleType = roleType;
            Position = position;
            HorizontalRotation = horizontalRotation;
        }
        public RoleTypeId RoleType { get; }
        public Vector3 Position { get; }
        public float HorizontalRotation { get; }
    }
}