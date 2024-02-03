using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Commands.Owners;

public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, OwnerId>
{
    private readonly IOwnerRepository _ownerRepository;

    public CreateOwnerCommandHandler(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public async Task<OwnerId> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
    {
        var newOwner = new Owner(Guid.NewGuid(), request.FirstName, request.LastName, request.Address, request.Email, request.Phone);
        await _ownerRepository.Create(newOwner);
        return newOwner.Id;
    }
}
