using HahnCatFacts.Application.DTOs;
using HahnCatFacts.Application.Interfaces.Services;
using HahnCatFacts.Domain.Entities;
using HahnCatFacts.Domain.Interfaces;

namespace HahnCatFacts.Application.Services
{
    public class CatFactService : ICatFactService
    {
        private readonly ICatFactRepository _catFactRepository;
        private readonly ICatFactSourceApiService _catFactSourceApiService;

        public CatFactService(ICatFactRepository productRepository, ICatFactSourceApiService catFactSourceApiService)
        {
            _catFactRepository = productRepository;
            _catFactSourceApiService = catFactSourceApiService;
        }

        public List<CatFact> GetAllCatFacts()
        {
            try
            {
                return _catFactRepository.GetAllNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the cat facts.", ex);
            }
        }

        public async Task AddCatFactAsync(CatFactDto newCatFact)
        {
            if (newCatFact == null || string.IsNullOrEmpty(newCatFact.Description))
            {
                throw new ArgumentNullException(nameof(newCatFact.Description));
            }

            try
            {
                var catFactToAdd = new CatFact
                {
                    Description = newCatFact.Description
                };

                await _catFactRepository.AddAsync(catFactToAdd);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the cat fact.", ex);
            }
        }

        public async Task FetchAndAddNewCatFactAsync()
        {
            try
            {
                var newCatFact = await _catFactSourceApiService.FetchNewCatFactAsync();

                if (newCatFact == null || string.IsNullOrEmpty(newCatFact.Description))
                {
                    return;
                }

                var catFact = new CatFactDto
                {
                    Description = newCatFact.Description
                };

                await AddCatFactAsync(catFact);

                Console.WriteLine($"{DateTime.Now} | FetchAndAddNewCatFactAsync | SUCCESS | Cat fact inserted successfully");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred fetching and adding a cat fact.", ex);
            }
        }
    }
}