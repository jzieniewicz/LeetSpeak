namespace LeetSpeak.Services
{
    public interface ITranslationService
    {
        public Task<string> Translate(string text);
    }
}
