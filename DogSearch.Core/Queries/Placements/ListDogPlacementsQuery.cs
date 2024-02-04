using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Placements;
using MediatR;

namespace DogSearch.Core.Queries.Placements;

public class ListDogPlacementsQuery : IRequest<IEnumerable<Placement>>
{
    public ListDogPlacementsQuery(Guid dogId)
    {
        DogId = new DogId(dogId);
    }
    public DogId DogId { get; init; }
}