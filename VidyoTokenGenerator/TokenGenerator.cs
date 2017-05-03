using System;
using System.Text;
using PCLCrypto;

namespace VidyoTokenGenerator
{
    public class TokenGenerator
    {
        public string Key { get; set; }
        public string AppId { get; set; }
        public string Username { get; set; }
        public long ExpiresInSecs { get; set; }
        public string ExpiresAt { get; set; }

        public TokenGenerator(string key, string appId, string username, long expiresInSecs)
        {
            Key = key;
            AppId = appId;
            Username = username;
            ExpiresInSecs = expiresInSecs;
        }

        public TokenGenerator(string key, string appId, string username, string expiresAt)
        {
            Key = key;
            AppId = appId;
            Username = username;
            ExpiresAt = expiresAt;
        }

        /// <summary>
        /// This method generates a provision login token from a developer key
        /// </summary>
        /// <param name="key">Developer key supplied with the developer account</param>
        /// <param name="appId">ApplicationID supplied with the developer account</param>
        /// <param name="userName">Username to generate a token for</param>
        /// <param name="expiresInSecs">Number of seconds the token will be valid can be used instead of expiresAt</param>
        /// <param name="expiresAt">Time at which the token will expire ex: (2055-10-27T10:54:22Z) can be used instead of expiresInSecs</param>
        /// <returns></returns>
        public static string GenerateToken(string key, string appId, string userName, long expiresInSecs, string expiresAt)
        {
            // Check validity of arguments
            if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("AppID, Key and Username cannot be empty.");
            }

            if (expiresInSecs <= 0 && string.IsNullOrEmpty(expiresAt))
            {
                throw new ArgumentException("Either ExpiresInSecs or ExpiresAt have to be set.");
            }

            // Calculate expiration date
            string expires = CalculateExpiration(expiresInSecs, expiresAt);

            string jid = userName + "@" + appId;
            string body = "provision" + "\0" + jid + "\0" + expires + "\0" + "";

            string token = SignToken(key, body);

            return token;
        }

        private static string CalculateExpiration(long expiresInSecs, string expiresAt)
        {
            long epochSeconds = 62167219200;
            string expires = "";

            // Check if using expiresInSecs or expiresAt
            if (expiresInSecs > 0)
            {
                TimeSpan timeSinceEpoch = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
                expires = (Math.Floor(timeSinceEpoch.TotalSeconds) + epochSeconds + expiresInSecs).ToString();
            }
            else if (expiresAt != null)
            {
                try
                {
                    TimeSpan epochToExpires = DateTime.Parse(expiresAt).ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
                    expires = (Math.Floor(epochToExpires.TotalSeconds) + epochSeconds).ToString();
                }
                catch (Exception)
                {
                    throw new ArgumentException("Time format of ExpiresAt is probably invalid. Example of correct format: (2055-10-27T10:54:22Z)");
                }
            }

            return expires;
        }

        private static string SignToken(string key, string message)
        {            
            byte[] keyMaterial = Encoding.UTF8.GetBytes(key);
            byte[] data = Encoding.UTF8.GetBytes(message);

            var algorithm = WinRTCrypto.MacAlgorithmProvider.OpenAlgorithm(MacAlgorithm.HmacSha384);
            CryptographicHash hasher = algorithm.CreateHash(keyMaterial);
            hasher.Append(data);
            byte[] mac = hasher.GetValueAndReset();

            string macHex = BytesToHex(mac);
            string serialized = message + '\0' + macHex;

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serialized));
        }

        private static string BytesToHex(byte[] bytes)
        {
            var hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public string GenerateToken()
        {
            return GenerateToken(Key, AppId, Username, ExpiresInSecs, ExpiresAt);
        }
    }
}
