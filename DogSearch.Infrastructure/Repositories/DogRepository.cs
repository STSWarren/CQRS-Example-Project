using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogSearch.Infrastructure.Repositories;

public class DogRepository : IDogRepository
{
    public Task Create(Dog dog)
    {
        throw new NotImplementedException();
    }

    public Task Delete(DogId id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Dog>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Dog> GetById(DogId id)
    {
        throw new NotImplementedException();
    }

    public Task Update(DogId id, Dog dog)
    {
        throw new NotImplementedException();
    }
}
