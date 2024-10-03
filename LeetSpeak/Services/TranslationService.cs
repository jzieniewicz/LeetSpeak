using System.Text.Json;

namespace LeetSpeak.Services
{
    public class TranslationService : ITranslationService
    {
        public readonly HttpClient _httpClient;
        public TranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Translate(string text)
        {
            var responseData = "";
            var encodedText = Uri.EscapeDataString(text);
            var apiUrl = $"https://api.funtranslations.com/translate/leetspeak.json?text={encodedText}";
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    responseData = await response.Content.ReadAsStringAsync();
                    using (JsonDocument doc = JsonDocument.Parse(responseData))
                    {
                        JsonElement root = doc.RootElement;

                        var translatedText = root.GetProperty("contents").GetProperty("translated").GetString();

                        return translatedText ?? "Translation not found";
                    }
                }
                else
                {
                    responseData = "Error: Unable to fetch data from the API.";
                }
            }
            catch(Exception ex)
            {
                responseData = $"Exception: {ex.Message}";
            }

            return responseData;
        }
    }
}
