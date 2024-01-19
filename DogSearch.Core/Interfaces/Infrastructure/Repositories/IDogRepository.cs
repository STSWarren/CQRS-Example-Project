using DogSearch.Core.Entities.Dog;

namespace DogSearch.Core.Interfaces.Infrastructure.Repositories;

public interface IDogRepository
{
    Task Create(Dog dog);
    Task<IEnumerable<Dog>> GetAll();
    Task<Dog> GetById(DogId id);
    Task Update(DogId id, Dog dog);
    Task Delete(DogId id);
}