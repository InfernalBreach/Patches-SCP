using PlayerRoles.PlayableScps.Scp939;
using PlayerRoles.PlayableScps.Scp939.Mimicry;
using PlayerRoles.PlayableScps.Subroutines;
using PluginAPI.Core;

namespace InfernalExtensions.Extensions.SCPs
{
    public static class Scp939Extensions
    {
        public static SubroutineManagerModule SubroutineModule { get; }
        
        public static float AttackCooldown
        {
            get => SubroutineModule.TryGetSubroutine(out Scp939ClawAbility ability) ? ability.Cooldown.Remaining : 0f;
            set
            {
                if (SubroutineModule.TryGetSubroutine(out Scp939ClawAbility ability))
                {
                    ability.Cooldown.Remaining = value;
                    ability.ServerSendRpc(true);
                }
            }
        }
        public static bool IsFocused => SubroutineModule.TryGetSubroutine(out Scp939FocusAbility focus) && focus.TargetState;
        
        public static bool IsLunging => SubroutineModule.TryGetSubroutine(out Scp939LungeAbility ability) && ability.State != Scp939LungeState.None;
        
        public static float AmnesticCloudCooldown
        {
            get => SubroutineModule.TryGetSubroutine(out Scp939AmnesticCloudAbility ability) ? ability.Cooldown.Remaining : 0f;
            set
            {
                if (SubroutineModule.TryGetSubroutine(out Scp939AmnesticCloudAbility ability))
                {
                    ability.Cooldown.Remaining = value;
                    ability.ServerSendRpc(true);
                }
            }
        }
        public static float MimicryCooldown
        {
            get => SubroutineModule.TryGetSubroutine(out EnvironmentalMimicry ability) ? ability.Cooldown.Remaining : 0f;
            set
            {
                if (SubroutineModule.TryGetSubroutine(out EnvironmentalMimicry ability))
                {
                    ability.Cooldown.Remaining = value;
                    ability.ServerSendRpc(true);
                }
            }
        }
        public static int SavedVoices => SubroutineModule.TryGetSubroutine(out MimicryRecorder ability) ? ability.SavedVoices.Count : 0;
        
        public static bool MimicryPointActive => SubroutineModule.TryGetSubroutine(out EnvironmentalMimicry ability) && ability._mimicPoint.Active;
        
        public static void ClearRecordings(Player target = null)
        {
            if (!SubroutineModule.TryGetSubroutine(out MimicryRecorder ability))
                return;

            if (target is null)
            {
                ability.SavedVoices.Clear();
                ability._serverSentVoices.Clear();
            }
            else
            {
                ability.RemoveRecordingsOfPlayer(target.ReferenceHub);
            }

            ability.SavedVoicesModified = true;
        }
    }
}