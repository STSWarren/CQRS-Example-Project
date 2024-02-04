using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Entities.Shows;

namespace DogSearch.Core.Interfaces.Infrastructure.Repositories;

public interface IPlacementRepository
{
    Task Create(Placement placement);
    Task<Placement> Get(DogId dogId, ShowId showId, string category);
    Task<IEnumerable<Placement>> GetAll();
    Task<IEnumerable<Placement>> GetAllByDogId(DogId dogId);
    Task<IEnumerable<Placement>> GetAllByShowId(ShowId showId);
    Task<IEnumerable<Placement>> GetAllByShowIdAndDogId(ShowId showId, DogId dogId);
    Task<IEnumerable<Placement>> GetAllByShowIdAndCategory(ShowId showId, string category);
    Task Update(DogId dogId, ShowId showId, string category, int place);
    Task Delete(DogId dogId, ShowId showId, string category);
}