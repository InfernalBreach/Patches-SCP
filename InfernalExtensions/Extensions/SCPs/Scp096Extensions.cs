using PlayerRoles.PlayableScps.Scp096;
using PlayerRoles.PlayableScps.Subroutines;

namespace InfernalExtensions.Extensions.SCPs
{
    public static class Scp096Extensions
    {
        public static SubroutineManagerModule SubroutineModule { get; }
        
        public static bool CanReceiveTargets => SubroutineModule.TryGetSubroutine(out Scp096RageCycleAbility ability) && ability._targetsTracker.CanReceiveTargets;
        
        public static float ChargeCooldown
        {
            get => SubroutineModule.TryGetSubroutine(out Scp096RageCycleAbility ability) ? ability._timeToChangeState : 0;
            set
            {
                if (SubroutineModule.TryGetSubroutine(out Scp096RageCycleAbility ability))
                {
                    ability._timeToChangeState = value;
                    ability.ServerSendRpc(true);
                }
            }
        }
        public static float EnrageCooldown
        {
            get => SubroutineModule.TryGetSubroutine(out Scp096RageCycleAbility ability) ? ability._activationTime.Remaining : 0;
            set
            {
                if (SubroutineModule.TryGetSubroutine(out Scp096RageCycleAbility ability))
                {
                    ability._activationTime.Remaining = value;
                    ability.ServerSendRpc(true);
                }
            }
        }
        public static float EnragedTimeLeft
        {
            get => SubroutineModule.TryGetSubroutine(out Scp096RageManager ability) ? ability.EnragedTimeLeft : 0;
            set
            {
                if (SubroutineModule.TryGetSubroutine(out Scp096RageManager ability))
                {
                    ability.EnragedTimeLeft = value;
                    ability.ServerSendRpc(true);
                }
            }
        }
        public static float TotalEnrageTime
        {
            get => SubroutineModule.TryGetSubroutine(out Scp096RageManager ability) ? ability.TotalRageTime : 0;
            set
            {
                if (SubroutineModule.TryGetSubroutine(out Scp096RageManager ability))
                {
                    ability.TotalRageTime = value;
                    ability.ServerSendRpc(true);
                }
            }
        }
    }
}