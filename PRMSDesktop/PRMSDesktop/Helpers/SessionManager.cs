using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRMSDesktop.Helpers
{
    public static class SessionManager
    {
        public static string Token { get; private set; } = string.Empty;
        public static DateTime ExpiryTimeUtc { get; private set; } = DateTime.MinValue;

        public static bool IsLoggedIn =>
            !string.IsNullOrWhiteSpace(Token);

        public static void SetSession(string token, int expiresInSeconds)
        {
            Token = token;
            ExpiryTimeUtc = DateTime.UtcNow.AddSeconds(expiresInSeconds);

            SecureTokenStore.Save(Token, ExpiryTimeUtc);
        }

        public static bool RestoreSession()
        {
            var stored = SecureTokenStore.Load();

            if (stored == null || string.IsNullOrWhiteSpace(stored.Token))
            {
                Clear(false);
                return false;
            }

            var expiryUtc = new DateTime(stored.ExpiryTimeUtcTicks, DateTimeKind.Utc);

            if (DateTime.UtcNow >= expiryUtc)
            {
                Clear();
                return false;
            }

            Token = stored.Token;
            ExpiryTimeUtc = expiryUtc;
            return true;
        }

        public static void Clear(bool removeFromDisk = true)
        {
            Token = string.Empty;
            ExpiryTimeUtc = DateTime.MinValue;

            if (removeFromDisk)
                SecureTokenStore.Clear();
        }
    }
}
