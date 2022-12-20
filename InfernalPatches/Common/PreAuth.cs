using System;
using LiteNetLib.Utils;

namespace InfernalPatches.Common
{
    public class PreAuth
    {
        public bool IsChallenge { get; private set; }
        public byte PlaceholderByte { get; private set; }
        public byte Major { get; private set; }
        public byte Minor { get; private set; }
        public byte Revision { get; private set; }
        public byte BackwardRevision { get; private set; }
        public bool Flag { get; private set; }
        public int ChallengeID { get; private set; }
        public byte[] Challenge { get; private set; }
        public string UserID { get; private set; } = "Unknown UserID";
        public long Expiration { get; private set; }
        public byte Flags { get; private set; }
        public string Region { get; private set; } = "Unknown Region";
        public byte[] Signature { get; private set; } = Array.Empty<byte>();

        public static PreAuth ReadPreAuth(NetDataReader reader)
        {
            PreAuth model = new PreAuth();

            if (reader.TryGetByte(out byte b))
                model.PlaceholderByte = b;

            byte cBackwardRevision = 0;
            byte cMajor;
            byte cMinor;
            byte cRevision;
            bool cflag;
            if (!reader.TryGetByte(out cMajor) || !reader.TryGetByte(out cMinor) || !reader.TryGetByte(out cRevision) ||
                !reader.TryGetBool(out cflag) || (cflag && !reader.TryGetByte(out cBackwardRevision)))
            {
                return null;
            }
            model.Major = cMajor;
            model.Minor = cMinor;
            model.Revision = cRevision;
            model.BackwardRevision = cBackwardRevision;
            model.Flag = cflag;

            if (reader.TryGetInt(out int challengeID))
            {
                model.IsChallenge = true;
                model.ChallengeID = challengeID;
            }

            if (reader.TryGetBytesWithLength(out byte[] challenge))
                model.Challenge = challenge;
            if (reader.TryGetString(out string userid))
                model.UserID = userid;
            if (reader.TryGetLong(out long expiration))
                model.Expiration = expiration;
            if (reader.TryGetByte(out byte flags))
                model.Flags = flags;
            if (reader.TryGetString(out string region))
                model.Region = region;
            if (reader.TryGetBytesWithLength(out byte[] signature))
                model.Signature = signature;

            return model;
        }
    }
}