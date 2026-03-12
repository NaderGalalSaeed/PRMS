using PRMSDesktop.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PRMSDesktop.Helpers
{
    public static class SecureTokenStore
    {
        private static readonly byte[] _entropy =
            Encoding.UTF8.GetBytes("PropertyRental.WinForms.Session.v1");

        private static readonly string _folderPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "PropertyRentalManagement");

        private static readonly string _filePath =
            Path.Combine(_folderPath, "session.dat");

        public static void Save(string token, DateTime expiryTimeUtc)
        {
            var payload = new StoredSession
            {
                Token = token,
                ExpiryTimeUtcTicks = expiryTimeUtc.Ticks
            };

            var json = JsonSerializer.Serialize(payload);
            var plainBytes = Encoding.UTF8.GetBytes(json);

            var protectedBytes = ProtectedData.Protect(
                plainBytes,
                _entropy,
                DataProtectionScope.CurrentUser);

            Directory.CreateDirectory(_folderPath);
            File.WriteAllBytes(_filePath, protectedBytes);
        }

        public static StoredSession? Load()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return null;

                var protectedBytes = File.ReadAllBytes(_filePath);

                var plainBytes = ProtectedData.Unprotect(
                    protectedBytes,
                    _entropy,
                    DataProtectionScope.CurrentUser);

                var json = Encoding.UTF8.GetString(plainBytes);

                return JsonSerializer.Deserialize<StoredSession>(json);
            }
            catch
            {
                Clear();
                return null;
            }
        }

        public static void Clear()
        {
            try
            {
                if (File.Exists(_filePath))
                    File.Delete(_filePath);
            }
            catch
            {
                //Log it later
            }
        }
    }
}
