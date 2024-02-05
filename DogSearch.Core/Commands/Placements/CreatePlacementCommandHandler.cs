using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Commands.Placements;

public class CreatePlacementCommandHandler : IRequestHandler<CreatePlacementCommand>
{
    private readonly IPlacementRepository _repository;

    public CreatePlacementCommandHandler(IPlacementRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(CreatePlacementCommand request, CancellationToken cancellationToken)
    {
        return _repository.Create(new Placement(Guid.NewGuid(), request.DogId.Value, request.ShowId.Value, request.Category, request.Place));
    }
}