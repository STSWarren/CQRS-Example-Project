using DogSearch.Core.Entities.Dog;
using MediatR;

namespace DogSearch.Core.Queries;

public class GetAllDogsQuery : IRequest<IEnumerable<Dog>>
{
}