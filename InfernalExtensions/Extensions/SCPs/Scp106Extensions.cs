using PlayerRoles.PlayableScps.Scp106;
using PlayerRoles.PlayableScps.Subroutines;

namespace InfernalExtensions.Extensions.SCPs
{
    public static class Scp106Extensions
    {
        public static SubroutineManagerModule SubroutineModule { get; }
        
        public static float Vigor
        {
            get => SubroutineModule.TryGetSubroutine(out Scp106Vigor ability) ? ability.VigorAmount : 0;
            set
            {
                if (SubroutineModule.TryGetSubroutine(out Scp106Vigor ability))
                    ability.VigorAmount = value;
            }
        }
        public static float CaptureCooldown
        {
            get => SubroutineModule.TryGetSubroutine(out Scp106Attack ability) ? ability._hitCooldown : 0;
            set
            {
                if (SubroutineModule.TryGetSubroutine(out Scp106Attack ability))
                {
                    ability._hitCooldown = value;
                    ability.ServerSendRpc(true);
                }
            }
        }
        public static void UsePortal()
        {
            if (SubroutineModule.TryGetSubroutine(out Scp106HuntersAtlasAbility ability))
                ability.SetSubmerged(true);
        }
        public static void Stalk()
        {
            if (SubroutineModule.TryGetSubroutine(out Scp106StalkAbility ability))
                ability.IsActive = true;
        }
    }
}