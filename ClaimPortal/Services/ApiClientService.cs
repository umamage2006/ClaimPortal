using Newtonsoft.Json;
using System.Text;

namespace ClaimPortal.Services
{
    public class ApiClientService
    {
        private readonly HttpClient _client;

        public ApiClientService(HttpClient client)
        {
            _client = client;
        }

        // ------------------------------------------
        // GET
        // ------------------------------------------
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(
                    $"API GET failed ({response.StatusCode}) for {endpoint}: {json}");

            if (string.IsNullOrWhiteSpace(json))
                throw new InvalidOperationException($"API GET empty response for {endpoint}");

            // ✅ FIX: HTML detection (real <, not &lt;)
            if (json.TrimStart().StartsWith("<"))
                throw new InvalidOperationException(
                    $"Expected JSON but got HTML from {endpoint}: {json}");

            return JsonConvert.DeserializeObject<T>(json)!;
        }

        // ------------------------------------------
        // POST
        // ------------------------------------------
        public async Task<T> PostAsync<T>(string endpoint, object body)
        {
            var payload = JsonConvert.SerializeObject(body);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(endpoint, content);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(
                    $"API POST failed ({response.StatusCode}) for {endpoint}: {json}");

            if (string.IsNullOrWhiteSpace(json))
                throw new InvalidOperationException($"API POST empty response for {endpoint}");

            // ✅ FIX: HTML detection (real <)
            if (json.TrimStart().StartsWith("<"))
                throw new InvalidOperationException(
                    $"Expected JSON but got HTML from {endpoint}: {json}");

            // ✅ FIX: Works with dynamic OR strongly typed T
            return JsonConvert.DeserializeObject<T>(json)!;
        }
    }
}