using BLLServices.Payment.DTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;

namespace BLLServices.Payment
{
    public class PayPalClient(string clientId, string clientSecret, string baseUrl)
    {
        public string ClientId { get; } = clientId ?? throw new ArgumentNullException(nameof(clientId));
        public string ClientSecret { get; } = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
        public string BaseUrl => baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
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
            JsonNode? jsonNodeResponse = JsonNode.Parse(jsonResponse) ?? throw new Exception("Failed to parse authentication response");
            AuthResponse authResponse = new()
            {
                Scope = jsonNodeResponse["scope"]?.ToString() ?? throw new Exception("Scope is missing in the response"),
                AccessToken = jsonNodeResponse["access_token"]?.ToString() ?? throw new Exception("Access token is missing in the response"),
                TokenType = jsonNodeResponse["token_type"]?.ToString() ?? throw new Exception("Token type is missing in the response"),
                AppId = jsonNodeResponse["app_id"]?.ToString() ?? throw new Exception("App ID is missing in the response"),
                ExpiresIn = jsonNodeResponse["expires_in"]?.GetValue<int>() ?? throw new Exception("Expires in is missing in the response"),
                Nonce = jsonNodeResponse["nonce"]?.ToString()
            };
            return authResponse;
        }
            public async Task<CreateOrderResponse> CreateOrder(string value)
        {
            AuthResponse authResponse = await Authenticate();
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.AccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Create a new order request with the specified amount and currency
            JsonObject createOrderRequest = new()
            {
                ["intent"] = "CAPTURE",
                ["purchase_units"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["amount"] = new JsonObject
                        {
                            ["currency_code"] = "USD",
                            ["value"] = value
                        }
                    }
                }
            };
            StringContent content = new(createOrderRequest.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync($"{BaseUrl}/v2/checkout/orders", content);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            JsonNode? jsonNodeResponse = JsonNode.Parse(jsonResponse) ?? throw new Exception("Failed to parse CreateOrderResponse");
            CreateOrderResponse createOrderResponse = new()
            {
                Id = jsonNodeResponse["id"]?.ToString() ?? throw new Exception("Order ID is missing in the response"),
                Status = jsonNodeResponse["status"]?.ToString() ?? throw new Exception("Status is missing in the response"),
                Links = jsonNodeResponse["links"]?.AsArray().Select(x => new Link
                {
                    Href = x?["href"]?.ToString() ?? throw new Exception("Link href is missing in the response"),
                    Rel = x?["rel"]?.ToString() ?? throw new Exception("Link rel is missing in the response"),
                    Method = x?["method"]?.ToString() ?? throw new Exception("Link method is missing in the response")
                }).ToList() ?? throw new Exception("Links are missing in the response")
            };
            return createOrderResponse;
        }
        public async Task<CaptureOrderResponse> CaptureOrder(string orderId)
        {
            AuthResponse authResponse = await Authenticate();
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.AccessToken);
            StringContent content = new("", null, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync($"{BaseUrl}/v2/checkout/orders/{orderId}/capture", content);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            JsonNode? jsonNodeResponse = JsonNode.Parse(jsonResponse) ?? throw new Exception("Failed to parse CaptureOrderResponse");
            CaptureOrderResponse captureOrderResponse = new()
            {
                Id = jsonNodeResponse["id"]?.ToString() ?? throw new Exception("Capture ID is missing in the response"),
                Status = jsonNodeResponse["status"]?.ToString() ?? throw new Exception("Status is missing in the response")
            };
            return captureOrderResponse;
        }
    }
}
