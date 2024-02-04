using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Commands.Placements;

public class UpdatePlacementCommandHandler : IRequestHandler<UpdatePlacementCommand>
{
    private readonly IPlacementRepository _repository;

    public UpdatePlacementCommandHandler(IPlacementRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(UpdatePlacementCommand request, CancellationToken cancellationToken)
    {
        return _repository.Update(request.DogId, request.ShowId, request.Category, request.Place);
    }
}