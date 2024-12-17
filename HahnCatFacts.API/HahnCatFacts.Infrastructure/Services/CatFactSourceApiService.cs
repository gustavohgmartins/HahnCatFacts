using HahnCatFacts.Application.DTOs;
using HahnCatFacts.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HahnCatFacts.Infrastructure.Services
{
    public class CatFactSourceApiService : ICatFactSourceApiService
    {
        private readonly ILogger<CatFactSourceApiService> _logger;

        private readonly HttpClient _httpClient;
        private readonly string? _catFactUrl;
        public CatFactSourceApiService(ILogger<CatFactSourceApiService> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _catFactUrl = configuration["ExternalApi:CatFactSourceUrl"];
        }
        public async Task<CatFactDto?> FetchNewCatFactAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_catFactUrl))
                {
                    _logger.LogInformation($"{DateTimeOffset.Now} | FetchNewCatFactAsync | ERROR | Cat fact source url cannot be null");
                    return null;
                }

                var response = await _httpClient.GetAsync(_catFactUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"{DateTimeOffset.Now} | FetchNewCatFactAsync | ERROR | Request failed with status code: {response.StatusCode} | {response.RequestMessage}");
                    return null;
                }

                var responseString = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<CatFactSourceDto>(responseString);

                var newCatFact = responseObject?.Data.FirstOrDefault();

                if (string.IsNullOrEmpty(newCatFact))
                {
                    _logger.LogInformation($"{DateTimeOffset.Now} | FetchNewCatFactAsync | ERROR | Fetched data is null or empty");

                    return null;
                }

                return new CatFactDto
                {
                    Description = newCatFact
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTimeOffset.Now} | FetchNewCatFactAsync | ERROR | An error occurred fetching a cat fact. | {ex.Message}");
                return null;
            }
        }
    }
}
