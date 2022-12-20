using PluginAPI.Core;

namespace BanLogger
{
    public class BanInfo
    {
        public BanInfo(Player issuer, Player target, string reason, long duration)
        {
            IssuerName = issuer?.Nickname ?? "Consola";
            IssuerId = issuer?.UserId ?? "Consola";
            BannedName = target.Nickname;
            BannedId = target.UserId;
            Reason = reason;
            Duration = duration;
        }
        public BanInfo(Player issuer, string targetName, string targetId, string reason, long duration)
        {
            IssuerName = issuer?.Nickname ?? "Consola";
            IssuerId = issuer?.UserId ?? "Consola";
            BannedName = targetName;
            BannedId = targetId;
            Reason = reason;
            Duration = duration;
        }
        public string IssuerName { get; }
        public string IssuerId { get; }
        public string BannedName { get; }
        public string BannedId { get; }
        public string Reason { get; }
        public long Duration { get; }
    }
}