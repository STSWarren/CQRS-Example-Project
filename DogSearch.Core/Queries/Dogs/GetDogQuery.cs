using DogSearch.Core.Entities.Dog;
using MediatR;

namespace DogSearch.Core.Queries.Dogs;

public class GetDogQuery : IRequest<Dog>
{
    public GetDogQuery(Guid id)
    {
        Id = new DogId(id);
    }
    public DogId Id { get; init; }
}