using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Commands.Owners;

public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand>
{
    private readonly IOwnerRepository _ownerRepository;

    public UpdateOwnerCommandHandler(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public Task Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
    {
        return _ownerRepository.Update(request.Id, 
            new Owner(Guid.Empty, 
            request.FirstName, 
            request.LastName, 
            request.Address, 
            request.Email, 
            request.Phone));
    }
}