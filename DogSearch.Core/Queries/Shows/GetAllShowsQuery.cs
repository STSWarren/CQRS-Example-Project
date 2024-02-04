using DogSearch.Core.Entities.Shows;
using MediatR;

namespace DogSearch.Core.Queries.Shows;

public class GetAllShowsQuery:IRequest<IEnumerable<Show>>
{
}