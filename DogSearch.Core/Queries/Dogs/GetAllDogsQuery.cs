using DogSearch.Core.Entities.Dog;
using MediatR;

namespace DogSearch.Core.Queries.Dogs;

public class GetAllDogsQuery : IRequest<IEnumerable<Dog>>
{
}