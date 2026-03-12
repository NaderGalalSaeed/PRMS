using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRMSDesktop.Models.Auth
{
    public class StoredSession
    {
        public string Token { get; set; } = string.Empty;
        public long ExpiryTimeUtcTicks { get; set; }
    }
}
