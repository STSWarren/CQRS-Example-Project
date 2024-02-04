using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Commands.Placements;

public class DeletePlacementCommandHandler : IRequestHandler<DeletePlacementCommand>
{
    private readonly IPlacementRepository _repository;

    public DeletePlacementCommandHandler(IPlacementRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(DeletePlacementCommand request, CancellationToken cancellationToken)
    {
        return _repository.Delete(request.DogId, request.ShowId, request.Category);
    }
}