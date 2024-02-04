using DogSearch.Core.Entities.Shows;
using MediatR;

namespace DogSearch.Core.Queries.Shows;

public class ListShowsByIdQuery:IRequest<IEnumerable<Show>>
{
    public ListShowsByIdQuery(IEnumerable<ShowId> ids)
    {
        Ids = ids;
    }
    public IEnumerable<ShowId> Ids { get; init; }
}