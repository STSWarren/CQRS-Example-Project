using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Entities.Shows;
using MediatR;

namespace DogSearch.Core.Queries.Placements;

public class ListDogPlacementsInShowQuery : IRequest<IEnumerable<Placement>>
{
    public ListDogPlacementsInShowQuery(Guid dogId, Guid showId)
    {
        DogId = new DogId(dogId);
        ShowId = new ShowId(showId);
    }
    public DogId DogId { get; init; }
    public ShowId ShowId { get; init; }
}