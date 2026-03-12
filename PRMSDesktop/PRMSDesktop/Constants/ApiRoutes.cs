using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRMSDesktop.Constants
{
    public static class ApiRoutes
    {
        public const string ServerIP = "https://localhost:5223/";
        public const string EndPonit = "api";


        public const string Login = $"{EndPonit}/auth/login";


        public const string Properties = $"{EndPonit}/Properties";
        public static string PropertyById(long id) => $"{EndPonit}/Properties/{id}";
    }
}
