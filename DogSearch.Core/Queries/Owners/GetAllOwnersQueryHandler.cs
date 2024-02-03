using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Owners;

public class GetAllOwnersQueryHandler:IRequestHandler<GetAllOwnersQuery, IEnumerable<Owner>>
{
    private readonly IOwnerRepository _ownerRepository;

    public GetAllOwnersQueryHandler(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public Task<IEnumerable<Owner>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
    {
        return _ownerRepository.GetAll();
    }
}
