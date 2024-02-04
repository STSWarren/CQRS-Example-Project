using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Placements;

public class ListDogPlacementsInShowQueryHandler : IRequestHandler<ListDogPlacementsInShowQuery, IEnumerable<Placement>>
{
    private readonly IPlacementRepository _repository;

    public ListDogPlacementsInShowQueryHandler(IPlacementRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Placement>> Handle(ListDogPlacementsInShowQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetAllByShowIdAndDogId(request.ShowId, request.DogId);
    }
}