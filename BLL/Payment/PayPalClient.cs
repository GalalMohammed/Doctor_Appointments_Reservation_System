using BLLServices.Payment.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLLServices.Payment
{
    public class PayPalClient(string mode, string clientId, string clientSecret)
    {
        public string Mode { get; } = mode ?? throw new ArgumentNullException(nameof(mode));
        public string ClientId { get; } = clientId ?? throw new ArgumentNullException(nameof(clientId));
        public string ClientSecret { get; } = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
        public string BaseUrl => Mode == "live" ? "https://api-m.paypal.com" : "https://api-m.sandbox.paypal.com";
        async Task<AuthResponse> Authenticate()
        {
            using HttpClient httpClient = new();
            byte[] byteArray = Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            FormUrlEncodedContent content = new(
            [
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            ]);
            HttpResponseMessage response = await httpClient.PostAsync($"{BaseUrl}/v1/oauth2/token", content);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AuthResponse>(jsonResponse) ?? throw new Exception("Failed to deserialize AuthResponse");
        }
        public async Task<CreateOrderResponse> CreateOrder(string value, string currency, string reference)
        {
            AuthResponse authResponse = await Authenticate();
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.AccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            CreateOrderRequest request = new()
            {
                Intent = "CAPTURE",
                PurchaseUnits =
                [
                    new() {
                        Amount = new Amount
                        {
                            CurrencyCode = currency,
                            Value = value
                        },
                        ReferenceId = reference
                    }
                ]
            };
            string jsonRequest = JsonSerializer.Serialize(request);
            StringContent content = new(jsonRequest, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync($"{BaseUrl}/v2/checkout/orders", content);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CreateOrderResponse>(jsonResponse) ?? throw new Exception("Failed to deserialize CreateOrderResponse");
        }
        public async Task<CaptureOrderResponse> CaptureOrder(string orderId)
        {
            AuthResponse authResponse = await Authenticate();
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.AccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            StringContent content = new("", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync($"{BaseUrl}/v2/checkout/orders/{orderId}/capture", content);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CaptureOrderResponse>(jsonResponse) ?? throw new Exception("Failed to deserialize CaptureOrderResponse");
        }
    }
}
