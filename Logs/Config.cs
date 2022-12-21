using System.Collections.Generic;

namespace Logs
{
    public class Config
    {
        public Dictionary<WebhookType, string> Webhooks { get; set; } = new()
        {
            [WebhookType.CommandLogs] = "https://discord.com/api/webhooks/1055232737843351562/F4fWCK4strBoLBttG7r0Zgy3gOHmmFxeamp9PQlXSgY-FU02d4yySz1EV-zJmSdWs5k6",
            [WebhookType.GameLogs] = "https://discord.com/api/webhooks/1055232460453052476/PaRqif5EcuwPqShb0r3WicatgyBbzNyFTHpb3XSZpPViCwkqYv4m6OrlO-cA7XMyPak8",
            [WebhookType.ConsoleCommandLogs] = "https://discord.com/api/webhooks/1055232737843351562/F4fWCK4strBoLBttG7r0Zgy3gOHmmFxeamp9PQlXSgY-FU02d4yySz1EV-zJmSdWs5k6"
        };
    }
}