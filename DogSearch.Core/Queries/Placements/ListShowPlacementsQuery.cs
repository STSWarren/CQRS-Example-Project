using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Entities.Shows;
using MediatR;
namespace DogSearch.Core.Queries.Placements;

public class ListShowPlacementsQuery : IRequest<IEnumerable<Placement>>
{
    public ListShowPlacementsQuery(Guid showId)
    {
        ShowId = new ShowId(showId);
    }
    public ShowId ShowId { get; init; }
}