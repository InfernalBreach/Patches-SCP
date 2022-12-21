using System;
using HarmonyLib;
using PluginAPI.Core;
using RemoteAdmin;

namespace Logs;

/*[HarmonyPatch(typeof(QueryProcessor), nameof(QueryProcessor.ProcessGameConsoleQuery))]
public static class ConsoleCommandLogging
{
    [HarmonyPrefix]
    public static void Prefix(QueryProcessor __instance, string query) 
    {
        try
        {
            Player player = Player.Get(__instance._hub);
            if(player != null)
                WebhookSender.AddMessage($"{player.Nickname.DiscordParse()} ({player.UserId ?? "Srv"}) >> **`{query.DiscordParse()}`**", WebhookType.ConsoleCommandLogs);
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }
    }
}*/