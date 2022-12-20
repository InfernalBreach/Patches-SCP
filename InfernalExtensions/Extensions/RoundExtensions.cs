using System;
using GameCore;
using RoundRestarting;

namespace InfernalExtensions.Extensions
{
    public static class RoundExtensions
    {
        public static TimeSpan ElapsedTime => RoundStart.RoundLength;
        
        public static bool IsStarted => ReferenceHub.LocalHub?.characterClassManager.RoundStarted ?? false;
        
        public static bool InProgress => ReferenceHub.LocalHub is not null && RoundSummary.RoundInProgress();
        
        public static bool IsEnded => RoundSummary.singleton._roundEnded;
        
        public static bool IsLobby => !(IsEnded || IsStarted);
        
        public static bool IsLocked
        {
            get => RoundSummary.RoundLock;
            set => RoundSummary.RoundLock = value;
        }
        public static bool IsLobbyLocked
        {
            get => RoundStart.LobbyLock;
            set => RoundStart.LobbyLock = value;
        }

        public static int UptimeRounds => RoundRestart.UptimeRounds;
    }
}