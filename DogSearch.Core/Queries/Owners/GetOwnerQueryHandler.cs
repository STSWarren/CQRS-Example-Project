using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Owners;

public class GetOwnerQueryHandler : IRequestHandler<GetOwnerQuery,Owner>
{
    private readonly IOwnerRepository _ownerRepository;

    public GetOwnerQueryHandler(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public Task<Owner> Handle(GetOwnerQuery request, CancellationToken cancellationToken)
    {
        return _ownerRepository.GetById(request.Id);
    }
}