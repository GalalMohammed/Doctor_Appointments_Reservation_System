using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLLServices.Common.ReCaptchaService
{
    public class ReCaptchaService(string secretKey, string verificationUrl)
    {
        public async Task<bool> ValidateReCaptcha(string reCaptchaResponse)
        {
            // Validate the reCAPTCHA response
            if (string.IsNullOrEmpty(reCaptchaResponse))
                return false;
            using HttpClient httpClient = new();
            MultipartFormDataContent multipartFormDataContent = new()
            {
                { new StringContent(secretKey), "secret" },
                { new StringContent(reCaptchaResponse), "response" }
            };
            HttpResponseMessage response = await httpClient.PostAsync(verificationUrl, multipartFormDataContent);
            if (!response.IsSuccessStatusCode)
                return false;
            string jsonResponse = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(jsonResponse))
                return false;
            // Parse the JSON response
            JsonDocument json = JsonDocument.Parse(jsonResponse);
            bool isValid = json.RootElement.GetProperty("success").GetBoolean();
            return isValid;
        }
    }
}
