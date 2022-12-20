using PlayerRoles.PlayableScps.Scp049;
using PlayerRoles.PlayableScps.Subroutines;
using PluginAPI.Core;

namespace InfernalExtensions.Extensions.SCPs
{
    public static class Scp049Extensions
    {
        public static SubroutineManagerModule SubroutineModule { get; }
        
        public static bool IsRecalling => SubroutineModule.TryGetSubroutine(out Scp049ResurrectAbility ability) && ability.IsInProgress;
        
        public static bool IsCallActive => SubroutineModule.TryGetSubroutine(out Scp049CallAbility ability) && ability.IsMarkerShown;
        
        public static Player RecallingPlayer
        {
            get
            {
                if (!IsRecalling || !SubroutineModule.TryGetSubroutine(out Scp049ResurrectAbility ability))
                    return null;

                return Player.Get<Player>(ability.CurRagdoll.Info.OwnerHub);
            }
        }
        public static float CallCooldown
        {
            get => SubroutineModule.TryGetSubroutine(out Scp049CallAbility ability) ? ability.Cooldown.Remaining : 0f;
            set
            {
                if (SubroutineModule.TryGetSubroutine(out Scp049CallAbility ability))
                {
                    ability.Cooldown.Remaining = value;
                    ability.ServerSendRpc(true);
                }
            }
        }
        public static float GoodSenseCooldown
        {
            get => SubroutineModule.TryGetSubroutine(out Scp049SenseAbility ability) ? ability.Cooldown.Remaining : 0f;
            set
            {
                if (SubroutineModule.TryGetSubroutine(out Scp049SenseAbility ability))
                {
                    ability.Cooldown.Remaining = value;
                    ability.ServerSendRpc(true);
                }
            }
        }
        public static bool CanResurrect(BasicRagdoll ragdoll) => SubroutineModule.TryGetSubroutine(out Scp049ResurrectAbility ability) ? ability.CheckRagdoll(ragdoll) : false;
    }
}