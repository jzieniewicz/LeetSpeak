using LeetSpeak.Models;

namespace LeetSpeak.Services
{
    public interface ITranslationService
    {
        public Task<string> Translate(TranslationViewModel model);
        public Task Save(TranslationViewModel model);
    }
}
