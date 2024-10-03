
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
            var apiUrl = "https://api.funtranslations.com/translate/leetspeak.json?text=Where%20this%20is%20lots%20of%20love%20there%20is%20lots%20of%20fighting.";
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    responseData = await response.Content.ReadAsStringAsync();
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
