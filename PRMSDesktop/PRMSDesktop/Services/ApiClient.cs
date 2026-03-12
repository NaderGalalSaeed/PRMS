using PRMSDesktop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PRMSDesktop.Services
{
    public static class ApiClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static HttpClient Instance => _httpClient;

        public static void Configure(string baseUrl)
        {
            _httpClient.BaseAddress = new System.Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void ApplyAuthorizationHeader()
        {
            if (SessionManager.IsLoggedIn)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", SessionManager.Token);
            }
        }

        public static void ClearAuthorizationHeader()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
