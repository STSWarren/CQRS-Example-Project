using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Placements;

public class ListShowPlacementsQueryHandler : IRequestHandler<ListShowPlacementsQuery, IEnumerable<Placement>>
{
    private readonly IPlacementRepository _repository;

    public ListShowPlacementsQueryHandler(IPlacementRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Placement>> Handle(ListShowPlacementsQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetAllByShowId(request.ShowId);
    }
}