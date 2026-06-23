using DTO;
using System.Net.Http.Json;
using DTO.PagedResultModel;

namespace FrontEnd.Services
{
    public class TanksApi
    {
        private readonly HttpClient _http;

        public TanksApi(HttpClient http)
        {
            _http = http;
        }

        public async Task<PagedResult<FrontEndData>> GetTanksAsync(int page = 1, int pageSize = 50, string? search = null)
        {
            var url = $"api/tanks?page={page}&pageSize={pageSize}" +
                      (string.IsNullOrWhiteSpace(search) ? "" : $"&search={Uri.EscapeDataString(search)}");

            using var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PagedResult<FrontEndData>>()
                   ?? new PagedResult<FrontEndData>();
        }
    }
}