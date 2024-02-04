using DogSearch.Core.Commands.Placements;
using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Placements;

public class GetAllPlacementsQueryHandler : IRequestHandler<GetAllPlacementsQuery, IEnumerable<Placement>>
{
    private readonly IPlacementRepository _repository;

    public GetAllPlacementsQueryHandler(IPlacementRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Placement>> Handle(GetAllPlacementsQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetAll();
    }
}