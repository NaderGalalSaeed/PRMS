using PRMSDesktop.Constants;
using PRMSDesktop.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PRMSDesktop.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService()
        {
            _httpClient = ApiClient.Instance;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Login, request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }
    }
}
