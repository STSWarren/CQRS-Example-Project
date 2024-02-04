using DogSearch.Core.Entities.Shows;
using MediatR;

namespace DogSearch.Core.Queries.Shows;

public class GetShowQuery:IRequest<Show>
{
    public GetShowQuery(Guid id)
    {
        Id = new ShowId(id);
    }
    public ShowId Id { get; init; }
}
