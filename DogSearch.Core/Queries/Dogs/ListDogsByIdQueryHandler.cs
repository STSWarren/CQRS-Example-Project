using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Dogs;

public class ListDogsByIdQueryHandler : IRequestHandler<ListDogsByIdQuery, IEnumerable<Dog>>
{
    private readonly IDogRepository _repository;

    public ListDogsByIdQueryHandler(IDogRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Dog>> Handle(ListDogsByIdQuery request, CancellationToken cancellationToken)
    {
        return _repository.ListByIds(request.Ids);
    }
}
