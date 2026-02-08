using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly string _appitoken = "7531411928:AAGMShXf-A_XOZwiqvpeIHOJNpN8U_CrXPE";
        public TelegramService(TelegramRquest request, TelegramResponse response, HttpClient? httpclient = null)
        {
            Request = request;
            Response = response;
            _httpClient = httpclient ?? new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appitoken);
        }

        public async Task<TelegramResponse> SendVerificationCode(string cahtId, string code)
        {
            var payload = new
            {
                chat_id = cahtId,
                text = $"Ваш код авторизации {code}"
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            Debug.WriteLine($"Отправляю сообщение\n{cahtId}");
            Debug.WriteLine($"{json}\n{content}");
            var response = await _httpClient.PostAsync($"/bot{_appitoken}/sendMessage", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseContent);

            if (!response.IsSuccessStatusCode)
            {
                return new TelegramResponse
                {
                    success = false,
                    error = $"HTTP {(int)response.StatusCode}: {responseContent}"
                };
            }

            return new TelegramResponse
            {
                success = true
            };
        }
    }

    public class TelegramRquest
    {
        public string chat_id {  get; set; }
        public string text { get; set; }
    }

    public class TelegramResponse
    {
        public bool success { get; set; }
        public string message_id { get; set; }
        public string error { get; set; }
    }
}

// тут будет типа комит