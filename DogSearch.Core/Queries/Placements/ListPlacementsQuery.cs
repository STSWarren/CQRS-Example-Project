using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Entities.Shows;
using MediatR;

namespace DogSearch.Core.Queries.Placements;

public class ListPlacementsQuery : IRequest<IEnumerable<Placement>>
{
    public ListPlacementsQuery(Guid dogId, Guid showId, string category)
    {
        DogId = new DogId(dogId);
        ShowId = new ShowId(showId);
        Category = category;
    }
    public DogId DogId { get; init; }
    public ShowId ShowId { get; init; }
    public string Category { get; init; }
}