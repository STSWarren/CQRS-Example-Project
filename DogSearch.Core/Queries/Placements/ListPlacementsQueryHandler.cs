using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Placements;

public class ListPlacementsQueryHandler : IRequestHandler<ListPlacementsQuery, IEnumerable<Placement>>
{
    private readonly IPlacementRepository _repository;

    public ListPlacementsQueryHandler(IPlacementRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Placement>> Handle(ListPlacementsQuery request, CancellationToken cancellationToken)
    {
        if (!request.ShowId.Value.Equals(Guid.Empty))
        {
            return await SearchByShow(request);
        }
        if (!request.DogId.Value.Equals(Guid.Empty))
        {
            return await SearchByDog(request);
        }
        return new List<Placement>();
    }

    private Task<IEnumerable<Placement>> SearchByShow(ListPlacementsQuery request)
    {
        if (!request.DogId.Value.Equals(Guid.Empty))
        {
            return SearchByShowAndDog(request);
        }
        if (!request.Category.Equals(string.Empty))
        {
            return SearchByShowAndCategory(request);
        }
        return _repository.GetAllByShowId(request.ShowId);
    }

    private Task<IEnumerable<Placement>> SearchByDog(ListPlacementsQuery request)
    {
        return _repository.GetAllByDogId(request.DogId);
    }

    private Task<IEnumerable<Placement>> SearchByShowAndCategory(ListPlacementsQuery request)
    {
        return _repository.GetAllByShowIdAndCategory(request.ShowId, request.Category);
    }

    private Task<IEnumerable<Placement>> SearchByShowAndDog(ListPlacementsQuery request)
    {
        if (!request.Category.Equals(string.Empty))
        {
            return SearchByShowAndDogAndCategory(request);
        }
        return _repository.GetAllByShowIdAndDogId(request.ShowId, request.DogId);
    }
    private async Task<IEnumerable<Placement>> SearchByShowAndDogAndCategory(ListPlacementsQuery request)
    {
        var result = await _repository.Get(request.DogId, request.ShowId, request.Category);
        return new List<Placement>() { result };
    }
}
