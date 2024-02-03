using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Owners;

namespace DogSearch.Core.Interfaces.Infrastructure.Repositories;

public interface IOwnerRepository
{
    Task Create(Owner owner);
    Task<IEnumerable<Owner>> GetAll();
    Task<Owner> GetById(OwnerId id);
    Task Update(OwnerId id, Owner owner);
    Task Delete(OwnerId id);
}