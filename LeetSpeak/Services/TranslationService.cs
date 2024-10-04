using AutoMapper;
using LeetSpeak.Data;
using LeetSpeak.Models;
using System.Text.Json;

namespace LeetSpeak.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TranslationService(HttpClient httpClient, ApplicationDbContext context, IMapper mapper)
        {
            _httpClient = httpClient;
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Translate(TranslationViewModel model)
        {
            var responseData = "";
            var encodedText = Uri.EscapeDataString(model.InputText);
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
                        //model.TranslatedText = translatedText;
                        //await Save(model);
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

        public async Task Save(TranslationViewModel model)
        {
            var translation = _mapper.Map<Translation>(model);
            _context.Add(translation);
            await _context.SaveChangesAsync();
        }
    }
}
