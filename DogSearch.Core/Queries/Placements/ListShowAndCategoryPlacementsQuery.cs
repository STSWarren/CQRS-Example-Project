using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Entities.Shows;
using MediatR;

namespace DogSearch.Core.Queries.Placements;

public class ListShowAndCategoryPlacementsQuery : IRequest<IEnumerable<Placement>>
{
    public ListShowAndCategoryPlacementsQuery(Guid showId, string category)
    {
        ShowId = new ShowId(showId);
        Category = category;
    }
    public ShowId ShowId { get; init; }
    public string Category {  get; init; }
}