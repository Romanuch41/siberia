using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SiberiaApp.Classes
{
    public class TelegramService
    {
        TelegramRquest Request;
        TelegramResponse Response;
        private readonly HttpClient _httpClient;
        private const string baseUrl = "https://api.telegram.org";
        private readonly string _appitoken = "bot7531411928:AAGMShXf-A_XOZwiqvpeIHOJNpN8U_CrXPE";

        public TelegramService(TelegramRquest request, TelegramResponse response, HttpClient? httpclient = null)
        {
            Request = request;
            Response = response;
            _httpClient = httpclient ?? new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appitoken);
        }

        public async Task<TelegramResponse> SendVerificationCode(string phone, string code)
        {
            var request = new TelegramRquest
            {
                phone = phone,
                code = code,
                code_length = code.Length,
                ttl = 60
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/sendVerificationMessage", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new TelegramResponse
                {
                    success = false,
                    error = $"HTTP {(int)response.StatusCode}: {responseContent}"
                };
            }

            var result = JsonSerializer.Deserialize<TelegramResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true})
                ?? new TelegramResponse { success = true };

            return result;
        }
    }

    public class TelegramRquest
    {
        public string phone {  get; set; }
        public string code { get; set; }
        public int code_length { get; set; }
        public int ttl { get; set; }
    }

    public class TelegramResponse
    {
        public bool success { get; set; }
        public string message_id { get; set; }
        public string error { get; set; }
    }
}
