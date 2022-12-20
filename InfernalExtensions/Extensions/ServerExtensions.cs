using System.Collections.Generic;
using System.Reflection;
using GameCore;
using Mirror;
using PlayerRoles.RoleAssign;
using PluginAPI.Core;

namespace InfernalExtensions.Extensions
{
    public static class ServerExtensions
    {
        private static MethodInfo sendSpawnMessage;
        
        public static Player Host { get; internal set; }
        
        public static MethodInfo SendSpawnMessage => sendSpawnMessage ??= typeof(NetworkServer).GetMethod("SendSpawnMessage", BindingFlags.NonPublic | BindingFlags.Static);
        
        public static PermissionsHandler PermissionsHandler => ServerStatic.PermissionsHandler;
        
        public static bool LateJoinEnabled => LateJoinTime > 0;
        
        public static float LateJoinTime => ConfigFile.ServerConfig.GetFloat(RoleAssigner.LateJoinKey, 0f);
        
        public static Dictionary<string, object> SessionVariables { get; } = new();
        
        public static bool TryGetSessionVariable<T>(string key, out T result)
        {
            if (SessionVariables.TryGetValue(key, out object value) && value is T type)
            {
                result = type;
                return true;
            }

            result = default;
            return false;
        }
    }
}