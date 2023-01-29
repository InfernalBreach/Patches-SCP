using System.Collections.Generic;

namespace Logs
{
    public class Config
    {
        public Dictionary<WebhookType, string> Webhooks { get; set; } = new()
        {
            [WebhookType.CommandLogs] = "",
            [WebhookType.GameLogs] = "",
            [WebhookType.ConsoleCommandLogs] = ""
        };
    }
}
