using AutoMapper;
using LeetSpeak.Data;
using LeetSpeak.Services;
using LeetSpeak.Models;
using System.Net.Http;
using LeetSpeak.Models.Enums;
using Moq;
using Moq.Protected;
using System.Net;

namespace LeetSpeak.Tests
{
    public class TranslationServiceTest
    {
        private TranslationService _translationService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        Mock<HttpMessageHandler> _httpMessageHandlerMock;

        [SetUp]
        public void Setup()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"success\":{\"total\":1},\"contents\":{\"translated\":\"Where dis iz lotz of luv der iz lotz of fightin.\",\"text\":\"Where this is lots of love there is lots of fighting.\",\"translation\":\"leetspeak\"}}")
            });
            HttpClient httpClient = new HttpClient(_httpMessageHandlerMock.Object);

            _translationService = new TranslationService(httpClient, _context, _mapper);

        }

        [Test]
        public async Task Is_translated_correctlyAsync()
        {
            var model = new TranslationViewModel
            {
                InputText = "Where this is lots of love there is lots of fighting.",
                TranslationType = (int)TranslationType.LeetSpeak
            };
            var result = await _translationService.Translate(model);
            Assert.That(result, Is.EqualTo("Where dis iz lotz of luv der iz lotz of fightin."));
        }
    }
}
