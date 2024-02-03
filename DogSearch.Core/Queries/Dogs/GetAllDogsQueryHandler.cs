using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries.Dogs;

public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, IEnumerable<Dog>>
{
    private readonly IDogRepository _repository;

    public GetAllDogsQueryHandler(IDogRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetAll();
    }
}