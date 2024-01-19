using DogSearch.Core.Entities.Dog;
using MediatR;

namespace DogSearch.Core.Queries;

public class GetDogQuery : IRequest<Dog>
{
    public GetDogQuery(Guid id)
    {
        Id = new DogId(id);
    }
    public DogId Id { get; init; }
}
