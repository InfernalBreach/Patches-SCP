using System;
using InfernalExtensions.Internal;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Spawnpoints;
using UnityEngine;

namespace InfernalExtensions.Extensions
{
    public static class RoleExtensions
    {
        public static FpcStandardRoleBase FirstPersonController { get; }
        
        public static Color GetColor(this RoleTypeId typeId) => typeId == RoleTypeId.None ? Color.white : typeId.GetRoleBase().RoleColor;
        
        public static Team GetTeam(this RoleTypeId typeId) => typeId switch
        {
            RoleTypeId.ChaosConscript or RoleTypeId.ChaosMarauder or RoleTypeId.ChaosRepressor or RoleTypeId.ChaosRifleman => Team.ChaosInsurgency,
            RoleTypeId.Scientist => Team.Scientists,
            RoleTypeId.ClassD => Team.ClassD,
            RoleTypeId.Scp049 or RoleTypeId.Scp939 or RoleTypeId.Scp0492 or RoleTypeId.Scp079 or RoleTypeId.Scp096 or RoleTypeId.Scp106 or RoleTypeId.Scp173 => Team.SCPs,
            RoleTypeId.FacilityGuard or RoleTypeId.NtfCaptain or RoleTypeId.NtfPrivate or RoleTypeId.NtfSergeant or RoleTypeId.NtfSpecialist => Team.FoundationForces,
            RoleTypeId.Tutorial => Team.OtherAlive,
            _ => Team.Dead,
        };
        public static string GetFullName(this RoleTypeId typeId) => typeId.GetRoleBase().RoleName;
        public static PlayerRoleBase GetRoleBase(this RoleTypeId typeId) => ServerExtensions.Host.ReferenceHub.roleManager.GetRoleBase(typeId);
        public static RoundSummary.LeadingTeam GetLeadingTeam(this Team team) => team switch
        {
            Team.ClassD or Team.ChaosInsurgency => RoundSummary.LeadingTeam.ChaosInsurgency,
            Team.FoundationForces or Team.Scientists => RoundSummary.LeadingTeam.FacilityForces,
            Team.SCPs => RoundSummary.LeadingTeam.Anomalies,
            _ => RoundSummary.LeadingTeam.Draw,
        };
        
        public static SpawnLocation GetRandomSpawnLocation(this RoleTypeId roleType)
        {
            return !RoleSpawnpointManager.TryGetSpawnpointForRole(roleType, out ISpawnpointHandler spawnpoint) ||
                   !spawnpoint.TryGetSpawnpoint(out Vector3 position, out float horizontalRotation) ?
                null :
                new SpawnLocation(roleType, position, horizontalRotation);
        }
    }
}