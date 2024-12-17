using HahnCatFacts.Application.DTOs;

namespace HahnCatFacts.Application.Interfaces.Services
{
    public interface ICatFactSourceApiService
    {
        public Task<CatFactDto?> FetchNewCatFactAsync();
    }
}
