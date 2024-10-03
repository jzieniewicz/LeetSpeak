using LeetSpeak.Models;
using LeetSpeak.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeetSpeak.Controllers
{
    public class TranslationController : Controller
    {
        private readonly HttpClient _httpClient;
        private ITranslationService _translationService;

        public TranslationController(HttpClient httpClient, ITranslationService translationService)
        {
            _httpClient = httpClient;
            _translationService = translationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new TranslationViewModel();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(TranslationViewModel model)
        {
            var response = await _translationService.Translate(model.InputText);
            model.TranslatedText = response.ToString();
            return View(model);
        }
    }
}
