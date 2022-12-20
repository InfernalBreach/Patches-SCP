using PlayerRoles.PlayableScps.Scp049.Zombies;
using PlayerRoles.PlayableScps.Subroutines;

namespace InfernalExtensions.Extensions.SCPs
{
    public static class Scp0492Extensions
    {
        public static SubroutineManagerModule SubroutineModule { get; }
        
        public static float AttackDamage => SubroutineModule.TryGetSubroutine(out ZombieAttackAbility ability) ? ability.DamageAmount : 0;
        
        public static float SimulatedStare
        {
            get => SubroutineModule.TryGetSubroutine(out ZombieBloodlustAbility ability) ? ability.SimulatedStare : 0;
            set
            {
                if (!SubroutineModule.TryGetSubroutine(out ZombieBloodlustAbility ability))
                    return;

                ability.SimulatedStare = value;
            }
        }

        public static bool BloodlustActive => SubroutineModule.TryGetSubroutine(out ZombieBloodlustAbility ability)
            ? ability.LookingAtTarget
            : false;
        
        public static bool IsConsuming => SubroutineModule.TryGetSubroutine(out ZombieConsumeAbility ability) ? ability.IsInProgress : false;
        
        public static float AttackCooldown => SubroutineModule.TryGetSubroutine(out ZombieAttackAbility ability) ? ability.BaseCooldown : 0;
    }
}