using System;
using DSharp4Webhook.Core;
using DSharp4Webhook.Core.Constructor;
using PluginAPI.Core;

namespace BanLogger;

public class WebhookController : IDisposable
{
    private static readonly EmbedBuilder EmbedBuilder = ConstructorProvider.GetEmbedBuilder();
    private static readonly EmbedFieldBuilder FieldBuilder = ConstructorProvider.GetEmbedFieldBuilder();
    private static readonly MessageBuilder MessageBuilder = ConstructorProvider.GetMessageBuilder();
    private readonly MainClass plugin;
    private IWebhook _webhook;
    private bool _isDisposed;

    public WebhookController(MainClass plugin) => this.plugin = plugin;

    public void Start()
    {
        _webhook = WebhookProvider.CreateStaticWebhook(plugin.Config.WebhookUrl);
    }
    
    public void SendMessage(BanInfo banInfo)
    {
        if (_isDisposed)
            throw new ObjectDisposedException(nameof(WebhookController));

        _webhook.SendMessage(PrepareMessage(banInfo).Build()).Queue((result, isSuccessful) =>
        {
            if (!isSuccessful)
                Log.Error("Fallo al enviar la info de la Sancion." + result);
        });
    }
    public void Dispose()
    {
        _isDisposed = true;
        _webhook?.Dispose();
    }
    private static string TimeFormatter(long duration)
    {
        if (duration == 0)
            return "Kick";

        TimeSpan timespan = new TimeSpan(0, 0, (int)duration);
        string finalFormat = string.Empty;

        if (timespan.TotalDays >= 365)
            finalFormat += $" {timespan.TotalDays / 365}y";
        else if (timespan.TotalDays >= 30)
            finalFormat += $" {timespan.TotalDays / 30}mon";
        else if (timespan.TotalDays >= 1)
            finalFormat += $" {timespan.TotalDays}d";
        else if (timespan.Hours > 0)
            finalFormat += $" {timespan.Hours}h";
        if (timespan.Minutes > 0)
            finalFormat += $" {timespan.Minutes}min";
        if (timespan.Seconds > 0)
            finalFormat += $" {timespan.Seconds}s";

        return finalFormat.Trim();
    }
    private static string CodeLine(string message) => $"```{message}```";

    private MessageBuilder PrepareMessage(BanInfo banInfo)
    {
        if (_isDisposed)
            throw new ObjectDisposedException(nameof(WebhookController));

        EmbedBuilder.Reset();
        FieldBuilder.Reset();
        MessageBuilder.Reset();

        FieldBuilder.Inline = false;

        FieldBuilder.Name = "Usuario Sancionado";
        FieldBuilder.Value = CodeLine(banInfo.BannedName + " " + $"({banInfo.BannedId})");
        EmbedBuilder.AddField(FieldBuilder.Build());

        FieldBuilder.Name = "Staff";
        FieldBuilder.Value = CodeLine(banInfo.IssuerName + " " + $"({banInfo.IssuerId})");
        EmbedBuilder.AddField(FieldBuilder.Build());

        FieldBuilder.Name = "Razon";
        FieldBuilder.Value = CodeLine(banInfo.Reason);
        EmbedBuilder.AddField(FieldBuilder.Build());

        FieldBuilder.Name = "Duracion de la sancion";
        FieldBuilder.Value = CodeLine(TimeFormatter(banInfo.Duration));
        EmbedBuilder.AddField(FieldBuilder.Build());

        EmbedBuilder.Title = plugin.Config.WebhookTitle;
        EmbedBuilder.Timestamp = DateTimeOffset.UtcNow;
        EmbedBuilder.Color = (uint)DSharp4Webhook.Util.ColorUtil.FromHex("#D10E11");

        MessageBuilder.AddEmbed(EmbedBuilder.Build());

        return MessageBuilder;
    }
}