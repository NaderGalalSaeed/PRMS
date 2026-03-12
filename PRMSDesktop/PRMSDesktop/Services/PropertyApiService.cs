using PRMSDesktop.Constants;
using PRMSDesktop.Models.Common;
using PRMSDesktop.Models.Properties;
using System.Net;
using System.Net.Http.Json;

namespace PRMSDesktop.Services
{
    public class PropertyApiService
    {
        private readonly HttpClient _httpClient;

        public PropertyApiService()
        {
            _httpClient = ApiClient.Instance;
        }

        public async Task<List<PropertyViewModel>> GetAllAsync()
        {
            using var response = await _httpClient.GetAsync(ApiRoutes.Properties);
            await EnsureSuccessAsync(response);

            var result = await response.Content
                .ReadFromJsonAsync<ApiResponse<List<PropertyViewModel>>>();

            return result?.Data ?? new List<PropertyViewModel>();
        }

        public async Task<PropertyViewModel?> GetByIdAsync(long id)
        {
            using var response = await _httpClient.GetAsync(ApiRoutes.PropertyById(id));
            await EnsureSuccessAsync(response);

            var result = await response.Content
                .ReadFromJsonAsync<ApiResponse<PropertyViewModel>>();

            return result?.Data;
        }

        public async Task CreateAsync(SavePropertyRequest model)
        {
            using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Properties, model);
            await EnsureSuccessAsync(response);
        }

        public async Task UpdateAsync(long id, SavePropertyRequest model)
        {
            using var response = await _httpClient.PutAsJsonAsync(ApiRoutes.PropertyById(id), model);
            await EnsureSuccessAsync(response);
        }

        public async Task DeleteAsync(long id)
        {
            using var response = await _httpClient.DeleteAsync(ApiRoutes.PropertyById(id));
            await EnsureSuccessAsync(response);
        }

        private static async Task EnsureSuccessAsync(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException("Session ended, please login again.");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                throw new HttpRequestException(
                    string.IsNullOrWhiteSpace(error) ? "Request failed." : error);
            }
        }
    }
}