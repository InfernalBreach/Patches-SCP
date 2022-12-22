using System;
using HarmonyLib;
using PluginAPI.Core;
using RemoteAdmin;
using Logs;

namespace InfernalPatches
{
    [HarmonyPatch(typeof(QueryProcessor), nameof(QueryProcessor.ProcessGameConsoleQuery))]
    public static class ConsoleCommandLogging
    {
        [HarmonyPrefix]
        public static void Prefix(QueryProcessor __instance, string query) 
        {
            try
            {
                Player player = Player.Get<Player>(__instance._hub);

                if(player != null)
                    WebhookSender.AddMessage($"{player.Nickname.DiscordParse()} ({player.UserId ?? "Srv"}) >> **`{query.DiscordParse()}`**", WebhookType.ConsoleCommandLogs);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}