using System;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using UnityEngine;

namespace InfernalExtensions.Extensions
{
    public static class RoleExtensions
    {
        public static FpcStandardRoleBase FirstPersonController { get; }
        
        
        /// <summary>
        /// Get a <see cref="RoleTypeId">role's</see> <see cref="Color"/>.
        /// </summary>
        /// <param name="typeId">The <see cref="RoleTypeId"/> to get the color of.</param>
        /// <returns>The <see cref="Color"/> of the role.</returns>
        public static Color GetColor(this RoleTypeId typeId) => typeId == RoleTypeId.None ? Color.white : typeId.GetRoleBase().RoleColor;
        
        /// <summary>
        /// Get the <see cref="Team"/> of the given <see cref="RoleTypeId"/>.
        /// </summary>
        /// <param name="typeId">The <see cref="RoleTypeId"/>.</param>
        /// <returns><see cref="Team"/>.</returns>
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
        
        /// <summary>
        /// Gets the full name of the given <see cref="RoleTypeId"/>.
        /// </summary>
        /// <param name="typeId">The <see cref="RoleTypeId"/>.</param>
        /// <returns>The full name.</returns>
        public static string GetFullName(this RoleTypeId typeId) => typeId.GetRoleBase().RoleName;

        /// <summary>
        /// Gets the base <see cref="PlayerRoleBase"/> of the given <see cref="RoleTypeId"/>.
        /// </summary>
        /// <param name="typeId">The <see cref="RoleTypeId"/>.</param>
        /// <returns>The <see cref="PlayerRoleBase"/>.</returns>
        public static PlayerRoleBase GetRoleBase(this RoleTypeId typeId) => ServerExtensions.Host.ReferenceHub.roleManager.GetRoleBase(typeId);
        
        /// <summary>
        /// Get the <see cref="RoundSummary.LeadingTeam"/>.
        /// </summary>
        /// <param name="team">Team.</param>
        /// <returns><see cref="RoundSummary.LeadingTeam"/>.</returns>
        public static RoundSummary.LeadingTeam GetLeadingTeam(this Team team) => team switch
        {
            Team.ClassD or Team.ChaosInsurgency => RoundSummary.LeadingTeam.ChaosInsurgency,
            Team.FoundationForces or Team.Scientists => RoundSummary.LeadingTeam.FacilityForces,
            Team.SCPs => RoundSummary.LeadingTeam.Anomalies,
            _ => RoundSummary.LeadingTeam.Draw,
        };

        /// <summary>
        /// Gets a random spawn point of a <see cref="RoleTypeId"/>.
        /// </summary>
        /// <param name="roleType">The <see cref="RoleTypeId"/> to get the spawn point from.</param>
        /// <returns>Returns the spawn point <see cref="Vector3"/> and rotation <see cref="float"/>.</returns>
        public static Tuple<Vector3, Vector3> GetRandomSpawnProperties(this RoleTypeId roleType)
        {
            GameObject randomPosition = SpawnpointManager.GetRandomPosition(roleType);

            return randomPosition is null ?
                new Tuple<Vector3, Vector3>(Vector3.zero, Vector3.zero) :
                new Tuple<Vector3, Vector3>(randomPosition.transform.position, randomPosition.transform.rotation.eulerAngles);
        }
    }
}