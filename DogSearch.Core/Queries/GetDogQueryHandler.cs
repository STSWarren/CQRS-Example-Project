using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace DogSearch.Core.Queries;

public class GetDogQueryHandler:IRequestHandler<GetDogQuery, Dog>
{
    private readonly IDogRepository _repository;

    public GetDogQueryHandler(IDogRepository repository)
    {
        _repository = repository;
    }

    public Task<Dog> Handle(GetDogQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetById(request.Id);
    }
}