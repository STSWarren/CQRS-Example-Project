using DogSearch.Core.Entities.Owners;
using MediatR;

namespace DogSearch.Core.Queries.Owners;

public class GetAllOwnersQuery : IRequest<IEnumerable<Owner>>
{
}