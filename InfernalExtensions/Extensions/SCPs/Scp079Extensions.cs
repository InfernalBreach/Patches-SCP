using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Subroutines;

namespace InfernalExtensions.Extensions.SCPs
{
    public static class Scp079Extensions
    {
        public static SubroutineManagerModule SubroutineModule { get; }
        
        public static Scp079AbilityBase[] Abilities
        {
            get => SubroutineModule.TryGetSubroutine(out Scp079AuxManager ability) ? ability._abilities : null;
            set
            {
                if (!SubroutineModule.TryGetSubroutine(out Scp079AuxManager ability))
                    return;

                ability._abilities = value;
            }
        }
        public static int Experience
        {
            get => SubroutineModule.TryGetSubroutine(out Scp079TierManager ability) ? ability.TotalExp : 0;
            set
            {
                if (!SubroutineModule.TryGetSubroutine(out Scp079TierManager ability))
                    return;

                ability.TotalExp = value;
            }
        }
        public static int Level
        {
            get => SubroutineModule.TryGetSubroutine(out Scp079TierManager ability) ? ability.AccessTierLevel : 0;
            set
            {
                if (!SubroutineModule.TryGetSubroutine(out Scp079TierManager ability))
                    return;

                ability.AccessTierIndex = value - 1;
            }
        }
        public static int LevelIndex
        {
            get => SubroutineModule.TryGetSubroutine(out Scp079TierManager ability) ? ability.AccessTierIndex : 0;
            set => Level = value + 1;
        }
        public static int NextLevelThreshold => SubroutineModule.TryGetSubroutine(out Scp079TierManager ability) ? ability.NextLevelThreshold : 0;
        
        public static float Energy
        {
            get => SubroutineModule.TryGetSubroutine(out Scp079AuxManager ability) ? ability.CurrentAux : 0;
            set
            {
                if (!SubroutineModule.TryGetSubroutine(out Scp079AuxManager ability))
                    return;

                ability.CurrentAux = value;
            }
        }
        public static float MaxEnergy
        {
            get => SubroutineModule.TryGetSubroutine(out Scp079AuxManager ability) ? ability.MaxAux : 0;
            set
            {
                if (!SubroutineModule.TryGetSubroutine(out Scp079AuxManager ability))
                    return;

                ability._maxPerTier[LevelIndex] = value;
            }
        }
        public static float RoomLockdownCooldown
        {
            get => SubroutineModule.TryGetSubroutine(out Scp079LockdownRoomAbility ability) ? ability.RemainingCooldown : 0;
            set
            {
                if (!SubroutineModule.TryGetSubroutine(out Scp079LockdownRoomAbility ability))
                    return;

                ability.RemainingCooldown = value;
                ability.ServerSendRpc(true);
            }
        }
    }
}