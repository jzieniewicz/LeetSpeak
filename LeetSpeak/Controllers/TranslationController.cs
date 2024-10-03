using LeetSpeak.Models;
using LeetSpeak.Models.Enums;
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
            model.TranslationType = (int)TranslationType.LeetSpeak;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(TranslationViewModel model)
        {
            model.TranslatedText = await _translationService.Translate(model);
            return View(model);
        }
    }
}
