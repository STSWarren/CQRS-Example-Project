using DogSearch.Core.Entities.Placements;
using MediatR;

namespace DogSearch.Core.Queries.Placements;

public class GetAllPlacementsQuery : IRequest<IEnumerable<Placement>>
{
}
