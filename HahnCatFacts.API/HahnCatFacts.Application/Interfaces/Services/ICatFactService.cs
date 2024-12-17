using HahnCatFacts.Application.DTOs;
using HahnCatFacts.Domain.Entities;

namespace HahnCatFacts.Application.Interfaces.Services
{
    public interface ICatFactService
    {
        public List<CatFact> GetAllCatFacts();
        public Task AddCatFactAsync(CatFactDto newCatFact);
        public Task FetchAndAddNewCatFactAsync();
    }
}
