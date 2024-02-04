using DogSearch.Core.Entities.Shows;

namespace DogSearch.Core.Interfaces.Infrastructure.Repositories;

public interface IShowRepository
{
    Task Create(Show show);
    Task<IEnumerable<Show>> GetAll();
    Task<Show> GetById(ShowId id);
    Task Update(ShowId id, Show show);
    Task Delete(ShowId id);
}