using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Placements;

public class ListShowAndCategoryPlacementsQueryHandler : IRequestHandler<ListShowAndCategoryPlacementsQuery, IEnumerable<Placement>>
{
    private readonly IPlacementRepository _repository;

    public ListShowAndCategoryPlacementsQueryHandler(IPlacementRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Placement>> Handle(ListShowAndCategoryPlacementsQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetAllByShowIdAndCategory(request.ShowId, request.Category);
    }
}