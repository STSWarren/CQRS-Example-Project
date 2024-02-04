using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Commands.Owners;

public class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand>
{
    private readonly IOwnerRepository _repository;

    public DeleteOwnerCommandHandler(IOwnerRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
    {
        return _repository.Delete(request.Id);
    }
}