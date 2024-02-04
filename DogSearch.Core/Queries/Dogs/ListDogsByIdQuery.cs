using DogSearch.Core.Entities.Dog;
using MediatR;

namespace DogSearch.Core.Queries.Dogs;

public class ListDogsByIdQuery : IRequest<IEnumerable<Dog>>
{
    public ListDogsByIdQuery(IEnumerable<DogId> ids)
    {
        Ids = ids;
    }
    public IEnumerable<DogId> Ids { get; init; }
}