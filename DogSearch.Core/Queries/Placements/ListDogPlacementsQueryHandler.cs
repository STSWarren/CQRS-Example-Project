using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Placements;

public class ListDogPlacementsQueryHandler : IRequestHandler<ListDogPlacementsQuery, IEnumerable<Placement>>
{
    private readonly IPlacementRepository _repository;

    public ListDogPlacementsQueryHandler(IPlacementRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Placement>> Handle(ListDogPlacementsQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetAllByDogId(request.DogId);
    }
}